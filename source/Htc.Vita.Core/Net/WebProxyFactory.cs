using System;
using System.Collections.Generic;
using System.Net;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public abstract class WebProxyFactory
    {
        private static Dictionary<string, WebProxyFactory> Instances { get; } = new Dictionary<string, WebProxyFactory>();
        private static Type _defaultType = typeof(DefaultWebProxyFactory);

        private readonly Logger _logger;

        protected WebProxyFactory()
        {
            _logger = Logger.GetInstance();
        }

        public static void Register<T>() where T : WebProxyFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance().Info("Registered default web proxy factory type to " + _defaultType);
        }

        public static WebProxyFactory GetInstance()
        {
            WebProxyFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultWebProxyFactory).FullName + "...");
                instance = new DefaultWebProxyFactory();
            }
            return instance;
        }

        public static WebProxyFactory GetInstance<T>() where T : WebProxyFactory
        {
            WebProxyFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultWebProxyFactory).FullName + "...");
                instance = new DefaultWebProxyFactory();
            }
            return instance;
        }

        private static WebProxyFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get web request factory instance");
            }

            var key = type.FullName;
            WebProxyFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (WebProxyFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + typeof(DefaultWebProxyFactory).FullName + "...");
                instance = new DefaultWebProxyFactory();
            }
            if (!Instances.ContainsKey(key))
            {
                Instances.Add(key, instance);
            }
            return instance;
        }

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
                Logger.GetInstance().Error("Cannot parse web proxy uri: " + webProxy);
            }
            return result;
        }

        public IWebProxy GetWebProxy()
        {
            IWebProxy result = null;
            try
            {
                result = OnGetWebProxy();
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
            return result;
        }

        protected abstract IWebProxy OnGetWebProxy();
    }
}
