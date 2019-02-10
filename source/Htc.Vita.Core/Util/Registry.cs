using System;
using Htc.Vita.Core.Log;
using Microsoft.Win32;

namespace Htc.Vita.Core.Util
{
    public abstract class Registry
    {
        public static bool DeleteKey(Hive root, string keyPath, string keyName)
        {
            return DeleteKey32(root, keyPath, keyName) && DeleteKey64(root, keyPath, keyName);
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail deleting key \"" + keyName + "\" from 32-bit registry: " + e);
            }
            return false;
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail deleting key \"" + keyName + "\" from 64-bit registry: " + e);
            }
            return false;
        }

        public static bool DeleteValue(Hive root, string keyPath, string valueName)
        {
            return DeleteValue32(root, keyPath, valueName) && DeleteValue64(root, keyPath, valueName);
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail deleting value \"" + valueName + "\" from 32-bit registry: " + e);
            }
            return false;
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail deleting value \"" + valueName + "\" from 64-bit registry: " + e);
            }
            return false;
        }

        public static int GetDwordValue32(Hive root, string keyPath, string valueName)
        {
            return GetDwordValue32(root, keyPath, valueName, 0);
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail getting dword value with name \"" + valueName + "\" from 32-bit registry: " + e.Message);
            }

            return result;
        }

        public static int GetDwordValue64(Hive root, string keyPath, string valueName)
        {
            return GetDwordValue64(root, keyPath, valueName, 0);
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail getting dword value with name \"" + valueName + "\" from 64-bit registry: " + e.Message);
            }

            return result;
        }

        public static int GetIntValue(Hive root, string keyPath, string valueName)
        {
            return GetIntValue(root, keyPath, valueName, 0);
        }

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

        public static string GetStringValue(Hive root, string keyPath, string valueName)
        {
            return GetStringValue(root, keyPath, valueName, null);
        }

        public static string GetStringValue(Hive root, string keyPath, string valueName, string defaultValue)
        {
            return (GetStringValue64(root, keyPath, valueName) ?? GetStringValue32(root, keyPath, valueName)) ?? defaultValue;
        }

        public static string GetStringValue32(Hive root, string keyPath, string valueName)
        {
            return GetStringValue32(root, keyPath, valueName, null);
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail getting string value with name \"" + valueName + "\" from 32-bit registry: " + e.Message);
            }
            return result;
        }

        public static string GetStringValue64(Hive root, string keyPath, string valueName)
        {
            return GetStringValue64(root, keyPath, valueName, null);
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail getting string value with name \"" + valueName + "\" from 64-bit registry: " + e.Message);
            }
            return result;
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail setting string value with name \"" + valueName + "\" to registry: " + e.Message);
            }
            return result;
        }

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
                Logger.GetInstance(typeof(Registry)).Error("Fail setting dword value with name \"" + valueName + "\" to registry: " + e.Message);
            }
            return result;
        }

        public enum Hive
        {
            ClassesRoot = RegistryHive.ClassesRoot,
            CurrentUser = RegistryHive.CurrentUser,
            LocalMachine = RegistryHive.LocalMachine,
            Users = RegistryHive.Users,
            PerformanceData = RegistryHive.PerformanceData,
            CurrentConfig = RegistryHive.CurrentConfig,
            DynData = -2147483642 /* RegistryHive.DynData */
        }
    }
}
