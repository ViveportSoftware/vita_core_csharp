using System;
using System.Net;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    public class DefaultWebProxyFactory : WebProxyFactory
    {
        private const string TestUrl = "https://www.microsoft.com/";

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

            var webProxyStatus = OnGetWebProxyStatus(result);
            if (webProxyStatus == WebProxyStatus.Working || webProxyStatus == WebProxyStatus.NotSet)
            {
                return result;
            }
            return new WebProxy();
        }

        protected override WebProxyStatus OnGetWebProxyStatus(IWebProxy webProxy)
        {
            var webProxyUri = webProxy.GetProxy(new Uri(TestUrl));
            if (webProxyUri == null)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error("Can not get proxy uri.");
                return WebProxyStatus.CannotTest;
            }

            if (webProxyUri.ToString().StartsWith(TestUrl))
            {
                return WebProxyStatus.NotSet;
            }

            Logger.GetInstance(typeof(DefaultWebProxyFactory)).Info("Possible web proxy uri: \"" + webProxyUri + "\"");
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
                var response = request.GetResponse() as HttpWebResponse;
                if (response != null)
                {
                    return WebProxyStatus.Working;
                }
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error("Can not get web response to test proxy.");
                return WebProxyStatus.CannotTest;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Warn("Can not process proxy test on uri: \"" + webProxyUri + "\", " + e.Message);
            }

            return WebProxyStatus.Unknown;
        }
    }
}
