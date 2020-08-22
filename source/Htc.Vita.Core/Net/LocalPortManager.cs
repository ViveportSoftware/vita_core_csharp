using System;
using System.Net;
using System.Net.Sockets;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class LocalPortManager.
    /// </summary>
    public class LocalPortManager
    {
        /// <summary>
        /// Gets the random unused port.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(
                    IPAddress.Loopback,
                    0
            );
            try
            {
                listener.Start();
                return ((IPEndPoint)listener.LocalEndpoint).Port;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(LocalPortManager)).Error($"Can not get available port: {e}");
            }
            finally
            {
                listener.Stop();
            }
            return -1;
        }

        /// <summary>
        /// Gets the port status.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns>PortStatus.</returns>
        public static PortStatus GetPortStatus(int portNumber)
        {
            var listener = new TcpListener(
                    IPAddress.Loopback,
                    portNumber
            );
            try
            {
                listener.Start();
                return PortStatus.Available;
            }
            catch (SocketException e)
            {
                Logger.GetInstance(typeof(LocalPortManager)).Warn($"Can not get available port: {e.Message}");
                return PortStatus.InUse;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(LocalPortManager)).Error($"Can not get detect port status: {e}");
            }
            finally
            {
                listener.Stop();
            }

            return PortStatus.Unknown;
        }

        /// <summary>
        /// Verifies the port status.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns>PortStatus.</returns>
        public static PortStatus VerifyPortStatus(int portNumber)
        {
            try
            {
                using (new TcpClient(
                        "localhost",
                        portNumber))
                {
                    // do nothing
                }
                return PortStatus.InUse;
            }
            catch (SocketException e)
            {
                var socketErrorCode = e.SocketErrorCode;
                if (socketErrorCode != SocketError.ConnectionRefused)
                {
                    Logger.GetInstance(typeof(LocalPortManager)).Error($"Can not get verify port status: {e.Message}, socketErrorCode: {socketErrorCode}");
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(LocalPortManager)).Error($"Can not get verify port status: {e}");
            }

            return PortStatus.Available;
        }

        /// <summary>
        /// Enum PortStatus
        /// </summary>
        public enum PortStatus
        {
            /// <summary>
            /// Unknown port status
            /// </summary>
            Unknown,
            /// <summary>
            /// The port is in use
            /// </summary>
            InUse,
            /// <summary>
            /// The port is available
            /// </summary>
            Available
        }
    }
}
