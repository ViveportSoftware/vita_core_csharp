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

        private readonly Logger _logger;

        protected Dns(string resolver)
        {
            if (!string.IsNullOrWhiteSpace(resolver))
            {
                Resolver = resolver;
            }
            _logger = Logger.GetInstance();
        }

        public static void Register<T>() where T : Dns
        {
            _defaultType = typeof(T);
            Logger.GetInstance().Info("Registered default dns type to " + _defaultType);
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
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultDns).FullName + "...");
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
                Logger.GetInstance().Fatal("Instance initialization error " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultDns).FullName + "...");
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
                Logger.GetInstance().Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new[] { typeof(string) });
                if (constructor != null)
                {
                    instance = (Dns)constructor.Invoke(new object[] { resolver });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + typeof(DefaultDns).FullName + "[" + resolver + "]...");
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
                return null;
            }

            IPAddress[] result = null;
            try
            {
                result = OnGetHostAddresses(hostNameOrAddress);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
            return result;
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
                _logger.Error(e.ToString());
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
                _logger.Error(e.ToString());
            }
            return result;
        }

        protected abstract IPAddress[] OnGetHostAddresses(string hostNameOrAddress);
        protected abstract IPHostEntry OnGetHostEntry(IPAddress ipAddress);
        protected abstract IPHostEntry OnGetHostEntry(string hostNameOrAddress);
    }
}
