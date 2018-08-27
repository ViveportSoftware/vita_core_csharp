using System;
using System.Management;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public static partial class Platform
    {
        internal static class Windows
        {
            private static Interop.Windows.ExitType ConvertWindowsExitTypeFrom(ExitType exitType)
            {
                if (exitType == ExitType.Logoff)
                {
                    return Interop.Windows.ExitType.Force | Interop.Windows.ExitType.Logoff;
                }
                if (exitType == ExitType.Shutdown)
                {
                    return Interop.Windows.ExitType.Force | Interop.Windows.ExitType.Poweroff;
                }
                if (exitType == ExitType.Reboot)
                {
                    return Interop.Windows.ExitType.Force | Interop.Windows.ExitType.Reboot;
                }
                return Interop.Windows.ExitType.Force | Interop.Windows.ExitType.Reboot;
            }

            internal static void ExitInPlatform(ExitType exitType)
            {
                Interop.Windows.TokenPrivileges tokenPrivileges;
                var processHandle = Interop.Windows.GetCurrentProcess();
                var tokenHandle = IntPtr.Zero;
                var success = Interop.Windows.OpenProcessToken(
                        processHandle,
                        Interop.Windows.TokenAccessRight.AdjustPrivileges | Interop.Windows.TokenAccessRight.Query,
                        ref tokenHandle
                );
                if (!success)
                {
                    Logger.GetInstance(typeof(Platform)).Error("Can not open process token, error code: " + Marshal.GetLastWin32Error());
                    return;
                }

                tokenPrivileges.Count = 1;
                tokenPrivileges.Luid = 0;
                tokenPrivileges.Attr = Interop.Windows.SePrivilege.Enabled;
                success = Interop.Windows.LookupPrivilegeValueW(
                        null,
                        Interop.Windows.SeShutdownName,
                        ref tokenPrivileges.Luid
                );
                if (!success)
                {
                    Logger.GetInstance(typeof(Platform)).Error("Can not lookup privilege value, error code: " + Marshal.GetLastWin32Error());
                    return;
                }

                success = Interop.Windows.AdjustTokenPrivileges(
                        tokenHandle,
                        false,
                        ref tokenPrivileges,
                        0,
                        IntPtr.Zero,
                        IntPtr.Zero
                );
                if (!success)
                {
                    Logger.GetInstance(typeof(Platform)).Error("Can not adjust token privileges, error code: " + Marshal.GetLastWin32Error());
                    return;
                }

                success = Interop.Windows.ExitWindowsEx(
                        ConvertWindowsExitTypeFrom(exitType),
                        0
                );
                if (!success)
                {
                    Logger.GetInstance(typeof(Platform)).Error("Can not exit Windows, error code: " + Marshal.GetLastWin32Error());
                }
            }

            internal static string GetProductNameInPlatform()
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                    {
                        foreach (var o in searcher.Get())
                        {
                            var managementObject = o as ManagementObject;
                            if (managementObject == null)
                            {
                                continue;
                            }

                            try
                            {
                                return (string) managementObject.GetPropertyValue("Caption");
                            }
                            finally
                            {
                                /*
                                 * https://stackoverflow.com/questions/11896282/using-clause-fails-to-call-dispose
                                 */
                                managementObject.Dispose();
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Platform)).Error("Can not detect product name", e);
                }
                return "UNKNOWN";
            }
        }
    }
}
