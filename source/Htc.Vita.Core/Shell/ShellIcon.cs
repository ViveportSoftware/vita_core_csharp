using System;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.Shell
{
    public static class ShellIcon
    {
        public static bool FlushCache()
        {
            if (!Platform.IsWindows)
            {
                return false;
            }
            return FlushCacheInWindows();
        }

        private static bool FlushCacheInWindows()
        {
            var result = false;
            try
            {
                Windows.SHChangeNotify(
                        Windows.ShellChangeNotifyEventId.AssociationChanged,
                        Windows.ShellChangeNotifyFlags.IdList,
                        IntPtr.Zero,
                        IntPtr.Zero
                );
                result = true;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ShellIcon)).Error($"Can not notify association change to shell: {e.Message}");
            }
            return result;
        }
    }
}
