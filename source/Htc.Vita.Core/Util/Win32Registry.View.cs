namespace Htc.Vita.Core.Util
{
    public partial class Win32Registry
    {
        public enum View
        {
            Default    = 0,
            Registry64 = (int)Interop.Windows.RegistryKeyAccessRight.Wow6464Key,
            Registry32 = (int)Interop.Windows.RegistryKeyAccessRight.Wow6432Key
        };
    }
}
