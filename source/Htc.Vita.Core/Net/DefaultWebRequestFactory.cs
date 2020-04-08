using System;
using System.Net;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class DefaultWebRequestFactory.
    /// Implements the <see cref="WebRequestFactory" />
    /// </summary>
    /// <seealso cref="WebRequestFactory" />
    public class DefaultWebRequestFactory : WebRequestFactory
    {
        private const int TimeoutInMilli = 20 * 1000;

        /// <inheritdoc />
        protected override HttpWebRequest OnGetHttpWebRequest(Uri uri)
        {
            var result = WebRequest.Create(uri) as HttpWebRequest;
            if (result == null)
            {
                return null;
            }
            result.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            result.Proxy = WebProxyFactory.GetInstance().GetWebProxy();
            result.ServicePoint.Expect100Continue = false;
            result.Timeout = GetTimeoutInMilli();
            result.UserAgent = GetUserAgentString();
            return result;
        }

        /// <summary>
        /// Gets the user agent string.
        /// </summary>
        /// <returns>System.String.</returns>
        protected virtual string GetUserAgentString()
        {
            return new WebUserAgent().ToString();
        }

        /// <summary>
        /// Gets the timeout in millisecond.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected virtual int GetTimeoutInMilli()
        {
            return TimeoutInMilli;
        }
    }
}
