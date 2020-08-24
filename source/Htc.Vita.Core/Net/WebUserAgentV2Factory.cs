using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class WebUserAgentV2Factory.
    /// </summary>
    public abstract class WebUserAgentV2Factory
    {
        private const string DefaultName = "Unknown";

        private static string _name = DefaultName;

        static WebUserAgentV2Factory()
        {
            TypeRegistry.RegisterDefault<WebUserAgentV2Factory, DefaultWebUserAgentV2Factory>();
        }

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
        public static void Register<T>()
                where T : WebUserAgentV2Factory, new()
        {
            TypeRegistry.Register<WebUserAgentV2Factory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WebUserAgentV2Factory.</returns>
        public static WebUserAgentV2Factory GetInstance()
        {
            return TypeRegistry.GetInstance<WebUserAgentV2Factory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WebUserAgentV2Factory.</returns>
        public static WebUserAgentV2Factory GetInstance<T>()
                where T : WebUserAgentV2Factory, new()
        {
            return TypeRegistry.GetInstance<WebUserAgentV2Factory, T>();
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
