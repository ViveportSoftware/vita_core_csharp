using System.Collections.Generic;
using System.Diagnostics;

namespace Htc.Vita.Core.Runtime
{
    public static partial class ProcessManager
    {
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

        public static List<ProcessInfo> GetProcesses(string username)
        {
            return GetProcesses(username, null);
        }

        public static List<ProcessInfo> GetProcesses(string username, string serverName)
        {
            var result = new List<ProcessInfo>();

            var windowsProcesses = Windows.GetPlatformProcesses(serverName);
            foreach (var windowsProcess in windowsProcesses)
            {
                var processId = windowsProcess.Id;
                var processName = windowsProcess.Name;
                var processUsername = UserManager.Windows.GetPlatformUsernameBySid(windowsProcess.UserSid) ?? "";
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

        public static string GetProcessPathById(int processId)
        {
            return Windows.GetPlatformProcessPathById(processId);
        }

        public static bool IsCurrentUserProcess(Process process)
        {
            if (process == null)
            {
                return false;
            }
            return Windows.IsCurrentUserProcessInPlatform(process);
        }

        public static bool IsElevatedProcess(Process process)
        {
            if (process == null)
            {
                return false;
            }
            return Windows.IsElevatedProcessInPlatform(process);
        }

        public static bool KillProcessById(int processId)
        {
            return Windows.KillPlatformProcessById(processId);
        }

        public static ProcessInfo LaunchProcessAsUser(string fileName, string arguments)
        {
            return Windows.LaunchProcessAsUserInPlatform(fileName, arguments);
        }
    }
}
