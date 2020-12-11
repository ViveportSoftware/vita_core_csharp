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

        internal static Windows.BitsJobPriority ConvertFrom(FileTransferPriority data)
        {
            if (data == FileTransferPriority.Foreground)
            {
                return Windows.BitsJobPriority.Foreground;
            }

            if (data == FileTransferPriority.High)
            {
                return Windows.BitsJobPriority.High;
            }

            if (data == FileTransferPriority.Normal)
            {
                return Windows.BitsJobPriority.Normal;
            }

            if (data == FileTransferPriority.Low)
            {
                return Windows.BitsJobPriority.Low;
            }

            return Windows.BitsJobPriority.Foreground;
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

        internal static FileTransferPriority ConvertFrom(Windows.BitsJobPriority data)
        {
            if (data == Windows.BitsJobPriority.Foreground)
            {
                return FileTransferPriority.Foreground;
            }

            if (data == Windows.BitsJobPriority.High)
            {
                return FileTransferPriority.High;
            }

            if (data == Windows.BitsJobPriority.Normal)
            {
                return FileTransferPriority.Normal;
            }

            if (data == Windows.BitsJobPriority.Low)
            {
                return FileTransferPriority.Low;
            }

            return FileTransferPriority.Unknown;
        }

        internal static FileTransferState ConvertFrom(Windows.BitsJobState data)
        {
            if (data == Windows.BitsJobState.Acknowledged)
            {
                return FileTransferState.Acknowledged;
            }

            if (data == Windows.BitsJobState.Cancelled)
            {
                return FileTransferState.Cancelled;
            }

            if (data == Windows.BitsJobState.Connecting)
            {
                return FileTransferState.Connecting;
            }

            if (data == Windows.BitsJobState.Error)
            {
                return FileTransferState.Error;
            }

            if (data == Windows.BitsJobState.Queued)
            {
                return FileTransferState.Queued;
            }

            if (data == Windows.BitsJobState.Suspended)
            {
                return FileTransferState.Suspended;
            }

            if (data == Windows.BitsJobState.Transferred)
            {
                return FileTransferState.Transferred;
            }

            if (data == Windows.BitsJobState.Transferring)
            {
                return FileTransferState.Transferring;
            }

            if (data == Windows.BitsJobState.TransientError)
            {
                return FileTransferState.TransientError;
            }

            return FileTransferState.Unknown;
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
