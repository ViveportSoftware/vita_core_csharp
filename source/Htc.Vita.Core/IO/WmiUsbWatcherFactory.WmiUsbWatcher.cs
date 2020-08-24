using System;
using System.Management;

namespace Htc.Vita.Core.IO
{
    public partial class WmiUsbWatcherFactory
    {
        /// <summary>
        /// Class WmiUsbWatcher.
        /// Implements the <see cref="UsbWatcher" />
        /// </summary>
        /// <seealso cref="UsbWatcher" />
        public class WmiUsbWatcher : UsbWatcher
        {
            private readonly WqlEventQuery _connectEventQuery;
            private readonly WqlEventQuery _disconnectEventQuery;

            private ManagementEventWatcher _connectEventWatcher;
            private ManagementEventWatcher _disconnectEventWatcher;

            /// <summary>
            /// Initializes a new instance of the <see cref="WmiUsbWatcher"/> class.
            /// </summary>
            public WmiUsbWatcher()
            {
                _connectEventQuery = new WqlEventQuery
                {
                        EventClassName = "__InstanceCreationEvent",
                        WithinInterval = new TimeSpan(0, 0, 3),
                        Condition = "TargetInstance ISA 'Win32_USBControllerDevice'"
                };
                _disconnectEventQuery = new WqlEventQuery
                {
                        EventClassName = "__InstanceDeletionEvent",
                        WithinInterval = new TimeSpan(0, 0, 3),
                        Condition = "TargetInstance ISA 'Win32_USBControllerDevice'"
                };
            }

            /// <inheritdoc />
            protected override void OnDispose()
            {
                _connectEventWatcher?.Dispose();
                _connectEventWatcher = null;
                _disconnectEventWatcher?.Dispose();
                _disconnectEventWatcher = null;
            }

            /// <inheritdoc />
            protected override bool OnIsRunning()
            {
                return _connectEventWatcher != null || _disconnectEventWatcher != null;
            }

            /// <inheritdoc />
            protected override bool OnStart()
            {
                if (OnIsRunning())
                {
                    OnStop();
                }

                _connectEventWatcher = new ManagementEventWatcher(_connectEventQuery);
                _connectEventWatcher.EventArrived += OnDeviceConnectedEventArrived;
                _connectEventWatcher.Start();

                _disconnectEventWatcher = new ManagementEventWatcher(_disconnectEventQuery);
                _disconnectEventWatcher.EventArrived += OnDeviceDisconnectedEventArrived;
                _disconnectEventWatcher.Start();

                return OnIsRunning();
            }

            /// <inheritdoc />
            protected override bool OnStop()
            {
                if (_connectEventWatcher != null)
                {
                    _connectEventWatcher.Stop();
                    _connectEventWatcher.EventArrived -= OnDeviceConnectedEventArrived;
                    _connectEventWatcher.Dispose();
                    _connectEventWatcher = null;
                }

                if (_disconnectEventWatcher != null)
                {
                    _disconnectEventWatcher.Stop();
                    _disconnectEventWatcher.EventArrived -= OnDeviceDisconnectedEventArrived;
                    _disconnectEventWatcher.Dispose();
                    _disconnectEventWatcher = null;
                }

                return !OnIsRunning();
            }

            private void OnDeviceConnectedEventArrived(
                    object sender,
                    EventArrivedEventArgs e)
            {
                NotifyDeviceConnected();
            }

            private void OnDeviceDisconnectedEventArrived(
                    object sender,
                    EventArrivedEventArgs e)
            {
                NotifyDeviceDisconnected();
            }
        }
    }
}
