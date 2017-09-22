using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            [StructLayout(LayoutKind.Explicit)]
            public struct WTD_UNION_CHOICE
            {
                [FieldOffset(0)]
                public IntPtr pFile;

                [FieldOffset(0)]
                public IntPtr pCatalog;

                [FieldOffset(0)]
                public IntPtr pBlob;

                [FieldOffset(0)]
                public IntPtr pSgnr;

                [FieldOffset(0)]
                public IntPtr pCert;
            }
        }
    }
}
