using System;
using System.Collections.Generic;
using Htc.Vita.Core.Diagnosis;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public class IpcChannel
    {
        public string Input { get; set; }
        public string Output { get; set; }

        public abstract class Client
        {
            public static string OptionVerifyProvider = "option_verify_provider";

            private static Dictionary<string, Client> Instances { get; } = new Dictionary<string, Client>();
            private static Type _defaultType = typeof(NamedPipeIpcChannel.Client);

            private readonly Logger _logger;

            protected Client()
            {
                _logger = Logger.GetInstance();
            }

            public static void Register<T>() where T : Client
            {
                _defaultType = typeof(T);
                Logger.GetInstance().Info("Registered default ipc channel client type to " + _defaultType);
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
                    Logger.GetInstance().Fatal("Instance initialization error " + e);
                    Logger.GetInstance().Info("Initializing " + typeof(NamedPipeIpcChannel.Client).FullName + "...");
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
                    Logger.GetInstance().Fatal("Instance initialization error " + e);
                    Logger.GetInstance().Info("Initializing " + typeof(NamedPipeIpcChannel.Client).FullName + "...");
                    instance = new NamedPipeIpcChannel.Client();
                }
                return instance;
            }

            private static Client DoGetInstance(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentException("Invalid arguments to get ipc channel client instance");
                }

                var key = type.FullName;
                Client instance = null;
                if (Instances.ContainsKey(key))
                {
                    instance = Instances[key];
                }
                if (instance == null)
                {
                    Logger.GetInstance().Info("Initializing " + key + "...");
                    var constructor = type.GetConstructor(new Type[] { });
                    if (constructor != null)
                    {
                        instance = (Client)constructor.Invoke(new object[] { });
                    }
                }
                if (instance == null)
                {
                    Logger.GetInstance().Info("Initializing " + typeof(NamedPipeIpcChannel.Client).FullName + "...");
                    instance = new NamedPipeIpcChannel.Client();
                }
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
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
                    _logger.Error(e.ToString());
                }
                return result;
            }

            public string Request(string input)
            {
                string result = null;
                try
                {
                    result = OnRequest(input);
                }
                catch (Exception e)
                {
                    _logger.Error(e.ToString());
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
                    _logger.Error(e.ToString());
                }
                return result;
            }

            protected abstract bool OnIsReady(Dictionary<string, string> options);
            protected abstract string OnRequest(string input);
            protected abstract bool OnSetName(string name);
        }

        public abstract class Provider
        {
            public Action<IpcChannel, FileSignatureInfo> OnMessageHandled { protected get; set; }

            private static Dictionary<string, Provider> Instances { get; } = new Dictionary<string, Provider>();
            private static Type _defaultType = typeof(NamedPipeIpcChannel.Provider);

            private readonly Logger _logger;

            protected Provider()
            {
                _logger = Logger.GetInstance();
            }

            public static void Register<T>() where T : Provider
            {
                _defaultType = typeof(T);
                Logger.GetInstance().Info("Registered default ipc channel provider type to " + _defaultType);
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
                    Logger.GetInstance().Fatal("Instance initialization error " + e);
                    Logger.GetInstance().Info("Initializing " + typeof(NamedPipeIpcChannel.Provider).FullName + "...");
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
                    Logger.GetInstance().Fatal("Instance initialization error " + e);
                    Logger.GetInstance().Info("Initializing " + typeof(NamedPipeIpcChannel.Provider).FullName + "...");
                    instance = new NamedPipeIpcChannel.Provider();
                }
                return instance;
            }

            private static Provider DoGetInstance(Type type)
            {
                if (type == null)
                {
                    throw new ArgumentException("Invalid arguments to get ipc channel provider instance");
                }

                var key = type.FullName;
                Provider instance = null;
                if (Instances.ContainsKey(key))
                {
                    instance = Instances[key];
                }
                if (instance == null)
                {
                    Logger.GetInstance().Info("Initializing " + key + "...");
                    var constructor = type.GetConstructor(new Type[] { });
                    if (constructor != null)
                    {
                        instance = (Provider)constructor.Invoke(new object[] { });
                    }
                }
                if (instance == null)
                {
                    Logger.GetInstance().Info("Initializing " + typeof(NamedPipeIpcChannel.Provider).FullName + "...");
                    instance = new NamedPipeIpcChannel.Provider();
                }
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
                return instance;
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
                    _logger.Error(e.ToString());
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
                    _logger.Error(e.ToString());
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
                    _logger.Error(e.ToString());
                }
                return result;
            }

            protected abstract bool OnSetName(string name);
            protected abstract bool OnStart();
            protected abstract bool OnStop();
        }
    }
}
