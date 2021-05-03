using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsFile : IDisposable
        {
            private IBackgroundCopyFile _backgroundCopyFile;
            private bool _disposed;

            internal BitsFile(IBackgroundCopyFile backgroundCopyFile)
            {
                _backgroundCopyFile = backgroundCopyFile;
            }

            ~BitsFile()
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

                if (_backgroundCopyFile == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_backgroundCopyFile))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_backgroundCopyFile);
#pragma warning restore CA1416
                }
                _backgroundCopyFile = null;

                _disposed = true;
            }

            internal string GetLocalName()
            {
                if (_backgroundCopyFile == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFile), $"Cannot access a closed {nameof(IBackgroundCopyFile)}.");
                }

                string localName;
                var bitsResult = _backgroundCopyFile.GetLocalName(out localName);
                if (bitsResult == BitsResult.SOk)
                {
                    return localName;
                }
                Logger.GetInstance(typeof(BitsFile)).Error($"Cannot get local name. error: {bitsResult}");
                return null;
            }

            internal BitsFileProgress GetProgress()
            {
                if (_backgroundCopyFile == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFile), $"Cannot access a closed {nameof(IBackgroundCopyFile)}.");
                }

                BitsFileProgress fileProgress;
                var bitsResult = _backgroundCopyFile.GetProgress(out fileProgress);
                if (bitsResult == BitsResult.SOk)
                {
                    return fileProgress;
                }
                Logger.GetInstance(typeof(BitsFile)).Error($"Cannot get progress. error: {bitsResult}");
                return new BitsFileProgress();
            }

            internal string GetRemoteName()
            {
                if (_backgroundCopyFile == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFile), $"Cannot access a closed {nameof(IBackgroundCopyFile)}.");
                }

                string remoteName;
                var bitsResult = _backgroundCopyFile.GetRemoteName(out remoteName);
                if (bitsResult == BitsResult.SOk)
                {
                    return remoteName;
                }
                Logger.GetInstance(typeof(BitsFile)).Error($"Cannot get remote name. error: {bitsResult}");
                return null;
            }
        }
    }
}
