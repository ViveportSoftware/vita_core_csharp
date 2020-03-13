namespace Htc.Vita.Core.Net
{
    public static partial class RouteTracer
    {
        public class Hop
        {
            private const string NotAvailable = "N/A";

            private string _hostname = NotAvailable;
            private string _ip = NotAvailable;

            public int Node { get; set; }

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

            public long Time { get; set; }
            public string Status { get; set; }

            public override string ToString()
            {
                var result = "[" + Node + "] " + IP;
                if (!string.IsNullOrWhiteSpace(Hostname) && !NotAvailable.Equals(Hostname))
                {
                    result += " (" + Hostname + ")";
                }
                result += ", " + Time + "ms";
                return result;
            }
        }
    }
}
