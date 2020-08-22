namespace Htc.Vita.Core.Net
{
    public static partial class RouteTracer
    {
        /// <summary>
        /// Class Hop.
        /// </summary>
        public class Hop
        {
            private const string NotAvailable = "N/A";

            private string _hostname = NotAvailable;
            private string _ip = NotAvailable;

            /// <summary>
            /// Gets or sets the node.
            /// </summary>
            /// <value>The node.</value>
            public int Node { get; set; }

            /// <summary>
            /// Gets or sets the IP Address.
            /// </summary>
            /// <value>The ip.</value>
            public string IP
            {
                get { return _ip; }
                set
                {
                    if (!string.IsNullOrWhiteSpace(value))
                    {
                        _ip = value;
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
            /// Gets or sets the time.
            /// </summary>
            /// <value>The time.</value>
            public long Time { get; set; }
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public string Status { get; set; }

            /// <summary>
            /// Converts to string.
            /// </summary>
            /// <returns>System.String.</returns>
            public override string ToString()
            {
                var result = $"[{Node}] {IP}";
                if (!string.IsNullOrWhiteSpace(Hostname) && !NotAvailable.Equals(Hostname))
                {
                    result += $" ({Hostname})";
                }
                result += $", {Time}ms";
                return result;
            }
        }
    }
}
