using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal const string ComInterfaceDxgiAdapter = "2411e7e1-12ac-4ccf-bd14-9798e8534dc0";
        internal const string ComInterfaceDxgiFactory = "7b7166ec-21c7-44ae-b21a-c9ae321ae369";
        internal const string ComInterfaceDxgiObject = "aec22fb8-76f3-4639-9be0-28eb43a67a2e";
        internal const string ComInterfaceDxgiOutput = "ae02eedb-c735-4690-8d52-5a8dc20213aa";

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceDxgiAdapter)]
        internal interface IDxgiAdapter
        {
            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata
             */
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface
             */
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata
             */
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent
             */
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );

            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiadapter-enumoutputs
             */
            [PreserveSig]
            DxgiError EnumOutputs(
                    /* _In_         UINT          */ [In] uint index,
                    /* _COM_Outptr_ IDXGIOutput** */ [Out] out IDxgiOutput output
            );

            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiadapter-getdesc
             */
            [PreserveSig]
            DxgiError GetDesc(
                    /* _Out_ DXGI_ADAPTER_DESC* */ [Out] out DxgiAdapterDescription desc
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceDxgiFactory)]
        internal interface IDxgiFactory
        {
            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata
             */
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface
             */
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata
             */
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent
             */
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );

            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgifactory-enumadapters
             */
            [PreserveSig]
            DxgiError EnumAdapters(
                    /* _In_         UINT           */ [In] uint index,
                    /* _COM_Outptr_ IDXGIAdapter** */ [Out] out IDxgiAdapter adapter
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceDxgiObject)]
        internal interface IDxgiObject
        {
            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata
             */
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface
             */
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata
             */
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent
             */
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceDxgiOutput)]
        internal interface IDxgiOutput
        {
            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedata
             */
            [PreserveSig]
            DxgiError SetPrivateData(
                    /* _In_             REFGUID     */ [In] ref Guid name,
                    /* _In_             UINT        */ [In] uint dataSize,
                    /* _In_reads_bytes_ const void* */ [In] byte[] data
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-setprivatedatainterface
             */
            [PreserveSig]
            DxgiError SetPrivateDataInterface(
                    /* _In_      REFGUID         */ [In] ref Guid name,
                    /* _In_opt_  const IUnknown* */ [In] object unknown
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getprivatedata
             */
            [PreserveSig]
            DxgiError GetPrivateData(
                    /* _In_               REFGUID */ [In] ref Guid name,
                    /* _Inout_            UINT*   */ [In][Out] ref uint dataSize,
                    /* _Out_writes_bytes_ void*   */ [Out] out byte[] data
            );

            /**
             * From IDxgiObject
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgiobject-getparent
             */
            [PreserveSig]
            DxgiError GetParent(
                    /* _In_         REFGUID */ [In] ref Guid riid,
                    /* _COM_Outptr_ void**  */ [Out] out object unknown
            );

            /**
             * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-idxgioutput-getdesc
             */
            [PreserveSig]
            DxgiError GetDesc(
                    /* _Out_ DXGI_OUTPUT_DESC*  */ [Out] out DxgiOutputDescription desc
            );
        }
    }
}
