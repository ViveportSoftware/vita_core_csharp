using System.Collections.Generic;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace Htc.Vita.Core.Net
{
    public static partial class RouteTracer
    {
        public static List<Hop> Trace(string hostNameOrIpAddress, int maxHop, int timeoutInMilli)
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

            var ping = new Ping();
            var pingOptions = new PingOptions(1, true);
            var pingReplyTime = new Stopwatch();

            while (true)
            {
                pingReplyTime.Start();
                var reply = ping.Send(hostNameOrIpAddress, currentTimeoutInMilli, new byte[] { 0 }, pingOptions);
                pingReplyTime.Stop();

                result.Add(new Hop
                {
                        Node = pingOptions.Ttl,
                        IP = reply?.Address?.ToString(),
                        Hostname = Dns.GetInstance().GetHostEntry(reply?.Address)?.HostName,
                        Time = pingReplyTime.ElapsedMilliseconds,
                        Status = reply?.Status.ToString()
                });

                pingOptions.Ttl++;
                pingReplyTime.Reset();
                if (pingOptions.Ttl > currentMaxHop || IPStatus.Success == reply?.Status)
                {
                    break;
                }
            }

            return result;
        }
    }
}
