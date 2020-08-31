using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class Dns.
    /// </summary>
    public abstract class Dns
    {
        private static Dictionary<string, Dns> Instances { get; } = new Dictionary<string, Dns>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultDns);

        /// <summary>
        /// Gets the resolver.
        /// </summary>
        /// <value>The resolver.</value>
        public string Resolver { get; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dns"/> class.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        protected Dns(string resolver)
        {
            if (!string.IsNullOrWhiteSpace(resolver))
            {
                Resolver = resolver;
            }
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : Dns
        {
            var type = typeof(T);
            if (_defaultType == type)
            {
                return;
            }

            _defaultType = type;
            Logger.GetInstance(typeof(Dns)).Info($"Registered default {nameof(Dns)} type to {_defaultType}");
        }

        /// <summary>
        /// Flushes the cache.
        /// </summary>
        /// <returns><c>true</c> if flushing the cache successfully, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Flushes the cache.
        /// </summary>
        /// <param name="hostName">The host name.</param>
        /// <returns><c>true</c> if flushing the cache successfully, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>Dns.</returns>
        public static Dns GetInstance()
        {
            return GetInstance("");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="resolver">The resolver.</param>
        /// <returns>Dns.</returns>
        public static Dns GetInstance(string resolver)
        {
            Dns instance;
            try
            {
                instance = DoGetInstance(_defaultType, resolver);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(Dns)).Info($"Initializing {typeof(DefaultDns).FullName}...");
                instance = new DefaultDns(resolver);
            }
            return instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Dns.</returns>
        public static Dns GetInstance<T>() where T : Dns
        {
            return GetInstance<T>("");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="resolver">The resolver.</param>
        /// <returns>Dns.</returns>
        public static Dns GetInstance<T>(string resolver) where T : Dns
        {
            Dns instance;
            try
            {
                instance = DoGetInstance(typeof(T), resolver);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Fatal($"Instance initialization error {e}");
                Logger.GetInstance(typeof(Dns)).Info($"Initializing {typeof(DefaultDns).FullName}...");
                instance = new DefaultDns(resolver);
            }
            return instance;
        }

        private static Dns DoGetInstance(Type type, string resolver)
        {
            if (type == null || resolver == null)
            {
                throw new ArgumentException($"Invalid arguments to get {nameof(Dns)} instance");
            }

            var key = $"{type.FullName}_{resolver}";
            Dns instance = null;
            lock (InstancesLock)
            {
                if (Instances.ContainsKey(key))
                {
                    instance = Instances[key];
                }
                if (instance == null)
                {
                    Logger.GetInstance(typeof(Dns)).Info($"Initializing {key}...");
                    var constructor = type.GetConstructor(new[] { typeof(string) });
                    if (constructor != null)
                    {
                        instance = (Dns)constructor.Invoke(new object[] { resolver });
                    }
                }
                if (instance == null)
                {
                    Logger.GetInstance(typeof(Dns)).Info($"Initializing {typeof(DefaultDns).FullName}[{resolver}]...");
                    instance = new DefaultDns(resolver);
                }
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
        }

        /// <summary>
        /// Gets the host addresses.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or address.</param>
        /// <returns>IPAddress[].</returns>
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

        /// <summary>
        /// Gets the host entry.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>IPHostEntry.</returns>
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
            catch (SocketException)
            {
                // ignore
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Dns)).Error(e.ToString());
            }

            return result;
        }

        /// <summary>
        /// Gets the host entry.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or address.</param>
        /// <returns>IPHostEntry.</returns>
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

        /// <summary>
        /// Called when flushing cache.
        /// </summary>
        /// <returns><c>true</c> if flushing the cache successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnFlushCache();
        /// <summary>
        /// Called when flushing cache.
        /// </summary>
        /// <param name="hostName">Name of the host.</param>
        /// <returns><c>true</c> if flushing the cache successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnFlushCache(string hostName);
        /// <summary>
        /// Called when getting host addresses.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or address.</param>
        /// <returns>IPAddress[].</returns>
        protected abstract IPAddress[] OnGetHostAddresses(string hostNameOrAddress);
        /// <summary>
        /// Called when getting host entry.
        /// </summary>
        /// <param name="ipAddress">The ip address.</param>
        /// <returns>IPHostEntry.</returns>
        protected abstract IPHostEntry OnGetHostEntry(IPAddress ipAddress);
        /// <summary>
        /// Called when getting host entry.
        /// </summary>
        /// <param name="hostNameOrAddress">The host name or address.</param>
        /// <returns>IPHostEntry.</returns>
        protected abstract IPHostEntry OnGetHostEntry(string hostNameOrAddress);
    }
}
