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
        internal const string ComInterfaceIBackgroundCopyCallback = "97ea99c7-0186-4ad4-8df9-c5b4e0ed6b22";
        internal const string ComInterfaceIBackgroundCopyError = "19c613a0-fcb8-4f28-81ae-897c3d078f81";
        internal const string ComInterfaceIBackgroundCopyFile = "01b7bd23-fb88-4a77-8490-5891d3e4653a";
        internal const string ComInterfaceIBackgroundCopyJob = "37668d37-507e-4160-9316-26306d150b12";
        internal const string ComInterfaceIBackgroundCopyManager = "5ce34c0d-0dc9-4c1f-897c-daa1b78cee7c";
        internal const string ComInterfaceIDxgiAdapter = "2411e7e1-12ac-4ccf-bd14-9798e8534dc0";
        internal const string ComInterfaceIDxgiFactory = "7b7166ec-21c7-44ae-b21a-c9ae321ae369";
        internal const string ComInterfaceIDxgiObject = "aec22fb8-76f3-4639-9be0-28eb43a67a2e";
        internal const string ComInterfaceIDxgiOutput = "ae02eedb-c735-4690-8d52-5a8dc20213aa";
        internal const string ComInterfaceIEnumBackgroundCopyFiles = "ca51e165-c365-424c-8d41-24aaa4ff3c40";
        internal const string ComInterfaceIEnumBackgroundCopyJobs = "1af4f612-3b71-466f-8f58-7b6f73ac57ad";
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
        [Guid(ComInterfaceIBackgroundCopyCallback)]
        internal interface IBackgroundCopyCallback
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopycallback-jobtransferred")]
            [PreserveSig]
            BitsResult JobTransferred(
                    /* __RPC__in_opt IBackgroundCopyJob* */ [In][MarshalAs(UnmanagedType.Interface)] IBackgroundCopyJob pJob
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopycallback-joberror")]
            [PreserveSig]
            BitsResult JobError(
                    /* __RPC__in_opt IBackgroundCopyJob*   */ [In][MarshalAs(UnmanagedType.Interface)] IBackgroundCopyJob pJob,
                    /* __RPC__in_opt IBackgroundCopyError* */ [In][MarshalAs(UnmanagedType.Interface)] IBackgroundCopyError pError
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopycallback-jobmodification")]
            [PreserveSig]
            BitsResult JobModification(
                    /* __RPC__in_opt IBackgroundCopyJob* */ [In][MarshalAs(UnmanagedType.Interface)] IBackgroundCopyJob pJob,
                    /*               DWORD               */ [In] uint dwReserved
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIBackgroundCopyError)]
        internal interface IBackgroundCopyError
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyerror-geterror")]
            [PreserveSig]
            BitsResult GetError(
                    /* __RPC__out BG_ERROR_CONTEXT* */ [Out] out BitsErrorContext pContext,
                    /* __RPC__out HRESULT*          */ [Out] out HResult pCode
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyerror-getfile")]
            [PreserveSig]
            BitsResult GetFile(
                    /* __RPC__deref_out_opt IBackgroundCopyFile** */ [Out] out IBackgroundCopyFile pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyerror-geterrordescription")]
            [PreserveSig]
            BitsResult GetErrorDescription(
                    /*                      DWORD   */ [In] uint languageId,
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pErrorDescription
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyerror-geterrorcontextdescription")]
            [PreserveSig]
            BitsResult GetErrorContextDescription(
                    /*                      DWORD   */ [In] uint languageId,
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pContextDescription
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyerror-getprotocol")]
            [PreserveSig]
            BitsResult GetProtocol(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pProtocol
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIBackgroundCopyFile)]
        internal interface IBackgroundCopyFile
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyfile-getremotename")]
            [PreserveSig]
            BitsResult GetRemoteName(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyfile-getlocalname")]
            [PreserveSig]
            BitsResult GetLocalName(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyfile-getprogress")]
            [PreserveSig]
            BitsResult GetProgress(
                    /* __RPC__out BG_FILE_PROGRESS* */ [Out] out BitsFileProgress pVal
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIBackgroundCopyJob)]
        internal interface IBackgroundCopyJob
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-addfileset")]
            [PreserveSig]
            BitsResult AddFileSet(
                    /*                                   ULONG         */ [In] int cFileCount,
                    /* __RPC__in_ecount_full(cFileCount) BG_FILE_INFO* */ [In][MarshalAs(UnmanagedType.LPArray)] BitsFileInfo[] pFileSet
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-addfile")]
            [PreserveSig]
            BitsResult AddFile(
                    /* __RPC__in LPCWSTR */ [In] string remoteUrl,
                    /* __RPC__in LPCWSTR */ [In] string localName
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-enumfiles")]
            [PreserveSig]
            BitsResult EnumFiles(
                    /* __RPC__deref_out_opt IEnumBackgroundCopyFiles ** */ [Out] out IEnumBackgroundCopyFiles pEnum
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-suspend")]
            [PreserveSig]
            BitsResult Suspend();

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-resume")]
            [PreserveSig]
            BitsResult Resume();

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-cancel")]
            [PreserveSig]
            BitsResult Cancel();

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-complete")]
            [PreserveSig]
            BitsResult Complete();

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getid")]
            [PreserveSig]
            BitsResult GetId(
                    /* __RPC__out GUID* */ [Out] out Guid pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-gettype")]
            [PreserveSig]
            BitsResult GetType(
                    /* __RPC__out BG_JOB_TYPE* */ [Out] out BitsJobType pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getprogress")]
            [PreserveSig]
            BitsResult GetProgress(
                    /* __RPC__out BG_JOB_PROGRESS* */ [Out] out BitsJobProgress pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-gettimes")]
            [PreserveSig]
            BitsResult GetTimes(
                    /* __RPC__out BG_JOB_TIMES* */ [Out] out BitsJobTimes pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getstate")]
            [PreserveSig]
            BitsResult GetState(
                    /* __RPC__out BG_JOB_STATE* */ [Out] out BitsJobState pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-geterror")]
            [PreserveSig]
            BitsResult GetError(
                    /* __RPC__deref_out_opt IBackgroundCopyError** */ [Out] out IBackgroundCopyError ppError
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getowner")]
            [PreserveSig]
            BitsResult GetOwner(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setdisplayname")]
            [PreserveSig]
            BitsResult SetDisplayName(
                    /* __RPC__in LPCWSTR */ [In] string val
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getdisplayname")]
            [PreserveSig]
            BitsResult GetDisplayName(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setdescription")]
            [PreserveSig]
            BitsResult SetDescription(
                    /* __RPC__in LPCWSTR */ [In] string val
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getdescription")]
            [PreserveSig]
            BitsResult GetDescription(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setpriority")]
            [PreserveSig]
            BitsResult SetPriority(
                    /* BG_JOB_PRIORITY */ [In] BitsJobPriority val
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getpriority")]
            [PreserveSig]
            BitsResult GetPriority(
                    /* __RPC__out BG_JOB_PRIORITY* */ [Out] out BitsJobPriority pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setnotifyflags")]
            [PreserveSig]
            BitsResult SetNotifyFlags(
                    /* ULONG */ [In] BitsNotifyFlag val
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getnotifyflags")]
            [PreserveSig]
            BitsResult GetNotifyFlags(
                    /* __RPC__out ULONG* */ [Out] out BitsNotifyFlag pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setnotifyinterface")]
            [PreserveSig]
            BitsResult SetNotifyInterface(
                    /* __RPC__in_opt IUnknown* */ [In][MarshalAs(UnmanagedType.IUnknown)] object val
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getnotifyinterface")]
            [PreserveSig]
            BitsResult GetNotifyInterface(
                    /* __RPC__deref_out_opt IUnknown** */ [Out][MarshalAs(UnmanagedType.IUnknown)] out object pVal
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setminimumretrydelay")]
            [PreserveSig]
            BitsResult SetMinimumRetryDelay(
                    /* ULONG */ [In] uint seconds
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getminimumretrydelay")]
            [PreserveSig]
            BitsResult GetMinimumRetryDelay(
                    /* __RPC__out ULONG* */ [Out] out uint seconds
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setnoprogresstimeout")]
            [PreserveSig]
            BitsResult SetNoProgressTimeout(
                    /* ULONG */ [In] uint seconds
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getnoprogresstimeout")]
            [PreserveSig]
            BitsResult GetNoProgressTimeout(
                    /* __RPC__out ULONG* */ [Out] out uint seconds
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-geterrorcount")]
            [PreserveSig]
            BitsResult GetErrorCount(
                    /* __RPC__out ULONG* */ [Out] out uint errors
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setproxysettings")]
            [PreserveSig]
            BitsResult SetProxySettings(
                    /*                      BG_JOB_PROXY_USAGE */ [In] BitsJobProxyUsage proxyUsage,
                    /* __RPC__in_opt_string const WCHAR*       */ [In] string proxyList,
                    /* __RPC__in_opt_string const WCHAR*       */ [In] string proxyBypassList
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getproxysettings")]
            [PreserveSig]
            BitsResult GetProxySettings(
                    /* __RPC__out           BG_JOB_PROXY_USAGE* */ [Out] out BitsJobProxyUsage pProxyUsage,
                    /* __RPC__deref_out_opt LPWSTR*             */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pProxyList,
                    /* __RPC__deref_out_opt LPWSTR*             */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pProxyBypassList
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-takeownership")]
            [PreserveSig]
            BitsResult TakeOwnership();
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIBackgroundCopyManager)]
        internal interface IBackgroundCopyManager
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopymanager-createjob")]
            [PreserveSig]
            BitsResult CreateJob(
                    /* __RPC__in            LPCWSTR              */ [In] string displayName,
                    /*                      BG_JOB_TYPE          */ [In] BitsJobType type,
                    /* __RPC__out           GUID*                */ [Out] out Guid pJobId,
                    /* __RPC__deref_out_opt IBackgroundCopyJob** */ [Out] out IBackgroundCopyJob ppJob
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopymanager-getjob")]
            [PreserveSig]
            BitsResult GetJob(
                    /* __RPC__in            REFGUID              */ [In] ref Guid jobId,
                    /* __RPC__deref_out_opt IBackgroundCopyJob** */ [Out] out IBackgroundCopyJob ppJob
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopymanager-enumjobs")]
            [PreserveSig]
            BitsResult EnumJobs(
                    /*                      DWORD                     */ [In] BitsJobEnumOwnerScope dwFlags,
                    /* __RPC__deref_out_opt IEnumBackgroundCopyJobs** */ [Out] out IEnumBackgroundCopyJobs ppEnum
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopymanager-geterrordescription")]
            [PreserveSig]
            HResult GetErrorDescription(
                    /*                      HRESULT */ [In] BitsResult hResult,
                    /*                      DWORD   */ [In] uint languageId,
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string errorDescription
            );
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
        [Guid(ComInterfaceIEnumBackgroundCopyFiles)]
        internal interface IEnumBackgroundCopyFiles
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyfiles-next")]
            [PreserveSig]
            BitsResult Next(
                    /*                        ULONG                 */ [In] uint celt, // only allow 1 item at once
                    /* __RPC__out_ecount_part IBackgroundCopyFile** */ [Out] out IBackgroundCopyFile rgelt,
                    /* __RPC__inout_opt       ULONG*                */ [Out] out uint pceltFetched
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyfiles-skip")]
            [PreserveSig]
            BitsResult Skip(
                    /* ULONG */ [In] uint celt
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyfiles-reset")]
            [PreserveSig]
            BitsResult Reset();

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyfiles-clone")]
            [PreserveSig]
            BitsResult Clone(
                    /* __RPC__deref_out_opt IEnumBackgroundCopyFiles** */ [Out] out IEnumBackgroundCopyFiles ppenum
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyfiles-getcount")]
            [PreserveSig]
            BitsResult GetCount(
                    /* __RPC__out ULONG* */ [Out] out uint puCount
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceIEnumBackgroundCopyJobs)]
        internal interface IEnumBackgroundCopyJobs
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyjobs-next")]
            [PreserveSig]
            BitsResult Next(
                    /*                        ULONG                */ [In] uint celt, // only allow 1 item at once
                    /* __RPC__out_ecount_part IBackgroundCopyJob** */ [Out] out IBackgroundCopyJob rgelt,
                    /* __RPC__inout_opt       ULONG*               */ [Out] out uint pceltFetched
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyjobs-skip")]
            [PreserveSig]
            BitsResult Skip(
                    /* ULONG */ [In] uint celt
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyjobs-reset")]
            [PreserveSig]
            BitsResult Reset();

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyjobs-clone")]
            [PreserveSig]
            BitsResult Clone(
                    /* __RPC__deref_out_opt IEnumBackgroundCopyJobs** */ [Out] out IEnumBackgroundCopyJobs ppenum
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ienumbackgroundcopyjobs-getcount")]
            [PreserveSig]
            BitsResult GetCount(
                    /* __RPC__out ULONG* */ [Out] out uint puCount
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
