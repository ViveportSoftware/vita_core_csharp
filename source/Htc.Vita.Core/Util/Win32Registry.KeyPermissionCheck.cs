namespace Htc.Vita.Core.Util
{
    public static partial class Win32Registry
    {
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
    }
}
