using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public partial class ServiceManager
    {
        internal static class Windows
        {
            internal static ServiceInfo ChangeStartTypeInPlatform(
                    string serviceName,
                    StartType startType)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    return new ServiceInfo
                    {
                            ServiceName = serviceName,
                            ErrorCode = (int)Interop.Windows.Error.InvalidName,
                            ErrorMessage = $"Service name \"{serviceName}\" is invalid"
                    };
                }

                using (var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
                ))
                {
                    if (managerHandle == null || managerHandle.IsInvalid)
                    {
                        var errorCode = Marshal.GetLastWin32Error();
                        return new ServiceInfo
                        {
                                ServiceName = serviceName,
                                ErrorCode = errorCode,
                                ErrorMessage = $"Can not open Windows service controller manager, error code: {errorCode}"
                        };
                    }

                    var serviceInfo = new ServiceInfo
                    {
                        ServiceName = serviceName,
                        StartType = startType
                    };
                    using (var serviceHandle = Interop.Windows.OpenServiceW(
                            managerHandle,
                            serviceName,
                            Interop.Windows.ServiceAccessRight.ChangeConfig |
                                    Interop.Windows.ServiceAccessRight.QueryConfig |
                                    Interop.Windows.ServiceAccessRight.QueryStatus
                    ))
                    {
                        if (serviceHandle == null || serviceHandle.IsInvalid)
                        {
                            var errorCode = Marshal.GetLastWin32Error();
                            serviceInfo.ErrorCode = errorCode;
                            serviceInfo.ErrorMessage = $"Can not open Windows service \"{serviceName}\", error code: {errorCode}";
                        }
                        else
                        {
                            var success = Interop.Windows.ChangeServiceConfigW(
                                    serviceHandle,
                                    Interop.Windows.ServiceType.NoChange,
                                    ConvertToPlatform(startType),
                                    Interop.Windows.ServiceErrorControl.NoChange,
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
                                serviceInfo.ErrorMessage = $"Can not change Windows service \"{serviceName}\" config, error code: {errorCode}";
                            }

                            serviceInfo = UpdateCurrentState(serviceHandle, serviceInfo);
                        }

                        return serviceInfo;
                    }
                }
            }

            internal static bool CheckIfExistsInPlatform(string serviceName)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    return false;
                }

                using (var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
                ))
                {
                    if (managerHandle == null || managerHandle.IsInvalid)
                    {
                        var errorCode = Marshal.GetLastWin32Error();
                        Logger.GetInstance(typeof(Windows)).Error($"Can not open Windows service controller manager, error code: {errorCode}");
                        return false;
                    }

                    using (var serviceHandle = Interop.Windows.OpenServiceW(
                            managerHandle,
                            serviceName,
                            Interop.Windows.ServiceAccessRight.QueryConfig
                    ))
                    {
                        if (serviceHandle != null && !serviceHandle.IsInvalid)
                        {
                            return true;
                        }

                        var errorCode = Marshal.GetLastWin32Error();
                        if (errorCode != (int)Interop.Windows.Error.ServiceDoesNotExist)
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not open Windows service \"{serviceName}\", error code: {errorCode}");
                        }
                        return false;
                    }
                }
            }

            private static StartType ConvertFromPlatform(Interop.Windows.ServiceStartType startType)
            {
                if (startType == Interop.Windows.ServiceStartType.AutoStart)
                {
                    return StartType.Automatic;
                }
                if (startType == Interop.Windows.ServiceStartType.DemandStart)
                {
                    return StartType.Manual;
                }
                if (startType == Interop.Windows.ServiceStartType.Disabled)
                {
                    return StartType.Disabled;
                }
                Logger.GetInstance(typeof(Windows)).Error($"Can not convert Windows service start type {startType}. Use Automatic as fallback type");
                return StartType.Automatic;
            }

            private static CurrentState ConvertFromPlatform(Interop.Windows.ServiceCurrentState currentState)
            {
                if (currentState == Interop.Windows.ServiceCurrentState.ContinuePending)
                {
                    return CurrentState.ContinuePending;
                }
                if (currentState == Interop.Windows.ServiceCurrentState.Paused)
                {
                    return CurrentState.Paused;
                }
                if (currentState == Interop.Windows.ServiceCurrentState.PausePending)
                {
                    return CurrentState.PausePending;
                }
                if (currentState == Interop.Windows.ServiceCurrentState.Running)
                {
                    return CurrentState.Running;
                }
                if (currentState == Interop.Windows.ServiceCurrentState.StartPending)
                {
                    return CurrentState.StartPending;
                }
                if (currentState == Interop.Windows.ServiceCurrentState.Stopped)
                {
                    return CurrentState.Stopped;
                }
                if (currentState == Interop.Windows.ServiceCurrentState.StopPending)
                {
                    return CurrentState.StopPending;
                }
                Logger.GetInstance(typeof(Windows)).Error($"Can not convert Windows service current state {currentState}. Use Unknown as fallback state");
                return CurrentState.Unknown;
            }

            private static Interop.Windows.ServiceStartType ConvertToPlatform(StartType startType)
            {
                if (startType == StartType.Disabled)
                {
                    return Interop.Windows.ServiceStartType.Disabled;
                }
                if (startType == StartType.Manual)
                {
                    return Interop.Windows.ServiceStartType.DemandStart;
                }
                if (startType == StartType.Automatic)
                {
                    return Interop.Windows.ServiceStartType.AutoStart;
                }
                Logger.GetInstance(typeof(Windows)).Error($"Can not convert service start type {startType} in Windows. Use Interop.Windows.ServiceStartType.AutoStart as fallback type");
                return Interop.Windows.ServiceStartType.AutoStart;
            }

            internal static ServiceInfo QueryStartTypeInPlatform(string serviceName)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    return new ServiceInfo
                    {
                            ServiceName = serviceName,
                            ErrorCode = (int)Interop.Windows.Error.InvalidName,
                            ErrorMessage = $"Service name \"{serviceName}\" is invalid"
                    };
                }

                using (var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
                ))
                {
                    if (managerHandle.IsInvalid)
                    {
                        var errorCode = Marshal.GetLastWin32Error();
                        return new ServiceInfo
                        {
                                ServiceName = serviceName,
                                ErrorCode = errorCode,
                                ErrorMessage = $"Can not open Windows service controller manager, error code: {errorCode}"
                        };
                    }

                    var serviceInfo = new ServiceInfo
                    {
                        ServiceName = serviceName
                    };
                    using (var serviceHandle = Interop.Windows.OpenServiceW(
                            managerHandle,
                            serviceName,
                            Interop.Windows.ServiceAccessRight.QueryConfig | Interop.Windows.ServiceAccessRight.QueryStatus
                    ))
                    {
                        if (serviceHandle == null || serviceHandle.IsInvalid)
                        {
                            var errorCode = Marshal.GetLastWin32Error();
                            serviceInfo.ErrorCode = errorCode;
                            serviceInfo.ErrorMessage = $"Can not open Windows service \"{serviceName}\", error code: {errorCode}";
                        }
                        else
                        {
                            const uint bytesAllocated = 8192;
                            var serviceConfigPtr = Marshal.AllocHGlobal((int)bytesAllocated);
                            try
                            {
                                uint bytes = 0;
                                var success = Interop.Windows.QueryServiceConfigW(
                                        serviceHandle,
                                        serviceConfigPtr,
                                        bytesAllocated,
                                        ref bytes
                                );
                                if (success)
                                {
                                    var serviceConfig = (Interop.Windows.QueryServiceConfig)Marshal.PtrToStructure(
                                            serviceConfigPtr,
                                            typeof(Interop.Windows.QueryServiceConfig)
                                    );
                                    serviceInfo.StartType = ConvertFromPlatform(serviceConfig.dwStartType);
                                }
                                else
                                {
                                    var errorCode = Marshal.GetLastWin32Error();
                                    serviceInfo.ErrorCode = errorCode;
                                    serviceInfo.ErrorMessage = $"Can not query Windows service \"{serviceName}\" config, error code: {errorCode}";
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not query Windows service \"{serviceName}\" start type: {e.Message}");
                            }
                            finally
                            {
                                Marshal.FreeHGlobal(serviceConfigPtr);
                            }

                            serviceInfo = UpdateCurrentState(serviceHandle, serviceInfo);
                        }

                        return serviceInfo;
                    }
                }
            }

            internal static ServiceInfo StartInPlatform(string serviceName)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    return new ServiceInfo
                    {
                            ServiceName = serviceName,
                            ErrorCode = (int)Interop.Windows.Error.InvalidName,
                            ErrorMessage = $"Service name \"{serviceName}\" is invalid"
                    };
                }

                using (var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
                ))
                {
                    if (managerHandle == null || managerHandle.IsInvalid)
                    {
                        var errorCode = Marshal.GetLastWin32Error();
                        return new ServiceInfo
                        {
                                ServiceName = serviceName,
                                ErrorCode = errorCode,
                                ErrorMessage = $"Can not open Windows service controller manager, error code: {errorCode}"
                        };
                    }

                    var serviceInfo = new ServiceInfo
                    {
                        ServiceName = serviceName
                    };
                    using (var serviceHandle = Interop.Windows.OpenServiceW(
                            managerHandle,
                            serviceName,
                            Interop.Windows.ServiceAccessRight.Start | Interop.Windows.ServiceAccessRight.QueryStatus
                    ))
                    {
                        if (serviceHandle == null || serviceHandle.IsInvalid)
                        {
                            var errorCode = Marshal.GetLastWin32Error();
                            serviceInfo.ErrorCode = errorCode;
                            serviceInfo.ErrorMessage = $"Can not open Windows service \"{serviceName}\", error code: {errorCode}";
                        }
                        else
                        {
                            var success = Interop.Windows.StartServiceW(
                                    serviceHandle,
                                    0,
                                    null
                            );
                            if (success)
                            {
                                serviceInfo.CurrentState = CurrentState.Running;
                            }
                            else
                            {
                                var errorCode = Marshal.GetLastWin32Error();
                                serviceInfo.ErrorCode = errorCode;
                                serviceInfo.ErrorMessage = $"Can not start Windows service \"{serviceName}\", error code: {errorCode}";
                            }

                            serviceInfo = UpdateCurrentState(serviceHandle, serviceInfo);
                        }

                        return serviceInfo;
                    }
                }
            }

            internal static ServiceInfo UpdateCurrentState(
                    Interop.Windows.SafeServiceHandle serviceHandle,
                    ServiceInfo serviceInfo)
            {
                if (serviceHandle == null || serviceHandle.IsInvalid || serviceInfo == null)
                {
                    return serviceInfo;
                }

                var status = new Interop.Windows.ServiceStatus();
                var success = Interop.Windows.QueryServiceStatus(
                        serviceHandle,
                        ref status
                );
                if (success)
                {
                    serviceInfo.CurrentState = ConvertFromPlatform(status.dwCurrentState);
                }
                else if (serviceInfo.ErrorCode != 0)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    serviceInfo.ErrorCode = errorCode;
                    serviceInfo.ErrorMessage = $"Can not query Windows service \"{serviceInfo.ServiceName}\" status, error code: {errorCode}";
                }

                return serviceInfo;
            }
        }
    }
}
