namespace Htc.Vita.Core.Net
{
    public abstract partial class FileTransfer
    {
        /// <summary>
        /// Class FileTransferProgress.
        /// </summary>
        public class FileTransferProgress
        {
            /// <summary>
            /// Gets or sets the total bytes.
            /// </summary>
            /// <value>The total bytes.</value>
            public ulong TotalBytes { get; set; }
            /// <summary>
            /// Gets or sets the total files.
            /// </summary>
            /// <value>The total files.</value>
            public uint TotalFiles { get; set; }
            /// <summary>
            /// Gets or sets the transferred bytes.
            /// </summary>
            /// <value>The transferred bytes.</value>
            public ulong TransferredBytes { get; set; }
            /// <summary>
            /// Gets or sets the transferred files.
            /// </summary>
            /// <value>The transferred files.</value>
            public uint TransferredFiles { get; set; }
        }
    }
}
