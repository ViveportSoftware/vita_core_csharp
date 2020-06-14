using System.Net;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class DefaultDns.
    /// Implements the <see cref="Dns" />
    /// </summary>
    /// <seealso cref="Dns" />
    public class DefaultDns : Dns
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultDns"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        public DefaultDns(string resolver) : base(resolver)
        {
            Logger.GetInstance(typeof(DefaultDns)).Warn("This implementation does not support custom resolver");
        }

        /// <inheritdoc />
        protected override bool OnFlushCache()
        {
            return Interop.Windows.DnsFlushResolverCache();
        }

        /// <inheritdoc />
        protected override bool OnFlushCache(string hostName)
        {
            return Interop.Windows.DnsFlushResolverCacheEntry_W(hostName);
        }

        /// <inheritdoc />
        protected override IPAddress[] OnGetHostAddresses(string hostNameOrAddress)
        {
            return System.Net.Dns.GetHostAddresses(hostNameOrAddress);
        }

        /// <inheritdoc />
        protected override IPHostEntry OnGetHostEntry(IPAddress ipAddress)
        {
            return System.Net.Dns.GetHostEntry(ipAddress);
        }

        /// <inheritdoc />
        protected override IPHostEntry OnGetHostEntry(string hostNameOrAddress)
        {
            return System.Net.Dns.GetHostEntry(hostNameOrAddress);
        }
    }
}
