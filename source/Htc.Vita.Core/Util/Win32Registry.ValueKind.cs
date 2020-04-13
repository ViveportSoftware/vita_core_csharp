namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
        public enum ValueKind : uint
        {
            None         = Interop.Windows.RegistryValueType.None,
            String       = Interop.Windows.RegistryValueType.String,
            ExpandString = Interop.Windows.RegistryValueType.ExpandString,
            Binary       = Interop.Windows.RegistryValueType.Binary,
            DWord        = Interop.Windows.RegistryValueType.DWord,
            MultiString  = Interop.Windows.RegistryValueType.MultiString,
            QWord        = Interop.Windows.RegistryValueType.QWord
        }
    }
}
