using System;
using Htc.Vita.Core.Log;
using Microsoft.Win32;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class Registry.
    /// </summary>
    public abstract class Registry
    {
        /// <summary>
        /// Deletes the key.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool DeleteKey(Hive root, string keyPath, string keyName)
        {
            return DeleteKey32(root, keyPath, keyName) && DeleteKey64(root, keyPath, keyName);
        }

        /// <summary>
        /// Deletes the key in 32-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool DeleteKey32(Hive root, string keyPath, string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyPath) || string.IsNullOrEmpty(keyName))
            {
                return false;
            }
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry32))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        subKey?.DeleteSubKeyTree(keyName, false);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail deleting key \"{keyName}\" from 32-bit registry: {e}");
            }
            return false;
        }

        /// <summary>
        /// Deletes the key in 64-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="keyName">Name of the key.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool DeleteKey64(Hive root, string keyPath, string keyName)
        {
            if (string.IsNullOrWhiteSpace(keyPath) || string.IsNullOrEmpty(keyName))
            {
                return false;
            }
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry64))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        subKey?.DeleteSubKeyTree(keyName, false);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail deleting key \"{keyName}\" from 64-bit registry: {e}");
            }
            return false;
        }

        /// <summary>
        /// Deletes the value.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool DeleteValue(Hive root, string keyPath, string valueName)
        {
            return DeleteValue32(root, keyPath, valueName) && DeleteValue64(root, keyPath, valueName);
        }

        /// <summary>
        /// Deletes the value in 32-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool DeleteValue32(Hive root, string keyPath, string valueName)
        {
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry32))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        subKey?.DeleteValue(valueName, false);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail deleting value \"{valueName}\" from 32-bit registry: {e}");
            }
            return false;
        }

        /// <summary>
        /// Deletes the value in 64-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool DeleteValue64(Hive root, string keyPath, string valueName)
        {
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry64))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadWriteSubTree))
                    {
                        subKey?.DeleteValue(valueName, false);
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail deleting value \"{valueName}\" from 64-bit registry: {e}");
            }
            return false;
        }

        /// <summary>
        /// Gets the DWord value in 32-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetDwordValue32(Hive root, string keyPath, string valueName)
        {
            return GetDwordValue32(root, keyPath, valueName, 0);
        }

        /// <summary>
        /// Gets the DWord value in 32-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetDwordValue32(Hive root, string keyPath, string valueName, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(keyPath))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry32))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        if (subKey == null)
                        {
                            return defaultValue;
                        }
                        var value = subKey.GetValue(valueName);
                        if (value == null)
                        {
                            return defaultValue;
                        }
                        if (subKey.GetValueKind(valueName) == RegistryValueKind.DWord)
                        {
                            result = (int)value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail getting dword value with name \"{valueName}\" from 32-bit registry: {e.Message}");
            }

            return result;
        }

        /// <summary>
        /// Gets the DWord value in 64-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetDwordValue64(Hive root, string keyPath, string valueName)
        {
            return GetDwordValue64(root, keyPath, valueName, 0);
        }

        /// <summary>
        /// Gets the DWord value in 64-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetDwordValue64(Hive root, string keyPath, string valueName, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(keyPath))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry64))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        if (subKey == null)
                        {
                            return defaultValue;
                        }
                        var value = subKey.GetValue(valueName);
                        if (value == null)
                        {
                            return defaultValue;
                        }
                        if (subKey.GetValueKind(valueName) == RegistryValueKind.DWord)
                        {
                            result = (int)value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail getting dword value with name \"{valueName}\" from 64-bit registry: {e.Message}");
            }

            return result;
        }

        /// <summary>
        /// Gets the int value.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>System.Int32.</returns>
        [Obsolete("This method is obsoleted. Use Win32Registry.GetIntValue() instead.")]
        public static int GetIntValue(Hive root, string keyPath, string valueName)
        {
            return GetIntValue(root, keyPath, valueName, 0);
        }

        /// <summary>
        /// Gets the int value.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int GetIntValue(Hive root, string keyPath, string valueName, int defaultValue)
        {
            var result = Convert.ToInt32(GetStringValue(root, keyPath, valueName), defaultValue);
            if (result != defaultValue)
            {
                return result;
            }
            result = GetDwordValue64(root, keyPath, valueName, result);
            if (result != defaultValue)
            {
                return result;
            }
            return GetDwordValue32(root, keyPath, valueName, result);
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue(Hive root, string keyPath, string valueName)
        {
            return GetStringValue(root, keyPath, valueName, null);
        }

        /// <summary>
        /// Gets the string value.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue(Hive root, string keyPath, string valueName, string defaultValue)
        {
            return (GetStringValue64(root, keyPath, valueName) ?? GetStringValue32(root, keyPath, valueName)) ?? defaultValue;
        }

        /// <summary>
        /// Gets the string value in 32-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue32(Hive root, string keyPath, string valueName)
        {
            return GetStringValue32(root, keyPath, valueName, null);
        }

        /// <summary>
        /// Gets the string value in 32-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue32(Hive root, string keyPath, string valueName, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(keyPath))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry32))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        if (subKey == null)
                        {
                            return defaultValue;
                        }
                        var value = subKey.GetValue(valueName);
                        if (value == null)
                        {
                            return defaultValue;
                        }
                        if (subKey.GetValueKind(valueName) == RegistryValueKind.String)
                        {
                            result = (string) value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail getting string value with name \"{valueName}\" from 32-bit registry: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Gets the string value in 64-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue64(Hive root, string keyPath, string valueName)
        {
            return GetStringValue64(root, keyPath, valueName, null);
        }

        /// <summary>
        /// Gets the string value in 64-bit section.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string GetStringValue64(Hive root, string keyPath, string valueName, string defaultValue)
        {
            if (string.IsNullOrWhiteSpace(keyPath))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Registry64))
                {
                    using (var subKey = baseKey.OpenSubKey(keyPath, RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        if (subKey == null)
                        {
                            return defaultValue;
                        }
                        var value = subKey.GetValue(valueName);
                        if (value == null)
                        {
                            return defaultValue;
                        }
                        if (subKey.GetValueKind(valueName) == RegistryValueKind.String)
                        {
                            result = (string)value;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail getting string value with name \"{valueName}\" from 64-bit registry: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Sets the string value.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="valueData">The value data.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool SetStringValue(Hive root, string keyPath, string valueName, string valueData)
        {
            var result = false;
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Default))
                {
                    using (var key = baseKey.CreateSubKey(keyPath))
                    {
                        if (key != null)
                        {
                            key.SetValue(valueName, valueData, RegistryValueKind.String);
                            result = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail setting string value with name \"{valueName}\" to registry: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Sets the DWord value.
        /// </summary>
        /// <param name="root">The root.</param>
        /// <param name="keyPath">The key path.</param>
        /// <param name="valueName">Name of the value.</param>
        /// <param name="valueData">The value data.</param>
        /// <returns><c>true</c> if success, <c>false</c> otherwise.</returns>
        public static bool SetDwordValue(Hive root, string keyPath, string valueName, int valueData)
        {
            var result = false;
            try
            {
                using (var baseKey = RegistryKey.OpenBaseKey((RegistryHive)root, RegistryView.Default))
                {
                    using (var key = baseKey.CreateSubKey(keyPath))
                    {
                        if (key != null)
                        {
                            key.SetValue(valueName, valueData, RegistryValueKind.DWord);
                            result = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Registry)).Error($"Fail setting dword value with name \"{valueName}\" to registry: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Enum Hive
        /// </summary>
        public enum Hive
        {
            /// <summary>
            /// HKEY_CLASSES_ROOT
            /// </summary>
            ClassesRoot = RegistryHive.ClassesRoot,
            /// <summary>
            /// HKEY_CURRENT_USER
            /// </summary>
            CurrentUser = RegistryHive.CurrentUser,
            /// <summary>
            /// HKEY_LOCAL_MACHINE
            /// </summary>
            LocalMachine = RegistryHive.LocalMachine,
            /// <summary>
            /// HKEY_USERS
            /// </summary>
            Users = RegistryHive.Users,
            /// <summary>
            /// HKEY_PERFORMANCE_DATA
            /// </summary>
            PerformanceData = RegistryHive.PerformanceData,
            /// <summary>
            /// HKEY_CURRENT_CONFIG
            /// </summary>
            CurrentConfig = RegistryHive.CurrentConfig,
            /// <summary>
            /// HKEY_DYN_DATA
            /// </summary>
            DynData = -2147483642 /* RegistryHive.DynData */
        }
    }
}
