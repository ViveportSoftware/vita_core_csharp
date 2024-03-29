using System;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Util;
using Microsoft.Win32.SafeHandles;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_freepreparseddata")]
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_FreePreparsedData(
                IntPtr preparsedData
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getfeature")]
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

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_gethidguid")]
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void HidD_GetHidGuid(
                /* _Out_ LPGUID */ [In][Out] ref Guid hidGuid
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getindexedstring")]
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_GetIndexedString(
                /* _In_  HANDLE */ [In] SafeFileHandle hidDeviceObject,
                /* _In_  ULONG  */ [In] uint stringIndex,
                /* _Out_ PVOID  */ [In][Out] StringBuilder buffer,
                /* _In_  ULONG  */ [In] uint bufferLength
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getmanufacturerstring")]
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_GetManufacturerString(
                /* _In_  HANDLE */ [In] SafeFileHandle hidDeviceObject,
                /* _Out_ PVOID  */ [In][Out] StringBuilder buffer,
                /* _In_  ULONG  */ [In] uint bufferLength
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getpreparseddata")]
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_GetPreparsedData(
                /* _In_  HANDLE               */ [In] SafeFileHandle hidDeviceObject,
                /* _Out_ PHIDP_PREPARSED_DATA */ [In][Out] ref IntPtr preparsedData
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getproductstring")]
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool HidD_GetProductString(
                /* _In_  HANDLE */ [In] SafeFileHandle hidDeviceObject,
                /* _Out_ PVOID  */ [In][Out] StringBuilder buffer,
                /* _In_  ULONG  */ [In] uint bufferLength
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidsdi/nf-hidsdi-hidd_getserialnumberstring")]
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

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidpi/nf-hidpi-hidp_getcaps")]
        [DllImport(Libraries.WindowsHid,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern NtStatus HidP_GetCaps(
                /* _In_  PHIDP_PREPARSED_DATA */ [In] IntPtr preparsedData,
                /* _Out_ PHIDP_CAPS           */ [In][Out] ref HidDeviceCapability capabilities
        );
    }
}
