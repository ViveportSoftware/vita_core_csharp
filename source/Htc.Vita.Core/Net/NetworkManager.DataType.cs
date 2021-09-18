using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;

namespace Htc.Vita.Core.Net
{
    public partial class NetworkManager
    {
        /// <summary>
        /// Class NetworkTimeInfo.
        /// </summary>
        public class NetworkTimeInfo
        {
            /// <summary>
            /// Gets or sets the provider name.
            /// </summary>
            /// <value>The provider name.</value>
            public string ProviderName { get; set; }
            /// <summary>
            /// Gets or sets the time in UTC.
            /// </summary>
            /// <value>The time in UTC.</value>
            public DateTime TimeInUtc { get; set; }
        }

        /// <summary>
        /// Class RouteHopInfo.
        /// </summary>
        public class RouteHopInfo
        {
            private const string NotAvailable = "N/A";

            private IPAddress _address;
            private string _hostname;

            /// <summary>
            /// Gets or sets the address.
            /// </summary>
            /// <value>The address.</value>
            public IPAddress Address
            {
                get { return _address; }
                set
                {
                    if (value != null)
                    {
                        _address = value;
                    }
                }
            }
            /// <summary>
            /// Gets or sets the hostname.
            /// </summary>
            /// <value>The hostname.</value>
            public string Hostname
            {
                get { return _hostname; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        _hostname = value;
                    }
                }
            }
            /// <summary>
            /// Gets or sets the node.
            /// </summary>
            /// <value>The node.</value>
            public int Node { get; set; }
            /// <summary>
            /// Gets or sets the time in millisecond.
            /// </summary>
            /// <value>The time in millisecond.</value>
            public long TimeInMilli { get; set; }
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            internal IPStatus Status { get; set; }

            /// <summary>
            /// Converts to string.
            /// </summary>
            /// <returns>System.String.</returns>
            public override string ToString()
            {
                var result = $"[{Node}] {Address?.ToString() ?? NotAvailable}";
                if (!string.IsNullOrWhiteSpace(Hostname))
                {
                    result += $" ({Hostname})";
                }
                result += $", {TimeInMilli}ms";
                return result;
            }
        }

        /// <summary>
        /// Class RouteInfo.
        /// </summary>
        public class RouteInfo
        {
            /// <summary>
            /// Gets or sets the hops.
            /// </summary>
            /// <value>The hops.</value>
            public List<RouteHopInfo> Hops { get; set; }
            /// <summary>
            /// Gets or sets the target.
            /// </summary>
            /// <value>The target.</value>
            public string Target { get; set; }
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
