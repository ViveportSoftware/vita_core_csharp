using System;
using System.Net;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class WebProxyFactory.
    /// </summary>
    public abstract class WebProxyFactory
    {
        static WebProxyFactory()
        {
            TypeRegistry.RegisterDefault<WebProxyFactory, DefaultWebProxyFactory>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : WebProxyFactory, new()
        {
            TypeRegistry.Register<WebProxyFactory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WebProxyFactory.</returns>
        public static WebProxyFactory GetInstance()
        {
            return TypeRegistry.GetInstance<WebProxyFactory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WebProxyFactory.</returns>
        public static WebProxyFactory GetInstance<T>()
                where T : WebProxyFactory, new()
        {
            return TypeRegistry.GetInstance<WebProxyFactory, T>();
        }

        /// <summary>
        /// Parses the web proxy URI.
        /// </summary>
        /// <param name="webProxy">The web proxy.</param>
        /// <returns>Uri.</returns>
        protected static Uri ParseWebProxyUri(string webProxy)
        {
            if (string.IsNullOrWhiteSpace(webProxy))
            {
                return null;
            }

            Uri result = null;
            try
            {
                result = new Uri(webProxy);
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(WebProxyFactory)).Error($"Cannot parse web proxy uri: {webProxy}");
            }
            return result;
        }

        /// <summary>
        /// Gets the web proxy.
        /// </summary>
        /// <returns>IWebProxy.</returns>
        public IWebProxy GetWebProxy()
        {
            IWebProxy result = null;
            try
            {
                result = OnGetWebProxy();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebProxyFactory)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Gets the web proxy status.
        /// </summary>
        /// <param name="webProxy">The web proxy.</param>
        /// <returns>WebProxyStatus.</returns>
        public WebProxyStatus GetWebProxyStatus(IWebProxy webProxy)
        {
            if (webProxy == null)
            {
                return WebProxyStatus.NotSet;
            }

            var result = WebProxyStatus.Unknown;
            try
            {
                result = OnGetWebProxyStatus(webProxy);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebProxyFactory)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when getting web proxy.
        /// </summary>
        /// <returns>IWebProxy.</returns>
        protected abstract IWebProxy OnGetWebProxy();
        /// <summary>
        /// Called when getting web proxy status.
        /// </summary>
        /// <param name="webProxy">The web proxy.</param>
        /// <returns>WebProxyStatus.</returns>
        protected abstract WebProxyStatus OnGetWebProxyStatus(IWebProxy webProxy);

        /// <summary>
        /// Enum WebProxyStatus
        /// </summary>
        public enum WebProxyStatus
        {
            /// <summary>
            /// Unknown proxy status
            /// </summary>
            Unknown,
            /// <summary>
            /// The proxy status is not set
            /// </summary>
            NotSet,
            /// <summary>
            /// The proxy is working
            /// </summary>
            Working,
            /// <summary>
            /// Cannot test the proxy status
            /// </summary>
            CannotTest
        }
    }
}
