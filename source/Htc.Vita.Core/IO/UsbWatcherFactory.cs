using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class UsbWatcherFactory.
    /// </summary>
    public abstract class UsbWatcherFactory
    {
        static UsbWatcherFactory()
        {
            TypeRegistry.RegisterDefault<UsbWatcherFactory, WmiUsbWatcherFactory>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : UsbWatcherFactory, new()
        {
            TypeRegistry.Register<UsbWatcherFactory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>UsbWatcherFactory.</returns>
        public static UsbWatcherFactory GetInstance()
        {
            return TypeRegistry.GetInstance<UsbWatcherFactory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>UsbWatcherFactory.</returns>
        public static UsbWatcherFactory GetInstance<T>()
                where T : UsbWatcherFactory, new()
        {
            return TypeRegistry.GetInstance<UsbWatcherFactory, T>();
        }

        /// <summary>
        /// Creates the USB watcher.
        /// </summary>
        /// <returns>UsbWatcher.</returns>
        public UsbWatcher CreateUsbWatcher()
        {
            return OnCreateUsbWatcher();
        }

        /// <summary>
        /// Called when creating USB watcher.
        /// </summary>
        /// <returns>UsbWatcher.</returns>
        protected abstract UsbWatcher OnCreateUsbWatcher();
    }
}
