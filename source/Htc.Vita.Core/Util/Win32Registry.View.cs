namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
        public enum View : uint
        {
            Default    = 0,
            Registry64 = Interop.Windows.RegistryKeyAccessRight.Wow6464Key,
            Registry32 = Interop.Windows.RegistryKeyAccessRight.Wow6432Key
        }
    }
}
