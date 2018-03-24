using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public abstract class ProcessWatcherFactory
    {
        private static Dictionary<string, ProcessWatcherFactory> Instances { get; } = new Dictionary<string, ProcessWatcherFactory>();
        private static Type _defaultType = typeof(WmiProcessWatcherFactory);

        public static void Register<T>() where T : ProcessWatcherFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(ProcessWatcherFactory)).Info("Registered default process watcher factory type to " + _defaultType);
        }

        public static ProcessWatcherFactory GetInstance()
        {
            ProcessWatcherFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ProcessWatcherFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(ProcessWatcherFactory)).Info("Initializing " + typeof(WmiProcessWatcherFactory).FullName + "...");
                instance = new WmiProcessWatcherFactory();
            }
            return instance;
        }

        public static ProcessWatcherFactory GetInstance<T>() where T : ProcessWatcherFactory
        {
            ProcessWatcherFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ProcessWatcherFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(ProcessWatcherFactory)).Info("Initializing " + typeof(WmiProcessWatcherFactory).FullName + "...");
                instance = new WmiProcessWatcherFactory();
            }
            return instance;
        }

        private static ProcessWatcherFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get process watcher factory instance");
            }

            var key = type.FullName + "_";
            ProcessWatcherFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(ProcessWatcherFactory)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (ProcessWatcherFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(ProcessWatcherFactory)).Info("Initializing " + typeof(WmiProcessWatcherFactory).FullName + "...");
                instance = new WmiProcessWatcherFactory();
            }
            if (!Instances.ContainsKey(key))
            {
                Instances.Add(key, instance);
            }
            return instance;
        }

        public ProcessWatcher CreateProcessWatcher()
        {
            return CreateProcessWatcher(null);
        }

        public ProcessWatcher CreateProcessWatcher(string targetProcessName)
        {
            return OnCreateProcessWatcher(targetProcessName);
        }

        protected abstract ProcessWatcher OnCreateProcessWatcher(string targetProcessName);
    }
}
