using System;

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

        /// <summary>
        /// Enum KeyPermissionCheck
        /// </summary>
        public enum KeyPermissionCheck
        {
            /// <summary>
            /// The default permission
            /// </summary>
            Default          = 0,
            /// <summary>
            /// The permission to read subtree
            /// </summary>
            ReadSubTree      = 1,
            /// <summary>
            /// The permission to read write subtree
            /// </summary>
            ReadWriteSubTree = 2
        }

        [Flags]
        internal enum KeyStateFlag
        {
            SystemKey   = 0x0001,
            WriteAccess = 0x0002
        }
    }
}
