using System;
using System.Collections.Generic;
using System.Net;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public abstract class Dns
    {
        private static Dictionary<string, Dns> Instances { get; } = new Dictionary<string, Dns>();
        private static Type _defaultType = typeof(DefaultDns);

        public string Resolver { get; } = string.Empty;

        protected Dns(string resolver)
        {
            if (!string.IsNullOrWhiteSpace(resolver))
            {
                Resolver = resolver;
            }
        }

        public static void Register<T>() where T : Dns
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(Dns)).Info("Registered default dns type to " + _defaultType);
        }

        public bool FlushCache()
        {
            var result = false;
            try
            {
                result = OnFlushCache();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Error(e.ToString());
            }
            return result;
        }

        public bool FlushCache(string hostName)
        {
            if (string.IsNullOrWhiteSpace(hostName))
            {
                return false;
            }

            var result = false;
            try
            {
                result = OnFlushCache(hostName);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Error(e.ToString());
            }
            return result;
        }

        public static Dns GetInstance()
        {
            return GetInstance("");
        }

        public static Dns GetInstance(string resolver)
        {
            Dns instance;
            try
            {
                instance = DoGetInstance(_defaultType, resolver);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(Dns)).Info("Initializing " + typeof(DefaultDns).FullName + "...");
                instance = new DefaultDns(resolver);
            }
            return instance;
        }

        public static Dns GetInstance<T>() where T : Dns
        {
            return GetInstance<T>("");
        }

        public static Dns GetInstance<T>(string resolver) where T : Dns
        {
            Dns instance;
            try
            {
                instance = DoGetInstance(typeof(T), resolver);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Fatal("Instance initialization error " + e);
                Logger.GetInstance(typeof(Dns)).Info("Initializing " + typeof(DefaultDns).FullName + "...");
                instance = new DefaultDns(resolver);
            }
            return instance;
        }

        private static Dns DoGetInstance(Type type, string resolver)
        {
            if (type == null || resolver == null)
            {
                throw new ArgumentException("Invalid arguments to get dns instance");
            }

            var key = type.FullName + "_" + resolver;
            Dns instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Dns)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new[] { typeof(string) });
                if (constructor != null)
                {
                    instance = (Dns)constructor.Invoke(new object[] { resolver });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(Dns)).Info("Initializing " + typeof(DefaultDns).FullName + "[" + resolver + "]...");
                instance = new DefaultDns(resolver);
            }
            if (!Instances.ContainsKey(key))
            {
                Instances.Add(key, instance);
            }
            return instance;
        }

        public IPAddress[] GetHostAddresses(string hostNameOrAddress)
        {
            if (string.IsNullOrWhiteSpace(hostNameOrAddress))
            {
                return new IPAddress[] { };
            }

            IPAddress[] result = null;
            try
            {
                result = OnGetHostAddresses(hostNameOrAddress);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Error(e.ToString());
            }

            return result ?? new IPAddress[] { };
        }

        public IPHostEntry GetHostEntry(IPAddress ipAddress)
        {
            if (ipAddress == null)
            {
                return null;
            }

            IPHostEntry result = null;
            try
            {
                result = OnGetHostEntry(ipAddress);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Error(e.ToString());
            }
            return result;
        }

        public IPHostEntry GetHostEntry(string hostNameOrAddress)
        {
            if (string.IsNullOrWhiteSpace(hostNameOrAddress))
            {
                return null;
            }

            IPHostEntry result = null;
            try
            {
                result = OnGetHostEntry(hostNameOrAddress);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Error(e.ToString());
            }
            return result;
        }

        protected abstract bool OnFlushCache();
        protected abstract bool OnFlushCache(string hostName);
        protected abstract IPAddress[] OnGetHostAddresses(string hostNameOrAddress);
        protected abstract IPHostEntry OnGetHostEntry(IPAddress ipAddress);
        protected abstract IPHostEntry OnGetHostEntry(string hostNameOrAddress);
    }
}
