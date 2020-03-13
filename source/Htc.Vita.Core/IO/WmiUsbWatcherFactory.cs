namespace Htc.Vita.Core.IO
{
    public partial class WmiUsbWatcherFactory : UsbWatcherFactory
    {
        protected override UsbWatcher OnCreateUsbWatcher()
        {
            return new WmiUsbWatcher();
        }
    }
}
