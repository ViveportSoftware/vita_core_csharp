using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class DefaultWindowsStoreAppManager.
    /// Implements the <see cref="WindowsStoreAppManager" />
    /// </summary>
    /// <seealso cref="WindowsStoreAppManager" />
    public class DefaultWindowsStoreAppManager : WindowsStoreAppManager
    {
        private static readonly Dictionary<string, string> StringKeyWithResult = new Dictionary<string, string>();

        private static string GetAppPackageFullNameByFamilyName(string familyName)
        {
            var count = 0;
            var bufferSize = 0;
            StringBuilder buffer = null;
            var error = Windows.GetPackagesByPackageFamily(
                    familyName,
                    ref count,
                    null,
                    ref bufferSize,
                    null
            );

            if (error == Windows.Error.InsufficientBuffer)
            {
                var intPtrArray = new IntPtr[count];
                buffer = new StringBuilder(bufferSize);
                error = Windows.GetPackagesByPackageFamily(
                        familyName,
                        ref count,
                        intPtrArray,
                        ref bufferSize,
                        buffer
                );
            }

            if (buffer == null || error != Windows.Error.Success)
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Debug($"Can not get package full name for {familyName}. error code: {error}");
                return null;
            }

            if (count == 1)
            {
                return buffer.ToString();
            }

            if (count > 1)
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Error($"Multiple packages detected for \"{familyName}\". count: {count}");
            }

            return null;
        }

        private static DirectoryInfo GetAppPackagePathByFullName(string fullName)
        {
            var bufferSize = 0;
            var buffer = new StringBuilder(bufferSize);
            var packagePathType = Windows.PackagePathType.Effective;
            var error = Windows.GetPackagePathByFullName2(
                    fullName,
                    packagePathType,
                    ref bufferSize,
                    buffer
            );

            if (error == Windows.Error.InsufficientBuffer)
            {
                buffer = new StringBuilder(bufferSize);
                error = Windows.GetPackagePathByFullName2(
                        fullName,
                        packagePathType,
                        ref bufferSize,
                        buffer
                );
            }

            string result = null;
            if (error == Windows.Error.Success)
            {
                result = buffer.ToString();
            }
            else
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Debug($"Can not get package path for {fullName}. error code: {error}");
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                return null;
            }
            try
            {
                return new DirectoryInfo(result);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Debug($"Can not get package path for {fullName}. error: {e.Message}");
            }
            return null;
        }

        private static string GetCurrentAppPackageFamilyName()
        {
            if (StringKeyWithResult.ContainsKey(nameof(GetCurrentAppPackageFamilyName)))
            {
                return StringKeyWithResult[nameof(GetCurrentAppPackageFamilyName)];
            }

            var bufferSize = 0;
            var buffer = new StringBuilder(bufferSize);
            var error = Windows.GetCurrentPackageFamilyName(
                    ref bufferSize,
                    buffer
            );

            if (error == Windows.Error.InsufficientBuffer)
            {
                buffer = new StringBuilder(bufferSize);
                error = Windows.GetCurrentPackageFamilyName(
                        ref bufferSize,
                        buffer
                );
            }

            string result = null;
            if (error == Windows.Error.Success)
            {
                result = buffer.ToString();
            }
            else
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Debug($"Can not get current app package family name. error code: {error}");
            }

            StringKeyWithResult[nameof(GetCurrentAppPackageFamilyName)] = result;
            return result;
        }

        private static string GetCurrentAppPackageFullName()
        {
            if (StringKeyWithResult.ContainsKey(nameof(GetCurrentAppPackageFullName)))
            {
                return StringKeyWithResult[nameof(GetCurrentAppPackageFullName)];
            }

            var bufferSize = 0;
            var buffer = new StringBuilder(bufferSize);
            var error = Windows.GetCurrentPackageFullName(
                    ref bufferSize,
                    buffer
            );

            if (error == Windows.Error.InsufficientBuffer)
            {
                buffer = new StringBuilder(bufferSize);
                error = Windows.GetCurrentPackageFullName(
                        ref bufferSize,
                        buffer
                );
            }

            string result = null;
            if (error == Windows.Error.Success)
            {
                result = buffer.ToString();
            }
            else
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Debug($"Can not get current app package full name. error code: {error}");
            }

            StringKeyWithResult[nameof(GetCurrentAppPackageFullName)] = result;
            return result;
        }

        private static DirectoryInfo GetCurrentAppPackagePath()
        {
            var bufferSize = 0;
            var buffer = new StringBuilder(bufferSize);
            var packagePathType = Windows.PackagePathType.Effective;
            var error = Windows.GetCurrentPackagePath2(
                    packagePathType,
                    ref bufferSize,
                    buffer
            );

            if (error == Windows.Error.InsufficientBuffer)
            {
                buffer = new StringBuilder(bufferSize);
                error = Windows.GetCurrentPackagePath2(
                        packagePathType,
                        ref bufferSize,
                        buffer
                );
            }

            string result = null;
            if (error == Windows.Error.Success)
            {
                result = buffer.ToString();
            }
            else
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Debug($"Can not get current app package path. error code: {error}");
            }

            if (string.IsNullOrWhiteSpace(result))
            {
                return null;
            }
            try
            {
                return new DirectoryInfo(result);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWindowsStoreAppManager)).Debug($"Can not get current app package path. error: {e.Message}");
            }
            return null;
        }

        /// <inheritdoc />
        protected override GetAppPackageResult OnGetAppPackageByFamilyName(string familyName)
        {
            if (!Platform.IsWindows)
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.UnsupportedPlatform
                };
            }

            var version = Environment.OSVersion.Version;
            if (version.Major == 6
                    && version.Minor == 1)
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.UnsupportedPlatform
                };
            }

            var appPackageFullName = GetAppPackageFullNameByFamilyName(familyName);
            if (string.IsNullOrWhiteSpace(appPackageFullName))
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.PackageNotFound
                };
            }

            var appPackagePath = GetAppPackagePathByFullName(appPackageFullName);
            if (appPackagePath == null)
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.PackageNotFound
                };
            }

            var appPackage = new WindowsStoreAppPackageInfo
            {
                    FamilyName = familyName,
                    FullName = appPackageFullName,
                    Path = appPackagePath
            };
            appPackage.FullNameList.Add(appPackage.FullName);

            return new GetAppPackageResult
            {
                    AppPackage = appPackage,
                    Status = GetAppPackageStatus.Ok
            };
        }

        /// <inheritdoc />
        protected override GetAppPackageResult OnGetCurrentAppPackage()
        {
            if (!Platform.IsWindows)
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.UnsupportedPlatform
                };
            }

            var version = Environment.OSVersion.Version;
            if (version.Major == 6
                    && version.Minor == 1)
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.UnsupportedPlatform
                };
            }

            var currentAppPackageFullName = GetCurrentAppPackageFullName();
            if (string.IsNullOrWhiteSpace(currentAppPackageFullName))
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.PackageNotFound
                };
            }

            var currentAppPackageFamilyName = GetCurrentAppPackageFamilyName();
            if (string.IsNullOrWhiteSpace(currentAppPackageFamilyName))
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.PackageNotFound
                };
            }

            var currentAppPackagePath = GetCurrentAppPackagePath();
            if (currentAppPackagePath == null)
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.PackageNotFound
                };
            }

            var appPackage = new WindowsStoreAppPackageInfo
            {
                    FamilyName = currentAppPackageFamilyName,
                    FullName = currentAppPackageFullName,
                    Path = currentAppPackagePath
            };
            appPackage.FullNameList.Add(appPackage.FullName);

            return new GetAppPackageResult
            {
                    AppPackage = appPackage,
                    Status = GetAppPackageStatus.Ok
            };
        }
    }
}
