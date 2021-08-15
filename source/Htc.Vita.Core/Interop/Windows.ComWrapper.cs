using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsCallback : IBackgroundCopyCallback
        {
            private readonly BitsManager _bitsManager;

            internal BitsCallback(BitsManager bitsManager)
            {
                _bitsManager = bitsManager;
            }

            public BitsResult JobTransferred(IBackgroundCopyJob pJob)
            {
                if (pJob == null)
                {
                    return BitsResult.SOk;
                }

                if (_bitsManager == null)
                {
                    return BitsResult.SOk;
                }

                Guid guid;
                var bitsResult = pJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    _bitsManager.NotifyJobTransferred(guid.ToString());
                }

                return BitsResult.SOk;
            }

            public BitsResult JobError(
                    IBackgroundCopyJob pJob,
                    IBackgroundCopyError pError)
            {
                if (pJob == null)
                {
                    return BitsResult.SOk;
                }

                if (_bitsManager == null)
                {
                    return BitsResult.SOk;
                }

                Guid guid;
                var bitsResult = pJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    _bitsManager.NotifyJobError(guid.ToString());
                }

                return BitsResult.SOk;
            }

            public BitsResult JobModification(
                    IBackgroundCopyJob pJob,
                    uint dwReserved)
            {
                if (pJob == null)
                {
                    return BitsResult.SOk;
                }

                if (_bitsManager == null)
                {
                    return BitsResult.SOk;
                }

                Guid guid;
                var bitsResult = pJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    _bitsManager.NotifyJobModification(guid.ToString());
                }

                return BitsResult.SOk;
            }
        }

        internal class BitsError : IDisposable
        {
            private IBackgroundCopyError _backgroundCopyError;
            private bool _disposed;

            internal BitsError(IBackgroundCopyError backgroundCopyError)
            {
                _backgroundCopyError = backgroundCopyError;
            }

            ~BitsError()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }

                if (disposing)
                {
                    // Disposing managed resource
                }

                if (_backgroundCopyError == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_backgroundCopyError))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_backgroundCopyError);
#pragma warning restore CA1416
                }
                _backgroundCopyError = null;

                _disposed = true;
            }

            internal BitsErrorContext GetErrorContext()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                BitsErrorContext errorContext;
                HResult hResult;
                var bitsResult = _backgroundCopyError.GetError(
                        out errorContext,
                        out hResult
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return errorContext;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get error context. error: {bitsResult}");
                return BitsErrorContext.Unknown;
            }

            internal string GetErrorContextDescription()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                string errorContextDescription;
                var bitsResult = _backgroundCopyError.GetErrorContextDescription(
                        0, // 0 for language neutral
                        out errorContextDescription
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return errorContextDescription;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get error context description. error: {bitsResult}");
                return null;
            }

            internal string GetErrorDescription()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                string errorDescription;
                var bitsResult = _backgroundCopyError.GetErrorDescription(
                        0, // 0 for language neutral
                        out errorDescription
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return errorDescription;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get error description. error: {bitsResult}");
                return null;
            }

            internal BitsFile GetFile()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                IBackgroundCopyFile iBackgroundCopyFile;
                var bitsResult = _backgroundCopyError.GetFile(out iBackgroundCopyFile);
                if (bitsResult == BitsResult.SOk)
                {
                    return new BitsFile(iBackgroundCopyFile);
                }

                if (bitsResult != BitsResult.EFileNotAvailable)
                {
                    Logger.GetInstance(typeof(BitsError)).Error($"Cannot get file. error: {bitsResult}");
                }
                return null;
            }

            internal string GetProtocol()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                string protocol;
                var bitsResult = _backgroundCopyError.GetProtocol(out protocol);
                if (bitsResult == BitsResult.SOk)
                {
                    return protocol;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get protocol. error: {bitsResult}");
                return null;
            }
        }
    }
}
