using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public class UserManager
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(UserManager));

        public static string GetFirstActiveUser()
        {
            return GetFirstActiveUser(null);
        }

        public static string GetFirstActiveUser(string serverName)
        {
            var windowsUsers = GetWindowsUsers(serverName);
            return (from windowsUser
                    in windowsUsers
                    where windowsUser.State == Windows.WindowsTerminalServiceConnectStateClass.Active
                    select string.Format($"{windowsUser.Domain}\\{windowsUser.Username}")
            ).FirstOrDefault();
        }

        internal static string GetWindowsUsernameBySid(string userSid)
        {
            return GetWindowsUsernameBySid(userSid, null);
        }

        internal static string GetWindowsUsernameBySid(string userSid, string serverName)
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
                var success = Windows.ConvertStringSidToSidW(
                    userSid,
                    userSidPtr
                );

                if (success)
                {
                    var sidType = Windows.SidType.Unknown;
                    success = Windows.LookupAccountSidW(
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
                        if (sidType == Windows.SidType.User)
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

        internal static List<WindowsUserInfo> GetWindowsUsers()
        {
            return GetWindowsUsers(null);
        }

        internal static List<WindowsUserInfo> GetWindowsUsers(string serverName)
        {
            var machineName = serverName;
            if (string.IsNullOrWhiteSpace(machineName))
            {
                machineName = Environment.MachineName;
            }

            var results = new List<WindowsUserInfo>();
            var serverHandle = Windows.WTSOpenServerW(machineName);

            try
            {
                var sessionInfoPtr = IntPtr.Zero;
                var sessionCount = 0U;
                var success = Windows.WTSEnumerateSessionsW(
                        serverHandle,
                        0,
                        1,
                        ref sessionInfoPtr,
                        ref sessionCount
                );
                var dataSize = Marshal.SizeOf(typeof(Windows.WindowsTerminalServiceSessionInfo));
                var currentSessionInfoPtr = sessionInfoPtr;

                if (success)
                {
                    for (var sessionIndex = 0U; sessionIndex < sessionCount; sessionIndex++)
                    {
                        var sessionInfo = (Windows.WindowsTerminalServiceSessionInfo)Marshal.PtrToStructure(
                                currentSessionInfoPtr,
                                typeof(Windows.WindowsTerminalServiceSessionInfo)
                        );
                        currentSessionInfoPtr += dataSize;

                        uint bytes = 0;
                        var usernamePtr = IntPtr.Zero;
                        var ret = Windows.WTSQuerySessionInformationW(
                            serverHandle,
                            sessionInfo.sessionId,
                            Windows.WindowsTerminalServiceInfoClass.UserName,
                            ref usernamePtr,
                            ref bytes
                        );
                        if (ret == false)
                        {
                            continue;
                        }

                        string username = Marshal.PtrToStringUni(usernamePtr);
                        Windows.WTSFreeMemory(usernamePtr);

                        var domainPtr = IntPtr.Zero;
                        ret = Windows.WTSQuerySessionInformationW(
                                serverHandle,
                                sessionInfo.sessionId,
                                Windows.WindowsTerminalServiceInfoClass.DomainName,
                                ref domainPtr,
                                ref bytes
                        );
                        if (ret == false)
                        {
                            continue;
                        }

                        string domain = Marshal.PtrToStringUni(domainPtr);
                        Windows.WTSFreeMemory(domainPtr);

                        var userInfo = new WindowsUserInfo
                        {
                            State = sessionInfo.state,
                            Domain = domain,
                            Username = username
                        };
                        results.Add(userInfo);
                    }
                    Windows.WTSFreeMemory(sessionInfoPtr);
                }
            }
            catch (Exception e)
            {
                Log.Error("Can not get Windows user list: " + e.Message);
            }

            if (serverHandle != Windows.WindowsTerminalServiceCurrentServerHandle)
            {
                Windows.WTSCloseServer(serverHandle);
            }
            return results;
        }

        internal class WindowsUserInfo
        {
            public Windows.WindowsTerminalServiceConnectStateClass State { get; set; }
            public string Domain { get; set; }
            public string Username { get; set; }
        }
    }
}