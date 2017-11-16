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
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            [Flags]
            public enum CONTROL_ACCEPTED : uint
            {
                SERVICE_ACCEPT_STOP = 0x00000001,
                SERVICE_ACCEPT_PAUSE_CONTINUE = 0x00000002,
                SERVICE_ACCEPT_SHUTDOWN = 0x00000004,
                SERVICE_ACCEPT_PARAMCHANGE = 0x00000008,
                SERVICE_ACCEPT_NETBINDCHANGE = 0x00000010,
                SERVICE_ACCEPT_HARDWAREPROFILECHANGE = 0x00000020,
                SERVICE_ACCEPT_POWEREVENT = 0x00000040,
                SERVICE_ACCEPT_SESSIONCHANGE = 0x00000080,
                SERVICE_ACCEPT_PRESHUTDOWN = 0x00000100,
                SERVICE_ACCEPT_TIMECHANGE = 0x00000200,
                SERVICE_ACCEPT_TRIGGEREVENT = 0x00000400,
                SERVICE_ACCEPT_USER_LOGOFF = 0x00000800,
                SERVICE_ACCEPT_LOWRESOURCES = 0x00002000,
                SERVICE_ACCEPT_SYSTEMLOWRESOURCES = 0x00004000
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            public enum CURRENT_STATE : uint
            {
                SERVICE_STOPPED = 0x00000001,
                SERVICE_START_PENDING = 0x00000002,
                SERVICE_STOP_PENDING = 0x00000003,
                SERVICE_RUNNING = 0x00000004,
                SERVICE_CONTINUE_PENDING = 0x00000005,
                SERVICE_PAUSE_PENDING = 0x00000006,
                SERVICE_PAUSED = 0x00000007
            }

            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            public enum ERROR_CONTROL_TYPE : uint
            {
                SERVICE_ERROR_IGNORE = 0x00000000,
                SERVICE_ERROR_NORMAL = 0x00000001,
                SERVICE_ERROR_SEVERE = 0x00000002,
                SERVICE_ERROR_CRITICAL = 0x00000003,
                SERVICE_NO_CHANGE = 0xffffffff
            }

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

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa376399.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ConvertSidToStringSidW(
                    [In] IntPtr pSid,
                    [MarshalAs(UnmanagedType.LPTStr)] ref string sid
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa376402.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool ConvertStringSidToSidW(
                    string sid,
                    IntPtr pSid
            );

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa379166.aspx
             */
            [DllImport(Libraries.Windows_advapi32,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool LookupAccountSidW(
                    [In] string pSystemName,
                    IntPtr pSid,
                    StringBuilder name,
                    [MarshalAs(UnmanagedType.U4)] ref int cchName,
                    StringBuilder referencedDomainName,
                    [MarshalAs(UnmanagedType.U4)] ref int cchReferencedDomainName,
                    out SID_NAME_USE peUse
            );
        }
    }
}
