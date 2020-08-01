using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/userenv/nf-userenv-createenvironmentblock")]
        [DllImport(Libraries.WindowsUserenv,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CreateEnvironmentBlock(
                /* _Out_ LPVOID */ [Out] out IntPtr lpEnvironment,
                /* _In_  HANDLE */ [In] SafeTokenHandle hToken,
                /* _In_  BOOL   */ [In] bool bInherit
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/userenv/nf-userenv-destroyenvironmentblock")]
        [DllImport(Libraries.WindowsUserenv,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DestroyEnvironmentBlock(
                /* _In_ LPVOID */ [In] IntPtr lpEnvironment
        );
    }
}
