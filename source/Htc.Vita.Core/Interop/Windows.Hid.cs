using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidpi/ns-hidpi-_hidp_caps
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct HidDeviceCapability
        {
            public /* USAGE  */ ushort usage;
            public /* USAGE  */ ushort usagePage;
            public /* USHORT */ ushort inputReportByteLength;
            public /* USHORT */ ushort outputReportByteLength;
            public /* USHORT */ ushort featureReportByteLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)] public /* USHORT */ ushort[] reserved;
            public /* USHORT */ ushort numberLinkCollectionNodes;
            public /* USHORT */ ushort numberInputButtonCaps;
            public /* USHORT */ ushort numberInputValueCaps;
            public /* USHORT */ ushort numberInputDataIndices;
            public /* USHORT */ ushort numberOutputButtonCaps;
            public /* USHORT */ ushort numberOutputValueCaps;
            public /* USHORT */ ushort numberOutputDataIndices;
            public /* USHORT */ ushort numberFeatureButtonCaps;
            public /* USHORT */ ushort numberFeatureValueCaps;
            public /* USHORT */ ushort numberFeatureDataIndices;
        }

        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_freepreparseddata
         */
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool HidD_FreePreparsedData(
                IntPtr preparsedData
        );


        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidpi/nf-hidpi-hidp_getcaps
         */
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        public static extern NtStatus HidP_GetCaps(
                /* _In_  PHIDP_PREPARSED_DATA */ [In] IntPtr preparsedData,
                /* _Out_ PHIDP_CAPS           */ [In][Out] ref HidDeviceCapability capabilities
        );

        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getfeature
         */
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_GetFeature(
                /* _In_  HANDLE */ [In] SafeFileHandle hidDeviceObject,
                /* _Out_ PVOID  */ [In][Out] byte[] reportBuffer,
                /* _In_  ULONG  */ [In] uint reportBufferLength
        );

        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_gethidguid
         */
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void HidD_GetHidGuid(
                /* _Out_ LPGUID */ [In][Out] ref Guid hidGuid
        );

        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getpreparseddata
         */
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool HidD_GetPreparsedData(
                /* _In_  HANDLE               */ [In] SafeFileHandle hidDeviceObject,
                /* _Out_ PHIDP_PREPARSED_DATA */ [In][Out] ref IntPtr preparsedData
        );

        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getserialnumberstring
         */
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_GetSerialNumberString(
                /* _In_  HANDLE */ [In] SafeFileHandle hidDeviceObject,
                /* _Out_ PVOID  */ [In][Out] StringBuilder buffer,
                /* _In_  ULONG  */ [In] uint bufferLength
        );
    }
}
