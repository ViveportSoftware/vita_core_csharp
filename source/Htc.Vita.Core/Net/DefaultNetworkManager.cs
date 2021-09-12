using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class DefaultNetworkManager.
    /// Implements the <see cref="NetworkManager" />
    /// </summary>
    /// <seealso cref="NetworkManager" />
    public class DefaultNetworkManager : NetworkManager
    {
        private readonly object _portLock = new object();

        private int _lastLocalPort;

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
                Logger.GetInstance(typeof(DefaultNetworkManager)).Debug($"Can not get available port: {e.Message}");
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

            var hops = new List<Hop>();
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
                        hops.Add(new Hop
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
