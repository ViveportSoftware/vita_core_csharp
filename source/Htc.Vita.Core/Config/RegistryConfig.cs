using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Config
{
    /// <summary>
    /// Class RegistryConfig.
    /// Implements the <see cref="Config" />
    /// </summary>
    /// <seealso cref="Config" />
    public class RegistryConfig : Config
    {
        private const string KeyPath = "SOFTWARE\\HTC\\Vita\\Config";

        private readonly Dictionary<string, string> _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistryConfig"/> class.
        /// </summary>
        public RegistryConfig()
        {
            _map = new Dictionary<string, string>();
            _map = AppendByStringValue(
                    _map,
                    Win32Registry.Hive.LocalMachine,
                    KeyPath,
                    Win32Registry.View.Registry32
            );
            _map = AppendByStringValue(
                    _map,
                    Win32Registry.Hive.LocalMachine,
                    KeyPath,
                    Win32Registry.View.Registry64
            );
        }

        private static Dictionary<string, string> AppendByStringValue(
                Dictionary<string, string> properties,
                Win32Registry.Hive root,
                string keyPath,
                Win32Registry.View view)
        {
            if (properties == null || string.IsNullOrEmpty(keyPath))
            {
                return properties;
            }
            try
            {
                using (var baseKey = Win32Registry.Key.OpenBaseKey(
                        root,
                        view))
                {
                    using (var subKey = baseKey.OpenSubKey(
                            keyPath,
                            Win32Registry.KeyPermissionCheck.ReadSubTree))
                    {
                        if (subKey != null)
                        {
                            foreach (var valueName in subKey.GetValueNames())
                            {
                                if (subKey.GetValueKind(valueName) == Win32Registry.ValueKind.String)
                                {
                                    properties.Add(
                                            valueName,
                                            (string)subKey.GetValue(valueName)
                                    );
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(RegistryConfig)).Error($"Getting registry string value error: {e.Message}");
            }
            return properties;
        }

        /// <inheritdoc />
        protected override bool OnHasKey(string key)
        {
            return _map != null && _map.ContainsKey(key);
        }

        /// <inheritdoc />
        protected override string OnGet(string key)
        {
            string result = null;
            if (_map != null && _map.ContainsKey(key))
            {
                result = _map[key];
            }
            return result;
        }
    }
}
