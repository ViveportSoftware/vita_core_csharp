using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public static partial class UserManager
    {
        internal static class Windows
        {
            internal static string GetFirstActiveUserInPlatform(string serverName)
            {
                var windowsUsers = GetPlatformUsers(serverName);
                return (from windowsUser
                        in windowsUsers
                        where windowsUser.State == Interop.Windows.WindowsTerminalServiceConnectState.Active
                        select string.Format($"{windowsUser.Domain}\\{windowsUser.Username}")
                ).FirstOrDefault();
            }

            internal static string GetPlatformUsernameBySid(string userSid)
            {
                return GetPlatformUsernameBySid(userSid, null);
            }

            internal static string GetPlatformUsernameBySid(string userSid, string serverName)
            {
                if (string.IsNullOrWhiteSpace(userSid))
                {
                    return null;
                }

                string result = null;
                var userSidPtr = IntPtr.Zero;
                var username = new StringBuilder();
                uint usernameLength = 0;
                var domain = new StringBuilder();
                uint domainLength = 0;
                try
                {
                    var success = Interop.Windows.ConvertStringSidToSidW(
                            userSid,
                            userSidPtr
                    );

                    if (success)
                    {
                        var sidType = Interop.Windows.SidType.Unknown;
                        success = Interop.Windows.LookupAccountSidW(
                                serverName,
                                userSidPtr,
                                username,
                                ref usernameLength,
                                domain,
                                ref domainLength,
                                ref sidType
                        );

                        if (success)
                        {
                            if (sidType == Interop.Windows.SidType.User)
                            {
                                result = string.Format($"{domain}\\{username}");
                            }
                            else
                            {
                                Logger.GetInstance(typeof(Windows)).Warn($"Can not translate sid type \"{sidType}\" to username.");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Windows)).Error($"Can not get Windows username: {e.Message}");
                }

                return result;
            }

            internal static List<WindowsUserInfo> GetPlatformUsers(string serverName)
            {
                var machineName = serverName;
                if (string.IsNullOrWhiteSpace(machineName))
                {
                    machineName = Environment.MachineName;
                }

                var results = new List<WindowsUserInfo>();
                using (var serverHandle = Interop.Windows.WTSOpenServerW(machineName))
                {
                    if (serverHandle == null || serverHandle.IsInvalid)
                    {
                        return results;
                    }

                    try
                    {
                        var sessionInfoPtr = IntPtr.Zero;
                        var sessionCount = 0U;
                        var success = Interop.Windows.WTSEnumerateSessionsW(
                                serverHandle,
                                0,
                                1,
                                ref sessionInfoPtr,
                                ref sessionCount
                        );
                        var dataSize = Marshal.SizeOf(typeof(Interop.Windows.WindowsTerminalServiceSessionInfo));
                        var currentSessionInfoPtr = sessionInfoPtr;

                        if (success)
                        {
                            if (sessionCount <= 0U)
                            {
                                Logger.GetInstance(typeof(Windows)).Error("Can not find available WTS session");
                            }

                            for (var sessionIndex = 0U; sessionIndex < sessionCount; sessionIndex++)
                            {
                                var sessionInfo = (Interop.Windows.WindowsTerminalServiceSessionInfo)Marshal.PtrToStructure(
                                        currentSessionInfoPtr,
                                        typeof(Interop.Windows.WindowsTerminalServiceSessionInfo)
                                );
                                currentSessionInfoPtr += dataSize;

                                uint bytes = 0;
                                var usernamePtr = IntPtr.Zero;
                                success = Interop.Windows.WTSQuerySessionInformationW(
                                        serverHandle,
                                        sessionInfo.sessionId,
                                        Interop.Windows.WindowsTerminalServiceInfo.UserName,
                                        ref usernamePtr,
                                        ref bytes
                                );
                                if (!success)
                                {
                                    continue;
                                }

                                var username = Marshal.PtrToStringUni(usernamePtr);
                                Interop.Windows.WTSFreeMemory(usernamePtr);

                                var domainPtr = IntPtr.Zero;
                                success = Interop.Windows.WTSQuerySessionInformationW(
                                        serverHandle,
                                        sessionInfo.sessionId,
                                        Interop.Windows.WindowsTerminalServiceInfo.DomainName,
                                        ref domainPtr,
                                        ref bytes
                                );
                                if (!success)
                                {
                                    continue;
                                }

                                var domain = Marshal.PtrToStringUni(domainPtr);
                                Interop.Windows.WTSFreeMemory(domainPtr);

                                var userInfo = new WindowsUserInfo
                                {
                                        State = sessionInfo.state,
                                        Domain = domain,
                                        Username = username
                                };
                                results.Add(userInfo);
                            }
                            Interop.Windows.WTSFreeMemory(sessionInfoPtr);
                        }
                        else
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not enumerate WTS session, error code: {Marshal.GetLastWin32Error()}");
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not get Windows user list: {e.Message}");
                    }

                    return results;
                }
            }

            internal static bool IsShellUserElevatedInPlatform()
            {
                var shellWindowHandle = Interop.Windows.GetShellWindow();
                if (shellWindowHandle == IntPtr.Zero)
                {
                    Logger.GetInstance(typeof(Windows)).Error($"Can not get shell window handle, error code: {Marshal.GetLastWin32Error()}");
                    return false;
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
                        return false;
                    }

                    using (var shellWindowProcess = Process.GetProcessById((int) shellWindowProcessId))
                    {
                        Logger.GetInstance(typeof(Windows)).Debug($"Current shell process: {shellWindowProcess.ProcessName} ({shellWindowProcessId})");
                        return ProcessManager.IsElevatedProcess(shellWindowProcess);
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Windows)).Error($"Can not check if shell user is elevated. {e.Message}");
                }
                finally
                {
                    if (shellWindowHandle != IntPtr.Zero)
                    {
                        Interop.Windows.CloseHandle(shellWindowHandle);
                    }
                }

                return false;
            }

            internal static bool SendMessageToFirstActiveUserInPlatform(
                    string title,
                    string message,
                    uint timeout,
                    string serverName)
            {
                if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(message))
                {
                    return false;
                }

                var machineName = serverName;
                if (string.IsNullOrWhiteSpace(machineName))
                {
                    machineName = Environment.MachineName;
                }

                using (var serverHandle = Interop.Windows.WTSOpenServerW(machineName))
                {
                    if (serverHandle == null || serverHandle.IsInvalid)
                    {
                        return false;
                    }

                    try
                    {
                        var sessionInfoPtr = IntPtr.Zero;
                        var sessionCount = 0U;
                        var success = Interop.Windows.WTSEnumerateSessionsW(
                                serverHandle,
                                0,
                                1,
                                ref sessionInfoPtr,
                                ref sessionCount
                        );
                        var dataSize = Marshal.SizeOf(typeof(Interop.Windows.WindowsTerminalServiceSessionInfo));
                        var currentSessionInfoPtr = sessionInfoPtr;

                        if (success)
                        {
                            if (sessionCount <= 0U)
                            {
                                Logger.GetInstance(typeof(Windows)).Error("Can not find available WTS session");
                                return false;
                            }

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

                                var dialogBoxResult = Interop.Windows.DialogBoxResult.None;
                                success = Interop.Windows.WTSSendMessageW(
                                        serverHandle,
                                        sessionInfo.sessionId,
                                        title,
                                        (uint) title.Length * 2,
                                        message,
                                        (uint) message.Length * 2,
                                        Interop.Windows.MessageBoxStyle.Ok,
                                        timeout,
                                        ref dialogBoxResult,
                                        true
                                );
                                if (!success)
                                {
                                    return false;
                                }

                                if (dialogBoxResult == Interop.Windows.DialogBoxResult.Ok)
                                {
                                    return true;
                                }
                            }
                            Interop.Windows.WTSFreeMemory(sessionInfoPtr);
                        }
                        else
                        {
                            Logger.GetInstance(typeof(Windows)).Error($"Can not enumerate WTS session, error code: {Marshal.GetLastWin32Error()}");
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(Windows)).Error($"Can not send message to active user: {e.Message}");
                    }

                    return false;
                }
            }
        }
    }
}
