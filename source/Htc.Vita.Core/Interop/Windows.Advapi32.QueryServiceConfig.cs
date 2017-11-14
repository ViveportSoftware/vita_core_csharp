using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684932.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool QueryServiceConfigW(
                    [In] IntPtr hService,
                    IntPtr lpServiceConfig,
                    uint cbBufSize,
                    out uint pcbBytesNeeded
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms684950.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct QUERY_SERVICE_CONFIG
            {
                public uint dwServiceType;

                public uint dwStartType;

                public uint dwErrorControl;

                public string lpBinaryPathName;

                public string lpLoadOrderGroup;

                public uint dwTagId;

                public string lpDependencies;

                public string lpServiceStartName;

                public string lpDisplayName;
            }

        }
    }
}
