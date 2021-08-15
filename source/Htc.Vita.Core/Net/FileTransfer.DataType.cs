using System;
using System.Collections.Generic;
using System.IO;
using Htc.Vita.Core.Log;

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
        /// Class FileTransferJob.
        /// </summary>
        public abstract class FileTransferJob
        {
            /// <summary>
            /// Adds the item.
            /// </summary>
            /// <param name="item">The item.</param>
            /// <returns><c>true</c> if the item is added successfully, <c>false</c> otherwise.</returns>
            public bool AddItem(FileTransferItem item)
            {
                if (item?.LocalPath == null || item.RemotePath == null)
                {
                    return false;
                }

                var result = false;
                try
                {
                    result = OnAddItem(item);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Cancels this job.
            /// </summary>
            /// <returns><c>true</c> if canceling this job successfully, <c>false</c> otherwise.</returns>
            public bool Cancel()
            {
                var result = false;
                try
                {
                    result = OnCancel();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Completes this job.
            /// </summary>
            /// <returns><c>true</c> if completing this job successfully, <c>false</c> otherwise.</returns>
            public bool Complete()
            {
                var result = false;
                try
                {
                    result = OnComplete();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the display name.
            /// </summary>
            /// <returns>System.String.</returns>
            public string GetDisplayName()
            {
                string result = null;
                try
                {
                    result = OnGetDisplayName();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the error.
            /// </summary>
            /// <returns>FileTransferError.</returns>
            public FileTransferError GetError()
            {
                FileTransferError result = null;
                try
                {
                    result = OnGetError();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the identifier.
            /// </summary>
            /// <returns>System.String.</returns>
            public string GetId()
            {
                string result = null;
                try
                {
                    result = OnGetId();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the item list.
            /// </summary>
            /// <returns>List&lt;FileTransferItem&gt;.</returns>
            public List<FileTransferItem> GetItemList()
            {
                List<FileTransferItem> result = null;
                try
                {
                    result = OnGetItemList();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result ?? new List<FileTransferItem>();
            }

            /// <summary>
            /// Gets the priority.
            /// </summary>
            /// <returns>FileTransferPriority.</returns>
            public FileTransferPriority GetPriority()
            {
                var result = FileTransferPriority.Unknown;
                try
                {
                    result = OnGetPriority();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the progress.
            /// </summary>
            /// <returns>FileTransferProgress.</returns>
            public FileTransferProgress GetProgress()
            {
                FileTransferProgress result = null;
                try
                {
                    result = OnGetProgress();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the state.
            /// </summary>
            /// <returns>FileTransferState.</returns>
            public FileTransferState GetState()
            {
                var result = FileTransferState.Unknown;
                try
                {
                    result = OnGetState();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Gets the transfer type.
            /// </summary>
            /// <returns>FileTransferType.</returns>
            public FileTransferType GetTransferType()
            {
                var result = FileTransferType.Unknown;
                try
                {
                    result = OnGetTransferType();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Resumes this job.
            /// </summary>
            /// <returns><c>true</c> if resuming this job successfully, <c>false</c> otherwise.</returns>
            public bool Resume()
            {
                var result = false;
                try
                {
                    result = OnResume();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Sets the priority.
            /// </summary>
            /// <param name="priority">The priority.</param>
            /// <returns><c>true</c> if setting the priority successfully, <c>false</c> otherwise.</returns>
            public bool SetPriority(FileTransferPriority priority)
            {
                if (priority == FileTransferPriority.Unknown)
                {
                    return false;
                }

                var result = false;
                try
                {
                    result = OnSetPriority(priority);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Suspends this job.
            /// </summary>
            /// <returns><c>true</c> if suspending this job successfully, <c>false</c> otherwise.</returns>
            public bool Suspend()
            {
                var result = false;
                try
                {
                    result = OnSuspend();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(FileTransferJob)).Error(e.ToString());
                }
                return result;
            }

            /// <summary>
            /// Called when adding the item.
            /// </summary>
            /// <param name="item">The item.</param>
            /// <returns><c>true</c> if the item is added successfully, <c>false</c> otherwise.</returns>
            protected abstract bool OnAddItem(FileTransferItem item);
            /// <summary>
            /// Called when canceling this job.
            /// </summary>
            /// <returns><c>true</c> if canceling this job successfully, <c>false</c> otherwise.</returns>
            protected abstract bool OnCancel();
            /// <summary>
            /// Called when completing this job.
            /// </summary>
            /// <returns><c>true</c> if completing this job successfully, <c>false</c> otherwise.</returns>
            protected abstract bool OnComplete();
            /// <summary>
            /// Called when getting the display name.
            /// </summary>
            /// <returns>System.String.</returns>
            protected abstract string OnGetDisplayName();
            /// <summary>
            /// Called when getting the error.
            /// </summary>
            /// <returns>FileTransferError.</returns>
            protected abstract FileTransferError OnGetError();
            /// <summary>
            /// Called when getting the identifier.
            /// </summary>
            /// <returns>System.String.</returns>
            protected abstract string OnGetId();
            /// <summary>
            /// Called when getting the item list.
            /// </summary>
            /// <returns>List&lt;FileTransferItem&gt;.</returns>
            protected abstract List<FileTransferItem> OnGetItemList();
            /// <summary>
            /// Called when getting the priority.
            /// </summary>
            /// <returns>FileTransferPriority.</returns>
            protected abstract FileTransferPriority OnGetPriority();
            /// <summary>
            /// Called when getting the progress.
            /// </summary>
            /// <returns>FileTransferProgress.</returns>
            protected abstract FileTransferProgress OnGetProgress();
            /// <summary>
            /// Called when getting the state.
            /// </summary>
            /// <returns>FileTransferState.</returns>
            protected abstract FileTransferState OnGetState();
            /// <summary>
            /// Called when getting the transfer type.
            /// </summary>
            /// <returns>FileTransferType.</returns>
            protected abstract FileTransferType OnGetTransferType();
            /// <summary>
            /// Called when resuming this job.
            /// </summary>
            /// <returns><c>true</c> if resuming this job successfully, <c>false</c> otherwise.</returns>
            protected abstract bool OnResume();
            /// <summary>
            /// Called when setting the priority.
            /// </summary>
            /// <param name="priority">The priority.</param>
            /// <returns><c>true</c> if setting priority successfully, <c>false</c> otherwise.</returns>
            protected abstract bool OnSetPriority(FileTransferPriority priority);
            /// <summary>
            /// Called when suspending this job.
            /// </summary>
            /// <returns><c>true</c> if suspending this job successfully, <c>false</c> otherwise.</returns>
            protected abstract bool OnSuspend();
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
