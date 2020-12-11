using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsManager : IDisposable
        {
            private readonly IBackgroundCopyManager _backgroundCopyManager;

            internal BitsManager(IBackgroundCopyManager backgroundCopyManager)
            {
                _backgroundCopyManager = backgroundCopyManager;
            }

            public void Dispose()
            {
            }

            internal static BitsManager GetInstance()
            {
                var iBackgroundCopyManager = Activator.CreateInstance(typeof(ClsidBackgroundCopyManager)) as IBackgroundCopyManager;
                if (iBackgroundCopyManager != null)
                {
                    return new BitsManager(iBackgroundCopyManager);
                }

                Logger.GetInstance(typeof(BitsManager)).Error($"Cannot create new {nameof(IBackgroundCopyManager)}");
                return null;
            }
        }
    }
}
