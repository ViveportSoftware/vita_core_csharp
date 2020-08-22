using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.IO
{
    public abstract class UsbWatcherFactory
    {
        static UsbWatcherFactory()
        {
            TypeRegistry.RegisterDefault<UsbWatcherFactory, WmiUsbWatcherFactory>();
        }

        public static void Register<T>()
                where T : UsbWatcherFactory, new()
        {
            TypeRegistry.Register<UsbWatcherFactory, T>();
        }

        public static UsbWatcherFactory GetInstance()
        {
            return TypeRegistry.GetInstance<UsbWatcherFactory>();
        }

        public static UsbWatcherFactory GetInstance<T>()
                where T : UsbWatcherFactory, new()
        {
            return TypeRegistry.GetInstance<UsbWatcherFactory, T>();
        }

        public UsbWatcher CreateUsbWatcher()
        {
            return OnCreateUsbWatcher();
        }

        protected abstract UsbWatcher OnCreateUsbWatcher();
    }
}
