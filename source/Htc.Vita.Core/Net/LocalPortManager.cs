using System;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class LocalPortManager.
    /// </summary>
    [Obsolete("This class is obsoleted.")]
    public class LocalPortManager
    {
        /// <summary>
        /// Gets the random unused port.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public static int GetRandomUnusedPort()
        {
            return GetRandomUnusedPort(false);
        }

        /// <summary>
        /// Gets the random unused port.
        /// </summary>
        /// <param name="shouldUseLastPortFirst">if set to <c>true</c> it should use last port first.</param>
        /// <returns>System.Int32.</returns>
        public static int GetRandomUnusedPort(bool shouldUseLastPortFirst)
        {
            var getUnusedLocalPortResult = NetworkManager.GetInstance().GetUnusedLocalPort(shouldUseLastPortFirst);
            var getUnusedLocalPortStatus = getUnusedLocalPortResult.Status;
            if (getUnusedLocalPortStatus != NetworkManager.GetUnusedLocalPortStatus.Ok)
            {
                return 0;
            }

            return getUnusedLocalPortResult.LocalPort;
        }

        /// <summary>
        /// Gets the port status.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns>PortStatus.</returns>
        public static PortStatus GetPortStatus(int portNumber)
        {
            var getLocalPortStatusResult = NetworkManager.GetInstance().GetLocalPortStatus(portNumber);
            var getLocalPortStatusStatus = getLocalPortStatusResult.Status;
            if (getLocalPortStatusStatus != NetworkManager.GetLocalPortStatusStatus.Ok)
            {
                return PortStatus.Unknown;
            }

            var portStatus = getLocalPortStatusResult.LocalPortStatus;
            if (portStatus == NetworkManager.PortStatus.Available)
            {
                return PortStatus.Available;
            }
            if (portStatus == NetworkManager.PortStatus.InUse)
            {
                return PortStatus.InUse;
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
            var verifyLocalPortStatusResult = NetworkManager.GetInstance().VerifyLocalPortStatus(portNumber);
            var verifyLocalPortStatusStatus = verifyLocalPortStatusResult.Status;
            if (verifyLocalPortStatusStatus != NetworkManager.VerifyLocalPortStatusStatus.Ok)
            {
                return PortStatus.Unknown;
            }

            var portStatus = verifyLocalPortStatusResult.LocalPortStatus;
            if (portStatus == NetworkManager.PortStatus.Available)
            {
                return PortStatus.Available;
            }
            if (portStatus == NetworkManager.PortStatus.InUse)
            {
                return PortStatus.InUse;
            }
            return PortStatus.Unknown;
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
