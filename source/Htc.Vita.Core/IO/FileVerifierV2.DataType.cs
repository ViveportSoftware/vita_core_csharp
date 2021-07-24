namespace Htc.Vita.Core.IO
{
    public partial class FileVerifierV2
    {
        /// <summary>
        /// Enum ChecksumType
        /// </summary>
        public enum ChecksumType
        {
            /// <summary>
            /// Unknown checksum type
            /// </summary>
            Unknown,
            /// <summary>
            /// Automatic-detected checksum type
            /// </summary>
            Auto,
            /// <summary>
            /// MD5
            /// </summary>
            Md5,
            /// <summary>
            /// SHA-1
            /// </summary>
            Sha1,
            /// <summary>
            /// SHA-2 256
            /// </summary>
            Sha256
        }
    }
}
