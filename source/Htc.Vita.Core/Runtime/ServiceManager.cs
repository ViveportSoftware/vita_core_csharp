using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public class ServiceManager
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(ServiceManager));

        public static ServiceInfo ChangeStartType(string serviceName, StartType startType)
        {
            return ChangeStartTypeInWindows(serviceName, startType);
        }

        public static ServiceInfo QueryStartType(string serviceName)
        {
            return QueryStartTypeInWindows(serviceName);
        }

        private static ServiceInfo ChangeStartTypeInWindows(
                string serviceName,
                StartType startType)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                return new ServiceInfo
                {
                        ServiceName = serviceName,
                        ErrorCode = Windows.ERROR_INVALID_NAME,
                        ErrorMessage = "Service name \"" + serviceName + "\" is invalid"
                };
            }

            var managerHandle = Windows.Advapi32.OpenSCManagerW(
                    null,
                    null,
                    Windows.Advapi32.SCMAccessRight.SC_MANAGER_CONNECT
            );
            if (managerHandle == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return new ServiceInfo
                {
                        ServiceName = serviceName,
                        ErrorCode = errorCode,
                        ErrorMessage = "Can not open Windows service controller manager, error code: " + errorCode
                };
            }

            var serviceInfo = new ServiceInfo
            {
                    ServiceName = serviceName,
                    StartType = startType
            };
            var serviceHandle = Windows.Advapi32.OpenServiceW(
                    managerHandle,
                    serviceName,
                    Windows.Advapi32.ServiceAccessRight.SERVICE_CHANGE_CONFIG | Windows.Advapi32.ServiceAccessRight.SERVICE_QUERY_CONFIG
            );
            if (serviceHandle == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                serviceInfo.ErrorCode = errorCode;
                serviceInfo.ErrorMessage = "Can not open Windows service \"" + serviceName + "\", error code: " + errorCode;
            }
            else
            {
                var success = Windows.Advapi32.ChangeServiceConfigW(
                        serviceHandle,
                        Windows.Advapi32.SERVICE_TYPE.SERVICE_NO_CHANGE,
                        ConvertToWindows(startType),
                        Windows.Advapi32.ERROR_CONTROL_TYPE.SERVICE_NO_CHANGE,
                        null,
                        null,
                        IntPtr.Zero,
                        null,
                        null,
                        null,
                        null
                );
                if (!success)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    serviceInfo.ErrorCode = errorCode;
                    serviceInfo.ErrorMessage = "Can not change Windows service \"" + serviceName + "\" config, error code: " + errorCode;
                }

                Windows.Advapi32.CloseServiceHandle(serviceHandle);
            }

            Windows.Advapi32.CloseServiceHandle(managerHandle);
            return serviceInfo;
        }

        private static ServiceInfo QueryStartTypeInWindows(string serviceName)
        {
            if (string.IsNullOrWhiteSpace(serviceName))
            {
                return new ServiceInfo
                {
                        ServiceName = serviceName,
                        ErrorCode = Windows.ERROR_INVALID_NAME,
                        ErrorMessage = "Service name \"" + serviceName + "\" is invalid"
                };
            }

            var managerHandle = Windows.Advapi32.OpenSCManagerW(
                    null,
                    null,
                    Windows.Advapi32.SCMAccessRight.SC_MANAGER_CONNECT
            );
            if (managerHandle == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                return new ServiceInfo
                {
                        ServiceName = serviceName,
                        ErrorCode = errorCode,
                        ErrorMessage = "Can not open Windows service controller manager, error code: " + errorCode
                };
            }

            var serviceInfo = new ServiceInfo
            {
                    ServiceName = serviceName
            };
            var serviceHandle = Windows.Advapi32.OpenServiceW(
                    managerHandle,
                    serviceName,
                    Windows.Advapi32.ServiceAccessRight.SERVICE_QUERY_CONFIG
            );
            if (serviceHandle == IntPtr.Zero)
            {
                var errorCode = Marshal.GetLastWin32Error();
                serviceInfo.ErrorCode = errorCode;
                serviceInfo.ErrorMessage = "Can not open Windows service \"" + serviceName + "\", error code: " + errorCode;
            }
            else
            {
                const uint bytesAllocated = 8192;
                var serviceConfigPtr = Marshal.AllocHGlobal((int)bytesAllocated);
                try
                {
                    uint bytes;
                    var success = Windows.Advapi32.QueryServiceConfigW(
                            serviceHandle,
                            serviceConfigPtr,
                            bytesAllocated,
                            out bytes
                    );
                    if (success)
                    {
                        var serviceConfig = (Windows.Advapi32.QUERY_SERVICE_CONFIG) Marshal.PtrToStructure(
                                serviceConfigPtr,
                                typeof(Windows.Advapi32.QUERY_SERVICE_CONFIG)
                        );
                        serviceInfo.StartType = ConvertFromWindows((Windows.Advapi32.START_TYPE) serviceConfig.dwStartType);
                    }
                    else
                    {
                        var errorCode = Marshal.GetLastWin32Error();
                        serviceInfo.ErrorCode = errorCode;
                        serviceInfo.ErrorMessage = "Can not query Windows service \"" + serviceName + "\" config, error code: " + errorCode;
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Can not query Windows service \"" + serviceName + "\" start type: " + e.Message);
                }
                finally
                {
                    Marshal.FreeHGlobal(serviceConfigPtr);
                }

                Windows.Advapi32.CloseServiceHandle(serviceHandle);
            }

            Windows.Advapi32.CloseServiceHandle(managerHandle);
            return serviceInfo;
        }

        private static Windows.Advapi32.START_TYPE ConvertToWindows(StartType startType)
        {
            if (startType == StartType.Disabled)
            {
                return Windows.Advapi32.START_TYPE.SERVICE_DISABLED;
            }
            if (startType == StartType.Manual)
            {
                return Windows.Advapi32.START_TYPE.SERVICE_DEMAND_START;
            }
            if (startType == StartType.Automatic)
            {
                return Windows.Advapi32.START_TYPE.SERVICE_AUTO_START;
            }
            Log.Error("Can not convert service start type " + startType + " in Windows. Use SERVICE_AUTO_START as fallback type");
            return Windows.Advapi32.START_TYPE.SERVICE_AUTO_START;
        }

        private static StartType ConvertFromWindows(Windows.Advapi32.START_TYPE startType)
        {
            if (startType == Windows.Advapi32.START_TYPE.SERVICE_AUTO_START)
            {
                return StartType.Automatic;
            }
            if (startType == Windows.Advapi32.START_TYPE.SERVICE_DEMAND_START)
            {
                return StartType.Manual;
            }
            if (startType == Windows.Advapi32.START_TYPE.SERVICE_DISABLED)
            {
                return StartType.Disabled;
            }
            Log.Error("Can not convert Windows service start type " + startType + ". Use Automatic as fallback type");
            return StartType.Automatic;
        }

        public enum StartType
        {
            Unknown = 0,
            NotAvailable = 1,
            Disabled = 2,
            Manual = 3,
            Automatic = 4,
            DelayedAutomatic = 5
        }
    }

    public class ServiceInfo
    {
        public string ServiceName { get; set; }
        public ServiceManager.StartType StartType { get; set; } = ServiceManager.StartType.Unknown;
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
