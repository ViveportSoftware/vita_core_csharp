using System.Net;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public class DefaultDns : Dns
    {
        public DefaultDns(string resolver) : base(resolver)
        {
            Logger.GetInstance(typeof(DefaultDns)).Warn("This implementation does not support custom resolver");
        }

        protected override bool OnFlushCache()
        {
            return Interop.Windows.DnsFlushResolverCache();
        }

        protected override bool OnFlushCache(string hostName)
        {
            return Interop.Windows.DnsFlushResolverCacheEntry_W(hostName);
        }

        protected override IPAddress[] OnGetHostAddresses(string hostNameOrAddress)
        {
            return System.Net.Dns.GetHostAddresses(hostNameOrAddress);
        }

        protected override IPHostEntry OnGetHostEntry(IPAddress ipAddress)
        {
            return System.Net.Dns.GetHostEntry(ipAddress);
        }

        protected override IPHostEntry OnGetHostEntry(string hostNameOrAddress)
        {
            return System.Net.Dns.GetHostEntry(hostNameOrAddress);
        }
    }
}
