using System;
using System.Collections.Generic;
using System.Linq;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Shell
{
    public abstract partial class UriSchemeManager
    {
        public static readonly string OptionAcceptWhitelistOnly = "option_accept_whitelist_only";

        private static Dictionary<string, UriSchemeManager> Instances { get; } = new Dictionary<string, UriSchemeManager>();

        private static readonly object InstancesLock = new object();

        private static Type defaultType = typeof(RegistryUriSchemeManager);

        public static void Register<T>() where T : UriSchemeManager
        {
            defaultType = typeof(T);
            Logger.GetInstance(typeof(UriSchemeManager)).Info("Registered default " + typeof(UriSchemeManager).Name + " type to " + defaultType);
        }

        public static UriSchemeManager GetInstance()
        {
            UriSchemeManager instance;
            try
            {
                instance = DoGetInstance(defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Fatal("Instance initialization error " + e);
                Logger.GetInstance(typeof(UriSchemeManager)).Info("Initializing " + typeof(RegistryUriSchemeManager).FullName + "...");
                instance = new RegistryUriSchemeManager();
            }
            return instance;
        }

        public static UriSchemeManager GetInstance<T>() where T : UriSchemeManager
        {
            UriSchemeManager instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(UriSchemeManager)).Info("Initializing " + typeof(RegistryUriSchemeManager).FullName + "...");
                instance = new RegistryUriSchemeManager();
            }
            return instance;
        }

        private static UriSchemeManager DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + typeof(UriSchemeManager).Name + " instance");
            }

            var key = type.FullName + "_";
            UriSchemeManager instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (RegistryUriSchemeManager)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Info("Initializing " + typeof(RegistryUriSchemeManager).FullName + "...");
                instance = new RegistryUriSchemeManager();
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

        public UriSchemeInfo GetSystemUriScheme(string name)
        {
            return GetSystemUriScheme(name, null);
        }

        public UriSchemeInfo GetSystemUriScheme(string name, Dictionary<string, string> options)
        {
            if (!name.All(c => char.IsLetterOrDigit(c) || c == '+' || c == '-' || c == '.'))
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Error("Do not find valid scheme name: \"" + name + "\"");
                return new UriSchemeInfo
                {
                        Name = name
                };
            }

            var opts = options ?? new Dictionary<string, string>();
            UriSchemeInfo result = null;
            try
            {
                result = OnGetSystemUriScheme(name, opts);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UriSchemeManager)).Error("Can not get system uri scheme: " + e.Message);
            }

            return result ?? new UriSchemeInfo
            {
                Name = name
            };
        }

        public bool IsSystemUriSchemeValid(string name)
        {
            return IsSystemUriSchemeValid(name, null);
        }

        public bool IsSystemUriSchemeValid(string name, Dictionary<string, string> options)
        {
            var uriSchemeInfo = GetSystemUriScheme(name, options);
            if (uriSchemeInfo == null)
            {
                return false;
            }

            return IsSystemUriSchemeValid(uriSchemeInfo);
        }

        public bool IsSystemUriSchemeValid(UriSchemeInfo uriSchemeInfo)
        {
            if (uriSchemeInfo == null)
            {
                return false;
            }

            return !string.IsNullOrWhiteSpace(uriSchemeInfo.Name)
                    && !string.IsNullOrWhiteSpace(uriSchemeInfo.CommandPath)
                    && !string.IsNullOrWhiteSpace(uriSchemeInfo.DefaultIcon);
        }

        protected abstract UriSchemeInfo OnGetSystemUriScheme(string schemeName, Dictionary<string, string> options);
    }
}
