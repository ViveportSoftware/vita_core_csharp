using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static Guid /* DRIVER_ACTION_VERIFY                 */ DriverActionVerify = new Guid("{F750E6C3-38EE-11d1-85E5-00C04FC295EE}");
        internal static Guid /* HTTPSPROV_ACTION                     */ HttpsProvAction = new Guid("{573E31F8-AABA-11d0-8CCB-00C04FC295EE}");
        internal static Guid /* OFFICESIGN_ACTION_VERIFY             */ OfficeSignActionVerify = new Guid("{5555C2CD-17FB-11d1-85C4-00C04FC295EE}");
        internal static Guid /* WINTRUST_ACTION_GENERIC_CHAIN_VERIFY */ WinTrustActionGenericChainVerify = new Guid("{FC451C16-AC75-11D1-B4B8-00C04FB66EA0}");
        internal static Guid /* WINTRUST_ACTION_GENERIC_VERIFY_V2    */ WinTrustActionGenericVerifyV2 = new Guid("{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}");
        internal static Guid /* WINTRUST_ACTION_TRUSTPROVIDER_TEST   */ WinTrustActionTrustProviderTest = new Guid("{573E31F8-DDBA-11d0-8CCB-00C04FC295EE}");

        /**
         * WTD_CHOICE enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
         */
        internal enum WinTrustDataChoice : uint
        {
            /* WTD_CHOICE_FILE    */ File = 1,
            /* WTD_CHOICE_CATALOG */ Catalog,
            /* WTD_CHOICE_BLOB    */ Blob,
            /* WTD_CHOICE_SIGNER  */ Signer,
            /* WTD_CHOICE_CERT    */ Cert
        }

        /**
         * WTD_PROVIDERFLAG enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
         */
        [Flags]
        internal enum WinTrustDataProviderFlag : uint
        {
            /* WTD_USE_IE4_TRUST_FLAG                  */ UseIe4TrustFlag = 1,
            /* WTD_NO_IE4_CHAIN_FLAG                   */ NoIe4ChainFlag = 2,
            /* WTD_NO_POLICY_USAGE_FLAG                */ NoPolicyUsageFlag = 4,
            /* WTD_REVOCATION_CHECK_NONE               */ RevocationCheckNone = 16,
            /* WTD_REVOCATION_CHECK_END_CERT           */ RevocationCheckEndCert = 32,
            /* WTD_REVOCATION_CHECK_CHAIN              */ RevocationCheckChain = 64,
            /* WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT */ RevocationCheckChainExcludeRoot = 128,
            /* WTD_SAFER_FLAG                          */ SaferFlag = 256,
            /* WTD_HASH_ONLY_FLAG                      */ HashOnlyFlag = 512,
            /* WTD_USE_DEFAULT_OSVER_CHECK             */ UseDefaultOsVerCheck = 1024,
            /* WTD_LIFETIME_SIGNING_FLAG               */ LifetimeSigningFlag = 2048,
            /* WTD_CACHE_ONLY_URL_RETRIEVAL            */ CacheOnlyUrlRetrieval = 4096,
            /* WTD_DISABLE_MD2_MD4                     */ DisableMd2Md4 = 8192,
            /* WTD_MOTW                                */ Motw = 16384
        }

        /**
         * WTD_REVOKE enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
         */
        internal enum WinTrustDataRevoke : uint
        {
            /* WTD_REVOKE_NONE       */ None,
            /* WTD_REVOKE_WHOLECHAIN */ WholeChain
        }

        /**
         * WTD_STATEACTION enumeration
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
         */
        internal enum WinTrustDataStateAction : uint
        {
            /* WTD_STATEACTION_IGNORE           */ Ignore,
            /* WTD_STATEACTION_VERIFY           */ Verify,
            /* WTD_STATEACTION_CLOSE            */ Close,
            /* WTD_STATEACTION_AUTO_CACHE       */ AutoCache,
            /* WTD_STATEACTION_AUTO_CACHE_FLUSH */ AutoCacheFlush
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
            public /* DWORD  */ WinTrustDataRevoke fdwRevocationChecks;
            public /* DWORD  */ WinTrustDataChoice dwUnionChoice;
            public /* union  */ WinTrustDataUnionChoice infoUnion;
            public /* DWORD  */ WinTrustDataStateAction dwStateAction;
            public /* HANDLE */ IntPtr hWVTStateData;
            public /* WCHAR* */ IntPtr pwszURLReference;
            public /* DWORD  */ WinTrustDataProviderFlag dwProvFlags;
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
        [DllImport(Libraries.WindowsWintrust,
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
