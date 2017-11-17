using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
            public static Guid DRIVER_ACTION_VERIFY = new Guid("{F750E6C3-38EE-11d1-85E5-00C04FC295EE}");
            public static Guid HTTPSPROV_ACTION = new Guid("{573E31F8-AABA-11d0-8CCB-00C04FC295EE}");
            public static Guid OFFICESIGN_ACTION_VERIFY = new Guid("{5555C2CD-17FB-11d1-85C4-00C04FC295EE}");
            public static Guid WINTRUST_ACTION_GENERIC_CHAIN_VERIFY = new Guid("{FC451C16-AC75-11D1-B4B8-00C04FB66EA0}");
            public static Guid WINTRUST_ACTION_GENERIC_VERIFY_V2 = new Guid("{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}");
            public static Guid WINTRUST_ACTION_TRUSTPROVIDER_TEST = new Guid("{573E31F8-DDBA-11d0-8CCB-00C04FC295EE}");

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_CHOICE
            {
                WTD_CHOICE_FILE = 1,
                WTD_CHOICE_CATALOG,
                WTD_CHOICE_BLOB,
                WTD_CHOICE_SIGNER,
                WTD_CHOICE_CERT
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            [Flags]
            public enum WTD_PROVIDERFLAG
            {
                WTD_USE_IE4_TRUST_FLAG = 1,
                WTD_NO_IE4_CHAIN_FLAG = 2,
                WTD_NO_POLICY_USAGE_FLAG = 4,
                WTD_REVOCATION_CHECK_NONE = 16,
                WTD_REVOCATION_CHECK_END_CERT = 32,
                WTD_REVOCATION_CHECK_CHAIN = 64,
                WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT = 128,
                WTD_SAFER_FLAG = 256,
                WTD_HASH_ONLY_FLAG = 512,
                WTD_USE_DEFAULT_OSVER_CHECK = 1024,
                WTD_LIFETIME_SIGNING_FLAG = 2048,
                WTD_CACHE_ONLY_URL_RETRIEVAL = 4096,
                WTD_DISABLE_MD2_MD4 = 8192,
                WTD_MOTW = 16384
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            public enum WTD_REVOKE
            {
                WTD_REVOKE_NONE,
                WTD_REVOKE_WHOLECHAIN
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388205.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct WINTRUST_DATA
            {
                public uint cbStruct;

                public IntPtr pPolicyCallbackData;

                public IntPtr pSIPCallbackData;

                public WTD_UI dwUIChoice;

                public WTD_REVOKE fdwRevocationChecks;

                public WTD_CHOICE dwUnionChoice;

                public WTD_UNION_CHOICE infoUnion;

                public WTD_STATEACTION dwStateAction;

                public IntPtr hWVTStateData;

                public IntPtr pwszURLReference;

                public WTD_PROVIDERFLAG dwProvFlags;

                public WTD_UICONTEXT dwUIContext;

                // TODO
                // WINTRUST_SIGNATURE_SETTINGS *pSignatureSettings;
            }

            /**
             * https://msdn.microsoft.com/en-us/library/windows/desktop/aa388206.aspx
             */
            [StructLayout(LayoutKind.Sequential)]
            public struct WINTRUST_FILE_INFO
            {
                public uint cbStruct;

                [MarshalAs(UnmanagedType.LPTStr)] public string filePath;

                public IntPtr hFile;

                public IntPtr pgKnownSubject;
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
            public static extern uint WinVerifyTrust(
                    [In] IntPtr hWnd,
                    [In] [MarshalAs(UnmanagedType.LPStruct)] Guid pgActionId,
                    [In] IntPtr pWinTrustData
            );
        }
    }
}
