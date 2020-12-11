using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public partial class FileTransfer
    {
        /// <summary>
        /// Class FileTransferJob.
        /// Implements the <see cref="System.IDisposable" />
        /// </summary>
        /// <seealso cref="System.IDisposable" />
        public abstract class FileTransferJob : IDisposable
        {
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

            /// <inheritdoc />
            public void Dispose()
            {
                OnDispose();
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
            /// Called when canceling this job.
            /// </summary>
            /// <returns><c>true</c> if canceling this job successfully, <c>false</c> otherwise.</returns>
            protected abstract bool OnCancel();
            /// <summary>
            /// Called when disposing.
            /// </summary>
            protected abstract void OnDispose();
            /// <summary>
            /// Called when getting the display name.
            /// </summary>
            /// <returns>System.String.</returns>
            protected abstract string OnGetDisplayName();
            /// <summary>
            /// Called when getting the identifier.
            /// </summary>
            /// <returns>System.String.</returns>
            protected abstract string OnGetId();
            /// <summary>
            /// Called when getting the transfer type.
            /// </summary>
            /// <returns>FileTransferType.</returns>
            protected abstract FileTransferType OnGetTransferType();
        }
    }
}
