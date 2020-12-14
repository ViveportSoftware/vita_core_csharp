using System;

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
    }
}
