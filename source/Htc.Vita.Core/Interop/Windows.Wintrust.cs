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

                [MarshalAs(UnmanagedType.LPTStr)]
                public string filePath;

                public IntPtr hFile;

                public IntPtr pgKnownSubject;
            }
        }
    }
}
