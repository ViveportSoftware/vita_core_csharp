using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public abstract class UsbWatcherFactory
    {
        private static Dictionary<string, UsbWatcherFactory> Instances { get; } = new Dictionary<string, UsbWatcherFactory>();

        private static readonly object InstancesLock = new object();

        private static Type defaultType = typeof(WmiUsbWatcherFactory);

        public static void Register<T>() where T : UsbWatcherFactory
        {
            defaultType = typeof(T);
            Logger.GetInstance(typeof(UsbWatcherFactory)).Info("Registered default " + typeof(UsbWatcherFactory).Name + " type to " + defaultType);
        }

        public static UsbWatcherFactory GetInstance()
        {
            UsbWatcherFactory instance;
            try
            {
                instance = DoGetInstance(defaultType);
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
                throw new ArgumentException("Invalid arguments to get " + typeof(UsbWatcherFactory).Name + " instance");
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
