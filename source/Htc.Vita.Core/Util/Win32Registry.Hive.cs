namespace Htc.Vita.Core.Util
{
    public partial class Win32Registry
    {
        public enum Hive
        {
            ClassesRoot     = Interop.Windows.RegistryKey.ClassesRoot,
            CurrentUser     = Interop.Windows.RegistryKey.CurrentUser,
            LocalMachine    = Interop.Windows.RegistryKey.LocalMachine,
            Users           = Interop.Windows.RegistryKey.Users,
            PerformanceData = Interop.Windows.RegistryKey.PerformanceData,
            CurrentConfig   = Interop.Windows.RegistryKey.CurrentConfig
        }
    }
}
