using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Wintrust
        {
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
        }
    }
}
