using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Setupapi
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff552344.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct SP_DEVINFO_DATA
            {
                public int cbSize;

                public Guid classGuid;

                public int devInst;

                public IntPtr reserved;
            }
        }
    }
}
