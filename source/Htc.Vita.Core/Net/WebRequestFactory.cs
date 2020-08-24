using System;
using System.Net;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class WebRequestFactory.
    /// </summary>
    public abstract class WebRequestFactory
    {
        static WebRequestFactory()
        {
            TypeRegistry.RegisterDefault<WebRequestFactory, DefaultWebRequestFactory>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : WebRequestFactory, new()
        {
            TypeRegistry.Register<WebRequestFactory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WebRequestFactory.</returns>
        public static WebRequestFactory GetInstance()
        {
            return TypeRegistry.GetInstance<WebRequestFactory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WebRequestFactory.</returns>
        public static WebRequestFactory GetInstance<T>()
                where T : WebRequestFactory, new()
        {
            return TypeRegistry.GetInstance<WebRequestFactory, T>();
        }

        /// <summary>
        /// Gets the HTTP web request.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>HttpWebRequest.</returns>
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

        /// <summary>
        /// Called when getting HTTP web request.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>HttpWebRequest.</returns>
        protected abstract HttpWebRequest OnGetHttpWebRequest(Uri uri);
    }
}
