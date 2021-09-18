using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class DefaultNetworkManager.
    /// Implements the <see cref="NetworkManager" />
    /// </summary>
    /// <seealso cref="NetworkManager" />
    public partial class DefaultNetworkManager : NetworkManager
    {
        private const int NtpPort = 123;
        private const string NtpPublicPool = "pool.ntp.org";
        private const string W32TimeNtpServerRegistryKey = "SYSTEM\\CurrentControlSet\\Services\\W32Time\\Parameters";
        private const string W32TimeNtpServerValueName = "NtpServer";

        private readonly object _portLock = new object();

        private int _lastLocalPort;

        private static GetNetworkTimeResult DoGetNetworkTimeViaNtp(
                IPAddress ipAddress,
                string hostname)
        {
            if (ipAddress == null)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error("Can not get NTP server IP address.");
                return new GetNetworkTimeResult();
            }

            Logger.GetInstance(typeof(DefaultNetworkManager)).Debug($"Try to get network time from {ipAddress} ({hostname})");

            var messageFormat = new byte[48];
            messageFormat[0] = GetNtpMessageFormatByte0(
                    NtpLeapIndicator.NoWarning,
                    NtpVersionNumber.IPv4Only,
                    NtpMode.Client
            );
            var ipEndPoint = new IPEndPoint(
                    ipAddress,
                    NtpPort
            );
            try
            {
                using (var socket = new Socket(
                        AddressFamily.InterNetwork,
                        SocketType.Dgram,
                        ProtocolType.Udp))
                {
                    socket.SendTimeout = 1000 * 10;
                    socket.ReceiveTimeout = 1000 * 10;
                    socket.Connect(ipEndPoint);
                    socket.Send(messageFormat);
                    socket.Receive(messageFormat);
                    socket.Close();
                }
            }
            catch (SocketException e)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not get NTP message from {ipAddress}. error: {e.Message}, socketErrorCode: {e.SocketErrorCode}");
                return new GetNetworkTimeResult
                {
                        Status = GetNetworkTimeStatus.NetworkError
                };
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not get NTP message from {ipAddress}. error: {e}");
                return new GetNetworkTimeResult();
            }

            var integerPart = 0
                    | (ulong)messageFormat[40] << 24
                    | (ulong)messageFormat[41] << 16
                    | (ulong)messageFormat[42] << 8
                    | (ulong)messageFormat[43] << 0;
            var fractionPart = 0
                    | (ulong)messageFormat[44] << 24
                    | (ulong)messageFormat[45] << 16
                    | (ulong)messageFormat[46] << 8
                    | (ulong)messageFormat[47] << 0;

            var milliseconds = integerPart * 1000 + fractionPart * 1000 / 0x100000000L;
            var networkDateTime = new DateTime(
                    1900,
                    1,
                    1
            ).AddMilliseconds((long)milliseconds);

            var providerNameBuilder = new StringBuilder();
            providerNameBuilder.Append("NTP: ");
            if (!string.IsNullOrWhiteSpace(hostname))
            {
                providerNameBuilder.Append(hostname).Append(" / ");
            }
            providerNameBuilder.Append(ipAddress);

            return new GetNetworkTimeResult
            {
                    NetworkTime = new NetworkTimeInfo
                    {
                            ProviderName = providerNameBuilder.ToString(),
                            TimeInUtc = networkDateTime
                    },
                    Status = GetNetworkTimeStatus.Ok
            };
        }

        private static GetUnusedLocalPortResult DoGetUnusedPort(int preferredPort)
        {
            var realPreferredPort = preferredPort;
            if (realPreferredPort < 0 || realPreferredPort > 65535)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Warn($"Preferred port number {preferredPort} is invalid.");
                realPreferredPort = 0;
            }
            var listener = new TcpListener(
                    IPAddress.Loopback,
                    realPreferredPort
            );
            try
            {
                listener.Start();
                return new GetUnusedLocalPortResult
                {
                        LocalPort = ((IPEndPoint)listener.LocalEndpoint).Port,
                        Status = GetUnusedLocalPortStatus.Ok
                };
            }
            catch (SocketException e)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not get available port: {e}");
                return new GetUnusedLocalPortResult
                {
                        Status = GetUnusedLocalPortStatus.NetworkError
                };
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not get available port: {e}");
            }
            finally
            {
                listener.Stop();
            }
            return new GetUnusedLocalPortResult();
        }

        private static byte GetNtpMessageFormatByte0(
                NtpLeapIndicator leapIndicator,
                NtpVersionNumber versionNumber,
                NtpMode mode)
        {
            var result = 0
                    | (int)leapIndicator << 6
                    | (int)versionNumber << 3
                    | (int)mode << 0;
            return (byte)result;
        }

        private static GetNetworkTimeResult GetNetworkTimeFromMachineNtpConfig()
        {
            var ntpServer = Win32Registry.GetStringValue(
                    Win32Registry.Hive.LocalMachine,
                    W32TimeNtpServerRegistryKey,
                    W32TimeNtpServerValueName
            );
            var index = ntpServer.IndexOf(',');
            if (index >= 0)
            {
                ntpServer = ntpServer.Substring(0, index);
            }

            if (string.IsNullOrWhiteSpace(ntpServer))
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error("Can not find NTP server from machine config, part 1");
                return new GetNetworkTimeResult
                {
                        Status = GetNetworkTimeStatus.NetworkError
                };
            }

            var addresses = Dns.GetInstance().GetHostEntry(ntpServer)?.AddressList;
            if (addresses == null || addresses.Length <= 0)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error("Can not find NTP server from machine config, part 2");
                return new GetNetworkTimeResult
                {
                        Status = GetNetworkTimeStatus.NetworkError
                };
            }

            return DoGetNetworkTimeViaNtp(
                    addresses[0],
                    ntpServer
            );
        }

        private static GetNetworkTimeResult GetNetworkTimeFromPublicNtpPool()
        {
            var addresses = Dns.GetInstance().GetHostEntry(NtpPublicPool)?.AddressList;
            if (addresses == null || addresses.Length <= 0)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error("Can not find NTP server from public pool");
                return new GetNetworkTimeResult
                {
                        Status = GetNetworkTimeStatus.NetworkError
                };
            }

            return DoGetNetworkTimeViaNtp(
                    addresses[0],
                    NtpPublicPool
            );
        }

        /// <inheritdoc />
        protected override GetLocalPortStatusResult OnGetLocalPortStatus(int portNumber)
        {
            var listener = new TcpListener(
                    IPAddress.Loopback,
                    portNumber
            );
            try
            {
                listener.Start();
                return new GetLocalPortStatusResult
                {
                        LocalPortStatus = PortStatus.Available,
                        Status = GetLocalPortStatusStatus.Ok
                };
            }
            catch (SocketException e)
            {
                var socketErrorCode = e.SocketErrorCode;
                if (socketErrorCode != SocketError.AddressAlreadyInUse)
                {
                    Logger.GetInstance(typeof(DefaultNetworkManager)).Debug($"Can not get available port: {e.Message}, socketErrorCode: {socketErrorCode}");
                }
                return new GetLocalPortStatusResult
                {
                        LocalPortStatus = PortStatus.InUse,
                        Status = GetLocalPortStatusStatus.Ok
                };
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not get detect port status: {e}");
            }
            finally
            {
                listener.Stop();
            }

            return new GetLocalPortStatusResult();
        }

        /// <inheritdoc />
        protected override GetNetworkTimeResult OnGetNetworkTime()
        {
            var result = GetNetworkTimeFromMachineNtpConfig();
            var resultStatus = result.Status;
            if (resultStatus != GetNetworkTimeStatus.Ok)
            {
                result = GetNetworkTimeFromPublicNtpPool();
            }
            return result;
        }

        /// <inheritdoc />
        protected override GetUnusedLocalPortResult OnGetUnusedLocalPort(bool shouldUseLastPortFirst)
        {
            lock (_portLock)
            {
                var result = new GetUnusedLocalPortResult();
                if (shouldUseLastPortFirst)
                {
                    result = DoGetUnusedPort(_lastLocalPort);
                }
                if (result.Status != GetUnusedLocalPortStatus.Ok)
                {
                    result = DoGetUnusedPort(0);
                }
                if (result.Status == GetUnusedLocalPortStatus.Ok)
                {
                    _lastLocalPort = result.LocalPort;
                }
                return result;
            }
        }

        /// <inheritdoc />
        protected override TraceRouteResult OnTraceRoute(
                string hostNameOrIpAddress,
                int maxHop,
                int timeoutInMilli,
                CancellationToken cancellationToken)
        {
            var pingOptions = new PingOptions(
                    1,
                    true
            );
            var pingReplyTime = new Stopwatch();

            var hops = new List<RouteHopInfo>();
            try
            {
                using (var ping = new Ping())
                {
                    while (true)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            return new TraceRouteResult
                            {
                                    Status = TraceRouteStatus.CancelledOperation
                            };
                        }

                        pingReplyTime.Start();
                        var reply = ping.Send(
                                hostNameOrIpAddress,
                                timeoutInMilli,
                                new byte[] { 0 },
                                pingOptions
                        );
                        pingReplyTime.Stop();

                        if (cancellationToken.IsCancellationRequested)
                        {
                            return new TraceRouteResult
                            {
                                    Status = TraceRouteStatus.CancelledOperation
                            };
                        }

                        var address = reply?.Address;
                        var status = reply?.Status ?? IPStatus.Unknown;
                        hops.Add(new RouteHopInfo
                        {
                                Address = address,
                                Hostname = Dns.GetInstance().GetHostEntry(address)?.HostName,
                                Node = pingOptions.Ttl,
                                TimeInMilli = pingReplyTime.ElapsedMilliseconds,
                                Status = status
                        });

                        if (cancellationToken.IsCancellationRequested)
                        {
                            return new TraceRouteResult
                            {
                                    Status = TraceRouteStatus.CancelledOperation
                            };
                        }

                        pingOptions.Ttl++;
                        pingReplyTime.Reset();
                        if (pingOptions.Ttl > maxHop || IPStatus.Success == status)
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not trace route: {e.Message}");
                return new TraceRouteResult
                {
                        Status = TraceRouteStatus.NetworkError
                };
            }

            return new TraceRouteResult
            {
                    Route = new RouteInfo
                    {
                            Hops = hops,
                            Target = hostNameOrIpAddress
                    },
                    Status = TraceRouteStatus.Ok
            };
        }

        /// <inheritdoc />
        protected override VerifyLocalPortStatusResult OnVerifyLocalPortStatus(int portNumber)
        {
            try
            {
                using (new TcpClient(
                        "localhost",
                        portNumber))
                {
                    // skip
                }
                return new VerifyLocalPortStatusResult
                {
                        LocalPortStatus = PortStatus.InUse,
                        Status = VerifyLocalPortStatusStatus.Ok
                };
            }
            catch (SocketException e)
            {
                var socketErrorCode = e.SocketErrorCode;
                if (socketErrorCode != SocketError.ConnectionRefused)
                {
                    Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not get verify port status: {e.Message}, socketErrorCode: {socketErrorCode}");
                }
                return new VerifyLocalPortStatusResult
                {
                        LocalPortStatus = PortStatus.Available,
                        Status = VerifyLocalPortStatusStatus.Ok
                };
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultNetworkManager)).Error($"Can not get verify port status: {e}");
            }

            return new VerifyLocalPortStatusResult();
        }
    }
}
