using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388206.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct WINTRUST_FILE_INFO
            {
                public uint cbStruct;

                [MarshalAs(UnmanagedType.LPTStr)] public string filePath;

                public IntPtr hFile;

                public IntPtr pgKnownSubject;
            }
        }
    }
}
