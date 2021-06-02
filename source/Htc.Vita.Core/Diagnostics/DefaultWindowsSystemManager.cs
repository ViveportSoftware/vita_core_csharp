using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class DefaultWindowsSystemManager.
    /// Implements the <see cref="WindowsSystemManager" />
    /// </summary>
    /// <seealso cref="WindowsSystemManager" />
    public class DefaultWindowsSystemManager : WindowsSystemManager
    {
        private static bool GetNativeVersion(ref Windows.OsVersionInfoExW osVersionInfoExW)
        {
            if (Windows.GetVersionExW(ref osVersionInfoExW))
            {
                return true;
            }
            Logger.GetInstance(typeof(DefaultWindowsSystemManager)).Error($"Can not get native version. GLE: {Marshal.GetLastWin32Error()}");
            return false;
        }

        private static string GetProductNameFromRegistry()
        {
            return Win32Registry.GetStringValue(
                    Win32Registry.Hive.LocalMachine,
                    "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
                    "ProductName"
            );
        }

        private static WindowsProductType GetProductTypeFromNativeVersion(Windows.OsVersionInfoExW osVersionInfoExW)
        {
            return osVersionInfoExW.wProductType == Windows.ProductType.Workstation
                    ? WindowsProductType.Client
                    : WindowsProductType.Server;
        }

        private static Version GetProductVersionFromNativeVersion(Windows.OsVersionInfoExW osVersionInfoExW)
        {
            var result = new Version(
                    osVersionInfoExW.dwMajorVersion,
                    osVersionInfoExW.dwMinorVersion,
                    osVersionInfoExW.dwBuildNumber
            );

            if (result.Major == 6
                    && result.Minor == 2
                    && result.Build == 9200)
            {
                Logger.GetInstance(typeof(DefaultWindowsSystemManager)).Warn($"Version {result} detected. You may add app.manifest into your executable to correct Version API for Windows 8.1 or later");
            }

            if (result.Major == 10
                    && result.Minor == 0)
            {
                result = new Version(
                        result.Major,
                        result.Minor,
                        result.Build,
                        GetWindows10ProductRevisionFromRegistry()
                );
            }

            return result;
        }

        private static int GetWindows10ProductRevisionFromRegistry()
        {
            return Win32Registry.GetIntValue(
                    Win32Registry.Hive.LocalMachine,
                    "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion",
                    "UBR"
            );
        }

        /// <inheritdoc />
        protected override CheckResult OnCheck()
        {
            var result = new CheckResult
            {
                    ProductName = GetProductNameFromRegistry()
            };

            var osVersionInfoExW = new Windows.OsVersionInfoExW
            {
                    dwOSVersionInfoSize = Marshal.SizeOf(typeof(Windows.OsVersionInfoExW))
            };
            if (GetNativeVersion(ref osVersionInfoExW))
            {
                result.ProductType = GetProductTypeFromNativeVersion(osVersionInfoExW);
                result.ProductVersion = GetProductVersionFromNativeVersion(osVersionInfoExW);
            }
            return result;
        }
    }
}
