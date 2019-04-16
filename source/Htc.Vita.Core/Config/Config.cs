using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Config
{
    public abstract class Config
    {
        private static Dictionary<string, Config> Instances { get; } = new Dictionary<string, Config>();

        private static readonly object InstancesLock = new object();

        private static Type defaultType = typeof(AppSettingsConfig);

        public static void Register<T>() where T : Config
        {
            defaultType = typeof(T);
            Logger.GetInstance(typeof(Config)).Info("Registered default " + typeof(Config).Name + " type to " + defaultType);
        }

        public static Config GetInstance()
        {
            Config instance;
            try
            {
                instance = DoGetInstance(defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Config)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(Config)).Info("Initializing " + typeof(AppSettingsConfig).FullName + "...");
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
                Logger.GetInstance(typeof(Config)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(Config)).Info("Initializing " + typeof(AppSettingsConfig).FullName + "...");
                instance = new AppSettingsConfig();
            }
            return instance;
        }

        private static Config DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + typeof(Config).Name + " instance");
            }

            var key = type.FullName + "_";
            Config instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Config)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (Config)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Config)).Info("Initializing " + typeof(AppSettingsConfig).FullName + "...");
                instance = new AppSettingsConfig();
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

        public bool HasKey(string key)
        {
            var result = false;
            try
            {
                result = OnHasKey(key);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Config)).Error(e.ToString());
            }
            return result;
        }

        public string Get(string key)
        {
            return Get(key, null);
        }

        public string Get(string key, string defaultValue)
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
                Logger.GetInstance(typeof(Config)).Error(e.ToString());
            }

            return result ?? defaultValue;
        }

        public bool GetBool(string key)
        {
            return GetBool(key, false);
        }

        public bool GetBool(string key, bool defaultValue)
        {
            return Util.Convert.ToBool(Get(key), defaultValue);
        }

        public double GetDouble(string key)
        {
            return GetDouble(key, 0.0D);
        }

        public double GetDouble(string key, double defaultValue)
        {
            return Util.Convert.ToDouble(Get(key), defaultValue);
        }

        public int GetInt(string key)
        {
            return GetInt(key, 0);
        }

        public int GetInt(string key, int defaultValue)
        {
            return Util.Convert.ToInt32(Get(key), defaultValue);
        }

        public long GetLong(string key)
        {
            return GetLong(key, 0L);
        }

        public long GetLong(string key, long defaultValue)
        {
            return Util.Convert.ToInt64(Get(key), defaultValue);
        }

        protected abstract bool OnHasKey(string key);
        protected abstract string OnGet(string key);
    }
}
