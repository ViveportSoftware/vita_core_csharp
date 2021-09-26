using System;
using System.Diagnostics;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class WindowsStoreAppManager.
    /// </summary>
    public abstract partial class WindowsStoreAppManager
    {
        private bool? _isCurrentProcessRunningInContainer;
        private bool? _isIdentityAvailableWithCurrentProcess;

        static WindowsStoreAppManager()
        {
            TypeRegistry.RegisterDefault<WindowsStoreAppManager, DefaultWindowsStoreAppManager>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : WindowsStoreAppManager, new()
        {
            TypeRegistry.Register<WindowsStoreAppManager, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WindowsStoreAppManager.</returns>
        public static WindowsStoreAppManager GetInstance()
        {
            return TypeRegistry.GetInstance<WindowsStoreAppManager>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WindowsStoreAppManager.</returns>
        public static WindowsStoreAppManager GetInstance<T>()
                where T : WindowsStoreAppManager, new()
        {
            return TypeRegistry.GetInstance<WindowsStoreAppManager, T>();
        }

        /// <summary>
        /// Determines whether current process is running in container.
        /// </summary>
        /// <returns><c>true</c> if current process is running in container; otherwise, <c>false</c>.</returns>
        public bool IsCurrentProcessRunningInContainer()
        {
            if (_isCurrentProcessRunningInContainer != null)
            {
                return _isCurrentProcessRunningInContainer.Value;
            }

            if (!IsIdentityAvailableWithCurrentProcess())
            {
                _isCurrentProcessRunningInContainer = false;
                return _isCurrentProcessRunningInContainer.Value;
            }

            var appPackagePath = GetCurrentAppPackage().AppPackage?.Path?.ToString();
            if (string.IsNullOrWhiteSpace(appPackagePath))
            {
                _isCurrentProcessRunningInContainer = false;
                return _isCurrentProcessRunningInContainer.Value;
            }

            var currentProcessRunningPath = Process.GetCurrentProcess().MainModule?.FileName;
            if (string.IsNullOrWhiteSpace(currentProcessRunningPath))
            {
                _isCurrentProcessRunningInContainer = false;
                return _isCurrentProcessRunningInContainer.Value;
            }

            _isCurrentProcessRunningInContainer = currentProcessRunningPath.StartsWith(appPackagePath);
            return _isCurrentProcessRunningInContainer.Value;
        }

        /// <summary>
        /// Determines whether identity is available with current process.
        /// </summary>
        /// <returns><c>true</c> if identity is available with current process; otherwise, <c>false</c>.</returns>
        public bool IsIdentityAvailableWithCurrentProcess()
        {
            if (_isIdentityAvailableWithCurrentProcess != null)
            {
                return _isIdentityAvailableWithCurrentProcess.Value;
            }

            var getAppPackageResult = GetCurrentAppPackage();
            var getAppPackageStatus = getAppPackageResult.Status;
            _isIdentityAvailableWithCurrentProcess = getAppPackageStatus == GetAppPackageStatus.Ok;
            return _isIdentityAvailableWithCurrentProcess.Value;
        }

        /// <summary>
        /// Gets the application package by family name.
        /// </summary>
        /// <param name="familyName">The family name.</param>
        /// <returns>GetAppPackageResult.</returns>
        public GetAppPackageResult GetAppPackageByFamilyName(string familyName)
        {
            if (string.IsNullOrWhiteSpace(familyName))
            {
                return new GetAppPackageResult
                {
                        Status = GetAppPackageStatus.InvalidData
                };
            }

            GetAppPackageResult result = null;
            try
            {
                result = OnGetAppPackageByFamilyName(familyName);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WindowsStoreAppManager)).Error(e.ToString());
            }
            return result ?? new GetAppPackageResult();
        }

        /// <summary>
        /// Gets the current application package.
        /// </summary>
        /// <returns>GetAppPackageResult.</returns>
        public GetAppPackageResult GetCurrentAppPackage()
        {
            GetAppPackageResult result = null;
            try
            {
                result = OnGetCurrentAppPackage();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WindowsStoreAppManager)).Error(e.ToString());
            }
            return result ?? new GetAppPackageResult();
        }

        /// <summary>
        /// Called when getting application package by family name.
        /// </summary>
        /// <param name="familyName">The family name.</param>
        /// <returns>GetAppPackageResult.</returns>
        protected abstract GetAppPackageResult OnGetAppPackageByFamilyName(string familyName);
        /// <summary>
        /// Called when getting current application package.
        /// </summary>
        /// <returns>GetAppPackageResult.</returns>
        protected abstract GetAppPackageResult OnGetCurrentAppPackage();
    }
}
