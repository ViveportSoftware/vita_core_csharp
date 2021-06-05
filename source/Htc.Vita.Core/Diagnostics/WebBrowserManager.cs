using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class WebBrowserManager.
    /// </summary>
    public abstract partial class WebBrowserManager
    {
        static WebBrowserManager()
        {
            TypeRegistry.RegisterDefault<WebBrowserManager, DefaultWebBrowserManager>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : WebBrowserManager, new()
        {
            TypeRegistry.Register<WebBrowserManager, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WebBrowserManager.</returns>
        public static WebBrowserManager GetInstance()
        {
            return TypeRegistry.GetInstance<WebBrowserManager>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WebBrowserManager.</returns>
        public static WebBrowserManager GetInstance<T>()
                where T : WebBrowserManager, new()
        {
            return TypeRegistry.GetInstance<WebBrowserManager, T>();
        }

        /// <summary>
        /// Gets the installed WebBrowser list.
        /// </summary>
        /// <returns>GetInstalledWebBrowserListResult.</returns>
        public GetInstalledWebBrowserListResult GetInstalledWebBrowserList()
        {
            GetInstalledWebBrowserListResult result = null;
            try
            {
                result = OnGetInstalledWebBrowserList();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebBrowserManager)).Error(e.ToString());
            }
            return result ?? new GetInstalledWebBrowserListResult();
        }

        /// <summary>
        /// Launches the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns><c>true</c> if launching the specific URI successfully, <c>false</c> otherwise.</returns>
        public bool Launch(Uri uri)
        {
            if (uri == null)
            {
                return false;
            }

            var getInstalledWebBrowserListResult = GetInstalledWebBrowserList();
            var getInstalledWebBrowserListStatus = getInstalledWebBrowserListResult.Status;
            if (getInstalledWebBrowserListStatus != GetInstalledWebBrowserListStatus.Ok)
            {
                Logger.GetInstance(typeof(WebBrowserManager)).Error($"Can not find available web browser to launch uri. Status: {getInstalledWebBrowserListStatus}");
                return false;
            }

            var webBrowserList = getInstalledWebBrowserListResult.WebBrowserList;
            if (webBrowserList.Count <= 0)
            {
                Logger.GetInstance(typeof(WebBrowserManager)).Error("There is no available web browser to launch uri");
                return false;
            }

            foreach (var webBrowserInfo in webBrowserList)
            {
                if (Launch(uri, webBrowserInfo))
                {
                    Logger.GetInstance(typeof(WebBrowserManager)).Info($"Launch {uri} via {webBrowserInfo.Type} successfully");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Launches the specified URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="webBrowserInfo">The web browser information.</param>
        /// <returns><c>true</c> if launching the specific URI successfully, <c>false</c> otherwise.</returns>
        public bool Launch(
                Uri uri,
                WebBrowserInfo webBrowserInfo)
        {
            if (uri == null || webBrowserInfo == null)
            {
                return false;
            }

            var supportedScheme = webBrowserInfo.SupportedScheme.ToString().ToLowerInvariant();
            var currentScheme = uri.Scheme.ToLowerInvariant();
            if (!supportedScheme.Equals(currentScheme))
            {
                return false;
            }

            var result = false;
            try
            {
                result = OnLaunch(uri, webBrowserInfo);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebBrowserManager)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when getting installed WebBrowser list.
        /// </summary>
        /// <returns>GetInstalledWebBrowserListResult.</returns>
        protected abstract GetInstalledWebBrowserListResult OnGetInstalledWebBrowserList();
        /// <summary>
        /// Called when launching the specific URI.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <param name="webBrowserInfo">The web browser information.</param>
        /// <returns><c>true</c> if launching the specific URI successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnLaunch(
                Uri uri,
                WebBrowserInfo webBrowserInfo
        );
    }
}
