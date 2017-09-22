using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wtsapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/aa383864.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct WTS_SESSION_INFO
            {
                public int SessionID;

                [MarshalAs(UnmanagedType.LPTStr)] public string pWinStationName;

                public WTS_CONNECTSTATE_CLASS State;
            }
        }
    }
}
