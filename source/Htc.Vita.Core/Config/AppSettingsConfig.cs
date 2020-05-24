using System;
using System.Configuration;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Config
{
    /// <summary>
    /// Class AppSettingsConfig.
    /// Implements the <see cref="Config" />
    /// </summary>
    /// <seealso cref="Config" />
    public class AppSettingsConfig : Config
    {
        private readonly KeyValueConfigurationCollection _properties;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppSettingsConfig"/> class.
        /// </summary>
        public AppSettingsConfig()
        {
            try
            {
                _properties = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(AppSettingsConfig)).Fatal("Getting app setting error: " + e);
            }
        }

        /// <inheritdoc />
        protected override bool OnHasKey(string key)
        {
            return _properties?[key] != null;
        }

        /// <inheritdoc />
        protected override string OnGet(string key)
        {
            return _properties?[key]?.Value;
        }
    }
}
