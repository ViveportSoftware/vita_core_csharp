using System.IO;

namespace Htc.Vita.Core.IO
{
    public partial class FileSystemManagerV2
    {
        /// <summary>
        /// Class DiskSpaceInfo.
        /// </summary>
        public class DiskSpaceInfo
        {
            /// <summary>
            /// Gets or sets the path.
            /// </summary>
            /// <value>The path.</value>
            public DirectoryInfo Path { get; set; }
            /// <summary>
            /// Gets or sets the free space of bytes.
            /// </summary>
            /// <value>The free space of bytes.</value>
            public long FreeOfBytes { get; set; } = -1;
            /// <summary>
            /// Gets or sets the total space of bytes.
            /// </summary>
            /// <value>The total space of bytes.</value>
            public long TotalOfBytes { get; set; } = -1;
            /// <summary>
            /// Gets or sets the total free space of bytes.
            /// </summary>
            /// <value>The total free space of bytes.</value>
            public long TotalFreeOfBytes { get; set; } = -1;
        }
    }
}
