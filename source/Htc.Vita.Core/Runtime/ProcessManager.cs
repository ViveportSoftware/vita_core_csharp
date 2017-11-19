using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public class ProcessManager
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(ProcessManager));

        public static List<ProcessInfo> GetProcessesByFirstActiveUser()
        {
            var username = UserManager.GetFirstActiveUser();
            if (string.IsNullOrWhiteSpace(username))
            {
                return new List<ProcessInfo>();
            }
            return GetProcessesByUser(username);
        }

        public static List<ProcessInfo> GetProcessesByUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new List<ProcessInfo>();
            }
            return GetProcesses(username);
        }

        public static List<ProcessInfo> GetProcesses()
        {
            return GetProcesses(null);
        }

        public static List<ProcessInfo> GetProcesses(string username, string serverName = null)
        {
            var result = new List<ProcessInfo>();

            var windowsProcesses = GetWindowsProcesses(serverName);
            foreach (var windowsProcess in windowsProcesses)
            {
                var processId = windowsProcess.Id;
                var processName = windowsProcess.Name;
                var processUsername = UserManager.GetWindowsUsernameBySid(windowsProcess.UserSid) ?? "";
                if (!string.IsNullOrWhiteSpace(username) && !username.Equals(processUsername))
                {
                    continue;
                }
                var processInfo = new ProcessInfo
                {
                    Id = processId,
                    Name = processName,
                    User = processUsername,
                    Path = ""
                };
                result.Add(processInfo);
            }
            return result;
        }

        public static bool KillProcessById(int processId)
        {
            return KillWindowsProcessById(processId);
        }

        internal static List<WindowsProcessInfo> GetWindowsProcesses(string serverName = null)
        {
            if (string.IsNullOrWhiteSpace(serverName))
            {
                serverName = Environment.MachineName;
            }

            var result = new List<WindowsProcessInfo>();
            var serverHandle = Windows.Wtsapi32.WTSOpenServerW(serverName);

            try
            {
                var processInfoPtr = IntPtr.Zero;
                var processCount = 0U;
                var success = Windows.Wtsapi32.WTSEnumerateProcessesW(
                        serverHandle,
                        0,
                        1,
                        ref processInfoPtr,
                        ref processCount
                );
                var dataSize = Marshal.SizeOf(typeof(Windows.Wtsapi32.WindowsTerminalServiceProcessInfo));
                var currentProcessInfoPtr = processInfoPtr;

                if (success)
                {
                    for (var processIndex = 0; processIndex < processCount; processIndex++)
                    {
                        var processInfo = (Windows.Wtsapi32.WindowsTerminalServiceProcessInfo)Marshal.PtrToStructure(
                                currentProcessInfoPtr,
                                typeof(Windows.Wtsapi32.WindowsTerminalServiceProcessInfo)
                        );
                        currentProcessInfoPtr += dataSize;

                        var userSid = string.Empty;
                        success = Windows.Advapi32.ConvertSidToStringSidW(processInfo.pUserSid, ref userSid);
                        if (!success)
                        {
                            userSid = string.Empty;
                        }

                        var windowsProcessInfo = new WindowsProcessInfo
                        {
                            Id = (int) processInfo.processId,
                            Name = processInfo.pProcessName,
                            SessionId = (int) processInfo.sessionId,
                            UserSid = userSid
                        };
                        result.Add(windowsProcessInfo);
                    }
                    Windows.Wtsapi32.WTSFreeMemory(processInfoPtr);
                }
            }
            catch (Exception e)
            {
                Log.Error("Can not get Windows process list: " + e.Message);
            }

            if (serverHandle != Windows.Wtsapi32.WTS_CURRENT_SERVER_HANDLE)
            {
                Windows.Wtsapi32.WTSCloseServer(serverHandle);
            }
            return result;
        }

        internal static bool KillWindowsProcessById(int processId, string serverName = null)
        {
            if (string.IsNullOrWhiteSpace(serverName))
            {
                serverName = Environment.MachineName;
            }

            var result = false;
            var serverHandle = Windows.Wtsapi32.WTSOpenServerW(serverName);

            try
            {
                result = Windows.Wtsapi32.WTSTerminateProcess(
                        serverHandle,
                        (uint) processId,
                        0
                );
            }
            catch (Exception e)
            {
                Log.Error("Can not kill Windows process by id: " + processId + ", " + e.Message);
            }

            if (serverHandle != Windows.Wtsapi32.WTS_CURRENT_SERVER_HANDLE)
            {
                Windows.Wtsapi32.WTSCloseServer(serverHandle);
            }
            return result;
        }

        public class ProcessInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public string User { get; set; }

        }

        internal class WindowsProcessInfo
        {
            public int Id { get; set; }
            public int SessionId { get; set; }
            public string Name { get; set; }
            public string UserSid { get; set; }
        }
    }
}
