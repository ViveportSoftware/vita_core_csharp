using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-certfreecertificatecontext")]
        [DllImport(Libraries.WindowsCrypt32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CertFreeCertificateContext(
                /* _In_opt_ PCCERT_CONTEXT */ [In] IntPtr pCertContext
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-certclosestore")]
        [DllImport(Libraries.WindowsCrypt32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CertCloseStore(
                /* _In_opt_ HCERTSTORE */ [In] IntPtr hCertStore,
                /* _In_     DWORD      */ [In] CertCloseStoreFlag dwFlags
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptmsgclose")]
        [DllImport(Libraries.WindowsCrypt32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptMsgClose(
                /* _In_opt_ HCRYPTMSG */ [In] IntPtr hCryptMsg
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptmsggetparam")]
        [DllImport(Libraries.WindowsCrypt32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern bool CryptMsgGetParam(
                /* _In_                                          HCRYPTMSG */ [In] SafeCryptMsgHandle hCryptMsg,
                /* _In_                                          DWORD     */ [In] CertMessageParameterType dwParamType,
                /* _In_                                          DWORD     */ [In] int dwIndex,
                /* _Out_writes_bytes_to_opt_(*pcbData, *pcbData) void*     */ [In][Out] byte[] vData,
                /* _Inout_                                       DWORD*    */ [In][Out] ref int pcbData
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject")]
        [DllImport(Libraries.WindowsCrypt32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool CryptQueryObject(
                /* _In_                          DWORD        */ [In] CertQueryObject dwObjectType,
                /* _In_                          const void*  */ [In] IntPtr pvObject,
                /* _In_                          DWORD        */ [In] CertQueryContentFlag dwExpectedContentTypeFlags,
                /* _In_                          DWORD        */ [In] CertQueryFormatFlag dwExpectedFormatTypeFlags,
                /* _In_                          DWORD        */ [In] int dwFlags,
                /* _Out_opt_                     DWORD*       */ [Out] out CertEncoding pdwMsgAndCertEncodingType,
                /* _Out_opt_                     DWORD*       */ [Out] out CertQueryContent pdwContentType,
                /* _Out_opt_                     DWORD*       */ [Out] out CertQueryFormat pdwFormatType,
                /* _Out_opt_                     HCERTSTORE*  */ [Out] out SafeCertStoreHandle phCertStore,
                /* _Out_opt_                     HCRYPTMSG*   */ [Out] out SafeCryptMsgHandle phMsg,
                /* _Outptr_opt_result_maybenull_ const void** */ [Out] out SafeCertContextHandle ppvContext
        );
    }
}
