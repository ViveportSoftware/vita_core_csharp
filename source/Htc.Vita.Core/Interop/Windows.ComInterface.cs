using System;
using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class ComInterfaceId
        {
            internal const string IBackgroundCopyCallback  = "97ea99c7-0186-4ad4-8df9-c5b4e0ed6b22";
            internal const string IBackgroundCopyError     = "19c613a0-fcb8-4f28-81ae-897c3d078f81";
            internal const string IBackgroundCopyFile      = "01b7bd23-fb88-4a77-8490-5891d3e4653a";
            internal const string IBackgroundCopyJob       = "37668d37-507e-4160-9316-26306d150b12";
            internal const string IBackgroundCopyJob2      = "54b50739-686f-45eb-9dff-d6a9a0faa9af";
            internal const string IBackgroundCopyManager   = "5ce34c0d-0dc9-4c1f-897c-daa1b78cee7c";
            internal const string ICivicAddressReport      = "c0b19f70-4adf-445d-87f2-cad8fd711792";
            internal const string IDxgiAdapter             = "2411e7e1-12ac-4ccf-bd14-9798e8534dc0";
            internal const string IDxgiFactory             = "7b7166ec-21c7-44ae-b21a-c9ae321ae369";
            internal const string IDxgiObject              = "aec22fb8-76f3-4639-9be0-28eb43a67a2e";
            internal const string IDxgiOutput              = "ae02eedb-c735-4690-8d52-5a8dc20213aa";
            internal const string IEnumBackgroundCopyFiles = "ca51e165-c365-424c-8d41-24aaa4ff3c40";
            internal const string IEnumBackgroundCopyJobs  = "1af4f612-3b71-466f-8f58-7b6f73ac57ad";
            internal const string ILatLongReport           = "7fed806d-0ef8-4f07-80ac-36a0beae3134";
            internal const string ILocation                = "ab2ece69-56d9-4f28-b525-de1b0ee44237";
            internal const string ILocationEvents          = "cae02bbf-798b-4508-a207-35a7906dc73d";
            internal const string ILocationReport          = "c8b7f7ee-75d0-4dB9-b62d-7a0f369ca456";
            internal const string IPortableDeviceManager   = "a1567595-4c2f-4574-a6fa-ecef917b9a40";
            internal const string IPropertyStore           = "886d8eeb-8cf2-4446-8d02-cdba1dbdcf99";
            internal const string IShellLinkW              = "000214f9-0000-0000-c000-000000000046";
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceId.IBackgroundCopyCallback)]
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
        [Guid(ComInterfaceId.IBackgroundCopyError)]
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
        [Guid(ComInterfaceId.IBackgroundCopyFile)]
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
        [Guid(ComInterfaceId.IBackgroundCopyJob2)]
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
                    /* __RPC__deref_out_opt IEnumBackgroundCopyFiles** */ [Out] out IEnumBackgroundCopyFiles pEnum
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
                    /* ULONG */ [In] BitsNotifyFlags val
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-getnotifyflags")]
            [PreserveSig]
            BitsResult GetNotifyFlags(
                    /* __RPC__out ULONG* */ [Out] out BitsNotifyFlags pVal
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

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-setnotifycmdline",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult SetNotifyCmdLine(
                    /* __RPC__in_opt LPCWSTR */ [In] string program,
                    /* __RPC__in_opt LPCWSTR */ [In] string parameters
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-getnotifycmdline",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult GetNotifyCmdLine(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pProgram,
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pParameters
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-getreplyprogress",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult GetReplyProgress(
                    /* __RPC__inout BG_JOB_REPLY_PROGRESS* */ [Out] out BitsJobReplyProgress pProgress
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-getreplydata",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult GetReplyData(
                    /* __RPC__deref_out_ecount_full_opt byte**  */ [In][Out] ref IntPtr ppBuffer,
                    /* __RPC__inout_opt                 UINT64* */ [Out] out ulong pLength
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-setreplyfilename",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult SetReplyFileName(
                    /* __RPC__in_opt LPCWSTR */ [In] string replyFileName
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-getreplyfilename",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult GetReplyFileName(
                    /* __RPC__deref_out_opt LPWSTR* */ [Out][MarshalAs(UnmanagedType.LPWStr)] out string pReplyFileName
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-setcredentials",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult SetCredentials(
                    /* __RPC__in BG_AUTH_CREDENTIALS* */ [In] ref BitsAuthCredentials credentials
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/nf-bits1_5-ibackgroundcopyjob2-removecredentials",
                    Description = "From IBackgroundCopyJob2")]
            [PreserveSig]
            BitsResult RemoveCredentials(
                    /* BG_AUTH_TARGET */ [In] BitsAuthTarget target,
                    /* BG_AUTH_SCHEME */ [In] BitsAuthScheme scheme
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceId.IBackgroundCopyManager)]
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
        [Guid(ComInterfaceId.IDxgiAdapter)]
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
        [Guid(ComInterfaceId.IDxgiFactory)]
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
        [Guid(ComInterfaceId.IDxgiObject)]
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
        [Guid(ComInterfaceId.IDxgiOutput)]
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
        [Guid(ComInterfaceId.IEnumBackgroundCopyFiles)]
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
        [Guid(ComInterfaceId.IEnumBackgroundCopyJobs)]
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
        [Guid(ComInterfaceId.ILocation)]
        internal interface ILocation
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-registerforreport",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult RegisterForReport(
                    /* __RPC__in_opt ILocationEvents* */ [In][MarshalAs(UnmanagedType.Interface)] ILocationEvents pEvents,
                    /* __RPC__in     REFIID           */ [In] ref Guid reportType,
                    /*               DWORD            */ [In] uint dwRequestedReportInterval
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-unregisterforreport",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult UnregisterForReport(
                    /* __RPC__in REFIID */ [In] ref Guid reportType
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-getreport",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult GetReport(
                    /* __RPC__in            REFIID            */ [In] ref Guid reportType,
                    /* __RPC__deref_out_opt ILocationReport** */ [Out][MarshalAs(UnmanagedType.Interface)] out ILocationReport ppLocationReport
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-getreportstatus",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult GetReportStatus(
                    /* __RPC__in  REFIID                       */ [In] ref Guid reportType,
                    /* __RPC__out enum LOCATION_REPORT_STATUS* */ [Out] out LocationReportStatus pStatus
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-getreportinterval",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult GetReportInterval(
                    /* __RPC__in  REFIID */ [In] ref Guid reportType,
                    /* __RPC__out DWORD* */ [Out] out uint pMilliseconds
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-setreportinterval",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult SetReportInterval(
                    /* __RPC__in REFIID */ [In] ref Guid reportType,
                    /*           DWORD  */ [In] uint millisecondsRequested
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-getdesiredaccuracy",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult GetDesiredAccuracy(
                    /* __RPC__in  REFIID                          */ [In] ref Guid reportType,
                    /* __RPC__out enum LOCATION_DESIRED_ACCURACY* */ [Out] LocationDesiredAccuracy pDesiredAccuracy
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-setdesiredaccuracy",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult SetDesiredAccuracy(
                    /* __RPC__in REFIID                         */ [In] ref Guid reportType,
                    /*           enum LOCATION_DESIRED_ACCURACY */ [In] LocationDesiredAccuracy desiredAccuracy
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocation-requestpermissions",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult RequestPermissions(
                    /* __RPC__in_opt         HWND  */ [In] IntPtr hParent,
                    /* __RPC__in_ecount_full IID*  */ [In] ref Guid pReportTypes,
                    /*                       ULONG */ [In] uint count,
                    /*                       BOOL  */ [In] bool fModal
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceId.ILocationEvents)]
        internal interface ILocationEvents
        {
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceId.ILocationReport)]
        internal interface ILocationReport
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocationreport-getsensorid",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult GetSensorID(
                    /* __RPC__out SENSOR_ID* */ [Out] out Guid pSensorID
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocationreport-gettimestamp",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult GetTimestamp(
                    /* __RPC__out SYSTEMTIME* */ [Out] out SystemTime pCreationTime
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/locationapi/nf-locationapi-ilocationreport-getvalue",
                    File = Headers.WindowsLocationapi)]
            [PreserveSig]
            HResult GetValue(
                    /* __RPC__in  REFPROPERTYKEY */ [In] ref PropertyKey pKey,
                    /* __RPC__out PROPVARIANT*   */ [In][Out] PropVariant pValue
            );
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceId.IPortableDeviceManager)]
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

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceId.IPropertyStore)]
        internal interface IPropertyStore
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/propsys/nf-propsys-ipropertystore-getcount")]
            [PreserveSig]
            HResult GetCount(
                    /* __RPC__out DWORD* */ [Out] out uint cProps
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/propsys/nf-propsys-ipropertystore-getat")]
            [PreserveSig]
            HResult GetAt(
                    /*            DWORD        */ [In] uint iProp,
                    /* __RPC__out PROPERTYKEY* */ [Out] out PropertyKey pKey
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/propsys/nf-propsys-ipropertystore-getvalue")]
            [PreserveSig]
            HResult GetValue(
                    /* __RPC__in  REFPROPERTYKEY */ [In] ref PropertyKey key,
                    /* __RPC__out PROPVARIANT*   */ [Out] out PropVariant pv
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/propsys/nf-propsys-ipropertystore-setvalue")]
            [PreserveSig]
            HResult SetValue(
                    /* __RPC__in REFPROPERTYKEY */ [In] ref PropertyKey key,
                    /* __RPC__in REFPROPVARIANT */ [In] ref PropVariant propVar
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/propsys/nf-propsys-ipropertystore-commit")]
            [PreserveSig]
            HResult Commit();
        }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid(ComInterfaceId.IShellLinkW)]
        internal interface IShellLinkW
        {
            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-getpath")]
            [PreserveSig]
            HResult GetPath(
                    /* __RPC__out_ecount_full_string LPWSTR            */ [Out][MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile,
                    /*                               int               */ [In] int cch,
                    /* __RPC__inout_opt              WIN32_FIND_DATAW* */ [Out] Win32FindData pfd,
                    /*                               DWORD             */ [In] uint fFlags
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-getidlist")]
            [PreserveSig]
            HResult GetIDList(
                    /* __RPC__deref_out_opt PIDLIST_ABSOLUTE* */ [Out] out IntPtr ppidl
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-setidlist")]
            [PreserveSig]
            HResult SetIDList(
                    /* __RPC__in PCIDLIST_ABSOLUTE */ [In] IntPtr pidl
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-getdescription")]
            [PreserveSig]
            HResult GetDescription(
                    /* __RPC__out_ecount_full_string LPSTR */ [Out][MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName,
                    /*                               int   */ [In] int cch
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-setdescription")]
            [PreserveSig]
            HResult SetDescription(
                    /* __RPC__in_string LPCSTR */ [In] string pszName
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-getworkingdirectory")]
            [PreserveSig]
            HResult GetWorkingDirectory(
                    /* __RPC__out_ecount_full_string LPSTR */ [Out][MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir,
                    /*                               int   */ [In] int cch
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-setworkingdirectory")]
            [PreserveSig]
            HResult SetWorkingDirectory(
                    /* __RPC__in_string LPCSTR */ [In] string pszDir
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-getarguments")]
            [PreserveSig]
            HResult GetArguments(
                    /* __RPC__out_ecount_full_string LPSTR */ [Out][MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs,
                    /*                               int   */ [In] int cch
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-setarguments")]
            [PreserveSig]
            HResult SetArguments(
                    /* __RPC__in_string LPCSTR */ [In] string pszArgs
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-gethotkey")]
            [PreserveSig]
            HResult GetHotkey(
                    /* __RPC__out WORD* */ [Out] out ushort pwHotkey
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-sethotkey")]
            [PreserveSig]
            HResult SetHotkey(
                    /* WORD */ [In] ushort wHotkey
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-getshowcmd")]
            [PreserveSig]
            HResult GetShowCmd(
                    /* __RPC__out int* */ [Out] out ShowWindowCommand piShowCmd
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-setshowcmd")]
            [PreserveSig]
            HResult SetShowCmd(
                    /* int */ [In] ShowWindowCommand iShowCmd
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-geticonlocation")]
            [PreserveSig]
            HResult GetIconLocation(
                    /* __RPC__out_ecount_full_string LPWSTR */ [Out][MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath,
                    /*                               int    */ [In] int cch,
                    /* __RPC__out                    int*   */ [Out] int piIcon
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-seticonlocation")]
            [PreserveSig]
            HResult SetIconLocation(
                    /* __RPC__in_string LPCWSTR */ [In] string pszIconPath,
                    /*                  int     */ [In] int iIcon
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-setrelativepath")]
            [PreserveSig]
            HResult SetRelativePath(
                    /* __RPC__in_string LPCWSTR */ [In] string pszPathRel,
                    /*                  DWORD   */ [In] uint dwReserved
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-resolve")]
            [PreserveSig]
            HResult Resolve(
                    /* __RPC__in_opt HWND  */ [In] IntPtr hwnd,
                    /*               DWORD */ [In] uint fFlags
            );

            [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/shobjidl_core/nf-shobjidl_core-ishelllinkw-setpath")]
            [PreserveSig]
            HResult SetPath(
                    /* __RPC__in_string LPCWSTR */ [In] string pszFile
            );
        }
    }
}
