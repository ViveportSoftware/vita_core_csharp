using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Hid
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff538924.aspx
             */
            [DllImport(Libraries.Windows_hid,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            public static extern void HidD_GetHidGuid(
                    out Guid guid
            );
        }
    }
}
