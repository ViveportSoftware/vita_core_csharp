using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsFiles : IDisposable
        {
            private readonly IEnumBackgroundCopyFiles _enumBackgroundCopyFiles;

            internal BitsFiles(IEnumBackgroundCopyFiles enumBackgroundCopyFiles)
            {
                _enumBackgroundCopyFiles = enumBackgroundCopyFiles;
            }

            public void Dispose()
            {
                if (_enumBackgroundCopyFiles == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_enumBackgroundCopyFiles))
                {
                    Marshal.ReleaseComObject(_enumBackgroundCopyFiles);
                }
            }

            internal uint GetCount()
            {
                if (_enumBackgroundCopyFiles == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFiles), $"Cannot access a closed {nameof(IEnumBackgroundCopyFiles)}.");
                }

                uint count;
                var bitsResult = _enumBackgroundCopyFiles.GetCount(out count);
                if (bitsResult == BitsResult.SOk)
                {
                    return count;
                }
                Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot get file count. error: {bitsResult}");
                return 0;
            }

            internal BitsFile GetFile(uint index)
            {
                if (_enumBackgroundCopyFiles == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFiles), $"Cannot access a closed {nameof(IEnumBackgroundCopyFiles)}.");
                }

                var bitsResult = _enumBackgroundCopyFiles.Reset();
                if (bitsResult != BitsResult.SOk)
                {
                    Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot reset file index. error: {bitsResult}");
                    return null;
                }

                if (index > 0)
                {
                    bitsResult = _enumBackgroundCopyFiles.Skip(index);
                    if (bitsResult != BitsResult.SOk)
                    {
                        Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot skip file items. error: {bitsResult}");
                        return null;
                    }
                }

                IBackgroundCopyFile iBackgroundCopyFile;
                uint fetchedFileCount;
                bitsResult = _enumBackgroundCopyFiles.Next(
                        1,
                        out iBackgroundCopyFile,
                        out fetchedFileCount
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return new BitsFile(iBackgroundCopyFile);
                }

                Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot get file item. error: {bitsResult}");
                return null;
            }
        }
    }
}
