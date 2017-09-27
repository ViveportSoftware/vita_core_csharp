using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Kernel32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa364937.aspx
             */
            [DllImport(Libraries.Windows_kernel32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool GetDiskFreeSpaceExW(
                    [In] string directoryName,
                    out ulong freeBytesAvailable,
                    out ulong totalNumberOfBytes,
                    out ulong totalNumberOfFreeBytes
            );
        }
    }
}
