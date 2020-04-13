namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
        /// <summary>
        /// Enum Hive
        /// </summary>
        public enum Hive
        {
            /// <summary>
            /// HKEY_CLASSES_ROOT
            /// </summary>
            ClassesRoot     = Interop.Windows.RegistryKey.ClassesRoot,
            /// <summary>
            /// HKEY_CURRENT_USER
            /// </summary>
            CurrentUser     = Interop.Windows.RegistryKey.CurrentUser,
            /// <summary>
            /// HKEY_LOCAL_MACHINE
            /// </summary>
            LocalMachine    = Interop.Windows.RegistryKey.LocalMachine,
            /// <summary>
            /// HKEY_USERS
            /// </summary>
            Users           = Interop.Windows.RegistryKey.Users,
            /// <summary>
            /// HKEY_PERFORMANCE_DATA
            /// </summary>
            PerformanceData = Interop.Windows.RegistryKey.PerformanceData,
            /// <summary>
            /// HKEY_CURRENT_CONFIG
            /// </summary>
            CurrentConfig   = Interop.Windows.RegistryKey.CurrentConfig
        }
    }
}
