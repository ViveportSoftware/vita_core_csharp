using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Setupapi
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551069.aspx
             */
            [DllImport(Libraries.Windows_setupapi,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            public static extern IntPtr SetupDiGetClassDevsW(
                    ref Guid classGuid,
                    [MarshalAs(UnmanagedType.LPTStr)] string enumerator,
                    IntPtr hwndParent,
                    DIGCF flags
            );
        }
    }
}
