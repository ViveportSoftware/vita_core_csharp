namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
        /// <summary>
        /// Enum View
        /// </summary>
        public enum View : uint
        {
            /// <summary>
            /// The default view
            /// </summary>
            Default    = 0,
            /// <summary>
            /// The 64-bit view
            /// </summary>
            Registry64 = Interop.Windows.RegistryKeyAccessRight.Wow6464Key,
            /// <summary>
            /// The 32-bit view
            /// </summary>
            Registry32 = Interop.Windows.RegistryKeyAccessRight.Wow6432Key
        }
    }
}
