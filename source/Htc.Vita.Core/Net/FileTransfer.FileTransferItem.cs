using System;
using System.IO;

namespace Htc.Vita.Core.Net
{
    public partial class FileTransfer
    {
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
    }
}
