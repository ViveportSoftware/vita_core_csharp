using System;
using System.Collections.Generic;
using System.Threading;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class RouteTracer.
    /// </summary>
    [Obsolete("This class is obsoleted.")]
    public static partial class RouteTracer
    {
        /// <summary>
        /// Traces the specified host name or ip address.
        /// </summary>
        /// <param name="hostNameOrIpAddress">The host name or ip address.</param>
        /// <param name="maxHop">The maximum hop.</param>
        /// <param name="timeoutInMilli">The timeout in millisecond.</param>
        /// <returns>List&lt;Hop&gt;.</returns>
        public static List<Hop> Trace(
                string hostNameOrIpAddress,
                int maxHop,
                int timeoutInMilli)
        {
            var result = new List<Hop>();
            if (string.IsNullOrWhiteSpace(hostNameOrIpAddress))
            {
                return result;
            }

            var currentMaxHop = maxHop;
            if (currentMaxHop <= 0)
            {
                currentMaxHop = 1;
            }

            if (currentMaxHop > 30)
            {
                currentMaxHop = 30;
            }

            var currentTimeoutInMilli = timeoutInMilli;
            if (currentTimeoutInMilli <= 0)
            {
                currentTimeoutInMilli = 1;
            }

            var traceRouteResult = NetworkManager.GetInstance().TraceRoute(
                    hostNameOrIpAddress,
                    currentMaxHop,
                    currentTimeoutInMilli,
                    CancellationToken.None
            );
            var traceRouteStatus = traceRouteResult.Status;
            if (traceRouteStatus != NetworkManager.TraceRouteStatus.Ok)
            {
                return result;
            }

            var hops = traceRouteResult.Route.Hops;
            foreach (var routeHopInfo in hops)
            {
                if (routeHopInfo == null)
                {
                    continue;
                }

                result.Add(new Hop
                {
                        Hostname = routeHopInfo.Hostname,
                        IP = routeHopInfo.Address?.ToString(),
                        Node = routeHopInfo.Node,
                        Status = routeHopInfo.Status.ToString(),
                        Time = routeHopInfo.TimeInMilli
                });
            }

            return result;
        }
    }
}
