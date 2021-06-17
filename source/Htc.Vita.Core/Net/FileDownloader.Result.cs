using System;

namespace Htc.Vita.Core.Net
{
    public partial class FileDownloader
    {
        /// <summary>
        /// Class DownloadOperationResult.
        /// </summary>
        public class DownloadOperationResult
        {
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public DownloadStatus Status { get; set; }
            /// <summary>
            /// Gets or sets the exception.
            /// </summary>
            /// <value>The exception.</value>
            public Exception Exception { get; set; }

            /// <summary>
            /// Performs an explicit conversion from <see cref="DownloadOperationResult"/> to <see cref="System.Int32"/>.
            /// </summary>
            /// <param name="status">The status.</param>
            /// <returns>The result of the conversion.</returns>
            public static explicit operator int(DownloadOperationResult status)
            {
                return (int)status.Status;
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="DownloadStatus"/> to <see cref="DownloadOperationResult"/>.
            /// </summary>
            /// <param name="status">The status.</param>
            /// <returns>The result of the conversion.</returns>
            public static implicit operator DownloadOperationResult(DownloadStatus status)
            {
                return new DownloadOperationResult(status);
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult"/> class.
            /// </summary>
            /// <param name="status">The status.</param>
            public DownloadOperationResult(DownloadStatus status)
            {
                Status = status;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult"/> class.
            /// </summary>
            /// <param name="status">The status.</param>
            /// <param name="exception">The exception.</param>
            public DownloadOperationResult(DownloadStatus status, Exception exception)
            {
                Status = status;
                Exception = exception;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult"/> class.
            /// </summary>
            /// <param name="operationResult">The operation result.</param>
            public DownloadOperationResult(DownloadOperationResult operationResult)
            {
                Status = operationResult.Status;
                Exception = operationResult.Exception;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult"/> class.
            /// </summary>
            public DownloadOperationResult()
            {
                Status = DownloadStatus.Success;
            }

            /// <summary>
            /// Gets a value indicating whether this <see cref="DownloadOperationResult"/> is success.
            /// </summary>
            /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
            public bool Success => Status == DownloadStatus.Success;
            /// <summary>
            /// Gets a value indicating whether this <see cref="DownloadOperationResult"/> is cancel.
            /// </summary>
            /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
            public bool Cancel => Status == DownloadStatus.Cancelled;
        }

        /// <summary>
        /// Class DownloadOperationResult.
        /// Implements the <see cref="DownloadOperationResult" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <seealso cref="DownloadOperationResult" />
        public class DownloadOperationResult<T> : DownloadOperationResult
        {
            /// <summary>
            /// Gets or sets the ret value.
            /// </summary>
            /// <value>The ret value.</value>
            public T RetValue { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult{T}"/> class.
            /// </summary>
            /// <param name="status">The status.</param>
            public DownloadOperationResult(DownloadStatus status) : base(status) { }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult{T}"/> class.
            /// </summary>
            /// <param name="retValue">The ret value.</param>
            public DownloadOperationResult(T retValue)
            {
                RetValue = retValue;
                Status = DownloadStatus.Success;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult{T}"/> class.
            /// </summary>
            /// <param name="status">The status.</param>
            /// <param name="exception">The exception.</param>
            public DownloadOperationResult(DownloadStatus status, Exception exception) : base(status)
            {
                Status = status;
                Exception = exception;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="DownloadOperationResult{T}"/> class.
            /// </summary>
            /// <param name="baseObject">The base object.</param>
            public DownloadOperationResult(DownloadOperationResult baseObject)
            {
                Status = baseObject.Status;
                Exception = baseObject.Exception;
            }

            /// <summary>
            /// Performs an implicit conversion from <see cref="DownloadStatus"/> to <see cref="DownloadOperationResult{T}"/>.
            /// </summary>
            /// <param name="status">The status.</param>
            /// <returns>The result of the conversion.</returns>
            public static implicit operator DownloadOperationResult<T>(DownloadStatus status)
            {
                return new DownloadOperationResult<T>(status);
            }
        }
    }
}
