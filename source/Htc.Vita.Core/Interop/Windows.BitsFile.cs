using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsFile : IDisposable
        {
            private readonly IBackgroundCopyFile _backgroundCopyFile;

            internal BitsFile(IBackgroundCopyFile backgroundCopyFile)
            {
                _backgroundCopyFile = backgroundCopyFile;
            }

            public void Dispose()
            {
                if (_backgroundCopyFile == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_backgroundCopyFile))
                {
                    Marshal.ReleaseComObject(_backgroundCopyFile);
                }
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
