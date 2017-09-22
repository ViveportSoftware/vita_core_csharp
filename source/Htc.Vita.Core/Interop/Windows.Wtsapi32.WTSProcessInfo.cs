using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wtsapi32
        {
            /**
             * https://msdn.microsoft.com/zh-tw/library/aa383862.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct WTS_PROCESS_INFO
            {
                public int SessionID;

                public int ProcessID;

                [MarshalAs(UnmanagedType.LPTStr)] public string pProcessName;

                public IntPtr pUserSid;
            }
        }
    }
}
