using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class FileTransfer.
    /// </summary>
    public abstract partial class FileTransfer
    {
        /// <summary>
        /// Occurs when on job error.
        /// </summary>
        public event Action<string> OnJobError;
        /// <summary>
        /// Occurs when on job transferred.
        /// </summary>
        public event Action<string> OnJobTransferred;

        static FileTransfer()
        {
            TypeRegistry.RegisterDefault<FileTransfer, BitsFileTransfer>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : FileTransfer, new()
        {
            TypeRegistry.Register<FileTransfer, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>FileTransfer.</returns>
        public static FileTransfer GetInstance()
        {
            return TypeRegistry.GetInstance<FileTransfer>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>FileTransfer.</returns>
        public static FileTransfer GetInstance<T>()
                where T : FileTransfer, new()
        {
            return TypeRegistry.GetInstance<FileTransfer, T>();
        }

        /// <summary>
        /// Gets the job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns>FileTransferJob.</returns>
        public FileTransferJob GetJob(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                return null;
            }

            FileTransferJob result = null;
            try
            {
                result = OnGetJob(jobId);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileTransfer)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Gets the job identifier list.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        public List<string> GetJobIdList()
        {
            List<string> result = null;
            try
            {
                result = OnGetJobIdList();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileTransfer)).Error(e.ToString());
            }
            return result ?? new List<string>();
        }

        /// <summary>
        /// Listens the job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns><c>true</c> if the job is able to be listened, <c>false</c> otherwise.</returns>
        public bool ListenJob(FileTransferJob job)
        {
            if (job == null)
            {
                return false;
            }

            var result = false;
            try
            {
                result = OnListenJob(job);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileTransfer)).Error(e.ToString());
            }
            return result;
        }

        internal void NotifyJobError(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                return;
            }

            Task.Run(() =>
            {
                    try
                    {
                        OnJobError?.Invoke(jobId);
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(FileTransfer)).Error(e.ToString());
                    }
            });
        }

        internal void NotifyJobTransferred(string jobId)
        {
            if (string.IsNullOrWhiteSpace(jobId))
            {
                return;
            }

            Task.Run(() =>
            {
                    try
                    {
                        OnJobTransferred?.Invoke(jobId);
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(FileTransfer)).Error(e.ToString());
                    }
            });
        }

        /// <summary>
        /// Requests the new download job.
        /// </summary>
        /// <param name="jobName">The job name.</param>
        /// <returns>FileTransferJob.</returns>
        public FileTransferJob RequestNewDownloadJob(string jobName)
        {
            return RequestNewJob(
                    jobName,
                    FileTransferType.Download
            );
        }

        /// <summary>
        /// Requests the new job.
        /// </summary>
        /// <param name="jobName">The job name.</param>
        /// <param name="fileTransferType">The file transfer type.</param>
        /// <returns>FileTransferJob.</returns>
        internal FileTransferJob RequestNewJob(
                string jobName,
                FileTransferType fileTransferType)
        {
            if (fileTransferType == FileTransferType.Unknown)
            {
                return null;
            }

            FileTransferJob result = null;
            try
            {
                result = OnRequestNewJob(
                        jobName,
                        fileTransferType
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileTransfer)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when getting job.
        /// </summary>
        /// <param name="jobId">The job identifier.</param>
        /// <returns>FileTransferJob.</returns>
        protected abstract FileTransferJob OnGetJob(string jobId);
        /// <summary>
        /// Called when getting job identifier list.
        /// </summary>
        /// <returns>List&lt;System.String&gt;.</returns>
        protected abstract List<string> OnGetJobIdList();
        /// <summary>
        /// Called when listening job.
        /// </summary>
        /// <param name="job">The job.</param>
        /// <returns><c>true</c> if the job is able to be listened, <c>false</c> otherwise.</returns>
        protected abstract bool OnListenJob(FileTransferJob job);
        /// <summary>
        /// Called when requesting new job.
        /// </summary>
        /// <param name="jobName">The job name.</param>
        /// <param name="fileTransferType">The file transfer type.</param>
        /// <returns>FileTransferJob.</returns>
        protected abstract FileTransferJob OnRequestNewJob(
                string jobName,
                FileTransferType fileTransferType
        );

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
    }
}
