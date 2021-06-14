using System;

namespace Htc.Vita.Core.IO
{
    public static partial class FileSystemManager
    {
        /// <summary>
        /// Class DiskSpaceInfo.
        /// </summary>
        [Obsolete("This class is obsoleted.")]
        public class DiskSpaceInfo
        {
            /// <summary>
            /// Gets or sets the path.
            /// </summary>
            /// <value>The path.</value>
            public string Path { get; set; } = "";
            /// <summary>
            /// Gets or sets the free space in bytes.
            /// </summary>
            /// <value>The free space in bytes.</value>
            public long FreeOfBytes { get; set; } = -1;
            /// <summary>
            /// Gets or sets the total space in bytes.
            /// </summary>
            /// <value>The total space in bytes.</value>
            public long TotalOfBytes { get; set; } = -1;
            /// <summary>
            /// Gets or sets the total free space in bytes.
            /// </summary>
            /// <value>The total free space in bytes.</value>
            public long TotalFreeOfBytes { get; set; } = -1;
        }
    }
}
