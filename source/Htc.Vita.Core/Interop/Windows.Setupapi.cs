using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdidestroydeviceinfolist")]
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiDestroyDeviceInfoList(
                /* _In_ HDEVINFO */ [In] IntPtr deviceInfoSet
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdienumdeviceinterfaces")]
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiEnumDeviceInterfaces(
                /* _In_           HDEVINFO                  */ [In] SafeDevInfoSetHandle deviceInfoSet,
                /* _In_opt_       PSP_DEVINFO_DATA          */ [In] IntPtr deviceInfoData,
                /* _In_     const GUID*                     */ [In] ref Guid interfaceClassGuid,
                /* _In_           DWORD                     */ [In] uint memberIndex,
                /* _Out_          PSP_DEVICE_INTERFACE_DATA */ [In][Out] ref SetupDeviceInterfaceData deviceInterfaceData
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetclassdevsw")]
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern SafeDevInfoSetHandle SetupDiGetClassDevsW(
                /* _In_opt_ const GUID   */ [In] ref Guid classGuid,
                /* _In_opt_       PCTSTR */ [In] string enumerator,
                /* _In_opt_       HWND   */ [In] IntPtr hwndParent,
                /* _In_           DWORD  */ [In] DeviceInfoGetClassFlag flags
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceinterfacedetailw")]
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceInterfaceDetailW(
                /* _In_      HDEVINFO                         */ [In] SafeDevInfoSetHandle hDevInfo,
                /* _In_      PSP_DEVICE_INTERFACE_DATA        */ [In] ref SetupDeviceInterfaceData deviceInterfaceData,
                /* _Out_opt_ PSP_DEVICE_INTERFACE_DETAIL_DATA */ [In][Out] IntPtr deviceInterfaceDetailData,
                /* _In_      DWORD                            */ [In] int deviceInterfaceDetailDataSize,
                /* _Out_opt_ PDWORD                           */ [In][Out] ref int requiredSize,
                /* _Out_opt_ PSP_DEVINFO_DATA                 */ [In][Out] IntPtr deviceInfoData
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceinterfacedetailw")]
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceInterfaceDetailW(
                /* _In_      HDEVINFO                         */ [In] SafeDevInfoSetHandle hDevInfo,
                /* _In_      PSP_DEVICE_INTERFACE_DATA        */ [In] ref SetupDeviceInterfaceData deviceInterfaceData,
                /* _Out_opt_ PSP_DEVICE_INTERFACE_DETAIL_DATA */ [In][Out] IntPtr deviceInterfaceDetailData,
                /* _In_      DWORD                            */ [In] int deviceInterfaceDetailDataSize,
                /* _Out_opt_ PDWORD                           */ [In][Out] ref int requiredSize,
                /* _Out_opt_ PSP_DEVINFO_DATA                 */ [In][Out] ref SetupDeviceInfoData deviceInfoData
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceregistrypropertyw")]
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceRegistryPropertyW(
                /* _In_      HDEVINFO         */ [In] SafeDevInfoSetHandle deviceInfoSet,
                /* _In_      PSP_DEVINFO_DATA */ [In] ref SetupDeviceInfoData deviceInfoData,
                /* _In_      DWORD            */ [In] SetupDeviceRegistryProperty property,
                /* _Out_opt_ PDWORD           */ [In][Out] IntPtr propertyRegDataType,
                /* _Out_opt_ PBYTE            */ [In][Out] IntPtr propertyBuffer,
                /* _In_      DWORD            */ [In] uint propertyBufferSize,
                /* _Out_opt_ PDWORD           */ [In][Out] ref uint requiredSize
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceregistrypropertyw")]
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiGetDeviceRegistryPropertyW(
                /* _In_      HDEVINFO         */ [In] SafeDevInfoSetHandle deviceInfoSet,
                /* _In_      PSP_DEVINFO_DATA */ [In] ref SetupDeviceInfoData deviceInfoData,
                /* _In_      DWORD            */ [In] SetupDeviceRegistryProperty property,
                /* _Out_opt_ PDWORD           */ [In][Out] ref RegistryValueType propertyRegDataType,
                /* _Out_opt_ PBYTE            */ [In][Out] byte[] propertyBuffer,
                /* _In_      DWORD            */ [In] uint propertyBufferSize,
                /* _Out_opt_ PDWORD           */ [In][Out] ref uint requiredSize
        );
    }
}
