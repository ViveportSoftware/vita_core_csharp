using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
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
                            using (var processHandle = new Interop.Windows.SafeProcessHandle(clientProcess, false))
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
                                        Logger.GetInstance(typeof(Windows)).Error($"Can not get Windows process path from Process by id: {processId}, error code: {win32Error}");
                                        break;
                                    }

                                    if (bufferSize > 1024 * 30)
                                    {
                                        Logger.GetInstance(typeof(Windows)).Error($"Can not get Windows process path under length of {bufferSize}");
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
                catch (Win32Exception)
                {
                    using (var processHandle = Interop.Windows.OpenProcess(Interop.Windows.ProcessAccessRights.QueryLimitedInformation, false, (uint) processId))
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
                            if (win32Error == (int)Interop.Windows.Error.InvalidHandle)
                            {
                                Logger.GetInstance(typeof(Windows)).Debug($"Can not get Windows process path with valid handle by id: {processId}");
                                break;
                            }

                            if (win32Error != (int)Interop.Windows.Error.InsufficientBuffer)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not get Windows process path from process handle by id: {processId}, error code: {win32Error}");
                                break;
                            }

                            if (bufferSize > 1024 * 30)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not get Windows process path under length of {bufferSize}");
                                break;
                            }

                            bufferSize *= 2;
                        }

                        return null;
                    }
                }

                return null;
            }

            internal static bool IsCurrentUserProcessInPlatform(Process process)
            {
                var tokenInformation = IntPtr.Zero;
                try
                {
                    string currentUserSid;
                    using (var windowsIdentity = WindowsIdentity.GetCurrent())
                    {
                        currentUserSid = windowsIdentity.User?.ToString() ?? string.Empty;
                    }
                    if (string.IsNullOrWhiteSpace(currentUserSid))
                    {
                        Logger.GetInstance(typeof(Windows)).Error("Can not get current user sid");
                        return false;
                    }

                    using (var processHandle = new Interop.Windows.SafeProcessHandle(process, false))
                    {
                        Interop.Windows.SafeTokenHandle tokenHandle;
                        var success = Interop.Windows.OpenProcessToken(
                                processHandle,
                                Interop.Windows.TokenAccessRights.Query | Interop.Windows.TokenAccessRights.Duplicate,
                                out tokenHandle
                        );
                        if (!success)
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not open process token. error code: {Marshal.GetLastWin32Error()}");
                        }

                        var tokenInformationSize = 0U;
                        success = Interop.Windows.GetTokenInformation(
                                tokenHandle,
                                Interop.Windows.TokenInformationClass.User,
                                tokenInformation,
                                tokenInformationSize,
                                out tokenInformationSize
                        );
                        if (!success)
                        {
                            var win32Error = Marshal.GetLastWin32Error();
                            if (win32Error != (int) Interop.Windows.Error.InsufficientBuffer)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not get process token information size. error code: {win32Error}");
                                return false;
                            }
                        }

                        tokenInformation = Marshal.AllocHGlobal((int) tokenInformationSize);
                        success = Interop.Windows.GetTokenInformation(
                                tokenHandle,
                                Interop.Windows.TokenInformationClass.User,
                                tokenInformation,
                                tokenInformationSize,
                                out tokenInformationSize
                        );
                        if (!success)
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not get process token information. error code: {Marshal.GetLastWin32Error()}");
                            return false;
                        }

                        var tokenUser = (Interop.Windows.TokenUser) Marshal.PtrToStructure(
                                tokenInformation,
                                typeof(Interop.Windows.TokenUser)
                        );
                        var processUserSid = string.Empty;
                        success = Interop.Windows.ConvertSidToStringSidW(tokenUser.User.Sid, ref processUserSid);
                        if (!success)
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not convert sid to string sid, error code: {Marshal.GetLastWin32Error()}");
                            return false;
                        }

                        if (currentUserSid.Equals(processUserSid))
                        {
                            return true;
                        }

                        Logger.GetInstance(typeof(Windows)).Debug($"Process user SID: [{processUserSid}] is different from current user SID: [{currentUserSid}]");
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Windows)).Error($"Can not check if process is running under current user. {e.Message}");
                }
                finally
                {
                    if (tokenInformation != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(tokenInformation);
                    }
                }

                return false;
            }

            internal static bool IsElevatedProcessInPlatform(Process process)
            {
                var tokenInformation = IntPtr.Zero;
                try
                {
                    using (var processHandle = new Interop.Windows.SafeProcessHandle(process, false))
                    {
                        Interop.Windows.SafeTokenHandle tokenHandle;
                        var success = Interop.Windows.OpenProcessToken(
                                processHandle,
                                Interop.Windows.TokenAccessRights.Query,
                                out tokenHandle
                        );
                        if (!success)
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not open process token. error code: {Marshal.GetLastWin32Error()}");
                        }

                        var tokenInformationSize = 0U;
                        success = Interop.Windows.GetTokenInformation(
                                tokenHandle,
                                Interop.Windows.TokenInformationClass.Elevation,
                                tokenInformation,
                                tokenInformationSize,
                                out tokenInformationSize
                        );
                        if (!success)
                        {
                            var win32Error = Marshal.GetLastWin32Error();
                            if (win32Error != (int)Interop.Windows.Error.BadLength
                                    && win32Error != (int)Interop.Windows.Error.InsufficientBuffer)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not get process token information size. error code: {win32Error}.");
                                return false;
                            }
                        }

                        tokenInformation = Marshal.AllocHGlobal((int)tokenInformationSize);
                        success = Interop.Windows.GetTokenInformation(
                                tokenHandle,
                                Interop.Windows.TokenInformationClass.Elevation,
                                tokenInformation,
                                tokenInformationSize,
                                out tokenInformationSize
                        );
                        if (!success)
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not get process token information. error code: {Marshal.GetLastWin32Error()}");
                            return false;
                        }

                        var tokenElevation = (Interop.Windows.TokenElevation)Marshal.PtrToStructure(
                                tokenInformation,
                                typeof(Interop.Windows.TokenElevation)
                        );

                        return tokenElevation.TokenIsElevated != 0;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Windows)).Error($"Can not check if process is elevated. {e.Message}");
                }
                finally
                {
                    if (tokenInformation != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(tokenInformation);
                    }
                }

                return false;
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

            internal static ProcessInfo LaunchProcessAsShellUserInPlatform(
                    string fileName,
                    string arguments)
            {
                if (!IsElevatedProcess(Process.GetCurrentProcess()))
                {
                    Logger.GetInstance(typeof(Windows)).Error("This API should be invoked by elevated user process");
                    return null;
                }

                var shellWindowHandle = Interop.Windows.GetShellWindow();
                if (shellWindowHandle == IntPtr.Zero)
                {
                    Logger.GetInstance(typeof(Windows)).Error($"Can not get shell window handle, error code: {Marshal.GetLastWin32Error()}");
                    return null;
                }

                try
                {
                    var shellWindowProcessId = 0U;
                    Interop.Windows.GetWindowThreadProcessId(
                            shellWindowHandle,
                            ref shellWindowProcessId
                    );
                    if (shellWindowProcessId == 0)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not get shell window process id, error code: {Marshal.GetLastWin32Error()}");
                        return null;
                    }

                    using (var shellWindowProcess = Process.GetProcessById((int) shellWindowProcessId))
                    {
                        using (var processHandle = new Interop.Windows.SafeProcessHandle(shellWindowProcess, false))
                        {
                            Interop.Windows.SafeTokenHandle tokenHandle;
                            var success = Interop.Windows.OpenProcessToken(
                                    processHandle,
                                    Interop.Windows.TokenAccessRights.Duplicate,
                                    out tokenHandle
                            );
                            if (!success)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not open process token. error code: {Marshal.GetLastWin32Error()}");
                                return null;
                            }

                            Interop.Windows.SafeTokenHandle newTokenHandle;
                            var securityAttributes = new Interop.Windows.SecurityAttributes();
                            securityAttributes.nLength = Marshal.SizeOf(securityAttributes);
                            var tokenAccess = Interop.Windows.TokenAccessRights.AdjustDefault
                                            | Interop.Windows.TokenAccessRights.AssignPrimary
                                            | Interop.Windows.TokenAccessRights.AdjustSessionId
                                            | Interop.Windows.TokenAccessRights.Duplicate
                                            | Interop.Windows.TokenAccessRights.Query;
                            success = Interop.Windows.DuplicateTokenEx(
                                    tokenHandle,
                                    tokenAccess,
                                    securityAttributes,
                                    Interop.Windows.SecurityImpersonationLevel.SecurityImpersonation,
                                    Interop.Windows.TokenType.TokenPrimary,
                                    out newTokenHandle
                            );
                            if (!success)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not duplicate token, error code: {Marshal.GetLastWin32Error()}");
                                return null;
                            }

                            var commandLine = $"\"{fileName}\" {arguments}";
                            var startupInfo = new Interop.Windows.StartupInfo();
                            startupInfo.cb = Marshal.SizeOf(startupInfo);

                            Interop.Windows.ProcessInformation processInformation;
                            success = Interop.Windows.CreateProcessWithTokenW(
                                    newTokenHandle,
                                    Interop.Windows.LogonFlag.None,
                                    fileName,
                                    commandLine,
                                    Interop.Windows.ProcessCreationFlags.CreateUnicodeEnvironment,
                                    IntPtr.Zero,
                                    Path.GetDirectoryName(fileName),
                                    ref startupInfo,
                                    out processInformation
                            );

                            if (!success)
                            {
                                Logger.GetInstance(typeof(Windows)).Error($"Can not create process as shell user, error code: {Marshal.GetLastWin32Error()}");
                                return null;
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
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Windows)).Error($"Can not launch process as user: {e}");
                }
                finally
                {
                    if (shellWindowHandle != IntPtr.Zero)
                    {
                        Interop.Windows.CloseHandle(shellWindowHandle);
                    }
                }

                return null;
            }

            internal static ProcessInfo LaunchProcessAsUserInPlatform(
                    string fileName,
                    string arguments)
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
                            Logger.GetInstance(typeof(Windows)).Error($"Can not enumerate WTS session, error code: {Marshal.GetLastWin32Error()}");
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
                                    Interop.Windows.TokenAccessRights.AllAccess,
                                    securityAttributes,
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
                                    tokenInformation,
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
                                        tokenInformation,
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
                                    securityAttributes,
                                    securityAttributes,
                                    false,
                                    Interop.Windows.ProcessCreationFlags.CreateUnicodeEnvironment | Interop.Windows.ProcessCreationFlags.CreateNoWindow,
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
