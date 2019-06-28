using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static readonly Guid /* GUID_DEVCLASS_USB            */ DeviceClassUsb = new Guid("{36FC9E60-C465-11CF-8056-444553540000}");
        internal static readonly Guid /* GUID_DEVINTERFACE_HID        */ DeviceInterfaceHid = new Guid("{4D1E55B2-F16F-11CF-88CB-001111000030}");
        internal static readonly Guid /* GUID_DEVINTERFACE_USB_DEVICE */ DeviceInterfaceUsbDevice = new Guid("{A5DCBF10-6530-11D2-901F-00C04FB951ED}");

        /**
         * DIGCF enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetclassdevsw
         */
        [Flags]
        internal enum DeviceInfoGetClassFlag : uint
        {
            /* DIGCF_DEFAULT         */ Default = 0x00000001,
            /* DIGCF_PRESENT         */ Present = 0x00000002,
            /* DIGCF_ALLCLASSES      */ AllClasses = 0x00000004,
            /* DIGCF_PROFILE         */ Profile = 0x00000008,
            /* DIGCF_DEVICEINTERFACE */ DeviceInterface = 0x00000010
        }

        /**
         * SPDRP enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceregistrypropertyw
         */
        internal enum SetupDeviceRegistryProperty : uint
        {
            /* SPDRP_DEVICEDESC                  */ DeviceDesc = 0,
            /* SPDRP_HARDWAREID                  */ HardwareId = 1,
            /* SPDRP_COMPATIBLEIDS               */ CompatibleIds = 2,
            /* SPDRP_UNUSED0                     */ Unused0 = 3,
            /* SPDRP_SERVICE                     */ Service = 4,
            /* SPDRP_UNUSED1                     */ Unused1 = 5,
            /* SPDRP_UNUSED2                     */ Unused2 = 6,
            /* SPDRP_CLASS                       */ Class = 7,
            /* SPDRP_CLASSGUID                   */ ClassGuid = 8,
            /* SPDRP_DRIVER                      */ Driver = 9,
            /* SPDRP_CONFIGFLAGS                 */ ConfigFlags = 10,
            /* SPDRP_MFG                         */ Mfg = 11,
            /* SPDRP_FRIENDLYNAME                */ FriendlyName = 12,
            /* SPDRP_LOCATION_INFORMATION        */ LocationInformation = 13,
            /* SPDRP_PHYSICAL_DEVICE_OBJECT_NAME */ PhysicalDeviceObjectName = 14,
            /* SPDRP_CAPABILITIES                */ Capabilities = 15,
            /* SPDRP_UI_NUMBER                   */ UiNumber = 16,
            /* SPDRP_UPPERFILTERS                */ UpperFilters = 17,
            /* SPDRP_LOWERFILTERS                */ LowerFilters = 18,
            /* SPDRP_BUSTYPEGUID                 */ BusTypeGuid = 19,
            /* SPDRP_LEGACYBUSTYPE               */ LegacyBusType = 20,
            /* SPDRP_BUSNUMBER                   */ BusNumber = 21,
            /* SPDRP_ENUMERATOR_NAME             */ EnumeratorName = 22,
            /* SPDRP_SECURITY                    */ Security = 23,
            /* SPDRP_SECURITY_SDS                */ SecuritySds = 24,
            /* SPDRP_DEVTYPE                     */ DevType = 25,
            /* SPDRP_EXCLUSIVE                   */ Exclusive = 26,
            /* SPDRP_CHARACTERISTICS             */ Characteristics = 27,
            /* SPDRP_ADDRESS                     */ Address = 28,
            /* SPDRP_UI_NUMBER_DESC_FORMAT       */ UiNumberDescFormat = 29,
            /* SPDRP_DEVICE_POWER_DATA           */ PowerData = 30,
            /* SPDRP_REMOVAL_POLICY              */ RemovalPolicy = 31,
            /* SPDRP_REMOVAL_POLICY_HW_DEFAULT   */ RemovalPolicyHwDefault = 32,
            /* SPDRP_REMOVAL_POLICY_OVERRIDE     */ RemovalPolicyOverride = 33,
            /* SPDRP_INSTALL_STATE               */ InstallState = 34,
            /* SPDRP_LOCATION_PATHS              */ LocationPaths = 35,
            /* SPDRP_BASE_CONTAINERID            */ BaseContainerId = 36
        }

        /**
         * SP_DEVICE_INTERFACE_DATA structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/ns-setupapi-_sp_device_interface_data
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct SetupDeviceInterfaceData
        {
            internal /* DWORD     */ uint cbSize;
            internal /* GUID      */ Guid interfaceClassGuid;
            internal /* DWORD     */ uint flags;
            internal /* ULONG_PTR */ IntPtr reserved;
        }

        /**
         * SP_DEVINFO_DATA structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/ns-setupapi-_sp_devinfo_data
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct SetupDeviceInfoData
        {
            internal /* DWORD     */ uint cbSize;
            internal /* GUID      */ Guid classGuid;
            internal /* DWORD     */ uint devInst;
            internal /* ULONG_PTR */ IntPtr reserved;
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdidestroydeviceinfolist
         */
        [DllImport(Libraries.WindowsSetupapi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool SetupDiDestroyDeviceInfoList(
                /* _In_ HDEVINFO */ [In] IntPtr deviceInfoSet
        );

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdienumdeviceinterfaces
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetclassdevsw
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceinterfacedetailw
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceinterfacedetailw
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceregistrypropertyw
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceregistrypropertyw
         */
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
                /* _Out_opt_ PDWORD           */ [In][Out] ref RegType propertyRegDataType,
                /* _Out_opt_ PBYTE            */ [In][Out] byte[] propertyBuffer,
                /* _In_      DWORD            */ [In] uint propertyBufferSize,
                /* _Out_opt_ PDWORD           */ [In][Out] ref uint requiredSize
        );
    }
}
