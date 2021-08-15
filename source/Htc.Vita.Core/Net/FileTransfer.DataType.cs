using System;
using System.IO;

namespace Htc.Vita.Core.Net
{
    public partial class FileTransfer
    {
        /// <summary>
        /// Class FileTransferError.
        /// </summary>
        public class FileTransferError
        {
            /// <summary>
            /// Gets or sets the error description.
            /// </summary>
            /// <value>The error description.</value>
            public string ErrorDescription { get; set; }
        }

        /// <summary>
        /// Class FileTransferItem.
        /// </summary>
        public class FileTransferItem
        {
            /// <summary>
            /// Gets or sets the local path.
            /// </summary>
            /// <value>The local path.</value>
            public FileInfo LocalPath { get; set; }
            /// <summary>
            /// Gets or sets the remote path.
            /// </summary>
            /// <value>The remote path.</value>
            public Uri RemotePath { get; set; }
        }

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

        /// <summary>
        /// Enum FileTransferPriority
        /// </summary>
        public enum FileTransferPriority
        {
            /// <summary>
            /// Unknown priority
            /// </summary>
            Unknown,
            /// <summary>
            /// Foreground
            /// </summary>
            Foreground,
            /// <summary>
            /// High priority
            /// </summary>
            High,
            /// <summary>
            /// Normal priority
            /// </summary>
            Normal,
            /// <summary>
            /// Low priority
            /// </summary>
            Low
        }

        /// <summary>
        /// Enum FileTransferState
        /// </summary>
        public enum FileTransferState
        {
            /// <summary>
            /// Unknown state
            /// </summary>
            Unknown,
            /// <summary>
            /// Queued
            /// </summary>
            Queued,
            /// <summary>
            /// Connecting
            /// </summary>
            Connecting,
            /// <summary>
            /// Transferring
            /// </summary>
            Transferring,
            /// <summary>
            /// Suspended
            /// </summary>
            Suspended,
            /// <summary>
            /// Error
            /// </summary>
            Error,
            /// <summary>
            /// Transient error
            /// </summary>
            TransientError,
            /// <summary>
            /// Transferred
            /// </summary>
            Transferred,
            /// <summary>
            /// Acknowledged
            /// </summary>
            Acknowledged,
            /// <summary>
            /// Cancelled
            /// </summary>
            Cancelled
        }

        /// <summary>
        /// Enum FileTransferType
        /// </summary>
        public enum FileTransferType
        {
            /// <summary>
            /// Unknown type
            /// </summary>
            Unknown,
            /// <summary>
            /// Download
            /// </summary>
            Download,
            /// <summary>
            /// Upload
            /// </summary>
            Upload,
            /// <summary>
            /// Upload and reply
            /// </summary>
            UploadAndReply
        }
    }
}
