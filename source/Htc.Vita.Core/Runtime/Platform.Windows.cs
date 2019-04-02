using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

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
                using (var processHandle = new Interop.Windows.SafeProcessHandle(Process.GetCurrentProcess()))
                {
                    Interop.Windows.SafeTokenHandle tokenHandle;
                    var success = Interop.Windows.OpenProcessToken(
                            processHandle,
                            Interop.Windows.TokenAccessRight.AdjustPrivileges | Interop.Windows.TokenAccessRight.Query,
                            out tokenHandle
                    );
                    if (!success)
                    {
                        Logger.GetInstance(typeof(Platform)).Error("Can not open process token, error code: " + Marshal.GetLastWin32Error());
                        return;
                    }

                    Interop.Windows.TokenPrivileges tokenPrivileges;
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
            }

            /**
             * https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed
             */
            internal static string GetFrameworkNameInPlatform()
            {
                const string path = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
                var release = Registry.GetIntValue(
                        Registry.Hive.LocalMachine,
                        path,
                        "Release"
                );
                if (release >= 461808)
                {
                    return ".NET Framework 4.7.2 or later";
                }
                if (release >= 461308)
                {
                    return ".NET Framework 4.7.1";
                }
                if (release >= 460798)
                {
                    return ".NET Framework 4.7";
                }
                if (release >= 394802)
                {
                    return ".NET Framework 4.6.2";
                }
                if (release >= 394254)
                {
                    return ".NET Framework 4.6.1";
                }
                if (release >= 393295)
                {
                    return ".NET Framework 4.6";
                }
                if (release >= 379893)
                {
                    return ".NET Framework 4.5.2";
                }
                if (release >= 378675)
                {
                    return ".NET Framework 4.5.1";
                }
                if (release >= 378389)
                {
                    return ".NET Framework 4.5";
                }
                return "Unknown .NET Framework (release " + release + ")";
            }

            internal static string GetProductNameInPlatform()
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                    {
                        using (var managementObjectCollection = searcher.Get())
                        {
                            foreach (var o in managementObjectCollection)
                            {
                                var managementObject = o as ManagementObject;
                                if (managementObject == null)
                                {
                                    continue;
                                }

                                try
                                {
                                    return (string)managementObject.GetPropertyValue("Caption");
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
