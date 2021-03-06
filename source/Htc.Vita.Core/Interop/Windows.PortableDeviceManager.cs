using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class PortableDeviceManager : IDisposable
        {
            private IPortableDeviceManager _portableDeviceManager;
            private bool _disposed;

            internal PortableDeviceManager(IPortableDeviceManager portableDeviceManager)
            {
                _portableDeviceManager = portableDeviceManager;
            }

            ~PortableDeviceManager()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }

                if (disposing)
                {
                    // Disposing managed resource
                }

                if (_portableDeviceManager == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_portableDeviceManager))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_portableDeviceManager);
#pragma warning restore CA1416
                }
                _portableDeviceManager = null;

                _disposed = true;
            }

            internal string GetDeviceDescription(string deviceId)
            {
                var result = string.Empty;
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDeviceDescription(
                        deviceId,
                        null,
                        ref count
                );
                if (hResult != HResult.SOk
                        && hResult != HResult.EWin32InsufficientBuffer)
                {
                    if (hResult == HResult.EWin32InvalidData)
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Debug($"The device {deviceId} does not support description");
                    }
                    else
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device description (1). HResult: {hResult}");
                    }
                    return result;
                }

                var builder = new StringBuilder((int)count);
                hResult = _portableDeviceManager.GetDeviceDescription(
                        deviceId,
                        builder,
                        ref count
                );
                if (hResult == HResult.SOk)
                {
                    return builder.ToString();
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device description (2). HResult: {hResult}");
                return result;
            }

            internal string GetDeviceFriendlyName(string deviceId)
            {
                var result = string.Empty;
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDeviceFriendlyName(
                        deviceId,
                        null,
                        ref count
                );
                if (hResult != HResult.SOk
                        && hResult != HResult.EWin32InsufficientBuffer)
                {
                    if (hResult == HResult.EWin32InvalidData)
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Debug($"The device {deviceId} does not support friendly name");
                    }
                    else
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device friendly name (1). HResult: {hResult}");
                    }
                    return result;
                }

                var builder = new StringBuilder((int)count);
                hResult = _portableDeviceManager.GetDeviceFriendlyName(
                        deviceId,
                        builder,
                        ref count
                );
                if (hResult == HResult.SOk)
                {
                    return builder.ToString();
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device friendly name (2). HResult: {hResult}");
                return result;
            }

            internal List<string> GetDeviceList()
            {
                var result = new List<string>();
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDevices(
                        null,
                        ref count
                );
                if (hResult != HResult.SOk)
                {
                    Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device list. HResult: {hResult}");
                    return result;
                }

                if (count <= 0)
                {
                    return result;
                }

                IntPtr[] intPtrArray = null;
                try
                {
                    intPtrArray = new IntPtr[count];
                    _portableDeviceManager.GetDevices(
                            intPtrArray,
                            ref count
                    );

                    foreach (var ptr in intPtrArray)
                    {
                        var id = Marshal.PtrToStringUni(ptr);
                        if (!string.IsNullOrEmpty(id))
                        {
                            result.Add(id);
                        }
                    }
                }
                finally
                {
                    if (intPtrArray != null)
                    {
                        for (uint i = 0; i < count; i++)
                        {
                            Marshal.FreeCoTaskMem(intPtrArray[i]);
                        }
                    }
                }

                return result;
            }

            internal string GetDeviceManufacturer(string deviceId)
            {
                var result = string.Empty;
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDeviceManufacturer(
                        deviceId,
                        null,
                        ref count
                );
                if (hResult != HResult.SOk
                        && hResult != HResult.EWin32InsufficientBuffer)
                {
                    Logger.GetInstance(typeof(PortableDeviceManager)).Error(
                        $"Can not get device manufacturer (1). HResult: {hResult}");
                    return result;
                }

                var builder = new StringBuilder((int)count);
                hResult = _portableDeviceManager.GetDeviceManufacturer(
                        deviceId,
                        builder,
                        ref count
                );
                if (hResult == HResult.SOk)
                {
                    return builder.ToString();
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device manufacturer (2). HResult: {hResult}");
                return result;
            }

            internal static PortableDeviceManager GetInstance()
            {
                var iPortableDeviceManager = Activator.CreateInstance(typeof(ClsidPortableDeviceManager)) as IPortableDeviceManager;
                if (iPortableDeviceManager != null)
                {
                    return new PortableDeviceManager(iPortableDeviceManager);
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error("Can not create Portable Device Manager");
                return null;
            }
        }
    }
}
