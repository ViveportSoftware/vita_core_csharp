using System;
using System.IO;
using System.Threading;

namespace Htc.Vita.Core.Net
{
    partial class FileDownloader
    {
        public static bool IsDiskFullException(Exception ex)
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

        public static void StreamCopyTo(Stream source, Stream destination, IProgress<long> progress, CancellationToken cancellationToken, int sleepIntervalMs, int bufferSize)
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
    }
}
