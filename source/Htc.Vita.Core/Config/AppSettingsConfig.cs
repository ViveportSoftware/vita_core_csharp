using System;
using System.Configuration;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Config
{
    public class AppSettingsConfig : Config
    {
        private readonly KeyValueConfigurationCollection _properties;

        public AppSettingsConfig()
        {
            try
            {
                _properties = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).AppSettings.Settings;
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Getting app setting error: " + e);
            }
        }

        protected override bool OnHasKey(string key)
        {
            return _properties?[key] != null;
        }

        protected override string OnGet(string key)
        {
            return _properties?[key]?.Value;
        }
    }
}
