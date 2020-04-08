using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class WebUserAgentV2Factory.
    /// </summary>
    public abstract class WebUserAgentV2Factory
    {
        private const string DefaultName = "Unknown";

        private static Dictionary<string, WebUserAgentV2Factory> Instances { get; } = new Dictionary<string, WebUserAgentV2Factory>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultWebUserAgentV2Factory);
        private static string _name = DefaultName;

        /// <summary>
        /// Gets or sets the user agent name.
        /// </summary>
        /// <value>The user agent name.</value>
        public static string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
            }
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : WebUserAgentV2Factory
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(WebUserAgentV2Factory)).Info($"Registered default {typeof(WebUserAgentV2Factory).Name} type to {_defaultType}");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WebUserAgentV2Factory.</returns>
        public static WebUserAgentV2Factory GetInstance()
        {
            WebUserAgentV2Factory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebUserAgentV2Factory)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(WebUserAgentV2Factory)).Info($"Initializing {typeof(DefaultWebUserAgentV2Factory).FullName}...");
                instance = new DefaultWebUserAgentV2Factory();
            }
            return instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WebUserAgentV2Factory.</returns>
        public static WebUserAgentV2Factory GetInstance<T>() where T : WebUserAgentV2Factory
        {
            WebUserAgentV2Factory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebUserAgentV2Factory)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(WebUserAgentV2Factory)).Info($"Initializing {typeof(DefaultWebUserAgentV2Factory).FullName}...");
                instance = new DefaultWebUserAgentV2Factory();
            }
            return instance;
        }

        private static WebUserAgentV2Factory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException($"Invalid arguments to get {typeof(WebUserAgentV2Factory).Name} instance");
            }

            var key = $"{type.FullName}_";
            WebUserAgentV2Factory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(WebUserAgentV2Factory)).Info($"Initializing {key}...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (WebUserAgentV2Factory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(WebUserAgentV2Factory)).Info($"Initializing {typeof(DefaultWebUserAgentV2Factory).FullName}...");
                instance = new DefaultWebUserAgentV2Factory();
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
        /// Gets the web user agent.
        /// </summary>
        /// <returns>WebUserAgentV2.</returns>
        public WebUserAgentV2 GetWebUserAgent()
        {
            WebUserAgentV2 result = null;
            try
            {
                result = OnGetWebUserAgent();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebUserAgentV2Factory)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when getting web user agent.
        /// </summary>
        /// <returns>WebUserAgentV2.</returns>
        protected abstract WebUserAgentV2 OnGetWebUserAgent();
    }
}
