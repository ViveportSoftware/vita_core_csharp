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

        /// <summary>
        /// Gets a value indicating whether this <see cref="UsbWatcher"/> is disposed.
        /// </summary>
        /// <value><c>true</c> if disposed; otherwise, <c>false</c>.</value>
        protected bool Disposed { get; private set; }

        /// <summary>
        /// Finalizes an instance of the <see cref="UsbWatcher" /> class
        /// </summary>
        ~UsbWatcher()
        {
            Dispose(false);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            Disposed = true;
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
