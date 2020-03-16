using System;

namespace Htc.Vita.Core.Runtime
{
    public static partial class Platform
    {
        public class NativeLibInfo
        {
            public Exception InnerException { get; internal set; }
            public IntPtr Handle { get; internal set; }

            public bool IsNoError()
            {
                return InnerException == null;
            }
        }
    }
}
