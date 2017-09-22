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
