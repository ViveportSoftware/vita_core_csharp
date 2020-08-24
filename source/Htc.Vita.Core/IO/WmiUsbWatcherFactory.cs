namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class WmiUsbWatcherFactory.
    /// Implements the <see cref="UsbWatcherFactory" />
    /// </summary>
    /// <seealso cref="UsbWatcherFactory" />
    public partial class WmiUsbWatcherFactory : UsbWatcherFactory
    {
        /// <inheritdoc />
        protected override UsbWatcher OnCreateUsbWatcher()
        {
            return new WmiUsbWatcher();
        }
    }
}
