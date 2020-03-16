using System;

namespace Htc.Vita.Core.Net
{
    partial class FileDownloader
    {
        public class DownloadOperationResult
        {
            public Net.FileDownloader.DownloadStatus Status { get; set; }
            public Exception Exception { get; set; }

            public static explicit operator int(DownloadOperationResult status)
            {
                return (int)status.Status;
            }

            public static implicit operator DownloadOperationResult(DownloadStatus status)
            {
                return new DownloadOperationResult(status);
            }

            public DownloadOperationResult(DownloadStatus status)
            {
                Status = status;
            }

            public DownloadOperationResult(DownloadStatus status, Exception exception)
            {
                Status = status;
                Exception = exception;
            }

            public DownloadOperationResult(DownloadOperationResult operationResult)
            {
                Status = operationResult.Status;
                Exception = operationResult.Exception;
            }

            public DownloadOperationResult()
            {
                Status = Net.FileDownloader.DownloadStatus.Success;
            }

            public bool Success => Status == DownloadStatus.Success;
            public bool Cancel => Status == DownloadStatus.Cancelled;
        }

        public class DownloadOperationResult<T> : DownloadOperationResult
        {
            public T RetValue { get; set; }

            public DownloadOperationResult(DownloadStatus status) : base(status) { }

            public DownloadOperationResult(T retValue)
            {
                RetValue = retValue;
                Status = DownloadStatus.Success;
            }

            public DownloadOperationResult(DownloadStatus status, Exception exception) : base(status)
            {
                Status = status;
                Exception = exception;
            }

            public DownloadOperationResult(DownloadOperationResult baseObject)
            {
                Status = baseObject.Status;
                Exception = baseObject.Exception;
            }

            public static implicit operator DownloadOperationResult<T>(DownloadStatus status)
            {
                return new DownloadOperationResult<T>(status);
            }
        }
    }
}
