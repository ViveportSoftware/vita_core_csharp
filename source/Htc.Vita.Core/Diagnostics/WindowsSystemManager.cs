using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class WindowsSystemManager.
    /// </summary>
    public abstract partial class WindowsSystemManager
    {
        static WindowsSystemManager()
        {
            TypeRegistry.RegisterDefault<WindowsSystemManager, DefaultWindowsSystemManager>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : WindowsSystemManager, new()
        {
            TypeRegistry.Register<WindowsSystemManager, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>WindowsSystemManager.</returns>
        public static WindowsSystemManager GetInstance()
        {
            return TypeRegistry.GetInstance<WindowsSystemManager>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>WindowsSystemManager.</returns>
        public static WindowsSystemManager GetInstance<T>()
                where T : WindowsSystemManager, new()
        {
            return TypeRegistry.GetInstance<WindowsSystemManager, T>();
        }

        /// <summary>
        /// Checks basic Windows system information.
        /// </summary>
        /// <returns>CheckResult.</returns>
        public CheckResult Check()
        {
            CheckResult result = null;
            try
            {
                result = OnCheck();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WindowsSystemManager)).Error(e.ToString());
            }
            return result ?? new CheckResult();
        }

        /// <summary>
        /// Gets the installed update list.
        /// </summary>
        /// <returns>GetInstalledUpdateListResult.</returns>
        public GetInstalledUpdateListResult GetInstalledUpdateList()
        {
            GetInstalledUpdateListResult result = null;
            try
            {
                result = OnGetInstalledUpdateList();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(WindowsSystemManager)).Error(e.ToString());
            }
            return result ?? new GetInstalledUpdateListResult();
        }

        /// <summary>
        /// Called when checking basic Windows system information.
        /// </summary>
        /// <returns>CheckResult.</returns>
        protected abstract CheckResult OnCheck();
        /// <summary>
        /// Called when getting installed update list.
        /// </summary>
        /// <returns>GetInstalledUpdateListResult.</returns>
        protected abstract GetInstalledUpdateListResult OnGetInstalledUpdateList();
    }
}
