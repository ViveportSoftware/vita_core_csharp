using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public abstract class UsbWatcherFactory
    {
        private static Dictionary<string, UsbWatcherFactory> Instances { get; } = new Dictionary<string, UsbWatcherFactory>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(WmiUsbWatcherFactory);

        public static void Register<T>() where T : UsbWatcherFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(UsbWatcherFactory)).Info("Registered default " + nameof(UsbWatcherFactory) + " type to " + _defaultType);
        }

        public static UsbWatcherFactory GetInstance()
        {
            UsbWatcherFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UsbWatcherFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(UsbWatcherFactory)).Info("Initializing " + typeof(WmiUsbWatcherFactory).FullName + "...");
                instance = new WmiUsbWatcherFactory();
            }
            return instance;
        }

        public static UsbWatcherFactory GetInstance<T>() where T : UsbWatcherFactory
        {
            UsbWatcherFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UsbWatcherFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(UsbWatcherFactory)).Info("Initializing " + typeof(WmiUsbWatcherFactory).FullName + "...");
                instance = new WmiUsbWatcherFactory();
            }
            return instance;
        }

        private static UsbWatcherFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + nameof(UsbWatcherFactory) + " instance");
            }

            var key = type.FullName + "_";
            UsbWatcherFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(UsbWatcherFactory)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (UsbWatcherFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(UsbWatcherFactory)).Info("Initializing " + typeof(WmiUsbWatcherFactory).FullName + "...");
                instance = new WmiUsbWatcherFactory();
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

        public UsbWatcher CreateUsbWatcher()
        {
            return OnCreateUsbWatcher();
        }

        protected abstract UsbWatcher OnCreateUsbWatcher();
    }
}
