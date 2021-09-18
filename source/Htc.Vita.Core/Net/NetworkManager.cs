using System;
using System.Threading;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class NetworkManager.
    /// </summary>
    public abstract partial class NetworkManager
    {
        static NetworkManager()
        {
            TypeRegistry.RegisterDefault<NetworkManager, DefaultNetworkManager>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : NetworkManager, new()
        {
            TypeRegistry.Register<NetworkManager, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>NetworkManager.</returns>
        public static NetworkManager GetInstance()
        {
            return TypeRegistry.GetInstance<NetworkManager>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>NetworkManager.</returns>
        public static NetworkManager GetInstance<T>()
                where T : NetworkManager, new()
        {
            return TypeRegistry.GetInstance<NetworkManager, T>();
        }

        /// <summary>
        /// Gets the local port status.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns>GetLocalPortStatusResult.</returns>
        public GetLocalPortStatusResult GetLocalPortStatus(int portNumber)
        {
            if (portNumber < 0 || portNumber > 65535)
            {
                return new GetLocalPortStatusResult
                {
                        Status = GetLocalPortStatusStatus.InvalidData
                };
            }

            GetLocalPortStatusResult result = null;
            try
            {
                result = OnGetLocalPortStatus(portNumber);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkManager)).Error(e.ToString());
            }
            return result ?? new GetLocalPortStatusResult();
        }

        /// <summary>
        /// Gets the network time.
        /// </summary>
        /// <returns>GetNetworkTimeResult.</returns>
        public GetNetworkTimeResult GetNetworkTime()
        {
            GetNetworkTimeResult result = null;
            try
            {
                result = OnGetNetworkTime();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkManager)).Error(e.ToString());
            }
            return result ?? new GetNetworkTimeResult();
        }

        /// <summary>
        /// Gets the unused local port.
        /// </summary>
        /// <returns>GetUnusedLocalPortResult.</returns>
        public GetUnusedLocalPortResult GetUnusedLocalPort()
        {
            return GetUnusedLocalPort(false);
        }

        /// <summary>
        /// Gets the unused local port.
        /// </summary>
        /// <param name="shouldUseLastPortFirst">if set to <c>true</c> use last port first.</param>
        /// <returns>GetUnusedLocalPortResult.</returns>
        public GetUnusedLocalPortResult GetUnusedLocalPort(bool shouldUseLastPortFirst)
        {
            GetUnusedLocalPortResult result = null;
            try
            {
                result = OnGetUnusedLocalPort(shouldUseLastPortFirst);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkManager)).Error(e.ToString());
            }
            return result ?? new GetUnusedLocalPortResult();
        }

        /// <summary>
        /// Traces the route.
        /// </summary>
        /// <param name="hostNameOrIpAddress">The host name or ip address.</param>
        /// <returns>TraceRouteResult.</returns>
        public TraceRouteResult TraceRoute(string hostNameOrIpAddress)
        {
            return TraceRoute(
                    hostNameOrIpAddress,
                    CancellationToken.None
            );
        }

        /// <summary>
        /// Traces the route.
        /// </summary>
        /// <param name="hostNameOrIpAddress">The host name or ip address.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>TraceRouteResult.</returns>
        public TraceRouteResult TraceRoute(
                string hostNameOrIpAddress,
                CancellationToken cancellationToken)
        {
            return TraceRoute(
                    hostNameOrIpAddress,
                    20,
                    1000 * 5,
                    cancellationToken
            );
        }

        /// <summary>
        /// Traces the route.
        /// </summary>
        /// <param name="hostNameOrIpAddress">The host name or ip address.</param>
        /// <param name="maxHop">The maximum hop.</param>
        /// <param name="timeoutInMilli">The timeout in millisecond.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>TraceRouteResult.</returns>
        public TraceRouteResult TraceRoute(
                string hostNameOrIpAddress,
                int maxHop,
                int timeoutInMilli,
                CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(hostNameOrIpAddress))
            {
                return new TraceRouteResult
                {
                        Status = TraceRouteStatus.InvalidData
                };
            }

            if (maxHop <= 0 || timeoutInMilli <= 0)
            {
                return new TraceRouteResult
                {
                        Status = TraceRouteStatus.InvalidData
                };
            }

            TraceRouteResult result = null;
            try
            {
                result = OnTraceRoute(
                        hostNameOrIpAddress,
                        maxHop,
                        timeoutInMilli,
                        cancellationToken
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkManager)).Error(e.ToString());
            }
            return result ?? new TraceRouteResult();
        }

        /// <summary>
        /// Verifies the local port status.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns>VerifyLocalPortStatusResult.</returns>
        public VerifyLocalPortStatusResult VerifyLocalPortStatus(int portNumber)
        {
            if (portNumber < 0 || portNumber > 65535)
            {
                return new VerifyLocalPortStatusResult
                {
                        Status = VerifyLocalPortStatusStatus.InvalidData
                };
            }

            VerifyLocalPortStatusResult result = null;
            try
            {
                result = OnVerifyLocalPortStatus(portNumber);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkManager)).Error(e.ToString());
            }
            return result ?? new VerifyLocalPortStatusResult();
        }

        /// <summary>
        /// Called when getting local port status.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns>GetLocalPortStatusResult.</returns>
        protected abstract GetLocalPortStatusResult OnGetLocalPortStatus(int portNumber);
        /// <summary>
        /// Called when getting network time.
        /// </summary>
        /// <returns>GetNetworkTimeResult.</returns>
        protected abstract GetNetworkTimeResult OnGetNetworkTime();
        /// <summary>
        /// Called when getting unused local port.
        /// </summary>
        /// <param name="shouldUseLastPortFirst">if set to <c>true</c> use last port first.</param>
        /// <returns>GetUnusedLocalPortResult.</returns>
        protected abstract GetUnusedLocalPortResult OnGetUnusedLocalPort(bool shouldUseLastPortFirst);
        /// <summary>
        /// Called when tracing route.
        /// </summary>
        /// <param name="hostNameOrIpAddress">The host name or ip address.</param>
        /// <param name="maxHop">The maximum hop.</param>
        /// <param name="timeoutInMilli">The timeout in millisecond.</param>
        /// <param name="cancellationToken">The cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
        /// <returns>TraceRouteResult.</returns>
        protected abstract TraceRouteResult OnTraceRoute(
                string hostNameOrIpAddress,
                int maxHop,
                int timeoutInMilli,
                CancellationToken cancellationToken
        );
        /// <summary>
        /// Called when verifying local port status.
        /// </summary>
        /// <param name="portNumber">The port number.</param>
        /// <returns>VerifyLocalPortStatusResult.</returns>
        protected abstract VerifyLocalPortStatusResult OnVerifyLocalPortStatus(int portNumber);
    }
}
