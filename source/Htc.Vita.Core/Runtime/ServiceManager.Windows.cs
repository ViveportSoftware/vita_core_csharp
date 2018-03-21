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
                            ErrorMessage = "Service name \"" + serviceName + "\" is invalid"
                    };
                }

                var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
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
                var serviceHandle = Interop.Windows.OpenServiceW(
                        managerHandle,
                        serviceName,
                        Interop.Windows.ServiceAccessRight.ChangeConfig |
                                Interop.Windows.ServiceAccessRight.QueryConfig |
                                Interop.Windows.ServiceAccessRight.QueryStatus
                );
                if (serviceHandle == IntPtr.Zero)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    serviceInfo.ErrorCode = errorCode;
                    serviceInfo.ErrorMessage = "Can not open Windows service \"" + serviceName + "\", error code: " + errorCode;
                }
                else
                {
                    var success = Interop.Windows.ChangeServiceConfigW(
                            serviceHandle,
                            Interop.Windows.ServiceType.NoChange,
                            ConvertToPlatform(startType),
                            Interop.Windows.ErrorControlType.NoChange,
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

                    serviceInfo = UpdateCurrentState(serviceHandle, serviceInfo);

                    Interop.Windows.CloseServiceHandle(serviceHandle);
                }

                Interop.Windows.CloseServiceHandle(managerHandle);
                return serviceInfo;
            }

            internal static bool CheckIfExistsInPlatform(string serviceName)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    return false;
                }

                var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
                );
                if (managerHandle == IntPtr.Zero)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    Logger.GetInstance(typeof(Windows)).Error("Can not open Windows service controller manager, error code: " + errorCode);
                    return false;
                }

                var serviceHandle = Interop.Windows.OpenServiceW(
                        managerHandle,
                        serviceName,
                        Interop.Windows.ServiceAccessRight.QueryConfig
                );
                if (serviceHandle == IntPtr.Zero)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    if (errorCode != (int)Interop.Windows.Error.ServiceDoesNotExist)
                    {
                        Logger.GetInstance(typeof(Windows)).Error("Can not open Windows service \"" + serviceName + "\", error code: " + errorCode);
                    }
                    return false;
                }

                Interop.Windows.CloseServiceHandle(serviceHandle);
                return true;
            }

            private static StartType ConvertFromPlatform(Interop.Windows.StartType startType)
            {
                if (startType == Interop.Windows.StartType.AutoStart)
                {
                    return StartType.Automatic;
                }
                if (startType == Interop.Windows.StartType.DemandStart)
                {
                    return StartType.Manual;
                }
                if (startType == Interop.Windows.StartType.Disabled)
                {
                    return StartType.Disabled;
                }
                Logger.GetInstance(typeof(Windows)).Error("Can not convert Windows service start type " + startType + ". Use Automatic as fallback type");
                return StartType.Automatic;
            }

            private static CurrentState ConvertFromPlatform(Interop.Windows.CurrentState currentState)
            {
                if (currentState == Interop.Windows.CurrentState.ContinuePending)
                {
                    return CurrentState.ContinuePending;
                }
                if (currentState == Interop.Windows.CurrentState.Paused)
                {
                    return CurrentState.Paused;
                }
                if (currentState == Interop.Windows.CurrentState.PausePending)
                {
                    return CurrentState.PausePending;
                }
                if (currentState == Interop.Windows.CurrentState.Running)
                {
                    return CurrentState.Running;
                }
                if (currentState == Interop.Windows.CurrentState.StartPending)
                {
                    return CurrentState.StartPending;
                }
                if (currentState == Interop.Windows.CurrentState.Stopped)
                {
                    return CurrentState.Stopped;
                }
                if (currentState == Interop.Windows.CurrentState.StopPending)
                {
                    return CurrentState.StopPending;
                }
                Logger.GetInstance(typeof(Windows)).Error("Can not convert Windows service current state " + currentState + ". Use Unknown as fallback state");
                return CurrentState.Unknown;
            }

            private static Interop.Windows.StartType ConvertToPlatform(StartType startType)
            {
                if (startType == StartType.Disabled)
                {
                    return Interop.Windows.StartType.Disabled;
                }
                if (startType == StartType.Manual)
                {
                    return Interop.Windows.StartType.DemandStart;
                }
                if (startType == StartType.Automatic)
                {
                    return Interop.Windows.StartType.AutoStart;
                }
                Logger.GetInstance(typeof(Windows)).Error("Can not convert service start type " + startType + " in Windows. Use Interop.Windows.Advapi32.StartType.AutoStart as fallback type");
                return Interop.Windows.StartType.AutoStart;
            }

            internal static ServiceInfo QueryStartTypeInPlatform(string serviceName)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    return new ServiceInfo
                    {
                            ServiceName = serviceName,
                            ErrorCode = (int)Interop.Windows.Error.InvalidName,
                            ErrorMessage = "Service name \"" + serviceName + "\" is invalid"
                    };
                }

                var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
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
                var serviceHandle = Interop.Windows.OpenServiceW(
                        managerHandle,
                        serviceName,
                        Interop.Windows.ServiceAccessRight.QueryConfig | Interop.Windows.ServiceAccessRight.QueryStatus
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
                            serviceInfo.ErrorMessage = "Can not query Windows service \"" + serviceName + "\" config, error code: " + errorCode;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(Windows)).Error("Can not query Windows service \"" + serviceName + "\" start type: " + e.Message);
                    }
                    finally
                    {
                        Marshal.FreeHGlobal(serviceConfigPtr);
                    }

                    serviceInfo = UpdateCurrentState(serviceHandle, serviceInfo);

                    Interop.Windows.CloseServiceHandle(serviceHandle);
                }

                Interop.Windows.CloseServiceHandle(managerHandle);
                return serviceInfo;
            }

            internal static ServiceInfo StartInPlatform(string serviceName)
            {
                if (string.IsNullOrWhiteSpace(serviceName))
                {
                    return new ServiceInfo
                    {
                            ServiceName = serviceName,
                            ErrorCode = (int)Interop.Windows.Error.InvalidName,
                            ErrorMessage = "Service name \"" + serviceName + "\" is invalid"
                    };
                }

                var managerHandle = Interop.Windows.OpenSCManagerW(
                        null,
                        null,
                        Interop.Windows.ServiceControlManagerAccessRight.Connect
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
                var serviceHandle = Interop.Windows.OpenServiceW(
                        managerHandle,
                        serviceName,
                        Interop.Windows.ServiceAccessRight.Start | Interop.Windows.ServiceAccessRight.QueryStatus
                );
                if (serviceHandle == IntPtr.Zero)
                {
                    var errorCode = Marshal.GetLastWin32Error();
                    serviceInfo.ErrorCode = errorCode;
                    serviceInfo.ErrorMessage = "Can not open Windows service \"" + serviceName + "\", error code: " + errorCode;
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
                        serviceInfo.ErrorMessage = "Can not start Windows service \"" + serviceName + "\", error code: " + errorCode;
                    }

                    serviceInfo = UpdateCurrentState(serviceHandle, serviceInfo);

                    Interop.Windows.CloseServiceHandle(serviceHandle);
                }

                Interop.Windows.CloseServiceHandle(managerHandle);
                return serviceInfo;
            }

            internal static ServiceInfo UpdateCurrentState(
                    IntPtr serviceHandle,
                    ServiceInfo serviceInfo)
            {
                if (serviceHandle == IntPtr.Zero || serviceInfo == null)
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
                    serviceInfo.ErrorMessage = "Can not query Windows service \"" + serviceInfo.ServiceName + "\" status, error code: " + errorCode;
                }

                return serviceInfo;
            }
        }
    }
}
