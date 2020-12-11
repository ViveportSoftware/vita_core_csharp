using System.Collections.Generic;
using Htc.Vita.Core.Interop;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class BitsFileTransfer.
    /// Implements the <see cref="FileTransfer" />
    /// </summary>
    /// <seealso cref="FileTransfer" />
    public partial class BitsFileTransfer : FileTransfer
    {
        private readonly Windows.BitsManager _bitsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitsFileTransfer"/> class.
        /// </summary>
        public BitsFileTransfer()
        {
            _bitsManager = Windows.BitsManager.GetInstance();
        }

        internal static Windows.BitsFileInfo ConvertFrom(FileTransferItem data)
        {
            return new Windows.BitsFileInfo
            {
                    LocalName = data.LocalPath.ToString(),
                    RemoteName = data.RemotePath.ToString()
            };
        }

        internal static Windows.BitsJobType ConvertFrom(FileTransferType data)
        {
            if (data == FileTransferType.Download)
            {
                return Windows.BitsJobType.Download;
            }

            if (data == FileTransferType.Upload)
            {
                return Windows.BitsJobType.Upload;
            }

            if (data == FileTransferType.UploadAndReply)
            {
                return Windows.BitsJobType.UploadReply;
            }

            return Windows.BitsJobType.Download;
        }

        internal static FileTransferType ConvertFrom(Windows.BitsJobType data)
        {
            if (data == Windows.BitsJobType.Download)
            {
                return FileTransferType.Download;
            }

            if (data == Windows.BitsJobType.Upload)
            {
                return FileTransferType.Upload;
            }

            if (data == Windows.BitsJobType.UploadReply)
            {
                return FileTransferType.UploadAndReply;
            }

            return FileTransferType.Unknown;
        }

        /// <inheritdoc />
        protected override FileTransferJob OnGetJob(string jobId)
        {
            var bitsJob = _bitsManager?.GetJob(jobId);
            if (bitsJob == null)
            {
                return null;
            }
            return new BitsFileTransferJob(bitsJob);
        }

        /// <inheritdoc />
        protected override List<string> OnGetJobIdList()
        {
            return _bitsManager?.GetJobIdList();
        }

        /// <inheritdoc />
        protected override FileTransferJob OnRequestNewJob(
                string jobName,
                FileTransferType fileTransferType)
        {
            var bitsJob = _bitsManager?.CreateJob(
                    jobName,
                    ConvertFrom(fileTransferType)
            );
            if (bitsJob == null)
            {
                return null;
            }
            return new BitsFileTransferJob(bitsJob);
        }
    }
}
