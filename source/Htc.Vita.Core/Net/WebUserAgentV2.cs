using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class WebUserAgentV2.
    /// </summary>
    public abstract class WebUserAgentV2
    {
        /// <summary>
        /// Gets the module instance name.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetModuleInstanceName()
        {
            string result = null;
            try
            {
                result = OnGetModuleInstanceName();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebUserAgentV2)).Error(e.ToString());
            }
            return result ?? "UnknownInstance";
        }

        /// <summary>
        /// Gets the module name.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetModuleName()
        {
            string result = null;
            try
            {
                result = OnGetModuleName();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebUserAgentV2)).Error(e.ToString());
            }
            return result ?? "Unknown";
        }

        /// <summary>
        /// Gets the module version.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetModuleVersion()
        {
            string result = null;
            try
            {
                result = OnGetModuleVersion();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WebUserAgentV2)).Error(e.ToString());
            }
            return result ?? "0.0.0.0";
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            return OnToString();
        }

        /// <summary>
        /// Called when getting module instance name.
        /// </summary>
        /// <returns>System.String.</returns>
        protected abstract string OnGetModuleInstanceName();
        /// <summary>
        /// Called when getting module name.
        /// </summary>
        /// <returns>System.String.</returns>
        protected abstract string OnGetModuleName();
        /// <summary>
        /// Called when getting module version.
        /// </summary>
        /// <returns>System.String.</returns>
        protected abstract string OnGetModuleVersion();
        /// <summary>
        /// Called when to string.
        /// </summary>
        /// <returns>System.String.</returns>
        protected abstract string OnToString();
    }
}
