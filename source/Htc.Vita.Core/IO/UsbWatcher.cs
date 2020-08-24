using System;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class UsbWatcher.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public abstract class UsbWatcher : IDisposable
    {
        /// <summary>
        /// Occurs when device is connected.
        /// </summary>
        public event Action OnDeviceConnected;
        /// <summary>
        /// Occurs when device is disconnected.
        /// </summary>
        public event Action OnDeviceDisconnected;

        /// <inheritdoc />
        public void Dispose()
        {
            OnDispose();
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Notifies the device is connected.
        /// </summary>
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

        /// <summary>
        /// Notifies the device is disconnected.
        /// </summary>
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

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns><c>true</c> if started successfully, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <returns><c>true</c> if stopped successfully, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Called when disposing.
        /// </summary>
        protected abstract void OnDispose();
        /// <summary>
        /// Called when determining whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if determining whether this instance is running successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnIsRunning();
        /// <summary>
        /// Called when starting this instance.
        /// </summary>
        /// <returns><c>true</c> if starting this instance successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnStart();
        /// <summary>
        /// Called when stopping this instance.
        /// </summary>
        /// <returns><c>true</c> if stopping this instance successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnStop();
    }
}
