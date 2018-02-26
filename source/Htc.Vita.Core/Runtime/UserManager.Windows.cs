using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Runtime
{
    public static partial class UserManager
    {
        internal static class Windows
        {
            internal static string GetFirstActiveUser(string serverName)
            {
                var windowsUsers = GetPlatformUsers(serverName);
                return (from windowsUser
                        in windowsUsers
                        where windowsUser.State == Interop.Windows.WindowsTerminalServiceConnectStateClass.Active
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
                                Log.Warn("Can not translate sid type " + sidType + " to username.");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Can not get Windows username: " + e.Message);
                }

                return result;
            }

            internal static List<WindowsUserInfo> GetPlatformUsers()
            {
                return GetPlatformUsers(null);
            }

            internal static List<WindowsUserInfo> GetPlatformUsers(string serverName)
            {
                var machineName = serverName;
                if (string.IsNullOrWhiteSpace(machineName))
                {
                    machineName = Environment.MachineName;
                }

                var results = new List<WindowsUserInfo>();
                var serverHandle = Interop.Windows.WTSOpenServerW(machineName);

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
                        for (var sessionIndex = 0U; sessionIndex < sessionCount; sessionIndex++)
                        {
                            var sessionInfo = (Interop.Windows.WindowsTerminalServiceSessionInfo)Marshal.PtrToStructure(
                                    currentSessionInfoPtr,
                                    typeof(Interop.Windows.WindowsTerminalServiceSessionInfo)
                            );
                            currentSessionInfoPtr += dataSize;

                            uint bytes = 0;
                            var usernamePtr = IntPtr.Zero;
                            var ret = Interop.Windows.WTSQuerySessionInformationW(
                                    serverHandle,
                                    sessionInfo.sessionId,
                                    Interop.Windows.WindowsTerminalServiceInfoClass.UserName,
                                    ref usernamePtr,
                                    ref bytes
                            );
                            if (ret == false)
                            {
                                continue;
                            }

                            var username = Marshal.PtrToStringUni(usernamePtr);
                            Interop.Windows.WTSFreeMemory(usernamePtr);

                            var domainPtr = IntPtr.Zero;
                            ret = Interop.Windows.WTSQuerySessionInformationW(
                                    serverHandle,
                                    sessionInfo.sessionId,
                                    Interop.Windows.WindowsTerminalServiceInfoClass.DomainName,
                                    ref domainPtr,
                                    ref bytes
                            );
                            if (ret == false)
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
                }
                catch (Exception e)
                {
                    Log.Error("Can not get Windows user list: " + e.Message);
                }

                if (serverHandle != Interop.Windows.WindowsTerminalServiceCurrentServerHandle)
                {
                    Interop.Windows.WTSCloseServer(serverHandle);
                }
                return results;
            }
        }
    }
}
