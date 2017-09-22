using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Config
{
    public abstract class Config
    {
        private static Dictionary<string, Config> Instances { get; } = new Dictionary<string, Config>();
        private static Type _defaultType = typeof(AppSettingsConfig);

        private readonly Logger _logger;

        protected Config()
        {
            _logger = Logger.GetInstance();
        }

        public static void Register<T>() where T : Config
        {
            _defaultType = typeof(T);
            Logger.GetInstance().Info("Registered default config type to " + _defaultType);
        }

        public static Config GetInstance()
        {
            Config instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(AppSettingsConfig).FullName + "...");
                instance = new AppSettingsConfig();
            }
            return instance;
        }

        public static Config GetInstance<T>() where T : Config
        {
            Config instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(AppSettingsConfig).FullName + "...");
                instance = new AppSettingsConfig();
            }
            return instance;
        }

        private static Config DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get config instance");
            }

            var key = type.FullName;
            Config instance = null;
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
                    instance = (Config)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + typeof(AppSettingsConfig).FullName + "...");
                instance = new AppSettingsConfig();
            }
            if (!Instances.ContainsKey(key))
            {
                Instances.Add(key, instance);
            }
            return instance;
        }

        public bool HasKey(string key)
        {
            var result = false;
            try
            {
                result = OnHasKey(key);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
            return result;
        }

        public string Get(string key, string defaultValue = null)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                return defaultValue;
            }

            string result = null;
            try
            {
                result = OnGet(key);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }

            return result ?? defaultValue;
        }

        public bool GetBool(string key, bool defaultValue = false)
        {
            return Util.Convert.ToBool(Get(key), defaultValue);
        }

        public double GetDouble(string key, double defaultValue = 0.0D)
        {
            return Util.Convert.ToDouble(Get(key), defaultValue);
        }

        public int GetInt(string key, int defaultValue = 0)
        {
            return Util.Convert.ToInt32(Get(key), defaultValue);
        }

        public float GetLong(string key, long defaultValue = 0L)
        {
            return Util.Convert.ToInt64(Get(key), defaultValue);
        }

        protected abstract bool OnHasKey(string key);
        protected abstract string OnGet(string key);
    }
}
