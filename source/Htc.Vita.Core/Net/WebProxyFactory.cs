using System;
using System.Collections.Generic;
using System.Net;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class WebProxyFactory.
    /// </summary>
    public abstract class WebProxyFactory
    {
        private static Dictionary<string, WebProxyFactory> Instances { get; } = new Dictionary<string, WebProxyFactory>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultWebProxyFactory);

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : WebProxyFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(WebProxyFactory)).Info($"Registered default {typeof(WebProxyFactory).Name} type to {_defaultType}");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WebProxyFactory.</returns>
        public static WebProxyFactory GetInstance()
        {
            WebProxyFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebProxyFactory)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(WebProxyFactory)).Info($"Initializing {typeof(DefaultWebProxyFactory).FullName}...");
                instance = new DefaultWebProxyFactory();
            }
            return instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WebProxyFactory.</returns>
        public static WebProxyFactory GetInstance<T>() where T : WebProxyFactory
        {
            WebProxyFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebProxyFactory)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(WebProxyFactory)).Info($"Initializing {typeof(DefaultWebProxyFactory).FullName}...");
                instance = new DefaultWebProxyFactory();
            }
            return instance;
        }

        private static WebProxyFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException($"Invalid arguments to get {typeof(WebProxyFactory).Name} instance");
            }

            var key = $"{type.FullName}_";
            WebProxyFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(WebProxyFactory)).Info($"Initializing {key}...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (WebProxyFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(WebProxyFactory)).Info($"Initializing {typeof(DefaultWebProxyFactory).FullName}...");
                instance = new DefaultWebProxyFactory();
            }
            lock (InstancesLock)
            {
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
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
