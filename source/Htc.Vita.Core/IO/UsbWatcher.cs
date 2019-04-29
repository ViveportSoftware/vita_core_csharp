using System;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public abstract class UsbWatcher : IDisposable
    {
        public event Action OnDeviceConnected;
        public event Action OnDeviceDisconnected;

        public void Dispose()
        {
            OnDispose();
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
                Logger.GetInstance(typeof(UsbWatcher)).Error(e.ToString());
            }
            return result;
        }

        protected void NotifyDeviceConnected()
        {
            Task.Run(() =>
            {
                    try
                    {
                        OnDeviceConnected?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(UsbWatcher)).Error(e.ToString());
                    }
            });
        }

        protected void NotifyDeviceDisconnected()
        {
            Task.Run(() =>
            {
                    try
                    {
                        OnDeviceDisconnected?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(UsbWatcher)).Error(e.ToString());
                    }
            });
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
                Logger.GetInstance(typeof(UsbWatcher)).Error(e.ToString());
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
                Logger.GetInstance(typeof(UsbWatcher)).Error(e.ToString());
            }
            return result;
        }

        protected abstract void OnDispose();
        protected abstract bool OnIsRunning();
        protected abstract bool OnStart();
        protected abstract bool OnStop();
    }
}
