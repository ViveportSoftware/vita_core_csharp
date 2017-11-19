using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class Wintrust
        {
            internal static Guid DRIVER_ACTION_VERIFY = new Guid("{F750E6C3-38EE-11d1-85E5-00C04FC295EE}");
            internal static Guid HTTPSPROV_ACTION = new Guid("{573E31F8-AABA-11d0-8CCB-00C04FC295EE}");
            internal static Guid OFFICESIGN_ACTION_VERIFY = new Guid("{5555C2CD-17FB-11d1-85C4-00C04FC295EE}");
            internal static Guid WINTRUST_ACTION_GENERIC_CHAIN_VERIFY = new Guid("{FC451C16-AC75-11D1-B4B8-00C04FB66EA0}");
            internal static Guid WINTRUST_ACTION_GENERIC_VERIFY_V2 = new Guid("{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}");
            internal static Guid WINTRUST_ACTION_TRUSTPROVIDER_TEST = new Guid("{573E31F8-DDBA-11d0-8CCB-00C04FC295EE}");

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            internal enum WTD_CHOICE : uint
            {
                /* WTD_CHOICE_FILE    */ WTD_CHOICE_FILE = 1,
                /* WTD_CHOICE_CATALOG */ WTD_CHOICE_CATALOG,
                /* WTD_CHOICE_BLOB    */ WTD_CHOICE_BLOB,
                /* WTD_CHOICE_SIGNER  */ WTD_CHOICE_SIGNER,
                /* WTD_CHOICE_CERT    */ WTD_CHOICE_CERT
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            [Flags]
            internal enum WTD_PROVIDERFLAG : uint
            {
                /* WTD_USE_IE4_TRUST_FLAG                  */ WTD_USE_IE4_TRUST_FLAG = 1,
                /* WTD_NO_IE4_CHAIN_FLAG                   */ WTD_NO_IE4_CHAIN_FLAG = 2,
                /* WTD_NO_POLICY_USAGE_FLAG                */ WTD_NO_POLICY_USAGE_FLAG = 4,
                /* WTD_REVOCATION_CHECK_NONE               */ WTD_REVOCATION_CHECK_NONE = 16,
                /* WTD_REVOCATION_CHECK_END_CERT           */ WTD_REVOCATION_CHECK_END_CERT = 32,
                /* WTD_REVOCATION_CHECK_CHAIN              */ WTD_REVOCATION_CHECK_CHAIN = 64,
                /* WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT */ WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 128,
                /* WTD_SAFER_FLAG                          */ WTD_SAFER_FLAG = 256,
                /* WTD_HASH_ONLY_FLAG                      */ WTD_HASH_ONLY_FLAG = 512,
                /* WTD_USE_DEFAULT_OSVER_CHECK             */ WTD_USE_DEFAULT_OSVER_CHECK = 1024,
                /* WTD_LIFETIME_SIGNING_FLAG               */ WTD_LIFETIME_SIGNING_FLAG = 2048,
                /* WTD_CACHE_ONLY_URL_RETRIEVAL            */ WTD_CACHE_ONLY_URL_RETRIEVAL = 4096,
                /* WTD_DISABLE_MD2_MD4                     */ WTD_DISABLE_MD2_MD4 = 8192,
                /* WTD_MOTW                                */ WTD_MOTW = 16384
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            internal enum WTD_REVOKE : uint
            {
                /* WTD_REVOKE_NONE       */ WTD_REVOKE_NONE,
                /* WTD_REVOKE_WHOLECHAIN */ WTD_REVOKE_WHOLECHAIN
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            internal enum WTD_STATEACTION : uint
            {
                /* WTD_STATEACTION_IGNORE           */ WTD_STATEACTION_IGNORE,
                /* WTD_STATEACTION_VERIFY           */ WTD_STATEACTION_VERIFY,
                /* WTD_STATEACTION_CLOSE            */ WTD_STATEACTION_CLOSE,
                /* WTD_STATEACTION_AUTO_CACHE       */ WTD_STATEACTION_AUTO_CACHE,
                /* WTD_STATEACTION_AUTO_CACHE_FLUSH */ WTD_STATEACTION_AUTO_CACHE_FLUSH
            }

            /**
             * WTD_UI enumeration
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            internal enum WinTrustDataUi : uint
            {
                /* WTD_UI_ALL    */ All = 1,
                /* WTD_UI_NONE   */ None,
                /* WTD_UI_NOBAD  */ NoBad,
                /* WTD_UI_NOGOOD */ NoGood
            }

            /**
             * WTD_UICONTEXT enumeration
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            internal enum WinTrustDataUiContext : uint
            {
                /* WTD_UICONTEXT_EXECUTE */ Execute,
                /* WTD_UICONTEXT_INSTALL */ Install
            }

            /**
             * WINTRUST_DATA structure
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct WinTrustData
            {
                public /* DWORD  */ uint cbStruct;
                public /* LPVOID */ IntPtr pPolicyCallbackData;
                public /* LPVOID */ IntPtr pSIPCallbackData;
                public /* DWORD  */ WinTrustDataUi dwUIChoice;
                public /* DWORD  */ WTD_REVOKE fdwRevocationChecks;
                public /* DWORD  */ WTD_CHOICE dwUnionChoice;
                public /* union  */ WinTrustDataUnionChoice infoUnion;
                public /* DWORD  */ WTD_STATEACTION dwStateAction;
                public /* HANDLE */ IntPtr hWVTStateData;
                public /* WCHAR* */ IntPtr pwszURLReference;
                public /* DWORD  */ WTD_PROVIDERFLAG dwProvFlags;
                public /* DWORD  */ WinTrustDataUiContext dwUIContext;

                // TODO
                // WINTRUST_SIGNATURE_SETTINGS *pSignatureSettings;
            }

            /**
             * WINTRUST_FILE_INFO structure
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388206.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            internal struct WinTrustFileInfo
            {
                public /* DWORD  */ uint cbStruct;
                [MarshalAs(UnmanagedType.LPWStr)] public /* LPCWSTR */ string pcwszFilePath;
                public /* HANDLE */ IntPtr hFile;
                public /* GUID*  */ IntPtr pgKnownSubject;
            }

            /**
             * WTD_UNION_CHOICE union
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            [StructLayout(LayoutKind.Explicit)]
            internal struct WinTrustDataUnionChoice
            {
                [FieldOffset(0)] public /* struct WINTRUST_FILE_INFO_    */ IntPtr pFile;
                [FieldOffset(0)] public /* struct WINTRUST_CATALOG_INFO_ */ IntPtr pCatalog;
                [FieldOffset(0)] public /* struct WINTRUST_BLOB_INFO_    */ IntPtr pBlob;
                [FieldOffset(0)] public /* struct WINTRUST_SGNR_INFO_    */ IntPtr pSgnr;
                [FieldOffset(0)] public /* struct WINTRUST_CERT_INFO_    */ IntPtr pCert;
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388208.aspx
             */
            [DllImport(Libraries.Windows_wintrust,
                    CallingConvention = CallingConvention.Winapi,
                    CharSet = CharSet.Unicode,
                    ExactSpelling = true,
                    SetLastError = true)]
            [return: MarshalAs(UnmanagedType.U4)]
            internal static extern uint WinVerifyTrust(
                    /* _In_ HWND   */ [In] IntPtr hWnd,
                    /* _In_ GUID*  */ [In] ref Guid pgActionId,
                    /* _In_ LPVOID */ [In] IntPtr pWvtData
            );
        }
    }
}
