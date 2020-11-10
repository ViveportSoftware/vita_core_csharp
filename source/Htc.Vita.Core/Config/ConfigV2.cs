using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Config
{
    /// <summary>
    /// Class ConfigV2.
    /// </summary>
    public abstract class ConfigV2
    {
        static ConfigV2()
        {
            TypeRegistry.RegisterDefault<ConfigV2, DummyConfigV2>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : ConfigV2, new()
        {
            TypeRegistry.Register<ConfigV2, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>ConfigV2.</returns>
        public static ConfigV2 GetInstance()
        {
            return TypeRegistry.GetInstance<ConfigV2>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>ConfigV2.</returns>
        public static ConfigV2 GetInstance<T>()
                where T : ConfigV2, new()
        {
            return TypeRegistry.GetInstance<ConfigV2, T>();
        }

        /// <summary>
        /// Get all keys.
        /// </summary>
        /// <returns>ISet&lt;System.String&gt;.</returns>
        public ISet<string> AllKeys()
        {
            ISet<string> result = null;
            try
            {
                result = OnAllKeys();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ConfigV2)).Error(e.ToString());
            }
            return result ?? new HashSet<string>();
        }


        /// <summary>
        /// Determines whether ConfigV2 has the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if ConfigV2 has the specified key; otherwise, <c>false</c>.</returns>
        public bool HasKey(string key)
        {
            var result = false;
            try
            {
                result = OnHasKey(key);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ConfigV2)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public string Get(string key)
        {
            return Get(key, null);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string Get(
                string key,
                string defaultValue)
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
                Logger.GetInstance(typeof(ConfigV2)).Error(e.ToString());
            }

            return result ?? defaultValue;
        }

        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Boolean.</returns>
        public bool GetBool(string key)
        {
            return GetBool(
                    key,
                    false
            );
        }

        /// <summary>
        /// Gets the bool value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Boolean.</returns>
        public bool GetBool(
                string key,
                bool defaultValue)
        {
            return Util.Convert.ToBool(
                    Get(key),
                    defaultValue
            );
        }

        /// <summary>
        /// Gets the double value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Double.</returns>
        public double GetDouble(string key)
        {
            return GetDouble(
                    key,
                    0.0D
            );
        }

        /// <summary>
        /// Gets the double value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public double GetDouble(
                string key,
                double defaultValue)
        {
            return Util.Convert.ToDouble(
                    Get(key),
                    defaultValue
            );
        }

        /// <summary>
        /// Gets the float value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Single.</returns>
        public float GetFloat(string key)
        {
            return GetFloat(
                    key,
                    0.0F
            );
        }

        /// <summary>
        /// Gets the float value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
        public float GetFloat(
                string key,
                float defaultValue)
        {
            return (float) Util.Convert.ToDouble(
                    Get(key),
                    defaultValue
            );
        }

        /// <summary>
        /// Gets the int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Int32.</returns>
        public int GetInt(string key)
        {
            return GetInt(
                    key,
                    0
            );
        }

        /// <summary>
        /// Gets the int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public int GetInt(
                string key,
                int defaultValue)
        {
            return Util.Convert.ToInt32(
                    Get(key),
                    defaultValue
            );
        }

        /// <summary>
        /// Gets the long value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Int64.</returns>
        public long GetLong(string key)
        {
            return GetLong(
                    key,
                    0L
            );
        }

        /// <summary>
        /// Gets the long value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public long GetLong(
                string key,
                long defaultValue)
        {
            return Util.Convert.ToInt64(
                    Get(key),
                    defaultValue
            );
        }

        /// <summary>
        /// Called when getting all keys.
        /// </summary>
        /// <returns>ISet&lt;System.String&gt;.</returns>
        protected abstract ISet<string> OnAllKeys();
        /// <summary>
        /// Called when determining whether Config has the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if Config has the specified key, <c>false</c> otherwise.</returns>
        protected abstract bool OnHasKey(string key);
        /// <summary>
        /// Called when getting value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnGet(string key);
    }
}
