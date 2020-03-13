using System;
using System.Collections.Generic;
using System.Net;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public abstract class WebRequestFactory
    {
        private static Dictionary<string, WebRequestFactory> Instances { get; } = new Dictionary<string, WebRequestFactory>();

        private static readonly object InstancesLock = new object();

        private static Type defaultType = typeof(DefaultWebRequestFactory);

        public static void Register<T>() where T : WebRequestFactory
        {
            defaultType = typeof(T);
            Logger.GetInstance(typeof(WebRequestFactory)).Info("Registered default " + typeof(WebRequestFactory).Name + " type to " + defaultType);
        }

        public static WebRequestFactory GetInstance()
        {
            WebRequestFactory instance;
            try
            {
                instance = DoGetInstance(defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebRequestFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(WebRequestFactory)).Info("Initializing " + typeof(DefaultWebRequestFactory).FullName + "...");
                instance = new DefaultWebRequestFactory();
            }
            return instance;
        }

        public static WebRequestFactory GetInstance<T>() where T : WebRequestFactory
        {
            WebRequestFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebRequestFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(WebRequestFactory)).Info("Initializing " + typeof(DefaultWebRequestFactory).FullName + "...");
                instance = new DefaultWebRequestFactory();
            }
            return instance;
        }

        private static WebRequestFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + typeof(WebRequestFactory).Name + " instance");
            }

            var key = type.FullName + "_";
            WebRequestFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(WebRequestFactory)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (WebRequestFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(WebRequestFactory)).Info("Initializing " + typeof(DefaultWebRequestFactory).FullName + "...");
                instance = new DefaultWebRequestFactory();
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

        public HttpWebRequest GetHttpWebRequest(Uri uri)
        {
            if (uri == null)
            {
                return null;
            }
            HttpWebRequest result = null;
            try
            {
                result = OnGetHttpWebRequest(uri);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebRequestFactory)).Error(e.ToString());
            }
            return result;
        }

        protected abstract HttpWebRequest OnGetHttpWebRequest(Uri uri);
    }
}
