using System;
using System.Collections.Generic;
using Htc.Vita.Core.Diagnostics;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public class IpcChannel
    {
        public string Input { get; set; }
        public string Output { get; set; }

        public abstract class Client
        {
            public static readonly string OptionVerifyProvider = "option_verify_provider";

            private static Dictionary<string, Client> Instances { get; } = new Dictionary<string, Client>();

            private static readonly object InstancesLock = new object();

            private static Type _defaultType = typeof(NamedPipeIpcChannel.Client);

            public static void Register<T>() where T : Client
            {
                _defaultType = typeof(T);
                Logger.GetInstance(typeof(Client)).Info("Registered default " + typeof(Client).Name + " type to " + _defaultType);
            }

            public static Client GetInstance()
            {
                Client instance;
                try
                {
                    instance = DoGetInstance(_defaultType);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Fatal("Instance initialization error " + e);
                    Logger.GetInstance(typeof(Client)).Info("Initializing " + typeof(NamedPipeIpcChannel.Client).FullName + "...");
                    instance = new NamedPipeIpcChannel.Client();
                }
                return instance;
            }

            public static Client GetInstance<T>() where T : Client
            {
                Client instance;
                try
                {
                    instance = DoGetInstance(typeof(T));
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Fatal("Instance initialization error: " + e);
                    Logger.GetInstance(typeof(Client)).Info("Initializing " + typeof(NamedPipeIpcChannel.Client).FullName + "...");
                    instance = new NamedPipeIpcChannel.Client();
                }
                return instance;
            }

            private static Client DoGetInstance(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentException("Invalid arguments to get " + typeof(Client).Name + " instance");
                }

                var key = type.FullName + "_";
                Client instance = null;
                if (Instances.ContainsKey(key))
                {
                    instance = Instances[key];
                }
                if (instance == null)
                {
                    Logger.GetInstance(typeof(Client)).Info("Initializing " + key + "...");
                    var constructor = type.GetConstructor(new Type[] { });
                    if (constructor != null)
                    {
                        instance = (Client)constructor.Invoke(new object[] { });
                    }
                }
                if (instance == null)
                {
                    Logger.GetInstance(typeof(Client)).Info("Initializing " + typeof(NamedPipeIpcChannel.Client).FullName + "...");
                    instance = new NamedPipeIpcChannel.Client();
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

            public bool IsReady()
            {
                return IsReady(null);
            }

            public bool IsReady(Dictionary<string, string> options)
            {
                var opts = options ?? new Dictionary<string, string>();
                var result = false;
                try
                {
                    result = OnIsReady(opts);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Error(e.ToString());
                }
                return result;
            }

            public string Request(string input)
            {
                if (input == null)
                {
                    return null;
                }
                string result = null;
                try
                {
                    result = OnRequest(input);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Error(e.ToString());
                }
                return result;
            }

            public bool SetName(string name)
            {
                var result = false;
                try
                {
                    result = OnSetName(name);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Client)).Error(e.ToString());
                }
                return result;
            }

            protected abstract bool OnIsReady(Dictionary<string, string> options);
            protected abstract string OnRequest(string input);
            protected abstract bool OnSetName(string name);
        }

        public abstract class Provider
        {
            public Action<IpcChannel, FilePropertiesInfo> OnMessageHandled { protected get; set; }

            private static Dictionary<string, Provider> Instances { get; } = new Dictionary<string, Provider>();

            private static readonly object InstancesLock = new object();

            private static Type _defaultType = typeof(NamedPipeIpcChannel.Provider);

            public static void Register<T>() where T : Provider
            {
                _defaultType = typeof(T);
                Logger.GetInstance(typeof(Provider)).Info("Registered default " + typeof(Provider).Name + " type to " + _defaultType);
            }

            public static Provider GetInstance()
            {
                Provider instance;
                try
                {
                    instance = DoGetInstance(_defaultType);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Fatal("Instance initialization error " + e);
                    Logger.GetInstance(typeof(Provider)).Info("Initializing " + typeof(NamedPipeIpcChannel.Provider).FullName + "...");
                    instance = new NamedPipeIpcChannel.Provider();
                }
                return instance;
            }

            public static Provider GetInstance<T>() where T : Provider
            {
                Provider instance;
                try
                {
                    instance = DoGetInstance(typeof(T));
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Fatal("Instance initialization error: " + e);
                    Logger.GetInstance(typeof(Provider)).Info("Initializing " + typeof(NamedPipeIpcChannel.Provider).FullName + "...");
                    instance = new NamedPipeIpcChannel.Provider();
                }
                return instance;
            }

            private static Provider DoGetInstance(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentException("Invalid arguments to get " + typeof(Provider).Name + " instance");
                }

                var key = type.FullName + "_";
                Provider instance = null;
                if (Instances.ContainsKey(key))
                {
                    instance = Instances[key];
                }
                if (instance == null)
                {
                    Logger.GetInstance(typeof(Provider)).Info("Initializing " + key + "...");
                    var constructor = type.GetConstructor(new Type[] { });
                    if (constructor != null)
                    {
                        instance = (Provider)constructor.Invoke(new object[] { });
                    }
                }
                if (instance == null)
                {
                    Logger.GetInstance(typeof(Provider)).Info("Initializing " + typeof(NamedPipeIpcChannel.Provider).FullName + "...");
                    instance = new NamedPipeIpcChannel.Provider();
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

            public bool IsRunning()
            {
                var result = false;
                try
                {
                    result = OnIsRunning();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            public bool SetName(string name)
            {
                var result = false;
                try
                {
                    result = OnSetName(name);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            public bool Start()
            {
                var result = false;
                try
                {
                    result = OnStart();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            public bool Stop()
            {
                var result = false;
                try
                {
                    result = OnStop();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Provider)).Error(e.ToString());
                }
                return result;
            }

            protected abstract bool OnIsRunning();
            protected abstract bool OnSetName(string name);
            protected abstract bool OnStart();
            protected abstract bool OnStop();
        }
    }
}
