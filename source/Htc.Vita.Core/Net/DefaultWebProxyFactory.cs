using System;
using System.Collections.Generic;
using System.Net;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class DefaultWebProxyFactory.
    /// Implements the <see cref="WebProxyFactory" />
    /// </summary>
    /// <seealso cref="WebProxyFactory" />
    public class DefaultWebProxyFactory : WebProxyFactory
    {
        private const long CachePeriodInMilli = 1000L * 5;
        private const string RegistryKeyPath = "SOFTWARE\\Vita\\Core\\Net";
        private const string TestUrl = "https://www.microsoft.com/";

        private static readonly Dictionary<string, KeyValuePair<WebProxyStatus, long>> WebProxyStatusMap = new Dictionary<string, KeyValuePair<WebProxyStatus, long>>();

        private static bool ShouldTreatProtocolDetectErrorAsProxyError()
        {
            const string valueName = "TreatProtocolDetectErrorAsProxyError";
            var result = Win32Registry.GetIntValue(
                    Win32Registry.Hive.CurrentUser,
                    RegistryKeyPath,
                    valueName
            ) > 0;
            if (!result)
            {
                result = Win32Registry.GetIntValue(
                        Win32Registry.Hive.LocalMachine,
                        RegistryKeyPath,
                        valueName
                ) > 0;
            }
            return result;
        }

        /// <inheritdoc />
        protected override IWebProxy OnGetWebProxy()
        {
            const string valueName = "Proxy";
            var webProxyUri = ParseWebProxyUri(
                    Win32Registry.GetStringValue(
                            Win32Registry.Hive.CurrentUser,
                            RegistryKeyPath,
                            valueName
                    )
            ) ?? ParseWebProxyUri(
                    Win32Registry.GetStringValue(
                            Win32Registry.Hive.LocalMachine,
                            RegistryKeyPath,
                            valueName
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
                webProxyStatus = GetWebProxyStatus(
                        result
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error($"Can not get web proxy status. error: {e.Message}");
            }
            if (webProxyStatus == WebProxyStatus.Working || webProxyStatus == WebProxyStatus.NotSet)
            {
                return result;
            }
            return new WebProxy();
        }

        /// <inheritdoc />
        protected override WebProxyStatus OnGetWebProxyStatus(IWebProxy webProxy)
        {
            var testTarget = new Uri(TestUrl);
            Uri webProxyUri = null;
            try
            {
                webProxyUri = webProxy.GetProxy(testTarget);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error($"Can not get proxy uri for target {testTarget}, error: {e.Message}");
            }
            if (webProxyUri == null)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error($"Can not get proxy uri for target {testTarget}.");
                return WebProxyStatus.CannotTest;
            }

            var proxyUrl = webProxyUri.ToString();
            if (proxyUrl.StartsWith(testTarget.ToString()))
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
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Error($"Can not get web request to test proxy {webProxyUri}.");
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
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    using (var errorHttpWebResponse = e.Response as HttpWebResponse)
                    {
                        Logger.GetInstance(typeof(DefaultWebProxyFactory)).Warn($"Can not access proxy \"{proxyUrl}\" directly, status code: {errorHttpWebResponse?.StatusCode}");
                        if (!ShouldTreatProtocolDetectErrorAsProxyError())
                        {
                            WebProxyStatusMap[proxyUrl] = new KeyValuePair<WebProxyStatus, long>(
                                    WebProxyStatus.Working,
                                    Util.Convert.ToTimestampInMilli(DateTime.Now)
                            );
                            return WebProxyStatus.Working;
                        }
                    }
                }
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Warn($"Can not process proxy test on uri: \"{proxyUrl}\", {e.Message}");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebProxyFactory)).Warn($"Can not process proxy test on uri: \"{proxyUrl}\", {e.Message}");
            }

            WebProxyStatusMap[proxyUrl] = new KeyValuePair<WebProxyStatus, long>(
                    WebProxyStatus.Unknown,
                    Util.Convert.ToTimestampInMilli(DateTime.Now)
            );
            return WebProxyStatus.Unknown;
        }
    }
}
