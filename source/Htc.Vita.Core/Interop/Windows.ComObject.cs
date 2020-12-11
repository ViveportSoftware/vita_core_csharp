using System;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal const string ComInterfaceClsidBackgroundCopyManager = "4991d34b-80a1-4291-83b6-3328366b9097";
        internal const string ComInterfaceClsidPortableDeviceManager = "0af10cec-2ecd-4b92-9581-34f6ae0637f3";
        internal const string ComInterfaceIBackgroundCopyManager = "5ce34c0d-0dc9-4c1f-897c-daa1b78cee7c";
        internal const string ComInterfaceIDxgiAdapter = "2411e7e1-12ac-4ccf-bd14-9798e8534dc0";
        internal const string ComInterfaceIDxgiFactory = "7b7166ec-21c7-44ae-b21a-c9ae321ae369";
        internal const string ComInterfaceIDxgiObject = "aec22fb8-76f3-4639-9be0-28eb43a67a2e";
        internal const string ComInterfaceIDxgiOutput = "ae02eedb-c735-4690-8d52-5a8dc20213aa";
        internal const string ComInterfaceIPortableDeviceManager = "a1567595-4c2f-4574-a6fa-ecef917b9a40";

        [ComImport]
        [Guid(ComInterfaceClsidBackgroundCopyManager)]
        internal class ClsidBackgroundCopyManager
        {
        }

        [ComImport]
        [Guid(ComInterfaceClsidPortableDeviceManager)]
        internal class ClsidPortableDeviceManager
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIBackgroundCopyManager)]
        internal interface IBackgroundCopyManager
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIDxgiAdapter)]
        internal interface IDxgiAdapter
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiadapter-enumoutputs")]
            [PreserveSig]
            DxgiError EnumOutputs(
                    /* _In_         UINT          */ [In] uint index,
                    /* _COM_Outptr_ IDXGIOutput** */ [Out] out IDxgiOutput output
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiadapter-getdesc")]
            [PreserveSig]
            DxgiError GetDesc(
                    /* _Out_ DXGI_ADAPTER_DESC* */ [Out] out DxgiAdapterDescription desc
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIDxgiFactory)]
        internal interface IDxgiFactory
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgifactory-enumadapters")]
            [PreserveSig]
            DxgiError EnumAdapters(
                    /* _In_         UINT           */ [In] uint index,
                    /* _COM_Outptr_ IDXGIAdapter** */ [Out] out IDxgiAdapter adapter
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIDxgiObject)]
        internal interface IDxgiObject
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata")]
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface")]
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata")]
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent")]
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIDxgiOutput)]
        internal interface IDxgiOutput
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent",
                    Description = "From IDxgiObject")]
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgioutput-getdesc")]
            [PreserveSig]
            DxgiError GetDesc(
                    /* _Out_ DXGI_OUTPUT_DESC*  */ [Out] out DxgiOutputDescription desc
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIPortableDeviceManager)]
        internal interface IPortableDeviceManager
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/portabledeviceapi/nf-portabledeviceapi-iportabledevicemanager-getdevices")]
            [PreserveSig]
            HResult GetDevices(
                    /* __RPC__deref_opt_inout_opt LPWSTR* */ [In][MarshalAs(UnmanagedType.LPArray)] IntPtr[] pPnPDeviceIDs,
                    /* __RPC__inout               DWORD*  */ [In][Out] ref uint pcPnPDeviceIDs
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/portabledeviceapi/nf-portabledeviceapi-iportabledevicemanager-refreshdevicelist")]
            [PreserveSig]
            HResult RefreshDeviceList();

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/portabledeviceapi/nf-portabledeviceapi-iportabledevicemanager-getdevicefriendlyname")]
            [PreserveSig]
            HResult GetDeviceFriendlyName(
                    /* __RPC__in        LPCWSTR */ [In] string pszPnPDeviceId,
                    /* __RPC__inout_opt WCHAR*  */ [In][Out] StringBuilder pDeviceFriendlyName,
                    /* __RPC__inout     DWORD*  */ [In][Out] ref uint pcchDeviceFriendlyName
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/portabledeviceapi/nf-portabledeviceapi-iportabledevicemanager-getdevicedescription")]
            [PreserveSig]
            HResult GetDeviceDescription(
                    /* __RPC__in        LPCWSTR */ [In] string pszPnPDeviceId,
                    /* __RPC__inout_opt WCHAR*  */ [In][Out] StringBuilder pDeviceDescription,
                    /* __RPC__inout     DWORD*  */ [In][Out] ref uint pcchDeviceDescription
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/portabledeviceapi/nf-portabledeviceapi-iportabledevicemanager-getdevicemanufacturer")]
            [PreserveSig]
            HResult GetDeviceManufacturer(
                    /* __RPC__in        LPCWSTR */ [In] string pszPnPDeviceId,
                    /* __RPC__inout_opt WCHAR*  */ [In][Out] StringBuilder pDeviceManufacturer,
                    /* __RPC__inout     DWORD*  */ [In][Out] ref uint pcchDeviceManufacturer
            );
        }
    }
}
