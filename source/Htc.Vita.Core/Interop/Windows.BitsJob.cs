using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsJob : IDisposable
        {
            private readonly IBackgroundCopyJob _backgroundCopyJob;

            internal BitsJob(IBackgroundCopyJob backgroundCopyJob)
            {
                _backgroundCopyJob = backgroundCopyJob;
            }

            internal bool AddFile(BitsFileInfo file)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var localName = file.LocalName;
                if (string.IsNullOrWhiteSpace(localName))
                {
                    return false;
                }

                var remoteName = file.RemoteName;
                if (string.IsNullOrWhiteSpace(remoteName))
                {
                    return false;
                }

                var bitsResult = _backgroundCopyJob.AddFile(
                        remoteName,
                        localName
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot add single file. error: {bitsResult}");
                return false;
            }

            internal bool Cancel()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.Cancel();
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot cancel job. error: {bitsResult}");
                return false;
            }

            public void Dispose()
            {
                if (_backgroundCopyJob == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_backgroundCopyJob))
                {
                    Marshal.ReleaseComObject(_backgroundCopyJob);
                }
            }

            internal string GetDisplayName()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                string displayName;
                var bitsResult = _backgroundCopyJob.GetDisplayName(out displayName);
                if (bitsResult == BitsResult.SOk)
                {
                    return displayName;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job display name. error: {bitsResult}");
                return null;
            }

            internal string GetId()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                Guid guid;
                var bitsResult = _backgroundCopyJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    return guid.ToString();
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job id. error: {bitsResult}");
                return null;
            }

            internal new BitsJobType GetType()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsJobType jobType;
                var bitsResult = _backgroundCopyJob.GetType(out jobType);
                if (bitsResult == BitsResult.SOk)
                {
                    return jobType;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job type. error: {bitsResult}");
                return BitsJobType.Download;
            }
        }
    }
}
