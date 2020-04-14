namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
        /// <summary>
        /// Enum ValueKind
        /// </summary>
        public enum ValueKind : uint
        {
            /// <summary>
            /// None
            /// </summary>
            None         = Interop.Windows.RegistryValueType.None,
            /// <summary>
            /// The string format
            /// </summary>
            String       = Interop.Windows.RegistryValueType.String,
            /// <summary>
            /// The expand string format
            /// </summary>
            ExpandString = Interop.Windows.RegistryValueType.ExpandString,
            /// <summary>
            /// The binary format
            /// </summary>
            Binary       = Interop.Windows.RegistryValueType.Binary,
            /// <summary>
            /// The double word format
            /// </summary>
            DWord        = Interop.Windows.RegistryValueType.DWord,
            /// <summary>
            /// The multi string format
            /// </summary>
            MultiString  = Interop.Windows.RegistryValueType.MultiString,
            /// <summary>
            /// The quad word format
            /// </summary>
            QWord        = Interop.Windows.RegistryValueType.QWord
        }
    }
}
