using System;
using System.Collections.Generic;
using System.Management;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
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
        private const string FipsPolicyRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Lsa\\FipsAlgorithmPolicy";
        private const string FipsPolicyValueName = "Enabled";
        private const string ProductNameRegistryKey = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
        private const string ProductNameValueName = "ProductName";
        private const string SecureBootRegistryKey = @"SYSTEM\CurrentControlSet\Control\SecureBoot\State";
        private const string SecureBootValueName = "UEFISecureBootEnabled";
        private const string Windows10ProductRevisionRegistryKey = "SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion";
        private const string Windows10ProductRevisionValueName = "UBR";

        private static WindowsFipsStatus GetFipsStatusFromRegistry()
        {
            const int defaultValue = 13579;
            var enabled = Win32Registry.GetIntValue(
                    Win32Registry.Hive.LocalMachine,
                    FipsPolicyRegistryKey,
                    FipsPolicyValueName,
                    defaultValue
            );

            if (enabled == defaultValue)
            {
                return WindowsFipsStatus.Refused;
            }

            if (enabled == 0)
            {
                return IsSystemMd5Available()
                        ? WindowsFipsStatus.Disabled
                        : WindowsFipsStatus.RebootRequired;
            }

            if (enabled == 1)
            {
                return !IsSystemMd5Available()
                        ? WindowsFipsStatus.Enabled
                        : WindowsFipsStatus.RebootRequired;
            }

            return WindowsFipsStatus.Unknown;
        }

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
                    ProductNameRegistryKey,
                    ProductNameValueName
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

        private static WindowsSecureBootStatus GetSecureBootStatusFromRegistry()
        {
            const int defaultValue = 13579;
            var enabled = Win32Registry.GetIntValue(
                    Win32Registry.Hive.LocalMachine,
                    SecureBootRegistryKey,
                    SecureBootValueName,
                    defaultValue
            );

            if (enabled == defaultValue)
            {
                return WindowsSecureBootStatus.Refused;
            }

            if (enabled == 0)
            {
                return WindowsSecureBootStatus.Disabled;
            }

            if (enabled == 1)
            {
                return WindowsSecureBootStatus.Enabled;
            }

            return WindowsSecureBootStatus.Unknown;
        }

        private static int GetWindows10ProductRevisionFromRegistry()
        {
            return Win32Registry.GetIntValue(
                    Win32Registry.Hive.LocalMachine,
                    Windows10ProductRevisionRegistryKey,
                    Windows10ProductRevisionValueName
            );
        }

        private static bool IsSystemMd5Available()
        {
            try
            {
                using (MD5.Create())
                {
                    // Skip
                }
            }
            catch (TargetInvocationException)
            {
                return false;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWindowsSystemManager)).Error("Unexpected exception when use system MD5", e);
                return false;
            }

            return true;
        }

        /// <inheritdoc />
        protected override CheckResult OnCheck()
        {
            var result = new CheckResult
            {
                    FipsStatus = GetFipsStatusFromRegistry(),
                    ProductName = GetProductNameFromRegistry(),
                    SecureBootStatus = GetSecureBootStatusFromRegistry()
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

        /// <inheritdoc />
        protected override GetInstalledUpdateListResult OnGetInstalledUpdateList()
        {
            var installedUpdateList = new List<WindowsUpdateInfo>();

#pragma warning disable CA1416
            using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_QuickFixEngineering"))
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
                            var hotfixId = (string) managementObject.GetPropertyValue("HotFixID");
                            if (string.IsNullOrWhiteSpace(hotfixId))
                            {
                                continue;
                            }
                            var installedOnString = (string) managementObject.GetPropertyValue("InstalledOn");
                            if (string.IsNullOrWhiteSpace(installedOnString))
                            {
                                continue;
                            }
                            DateTime installedOn;
                            if (!DateTime.TryParse(installedOnString, out installedOn))
                            {
                                Logger.GetInstance(typeof(DefaultWindowsSystemManager)).Error($"Can not parse \"{installedOnString}\" to DateTime");
                                continue;
                            }

                            installedUpdateList.Add(new WindowsUpdateInfo
                            {
                                    Id = hotfixId,
                                    InstalledOn = installedOn
                            });
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
#pragma warning restore CA1416

            return new GetInstalledUpdateListResult
            {
                    InstalledUpdateList = installedUpdateList,
                    Status = GetInstalledUpdateListStatus.Ok
            };
        }
    }
}
