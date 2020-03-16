using System;
using System.Collections.Generic;
using System.Net;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    public class DefaultWebProxyFactory : WebProxyFactory
    {
        private const string TestUrl = "https://www.microsoft.com/";
        private const long CachePeriodInMilli = 1000L * 5;

        private static readonly Dictionary<string, KeyValuePair<WebProxyStatus, long>> WebProxyStatusMap = new Dictionary<string, KeyValuePair<WebProxyStatus, long>>();

        protected override IWebProxy OnGetWebProxy()
        {
            var webProxyUri = ParseWebProxyUri(
                    Registry.GetStringValue(
                            Registry.Hive.CurrentUser,
                            "SOFTWARE\\Vita\\Core\\Net",
                            "Proxy"
                    )
            ) ?? ParseWebProxyUri(
                    Registry.GetStringValue(
                            Registry.Hive.LocalMachine,
                            "SOFTWARE\\Vita\\Core\\Net",
                            "Proxy"
                    )
            );
            var result = WebRequest.GetSystemWebProxy();
            if (webProxyUri != null)
            {
                result = new WebProxy(webProxyUri);
            }

            var webProxyStatus = WebProxyStatus.Unknown;
            try
            {
                webProxyStatus = OnGetWebProxyStatus(result);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error("Can not get web proxy status. error: " + e.Message);
            }
            if (webProxyStatus == WebProxyStatus.Working || webProxyStatus == WebProxyStatus.NotSet)
            {
                return result;
            }
            return new WebProxy();
        }

        protected override WebProxyStatus OnGetWebProxyStatus(IWebProxy webProxy)
        {
            Uri webProxyUri = null;
            try
            {
                webProxyUri = webProxy.GetProxy(new Uri(TestUrl));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error("Can not get proxy uri. error: " + e.Message);
            }
            if (webProxyUri == null)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error("Can not get proxy uri.");
                return WebProxyStatus.CannotTest;
            }

            var proxyUrl = webProxyUri.ToString();
            if (proxyUrl.StartsWith(TestUrl))
            {
                return WebProxyStatus.NotSet;
            }

            var currentTimeInMilli = Util.Convert.ToTimestampInMilli(DateTime.Now);
            if (WebProxyStatusMap.ContainsKey(proxyUrl))
            {
                var webProxyStatus = WebProxyStatusMap[proxyUrl].Key;
                var lastTimeInMilli = WebProxyStatusMap[proxyUrl].Value;
                if (currentTimeInMilli - lastTimeInMilli > 0L && currentTimeInMilli - lastTimeInMilli < CachePeriodInMilli)
                {
                    return webProxyStatus;
                }
            }

            var request = WebRequest.Create(webProxyUri) as HttpWebRequest;
            if (request == null)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error("Can not get web request to test proxy.");
                return WebProxyStatus.CannotTest;
            }

            request.Proxy = new WebProxy();
            request.Timeout = 2000;
            try
            {
                using (request.GetResponse())
                {
                    WebProxyStatusMap[proxyUrl] = new KeyValuePair<WebProxyStatus, long>(
                            WebProxyStatus.Working,
                            Util.Convert.ToTimestampInMilli(DateTime.Now)
                    );
                    return WebProxyStatus.Working;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Warn("Can not process proxy test on uri: \"" + proxyUrl + "\", " + e.Message);
            }

            WebProxyStatusMap[proxyUrl] = new KeyValuePair<WebProxyStatus, long>(
                    WebProxyStatus.Unknown,
                    Util.Convert.ToTimestampInMilli(DateTime.Now)
            );
            return WebProxyStatus.Unknown;
        }
    }
}
