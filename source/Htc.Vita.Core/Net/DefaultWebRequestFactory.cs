using System;
using System.Net;

namespace Htc.Vita.Core.Net
{
    public class DefaultWebRequestFactory : WebRequestFactory
    {
        private const int TimeoutInMiniSec = 20 * 1000;

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
            result.Timeout = GetTimeoutInMiniSec();
            result.UserAgent = GetUserAgentString();
            return result;
        }

        protected virtual string GetUserAgentString()
        {
            return new UserAgent().ToString();
        }

        protected virtual int GetTimeoutInMiniSec()
        {
            return TimeoutInMiniSec;
        }
    }
}
