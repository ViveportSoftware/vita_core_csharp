using System.Collections.Generic;
using System.Diagnostics;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class ProcessManager.
    /// </summary>
    public static partial class ProcessManager
    {
        /// <summary>
        /// Gets the processes by first active user.
        /// </summary>
        /// <returns>List&lt;ProcessInfo&gt;.</returns>
        public static List<ProcessInfo> GetProcessesByFirstActiveUser()
        {
            var username = UserManager.GetFirstActiveUser();
            if (string.IsNullOrWhiteSpace(username))
            {
                return new List<ProcessInfo>();
            }
            return GetProcessesByUser(username);
        }

        /// <summary>
        /// Gets the processes by user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>List&lt;ProcessInfo&gt;.</returns>
        public static List<ProcessInfo> GetProcessesByUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return new List<ProcessInfo>();
            }
            return GetProcesses(username);
        }

        /// <summary>
        /// Gets the processes.
        /// </summary>
        /// <returns>List&lt;ProcessInfo&gt;.</returns>
        public static List<ProcessInfo> GetProcesses()
        {
            return GetProcesses(null);
        }

        /// <summary>
        /// Gets the processes.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>List&lt;ProcessInfo&gt;.</returns>
        public static List<ProcessInfo> GetProcesses(string username)
        {
            return GetProcesses(username, null);
        }

        /// <summary>
        /// Gets the processes.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <returns>List&lt;ProcessInfo&gt;.</returns>
        public static List<ProcessInfo> GetProcesses(
                string username,
                string serverName)
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

        /// <summary>
        /// Gets the process path by identifier.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <returns>System.String.</returns>
        public static string GetProcessPathById(int processId)
        {
            return Windows.GetPlatformProcessPathById(processId);
        }

        /// <summary>
        /// Determines whether the specified process is a current user process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns><c>true</c> if the specified process is a current user process; otherwise, <c>false</c>.</returns>
        public static bool IsCurrentUserProcess(Process process)
        {
            if (process == null)
            {
                return false;
            }
            return Windows.IsCurrentUserProcessInPlatform(process);
        }

        /// <summary>
        /// Determines whether the specified process is an elevated process.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <returns><c>true</c> if the specified process is an elevated process; otherwise, <c>false</c>.</returns>
        public static bool IsElevatedProcess(Process process)
        {
            if (process == null)
            {
                return false;
            }
            return Windows.IsElevatedProcessInPlatform(process);
        }

        /// <summary>
        /// Kills the process by identifier.
        /// </summary>
        /// <param name="processId">The process identifier.</param>
        /// <returns><c>true</c> if killing the process successfully, <c>false</c> otherwise.</returns>
        public static bool KillProcessById(int processId)
        {
            return Windows.KillPlatformProcessById(processId);
        }

        /// <summary>
        /// Launches the process as shell user by elevated user.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>ProcessInfo.</returns>
        public static ProcessInfo LaunchProcessAsShellUser(
                string fileName,
                string arguments)
        {
            return Windows.LaunchProcessAsShellUserInPlatform(
                    fileName,
                    arguments
            );
        }

        /// <summary>
        /// Launches the process as user.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <returns>ProcessInfo.</returns>
        public static ProcessInfo LaunchProcessAsUser(
                string fileName,
                string arguments)
        {
            return Windows.LaunchProcessAsUserInPlatform(
                    fileName,
                    arguments
            );
        }
    }
}
