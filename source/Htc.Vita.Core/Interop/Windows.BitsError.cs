using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsError : IDisposable
        {
            private readonly IBackgroundCopyError _backgroundCopyError;

            internal BitsError(IBackgroundCopyError backgroundCopyError)
            {
                _backgroundCopyError = backgroundCopyError;
            }

            public void Dispose()
            {
                if (_backgroundCopyError == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_backgroundCopyError))
                {
                    Marshal.ReleaseComObject(_backgroundCopyError);
                }
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
        }
    }
}
