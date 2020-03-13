using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    partial class FileDownloader
    {
        internal static bool IsDiskFullException(Exception ex)
        {
            try
            {
                const int HR_ERROR_HANDLE_DISK_FULL = unchecked((int)0x80070027);
                const int HR_ERROR_DISK_FULL = unchecked((int)0x80070070);

                return ex.HResult == HR_ERROR_HANDLE_DISK_FULL
                       || ex.HResult == HR_ERROR_DISK_FULL;
            }
            catch (Exception) { }

            return false;
        }

        internal static void StreamCopyTo(Stream source, Stream destination, IProgress<long> progress, CancellationToken cancellationToken, int sleepIntervalMs, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            int bytesRead;
            long accumulatedBytesRead = 0;

            while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
            {
                destination.Write(buffer, 0, bytesRead);
                cancellationToken.ThrowIfCancellationRequested();
                accumulatedBytesRead += bytesRead;

                if (accumulatedBytesRead >= bufferSize)
                {
                    progress?.Report(accumulatedBytesRead);
                    accumulatedBytesRead = 0;

                    if (sleepIntervalMs > 0) SpinWait.SpinUntil(() => false, sleepIntervalMs);
                }
            }

            if (accumulatedBytesRead > 0) progress?.Report(accumulatedBytesRead);
        }

        internal static DownloadOperationResult DownloadErrorToOperationResult(Exception exc, CancellationToken? cancellationToken)
        {
            if (cancellationToken != null && ((CancellationToken)cancellationToken).IsCancellationRequested)
            {
                return DownloadStatus.Cancelled;
            }

            var exception = exc;

            if (exc.InnerException != null)
            {
                exception = exc.InnerException;
            }

            if (exception is TaskCanceledException || exception is OperationCanceledException)
            {
                return new DownloadOperationResult(DownloadStatus.TimeoutError, exc);
            }

            var httpStatusErrorException = exception as HttpFileDownloader.HttpStatusErrorException;

            if (httpStatusErrorException != null)
            {
                Logger.GetInstance(typeof(FileDownloader)).Error(
                    $"Found a HttpStatusErrorException: {httpStatusErrorException.HttpStatusCode.ToString()} => return {DownloadStatus.ServerResponseError}");
                return new DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
            }

            var webException = exception as WebException;

            if (webException != null)
            {
                var httpWebResponse = webException.Response as HttpWebResponse;
                if (httpWebResponse != null)
                {
                    Logger.GetInstance(typeof(FileDownloader)).Error(
                        $"Found a HttpStatusCode: {httpWebResponse.StatusCode.ToString()} => return {DownloadStatus.ServerResponseError}");
                    return new DownloadOperationResult(DownloadStatus.ServerResponseError, exc);
                }

                Logger.GetInstance(typeof(FileDownloader)).Error(
                    $"Found a WebExceptionStatus: {webException.Status.ToString()} => return {DownloadStatus.ServerConnectionError}");
                return new DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);
            }

            var socketException = exception as SocketException;

            if (socketException != null)
            {
                Logger.GetInstance(typeof(FileDownloader)).Error(
                    $"Found a SocketErrorCode: {socketException.SocketErrorCode.ToString()} => return {DownloadStatus.ServerConnectionError}");
                return new DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);

            }

            if (exception is HttpRequestException)
            {
                return new DownloadOperationResult(DownloadStatus.ServerConnectionError, exc);
            }

            if (exception is IOException)
            {
                if (IsDiskFullException(exception))
                {
                    return new DownloadOperationResult(DownloadStatus.OutOfFreeSpaceError, exc);
                }
                return new DownloadOperationResult(DownloadStatus.DiskIOException, exc);
            }

            return new DownloadOperationResult(DownloadStatus.InternalError, exc);
        }
    }
}
