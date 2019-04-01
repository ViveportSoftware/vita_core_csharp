using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public static partial class ProcessManager
    {
        internal static class Windows
        {
            internal static List<WindowsProcessInfo> GetPlatformProcesses(string serverName)
            {
                var machineName = serverName;
                if (string.IsNullOrWhiteSpace(machineName))
                {
                    machineName = Environment.MachineName;
                }

                var result = new List<WindowsProcessInfo>();
                var serverHandle = Interop.Windows.WTSOpenServerW(machineName);

                try
                {
                    var processInfoPtr = IntPtr.Zero;
                    var processCount = 0U;
                    var success = Interop.Windows.WTSEnumerateProcessesW(
                            serverHandle,
                            0,
                            1,
                            ref processInfoPtr,
                            ref processCount
                    );
                    var dataSize = Marshal.SizeOf(typeof(Interop.Windows.WindowsTerminalServiceProcessInfo));
                    var currentProcessInfoPtr = processInfoPtr;

                    if (success)
                    {
                        for (var processIndex = 0; processIndex < processCount; processIndex++)
                        {
                            var processInfo = (Interop.Windows.WindowsTerminalServiceProcessInfo)Marshal.PtrToStructure(
                                    currentProcessInfoPtr,
                                    typeof(Interop.Windows.WindowsTerminalServiceProcessInfo)
                            );
                            currentProcessInfoPtr += dataSize;

                            var userSid = string.Empty;
                            success = Interop.Windows.ConvertSidToStringSidW(processInfo.pUserSid, ref userSid);
                            if (!success)
                            {
                                userSid = string.Empty;
                            }

                            var windowsProcessInfo = new WindowsProcessInfo
                            {
                                Id = (int)processInfo.processId,
                                Name = processInfo.pProcessName,
                                SessionId = (int)processInfo.sessionId,
                                UserSid = userSid
                            };
                            result.Add(windowsProcessInfo);
                        }
                        Interop.Windows.WTSFreeMemory(processInfoPtr);
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Windows)).Error("Can not get Windows process list: " + e.Message);
                }

                if (serverHandle != Interop.Windows.WindowsTerminalServiceCurrentServerHandle)
                {
                    Interop.Windows.WTSCloseServer(serverHandle);
                }
                return result;
            }

            internal static string GetPlatformProcessPathById(int processId)
            {
                string processPath;
                using (var clientProcess = Process.GetProcessById(processId))
                {
                    try
                    {
                        processPath = clientProcess.MainModule.FileName;
                    }
                    catch (Win32Exception)
                    {
                        using (var processHandle = new Interop.Windows.SafeProcessHandle(clientProcess))
                        {
                            var fullPath = new StringBuilder(256);
                            Interop.Windows.GetModuleFileNameExW(
                                    processHandle,
                                    IntPtr.Zero,
                                    fullPath,
                                    (uint)fullPath.Capacity
                            );
                            processPath = fullPath.ToString();
                        }
                    }
                }

                return processPath;
            }

            internal static bool KillPlatformProcessById(int processId)
            {
                return KillPlatformProcessById(processId, null);
            }

            internal static bool KillPlatformProcessById(int processId, string serverName)
            {
                var machineName = serverName;
                if (string.IsNullOrWhiteSpace(machineName))
                {
                    machineName = Environment.MachineName;
                }

                var result = false;
                var serverHandle = Interop.Windows.WTSOpenServerW(machineName);

                try
                {
                    result = Interop.Windows.WTSTerminateProcess(
                        serverHandle,
                        (uint)processId,
                        0
                    );
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Windows)).Error("Can not kill Windows process by id: " + processId + ", " + e.Message);
                }

                if (serverHandle != Interop.Windows.WindowsTerminalServiceCurrentServerHandle)
                {
                    Interop.Windows.WTSCloseServer(serverHandle);
                }
                return result;
            }
        }
    }
}
