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
                using (var serverHandle = Interop.Windows.WTSOpenServerW(machineName))
                {
                    if (serverHandle == null || serverHandle.IsInvalid)
                    {
                        return result;
                    }

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

                    return result;
                }
            }

            internal static string GetPlatformProcessPathById(int processId)
            {
                try
                {
                    using (var clientProcess = Process.GetProcessById(processId))
                    {
                        try
                        {
                            return clientProcess.MainModule?.FileName;
                        }
                        catch (Win32Exception)
                        {
                            using (var processHandle = new Interop.Windows.SafeProcessHandle(clientProcess))
                            {
                                var bufferSize = 256;
                                while (true)
                                {
                                    var fullPath = new StringBuilder(bufferSize);
                                    var success = Interop.Windows.QueryFullProcessImageNameW(
                                            processHandle,
                                            0,
                                            fullPath,
                                            ref bufferSize
                                    );
                                    if (success)
                                    {
                                        return fullPath.ToString(0, bufferSize);
                                    }

                                    var win32Error = Marshal.GetLastWin32Error();
                                    if (win32Error != (int) Interop.Windows.Error.InsufficientBuffer)
                                    {
                                        Logger.GetInstance(typeof(Windows)).Error("Can not get Windows process path, error code: " + win32Error);
                                        break;
                                    }

                                    if (bufferSize > 1024 * 30)
                                    {
                                        Logger.GetInstance(typeof(Windows)).Error("Can not get Windows process path under length of " + bufferSize);
                                        break;
                                    }

                                    bufferSize *= 2;
                                }

                                return null;
                            }
                        }
                    }
                }
                catch (ArgumentException)
                {
                    // skip
                }

                return null;
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
                using (var serverHandle = Interop.Windows.WTSOpenServerW(machineName))
                {
                    if (serverHandle == null || serverHandle.IsInvalid)
                    {
                        return false;
                    }

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

                    return result;
                }
            }

            internal static ProcessInfo LaunchProcessAsUserInPlatform(string fileName, string arguments)
            {
                var machineName = Environment.MachineName;
                using (var serverHandle = Interop.Windows.WTSOpenServerW(machineName))
                {
                    if (serverHandle == null || serverHandle.IsInvalid)
                    {
                        return null;
                    }

                    var sessionInfoPtr = IntPtr.Zero;
                    try
                    {
                        var sessionCount = 0U;
                        var success = Interop.Windows.WTSEnumerateSessionsW(
                            serverHandle,
                            0,
                            1,
                            ref sessionInfoPtr,
                            ref sessionCount
                        );
                        if (!success)
                        {
                            Logger.GetInstance(typeof(Windows))
                                .Error($"Can not enumerate WTS session, error code: {Marshal.GetLastWin32Error()}");
                            return null;
                        }
                        if (sessionCount <= 0U)
                        {
                            Logger.GetInstance(typeof(Windows)).Error("Can not find available WTS session");
                            return null;
                        }

                        var currentSessionInfoPtr = sessionInfoPtr;
                        var dataSize = Marshal.SizeOf(typeof(Interop.Windows.WindowsTerminalServiceSessionInfo));
                        for (var sessionIndex = 0U; sessionIndex < sessionCount; sessionIndex++)
                        {
                            var sessionInfo = (Interop.Windows.WindowsTerminalServiceSessionInfo)Marshal.PtrToStructure(
                                    currentSessionInfoPtr,
                                    typeof(Interop.Windows.WindowsTerminalServiceSessionInfo)
                            );

                            currentSessionInfoPtr += dataSize;

                            if (sessionInfo.state != Interop.Windows.WindowsTerminalServiceConnectState.Active)
                            {
                                continue;
                            }

                            Interop.Windows.SafeTokenHandle tokenHandle;
                            success = Interop.Windows.WTSQueryUserToken(
                                    sessionInfo.sessionId,
                                    out tokenHandle
                            );
                            if (!success)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not query user token, error code: {Marshal.GetLastWin32Error()}");
                                continue;
                            }

                            Interop.Windows.SafeTokenHandle newTokenHandle;
                            var securityAttributes = new Interop.Windows.SecurityAttributes();
                            securityAttributes.nLength = Marshal.SizeOf(securityAttributes);
                            success = Interop.Windows.DuplicateTokenEx(
                                    tokenHandle,
                                    Interop.Windows.TokenAccessRight.AllAccess,
                                    ref securityAttributes,
                                    Interop.Windows.SecurityImpersonationLevel.SecurityImpersonation,
                                    Interop.Windows.TokenType.TokenPrimary,
                                    out newTokenHandle
                            );
                            if (!success)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not duplicate token, error code: {Marshal.GetLastWin32Error()}");
                                continue;
                            }

                            var tokenInformation = IntPtr.Zero;
                            var tokenInformationSize = 0U;
                            success = Interop.Windows.GetTokenInformation(
                                    newTokenHandle,
                                    Interop.Windows.TokenInformationClass.LinkedToken,
                                    ref tokenInformation,
                                    tokenInformationSize,
                                    out tokenInformationSize
                            );
                            if (!success)
                            {
                                var lastWin32Error = Marshal.GetLastWin32Error();
                                if (lastWin32Error == (int)Interop.Windows.Error.NoSuchLogonSession)
                                {
                                    Logger.GetInstance(typeof(Windows)).Info($"UAC has been disabled, error code: {lastWin32Error}");
                                }
                                else if (lastWin32Error != (int) Interop.Windows.Error.BadLength)
                                {
                                    Logger.GetInstance(typeof(Windows)).Error($"Can not get token information, error code: {lastWin32Error}");
                                }

                                success = Interop.Windows.GetTokenInformation(
                                        newTokenHandle,
                                        Interop.Windows.TokenInformationClass.LinkedToken,
                                        ref tokenInformation,
                                        tokenInformationSize,
                                        out tokenInformationSize
                                );
                                if (!success)
                                {
                                    lastWin32Error = Marshal.GetLastWin32Error();
                                    Logger.GetInstance(typeof(Windows)).Error($"Can not get token information again, error code: {lastWin32Error}");
                                }
                            }

                            IntPtr environmentPtr;
                            success = Interop.Windows.CreateEnvironmentBlock(
                                    out environmentPtr,
                                    newTokenHandle,
                                    false
                            );
                            if (!success)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not create environment block, error code: {Marshal.GetLastWin32Error()}");
                            }

                            var commandLine = $"\"{fileName}\" {arguments}";
                            var startupInfo = new Interop.Windows.StartupInfo();
                            startupInfo.cb = Marshal.SizeOf(startupInfo);

                            Interop.Windows.ProcessInformation processInformation;
                            success = Interop.Windows.CreateProcessAsUserW(
                                    newTokenHandle,
                                    null,
                                    commandLine,
                                    ref securityAttributes,
                                    ref securityAttributes,
                                    false,
                                    Interop.Windows.ProcessCreationFlag.CreateUnicodeEnvironment | Interop.Windows.ProcessCreationFlag.CreateNoWindow,
                                    environmentPtr,
                                    null,
                                    ref startupInfo,
                                    out processInformation
                            );

                            Interop.Windows.DestroyEnvironmentBlock(environmentPtr);

                            if (!success)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not create process as user, error code: {Marshal.GetLastWin32Error()}");
                                continue;
                            }

                            var processId = processInformation.dwProcessID;
                            var processPath = GetPlatformProcessPathById(processId);
                            string processName;
                            using (var process = Process.GetProcessById(processId))
                            {
                                processName = process.ProcessName;
                            }

                            return new ProcessInfo
                            {
                                    Id = processId,
                                    Name = processName,
                                    Path = processPath
                            };
                        }

                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not launch process as user: {e}");
                    }
                    finally
                    {
                        if (sessionInfoPtr != IntPtr.Zero)
                        {
                            Interop.Windows.WTSFreeMemory(sessionInfoPtr);
                        }
                    }
                }

                return null;
            }
        }
    }
}
