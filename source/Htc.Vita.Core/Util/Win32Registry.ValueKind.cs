namespace Htc.Vita.Core.Util
{
    public partial class Win32Registry
    {
        public enum ValueKind : uint
        {
            None         = Interop.Windows.RegType.None,
            String       = Interop.Windows.RegType.Sz,
            ExpandString = Interop.Windows.RegType.ExpandSz,
            Binary       = Interop.Windows.RegType.Binary,
            DWord        = Interop.Windows.RegType.Dword,
            MultiString  = Interop.Windows.RegType.MultiSz,
            QWord        = Interop.Windows.RegType.Qword
        }
    }
}
