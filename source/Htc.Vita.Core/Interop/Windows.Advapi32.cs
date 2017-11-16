using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ChangeServiceConfigW(
                    IntPtr hService,
                    SERVICE_TYPE serviceType,
                    START_TYPE startType,
                    ERROR_CONTROL_TYPE errorControl,
                    string binaryPathName,
                    string loadOrderGroup,
                    IntPtr lpTagId,
                    string dependencies,
                    string serviceStartName,
                    string password,
                    string displayName
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms682028.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool CloseServiceHandle(
                    IntPtr hSCObject
            );
        }
    }
}
