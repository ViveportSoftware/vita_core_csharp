using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal const int /* MAX_PATH */ MaxPath = 260;

        internal static readonly Guid /* GUID_DEVCLASS_USB                    */ DeviceClassUsb = new Guid("{36FC9E60-C465-11CF-8056-444553540000}");
        internal static readonly Guid /* GUID_DEVINTERFACE_HID                */ DeviceInterfaceHid = new Guid("{4D1E55B2-F16F-11CF-88CB-001111000030}");
        internal static readonly Guid /* GUID_DEVINTERFACE_USB_DEVICE         */ DeviceInterfaceUsbDevice = new Guid("{A5DCBF10-6530-11D2-901F-00C04FB951ED}");
        internal static readonly Guid /* GUID_DEVINTERFACE_USB_HUB            */ DeviceInterfaceUsbHub = new Guid("{F18A0E88-C30C-11D0-8815-00A0C906BED8}");
        internal static readonly Guid /* DRIVER_ACTION_VERIFY                 */ DriverActionVerify = new Guid("{F750E6C3-38EE-11d1-85E5-00C04FC295EE}");
        internal static readonly Guid /* HTTPSPROV_ACTION                     */ HttpsProvAction = new Guid("{573E31F8-AABA-11d0-8CCB-00C04FC295EE}");
        internal static readonly Guid /* OFFICESIGN_ACTION_VERIFY             */ OfficeSignActionVerify = new Guid("{5555C2CD-17FB-11d1-85C4-00C04FC295EE}");
        internal static readonly Guid /* WINTRUST_ACTION_GENERIC_CHAIN_VERIFY */ WinTrustActionGenericChainVerify = new Guid("{FC451C16-AC75-11D1-B4B8-00C04FB66EA0}");
        internal static readonly Guid /* WINTRUST_ACTION_GENERIC_VERIFY_V2    */ WinTrustActionGenericVerifyV2 = new Guid("{00AAC56B-CD44-11d0-8CC2-00C04FC295EE}");
        internal static readonly Guid /* WINTRUST_ACTION_TRUSTPROVIDER_TEST   */ WinTrustActionTrustProviderTest = new Guid("{573E31F8-DDBA-11d0-8CCB-00C04FC295EE}");

        internal static readonly IntPtr /* INVALID_HANDLE_VALUE      */ InvalidHandleValue = new IntPtr(-1);
        internal static readonly IntPtr /* WTS_CURRENT_SERVER_HANDLE */ WindowsTerminalServiceCurrentServerHandle = IntPtr.Zero;

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/ne-bits1_5-bg_auth_scheme",
                Description = "BG_AUTH_SCHEME enumeration")]
        internal enum BitsAuthScheme
        {
            /* BG_AUTH_SCHEME_BASIC     */ Basic     = 1,
            /* BG_AUTH_SCHEME_DIGEST    */ Digest    = 2,
            /* BG_AUTH_SCHEME_NTLM      */ Ntlm      = 3,
            /* BG_AUTH_SCHEME_NEGOTIATE */ Negotiate = 4,
            /* BG_AUTH_SCHEME_PASSPORT  */ Passport  = 5
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/ne-bits1_5-bg_auth_target",
                Description = "BG_AUTH_TARGET enumeration")]
        internal enum BitsAuthTarget
        {
            /* BG_AUTH_TARGET_SERVER */ Server = 1,
            /* BG_AUTH_TARGET_PROXY  */ Proxy  = 2
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ne-bits-bg_error_context",
                Description = "BG_ERROR_CONTEXT enumeration")]
        internal enum BitsErrorContext
        {
            /* BG_ERROR_CONTEXT_NONE                        */ None                      = 0,
            /* BG_ERROR_CONTEXT_UNKNOWN                     */ Unknown                   = 1,
            /* BG_ERROR_CONTEXT_GENERAL_QUEUE_MANAGER       */ GeneralQueueManager       = 2,
            /* BG_ERROR_CONTEXT_QUEUE_MANAGER_NOTIFICATION  */ QueueManagerNotification  = 3,
            /* BG_ERROR_CONTEXT_LOCAL_FILE                  */ LocalFile                 = 4,
            /* BG_ERROR_CONTEXT_REMOTE_FILE                 */ RemoteFile                = 5,
            /* BG_ERROR_CONTEXT_GENERAL_TRANSPORT           */ GeneralTransport          = 6,
            /* BG_ERROR_CONTEXT_REMOTE_APPLICATION          */ RemoteApplication         = 7,
            /* BG_ERROR_CONTEXT_SERVER_CERTIFICATE_CALLBACK */ ServerCertificateCallback = 8
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopymanager-enumjobs")]
        internal enum BitsJobEnumOwnerScope
        {
            /*                       */ CurrentUser = 0,
            /* BG_JOB_ENUM_ALL_USERS */ AllUsers    = 1
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ne-bits-bg_job_priority",
                Description = "BG_JOB_PRIORITY enumeration")]
        internal enum BitsJobPriority
        {
            /* BG_JOB_PRIORITY_FOREGROUND */ Foreground = 0,
            /* BG_JOB_PRIORITY_HIGH       */ High       = 1,
            /* BG_JOB_PRIORITY_NORMAL     */ Normal     = 2,
            /* BG_JOB_PRIORITY_LOW        */ Low        = 3
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ne-bits-bg_job_proxy_usage",
                Description = "BG_JOB_PROXY_USAGE enumeration")]
        internal enum BitsJobProxyUsage
        {
            /* BG_JOB_PROXY_USAGE_PRECONFIG  */ Preconfig  = 0,
            /* BG_JOB_PROXY_USAGE_NO_PROXY   */ NoProxy    = 1,
            /* BG_JOB_PROXY_USAGE_OVERRIDE   */ Override   = 2,
            /* BG_JOB_PROXY_USAGE_AUTODETECT */ Autodetect = 3
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ne-bits-bg_job_state",
                Description = "BG_JOB_STATE enumeration")]
        internal enum BitsJobState
        {
            /* BG_JOB_STATE_QUEUED          */ Queued         = 0,
            /* BG_JOB_STATE_CONNECTING      */ Connecting     = 1,
            /* BG_JOB_STATE_TRANSFERRING    */ Transferring   = 2,
            /* BG_JOB_STATE_SUSPENDED       */ Suspended      = 3,
            /* BG_JOB_STATE_ERROR           */ Error          = 4,
            /* BG_JOB_STATE_TRANSIENT_ERROR */ TransientError = 5,
            /* BG_JOB_STATE_TRANSFERRED     */ Transferred    = 6,
            /* BG_JOB_STATE_ACKNOWLEDGED    */ Acknowledged   = 7,
            /* BG_JOB_STATE_CANCELLED       */ Cancelled      = 8
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ne-bits-bg_job_type",
                Description = "BG_JOB_TYPE enumeration")]
        internal enum BitsJobType
        {
            /* BG_JOB_TYPE_DOWNLOAD     */ Download    = 0,
            /* BG_JOB_TYPE_UPLOAD       */ Upload      = 1,
            /* BG_JOB_TYPE_UPLOAD_REPLY */ UploadReply = 2
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/nf-bits-ibackgroundcopyjob-setnotifyflags")]
        [Flags]
        internal enum BitsNotifyFlags : uint
        {
            /*                                   */ None                  =      0,
            /* BG_NOTIFY_JOB_TRANSFERRED         */ JobTransferred        = 0x0001,
            /* BG_NOTIFY_JOB_ERROR               */ JobError              = 0x0002,
            /* BG_NOTIFY_DISABLE                 */ Disable               = 0x0004,
            /* BG_NOTIFY_JOB_MODIFICATION        */ JobModification       = 0x0008,
            /* BG_NOTIFY_FILE_TRANSFERRED        */ FileTransferred       = 0x0010,
            /* BG_NOTIFY_FILE_RANGES_TRANSFERRED */ FileRangesTransferred = 0x0020
        }

        internal enum BitsResult : uint
        {
            /* S_OK                                              */ SOk                                   = HResult.SOk,
            /* S_FALSE                                           */ SFalse                                = HResult.SFalse,
            /* BG_S_ERROR_CONTEXT_NONE                           */ SErrorContextNone                     = 0x00200006,
            /* BG_S_PARTIAL_COMPLETE                             */ SPartialComplete                      = 0x00200017,
            /* BG_S_UNABLE_TO_DELETE_FILES                       */ SUnableToDeleteFiles                  = 0x0020001a,
            /* E_NOTIMPL                                         */ ENotImpl                              = HResult.ENotImpl,
            /* E_ACCESSDENIED                                    */ EAccessDenied                         = HResult.EAccessDenied,
            /* E_OUTOFMEMORY                                     */ EOutOfMemory                          = HResult.EOutOfMemory,
            /* E_INVALIDARG                                      */ EInvalidArg                           = HResult.EInvalidArg,
            /* HRESULT_FROM_WIN32(ERROR_RESOURCE_LANG_NOT_FOUND) */ EWin32ResourceLangNotFound            = HResult.EWin32ResourceLangNotFound,
            /* BG_E_NOT_FOUND                                    */ ENotFound                             = 0x80200001,
            /* BG_E_INVALID_STATE                                */ EInvalidState                         = 0x80200002,
            /* BG_E_EMPTY                                        */ EEmpty                                = 0x80200003,
            /* BG_E_FILE_NOT_AVAILABLE                           */ EFileNotAvailable                     = 0x80200004,
            /* BG_E_PROTOCOL_NOT_AVAILABLE                       */ EProtocolNotAvailable                 = 0x80200005,
            /* BG_E_ERROR_CONTEXT_UNKNOWN                        */ EErrorContextUnknown                  = 0x80200007,
            /* BG_E_ERROR_CONTEXT_GENERAL_QUEUE_MANAGER          */ EErrorContextGeneralQueueManager      = 0x80200008,
            /* BG_E_ERROR_CONTEXT_LOCAL_FILE                     */ EErrorContextLocalFile                = 0x80200009,
            /* BG_E_ERROR_CONTEXT_REMOTE_FILE                    */ EErrorContextRemoteFile               = 0x8020000a,
            /* BG_E_ERROR_CONTEXT_GENERAL_TRANSPORT              */ EErrorContextGeneralTransport         = 0x8020000b,
            /* BG_E_ERROR_CONTEXT_QUEUE_MANAGER_NOTIFICATION     */ EErrorContextQueueManagerNotification = 0x8020000c,
            /* BG_E_DESTINATION_LOCKED                           */ EDestinationLocked                    = 0x8020000d,
            /* BG_E_VOLUME_CHANGED                               */ EVolumeChanged                        = 0x8020000e,
            /* BG_E_ERROR_INFORMATION_UNAVAILABLE                */ EErrorInformationUnavailable          = 0x8020000f,
            /* BG_E_NEW_OWNER_DIFF_MAPPING                       */ ENewOwnerDiffMapping                  = 0x80200015,
            /* BG_E_NEW_OWNER_NO_FILE_ACCESS                     */ ENewOwnerNoFileAccess                 = 0x80200016,
            /* BG_E_PROXY_LIST_TOO_LARGE                         */ EProxyListTooLarge                    = 0x80200018,
            /* BG_E_PROXY_BYPASS_LIST_TOO_LARGE                  */ EProxyBypassListTooLarge              = 0x80200019,
            /* BG_E_TOO_MANY_FILES                               */ ETooManyFiles                         = 0x8020001c,
            /* BG_E_STRING_TOO_LONG                              */ EStringTooLong                        = 0x80200021,
            /* BG_E_TOO_MANY_JOBS_PER_USER                       */ ETooManyJobsPerUser                   = 0x80200049,
            /* BG_E_TOO_MANY_JOBS_PER_MACHINE                    */ ETooManyJobsPerMachine                = 0x80200050,
            /* BG_E_TOO_MANY_FILES_IN_JOB                        */ ETooManyFilesInJob                    = 0x80200051
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-certclosestore")]
        internal enum CertCloseStoreFlag
        {
            /*                             */ Default =          0,
            /* CERT_CLOSE_STORE_FORCE_FLAG */ Force   = 0x00000001,
            /* CERT_CLOSE_STORE_CHECK_FLAG */ Check   = 0x00000002
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/privilege-constants")]
        internal const string SeShutdownName = "SeShutdownPrivilege";

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject")]
        internal enum CertEncoding
        {
            /*                     */ None           =          0,
            /* X509_ASN_ENCODING   */ X509Asn        = 0x00000001,
            /* X509_NDR_ENCODING   */ X509Ndr        = 0x00000002,
            /* PKCS_7_ASN_ENCODING */ Pkcs7Asn       = 0x00010000,
            /* PKCS_7_NDR_ENCODING */ Pkcs7Ndr       = 0x00020000,
            /*                     */ Pkcs7OrX509Asn = Pkcs7Asn
                                                     | X509Asn
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptmsggetparam")]
        internal enum CertMessageParameterType
        {
            /*                                              */ None                          =  0,
            /* CMSG_TYPE_PARAM                              */ Type                          =  1,
            /* CMSG_CONTENT_PARAM                           */ Content                       =  2,
            /* CMSG_BARE_CONTENT_PARAM                      */ BareContent                   =  3,
            /* CMSG_INNER_CONTENT_TYPE_PARAM                */ InnerContentType              =  4,
            /* CMSG_SIGNER_COUNT_PARAM                      */ SignerCount                   =  5,
            /* CMSG_SIGNER_INFO_PARAM                       */ SignerInfo                    =  6,
            /* CMSG_SIGNER_CERT_INFO_PARAM                  */ SignerCertInfo                =  7,
            /* CMSG_SIGNER_HASH_ALGORITHM_PARAM             */ SignerHashAlgorithm           =  8,
            /* CMSG_SIGNER_AUTH_ATTR_PARAM                  */ SignerAuthAttr                =  9,
            /* CMSG_SIGNER_UNAUTH_ATTR_PARAM                */ SignerUnauthAttr              = 10,
            /* CMSG_CERT_COUNT_PARAM                        */ CertCount                     = 11,
            /* CMSG_CERT_PARAM                              */ Cert                          = 12,
            /* CMSG_CRL_COUNT_PARAM                         */ CrlCount                      = 13,
            /* CMSG_CRL_PARAM                               */ Crl                           = 14,
            /* CMSG_ENVELOPE_ALGORITHM_PARAM                */ EnvelopeAlgorithm             = 15,
            /* CMSG_RECIPIENT_COUNT_PARAM                   */ RecipientCount                = 17,
            /* CMSG_RECIPIENT_INDEX_PARAM                   */ RecipientIndex                = 18,
            /* CMSG_RECIPIENT_INFO_PARAM                    */ RecipientInfo                 = 19,
            /* CMSG_HASH_ALGORITHM_PARAM                    */ HashAlgorithm                 = 20,
            /* CMSG_HASH_DATA_PARAM                         */ HashData                      = 21,
            /* CMSG_COMPUTED_HASH_PARAM                     */ ComputedHash                  = 22,
            /* CMSG_ENCRYPT_PARAM                           */ Encrypt                       = 26,
            /* CMSG_ENCRYPTED_DIGEST                        */ EncryptedDigest               = 27,
            /* CMSG_ENCODED_SIGNER                          */ EncodedSigner                 = 28,
            /* CMSG_ENCODED_MESSAGE                         */ EncodedMessage                = 29,
            /* CMSG_VERSION_PARAM                           */ Version                       = 30,
            /* CMSG_ATTR_CERT_COUNT_PARAM                   */ AttrCertCount                 = 31,
            /* CMSG_ATTR_CERT_PARAM                         */ AttrCert                      = 32,
            /* CMSG_CMS_RECIPIENT_COUNT_PARAM               */ CmsRecipientCount             = 33,
            /* CMSG_CMS_RECIPIENT_INDEX_PARAM               */ CmsRecipientIndex             = 34,
            /* CMSG_CMS_RECIPIENT_ENCRYPTED_KEY_INDEX_PARAM */ CmsRecipientEncryptedKeyIndex = 35,
            /* CMSG_CMS_RECIPIENT_INFO_PARAM                */ CmsRecipientInfo              = 36,
            /* CMSG_UNPROTECTED_ATTR_PARAM                  */ UnprotectedAttr               = 37,
            /* CMSG_SIGNER_CERT_ID_PARAM                    */ SignerCertId                  = 38,
            /* CMSG_CMS_SIGNER_INFO_PARAM                   */ CmsSignerInfo                 = 39
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject")]
        internal enum CertQueryContent
        {
            /*                                       */ None             =  0,
            /* CERT_QUERY_CONTENT_CERT               */ Cert             =  1,
            /* CERT_QUERY_CONTENT_CTL                */ Ctl              =  2,
            /* CERT_QUERY_CONTENT_CRL                */ Crl              =  3,
            /* CERT_QUERY_CONTENT_SERIALIZED_STORE   */ SerializedStore  =  4,
            /* CERT_QUERY_CONTENT_SERIALIZED_CERT    */ SerializedCert   =  5,
            /* CERT_QUERY_CONTENT_SERIALIZED_CTL     */ SerializedCtl    =  6,
            /* CERT_QUERY_CONTENT_SERIALIZED_CRL     */ SerializedCrl    =  7,
            /* CERT_QUERY_CONTENT_PKCS7_SIGNED       */ Pkcs7Signed      =  8,
            /* CERT_QUERY_CONTENT_PKCS7_UNSIGNED     */ Pkcs7Unsigned    =  9,
            /* CERT_QUERY_CONTENT_PKCS7_SIGNED_EMBED */ Pkcs7SignedEmbed = 10,
            /* CERT_QUERY_CONTENT_PKCS10             */ Pkcs10           = 11,
            /* CERT_QUERY_CONTENT_PFX                */ Pfx              = 12,
            /* CERT_QUERY_CONTENT_CERT_PAIR          */ CertPair         = 13
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject")]
        [Flags]
        internal enum CertQueryContentFlags : uint
        {
            /* CERT_QUERY_CONTENT_FLAG_CERT               */ Cert             = 1 << CertQueryContent.Cert,
            /* CERT_QUERY_CONTENT_FLAG_CTL                */ Ctl              = 1 << CertQueryContent.Ctl,
            /* CERT_QUERY_CONTENT_FLAG_CRL                */ Crl              = 1 << CertQueryContent.Crl,
            /* CERT_QUERY_CONTENT_FLAG_SERIALIZED_STORE   */ SerializedStore  = 1 << CertQueryContent.SerializedStore,
            /* CERT_QUERY_CONTENT_FLAG_SERIALIZED_CERT    */ SerializedCert   = 1 << CertQueryContent.SerializedCert,
            /* CERT_QUERY_CONTENT_FLAG_SERIALIZED_CTL     */ SerializedCtl    = 1 << CertQueryContent.SerializedCtl,
            /* CERT_QUERY_CONTENT_FLAG_SERIALIZED_CRL     */ SerializedCrl    = 1 << CertQueryContent.SerializedCrl,
            /* CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED       */ Pkcs7Signed      = 1 << CertQueryContent.Pkcs7Signed,
            /* CERT_QUERY_CONTENT_FLAG_PKCS7_UNSIGNED     */ Pkcs7Unsigned    = 1 << CertQueryContent.Pkcs7Unsigned,
            /* CERT_QUERY_CONTENT_FLAG_PKCS7_SIGNED_EMBED */ Pkcs7SignedEmbed = 1 << CertQueryContent.Pkcs7SignedEmbed,
            /* CERT_QUERY_CONTENT_FLAG_PKCS10             */ Pkcs10           = 1 << CertQueryContent.Pkcs10,
            /* CERT_QUERY_CONTENT_FLAG_PFX                */ Pfx              = 1 << CertQueryContent.Pfx,
            /* CERT_QUERY_CONTENT_FLAG_CERT_PAIR          */ CertPair         = 1 << CertQueryContent.CertPair,
            /* CERT_QUERY_CONTENT_FLAG_ALL                */ All              = Cert
                                                                              | Ctl
                                                                              | Crl
                                                                              | SerializedStore
                                                                              | SerializedCert
                                                                              | SerializedCtl
                                                                              | SerializedCrl
                                                                              | Pkcs7Signed
                                                                              | Pkcs7Unsigned
                                                                              | Pkcs7SignedEmbed
                                                                              | Pkcs10
                                                                              | Pfx
                                                                              | CertPair
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject")]
        internal enum CertQueryFormat
        {
            /*                                         */ None               = 0,
            /* CERT_QUERY_FORMAT_BINARY                */ Binary             = 1,
            /* CERT_QUERY_FORMAT_BASE64_ENCODED        */ Base64Encoded      = 2,
            /* CERT_QUERY_FORMAT_ASN_ASCII_HEX_ENCODED */ AsnAsciiHexEncoded = 3
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject")]
        internal enum CertQueryFormatFlag : uint
        {
            /* CERT_QUERY_FORMAT_FLAG_BINARY                */ Binary             = 1 << CertQueryFormat.Binary,
            /* CERT_QUERY_FORMAT_FLAG_BASE64_ENCODED        */ Base64Encoded      = 1 << CertQueryFormat.Base64Encoded,
            /* CERT_QUERY_FORMAT_FLAG_ASN_ASCII_HEX_ENCODED */ AsnAsciiHexEncoded = 1 << CertQueryFormat.AsnAsciiHexEncoded,
            /* CERT_QUERY_FORMAT_FLAG_ALL                   */ All                = Binary
                                                                                  | Base64Encoded
                                                                                  | AsnAsciiHexEncoded
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject")]
        internal enum CertQueryObject
        {
            /*                        */ None = 0,
            /* CERT_QUERY_OBJECT_FILE */ File = 1,
            /* CERT_QUERY_OBJECT_BLOB */ Blob = 2
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetclassdevsw",
                Description = "DIGCF enumeration")]
        [Flags]
        internal enum DeviceInfoGetClassFlags : uint
        {
            /* DIGCF_DEFAULT         */ Default         = 0x00000001,
            /* DIGCF_PRESENT         */ Present         = 0x00000002,
            /* DIGCF_ALLCLASSES      */ AllClasses      = 0x00000004,
            /* DIGCF_PROFILE         */ Profile         = 0x00000008,
            /* DIGCF_DEVICEINTERFACE */ DeviceInterface = 0x00000010
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-messagebox")]
        internal enum DialogBoxResult
        {
            /*            */ None     =     0,
            /* IDOK       */ Ok       =     1,
            /* IDCANCEL   */ Cancel   =     2,
            /* IDABORT    */ Abort    =     3,
            /* IDRETRY    */ Retry    =     4,
            /* IDIGNORE   */ Ignore   =     5,
            /* IDYES      */ Yes      =     6,
            /* IDNO       */ No       =     7,
            /* IDCLOSE    */ Close    =     8,
            /* IDHELP     */ Help     =     9,
            /* IDTRYAGAIN */ TryAgain =    10,
            /* IDCONTINUE */ Continue =    11,
            /* IDTIMEOUT  */ Timeout  = 32000,
            /* IDASYNC    */ Async    = 32001
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-_display_devicew")]
        [Flags]
        internal enum DisplayDeviceStateFlags : uint
        {
            /*                                    */ None              =          0,
            /* DISPLAY_DEVICE_ATTACHED_TO_DESKTOP */ AttachedToDesktop = 0x00000001,
            /* DISPLAY_DEVICE_ACTIVE              */ Active            = AttachedToDesktop,
            /* DISPLAY_DEVICE_MULTI_DRIVER        */ MultiDriver       = 0x00000002,
            /* DISPLAY_DEVICE_ATTACHED            */ Attached          = MultiDriver,
            /* DISPLAY_DEVICE_PRIMARY_DEVICE      */ PrimaryDevice     = 0x00000004,
            /* DISPLAY_DEVICE_MIRRORING_DRIVER    */ MirroringDriver   = 0x00000008,
            /* DISPLAY_DEVICE_VGA_COMPATIBLE      */ VgaCompatible     = 0x00000010,
            /* DISPLAY_DEVICE_REMOVABLE           */ Removable         = 0x00000020,
            /* DISPLAY_DEVICE_ACC_DRIVER          */ AccDriver         = 0x00000040,
            /* DISPLAY_DEVICE_RDPUDD              */ RdpUdd            = 0x01000000,
            /* DISPLAY_DEVICE_DISCONNECT          */ Disconnect        = 0x02000000,
            /* DISPLAY_DEVICE_REMOTE              */ Remote            = 0x04000000,
            /* DISPLAY_DEVICE_MODESPRUNED         */ ModesPruned       = 0x08000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/direct3ddxgi/dxgi-error",
                Description = "DXGI_ERROR enumeration")]
        internal enum DxgiError : uint
        {
            /* S_OK                                     */ SOk                        = HResult.SOk,
            /* DXGI_ERROR_INVALID_CALL                  */ InvalidCall                = 0x887a0001,
            /* DXGI_ERROR_NOT_FOUND                     */ NotFound                   = 0x887a0002,
            /* DXGI_ERROR_MORE_DATA                     */ MoreData                   = 0x887a0003,
            /* DXGI_ERROR_UNSUPPORTED                   */ Unsupported                = 0x887a0004,
            /* DXGI_ERROR_DEVICE_REMOVED                */ DeviceRemoved              = 0x887a0005,
            /* DXGI_ERROR_DEVICE_HUNG                   */ DeviceHung                 = 0x887a0006,
            /* DXGI_ERROR_DEVICE_RESET                  */ DeviceReset                = 0x887a0007,
            /* DXGI_ERROR_WAS_STILL_DRAWING             */ WasStillDrawing            = 0x887a000a,
            /* DXGI_ERROR_FRAME_STATISTICS_DISJOINT     */ FrameStatisticsDisjoint    = 0x887a000b,
            /* DXGI_ERROR_GRAPHICS_VIDPN_SOURCE_IN_USE  */ GraphicsVidpnSourceInUse   = 0x887a000c,
            /* DXGI_ERROR_DRIVER_INTERNAL_ERROR         */ DriverInternalError        = 0x887a0020,
            /* DXGI_ERROR_NONEXCLUSIVE                  */ Nonexclusive               = 0x887a0021,
            /* DXGI_ERROR_NOT_CURRENTLY_AVAILABLE       */ NotCurrentlyAvailable      = 0x887a0022,
            /* DXGI_ERROR_REMOTE_CLIENT_DISCONNECTED    */ RemoteClientDisconnected   = 0x887a0023,
            /* DXGI_ERROR_REMOTE_OUTOFMEMORY            */ RemoteOutOfMemory          = 0x887a0024,
            /* DXGI_ERROR_MODE_CHANGE_IN_PROGRESS       */ ModeChangeInProgress       = 0x887a0025,
            /* DXGI_ERROR_ACCESS_LOST                   */ AccessLost                 = 0x887a0026,
            /* DXGI_ERROR_WAIT_TIMEOUT                  */ WaitTimeout                = 0x887a0027,
            /* DXGI_ERROR_SESSION_DISCONNECTED          */ SessionDisconnected        = 0x887a0028,
            /* DXGI_ERROR_RESTRICT_TO_OUTPUT_STALE      */ RestrictToOutputStale      = 0x887a0029,
            /* DXGI_ERROR_CANNOT_PROTECT_CONTENT        */ CannotProtectContent       = 0x887a002a,
            /* DXGI_ERROR_ACCESS_DENIED                 */ AccessDenied               = 0x887a002b,
            /* DXGI_ERROR_NAME_ALREADY_EXISTS           */ NameAlreadyExists          = 0x887a002c,
            /* DXGI_ERROR_SDK_COMPONENT_MISSING         */ SdkComponentMissing        = 0x887a002d,
            /* DXGI_ERROR_NOT_CURRENT                   */ NotCurrent                 = 0x887a002e,
            /* DXGI_ERROR_HW_PROTECTION_OUTOFMEMORY     */ HwProtectionOutOfMemory    = 0x887a0030,
            /* DXGI_ERROR_DYNAMIC_CODE_POLICY_VIOLATION */ DynamicCodePolicyViolation = 0x887a0031,
            /* DXGI_ERROR_NON_COMPOSITED_UI             */ NonCompositedUI            = 0x887A0032,
            /* DXGI_ERROR_CACHE_CORRUPT                 */ CacheCorrupt               = 0x887a0033,
            /* DXGI_ERROR_CACHE_FULL                    */ CacheFull                  = 0x887a0034,
            /* DXGI_ERROR_CACHE_HASH_COLLISION          */ CacheHashCollision         = 0x887a0035,
            /* DXGI_ERROR_ALREADY_EXISTS                */ AlreadyExists              = 0x887a0036
        }

        [ExternalReference("https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/bb173065(v=vs.85)",
                Description = "DXGI_MODE_ROTATION enumeration")]
        internal enum DxgiModeRotation
        {
            /* DXGI_MODE_ROTATION_UNSPECIFIED */ Unspecified = 0,
            /* DXGI_MODE_ROTATION_IDENTITY    */ Identity    = 1,
            /* DXGI_MODE_ROTATION_ROTATE90    */ Rotate90    = 2,
            /* DXGI_MODE_ROTATION_ROTATE180   */ Rotate180   = 3,
            /* DXGI_MODE_ROTATION_ROTATE270   */ Rotate270   = 4
        }

        [Flags]
        internal enum EnumDisplayDeviceFlags : uint
        {
            /* None                          */ None                   =          0,
            /* EDD_GET_DEVICE_INTERFACE_NAME */ GetDeviceInterfaceName = 0x00000001
        }

        internal enum Error
        {
            /* ERROR_SUCCESS                    (0,   0x0) */ Success               =   0x0,
            /* ERROR_FILE_NOT_FOUND             (2,   0x2) */ FileNotFound          =   0x2,
            /* ERROR_ACCESS_DENIED              (5,   0x5) */ AccessDenied          =   0x5,
            /* ERROR_INVALID_HANDLE             (6,   0x6) */ InvalidHandle         =   0x6,
            /* ERROR_INVALID_DATA              (13,   0xd) */ InvalidData           =   0xd,
            /* ERROR_OUTOFMEMORY               (14,   0xe) */ OutOfMemory           =   0xe,
            /* ERROR_BAD_LENGTH                (24,  0x18) */ BadLength             =  0x18,
            /* ERROR_GEN_FAILURE               (31,  0x1f) */ GenFailure            =  0x1f,
            /* ERROR_HANDLE_DISK_FULL          (39,  0x27) */ HandleDiskFull        =  0x27,
            /* ERROR_NOT_SUPPORTED             (50,  0x32) */ NotSupported          =  0x32,
            /* ERROR_INVALID_PARAMETER         (87,  0x57) */ InvalidParameter      =  0x57,
            /* ERROR_DISK_FULL                (112,  0x70) */ DiskFull              =  0x70,
            /* ERROR_INSUFFICIENT_BUFFER      (122,  0x7a) */ InsufficientBuffer    =  0x7a,
            /* ERROR_INVALID_NAME             (123,  0x7b) */ InvalidName           =  0x7b,
            /* ERROR_FILENAME_EXCED_RANGE     (206,  0xce) */ FilenameExceedRange   =  0xce,
            /* ERROR_MORE_DATA                (234,  0xea) */ MoreData              =  0xea,
            /* ERROR_NO_MORE_ITEMS            (259, 0x103) */ NoMoreItems           = 0x103,
            /* ERROR_SERVICE_DOES_NOT_EXIST  (1060, 0x424) */ ServiceDoesNotExist   = 0x424,
            /* ERROR_DEVICE_NOT_CONNECTED    (1167, 0x48f) */ DeviceNotConnected    = 0x48f,
            /* ERROR_NOT_FOUND               (1168, 0x490) */ NotFound              = 0x490,
            /* ERROR_NO_SUCH_LOGON_SESSION   (1312, 0x520) */ NoSuchLogonSession    = 0x520,
            /* ERROR_BAD_IMPERSONATION_LEVEL (1346, 0x542) */ BadImpersonationLevel = 0x542,
            /* ERROR_RESOURCE_LANG_NOT_FOUND (1815, 0x717) */ ResourceLangNotFound  = 0x717
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-exitwindowsex")]
        [Flags]
        internal enum ExitTypes : uint
        {
            /* EWX_LOGOFF          */ Logoff         = 0x00000000,
            /* EWX_SHUTDOWN        */ Shutdown       = 0x00000001,
            /* EWX_REBOOT          */ Reboot         = 0x00000002,
            /* EWX_FORCE           */ Force          = 0x00000004,
            /* EWX_POWEROFF        */ Poweroff       = 0x00000008,
            /* EWX_FORCEIFHUNG     */ ForceIfHung    = 0x00000010,
            /* EWX_QUICKRESOLVE    */ QuickResolve   = 0x00000020,
            /* EWX_RESTARTAPPS     */ RestartApps    = 0x00000040,
            /* EWX_HYBRID_SHUTDOWN */ HybridShutdown = 0x00400000,
            /* EWX_BOOTOPTIONS     */ BootOptions    = 0x01000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/FileIO/file-attribute-constants",
                Description = "FILE_ATTRIBUTE_FLAG enumeration")]
        [Flags]
        internal enum FileAttributeFlags : uint
        {
            /* FILE_ATTRIBUTE_READONLY              */ AttributeReadonly           = 0x00000001,
            /* FILE_ATTRIBUTE_HIDDEN                */ AttributeHidden             = 0x00000002,
            /* FILE_ATTRIBUTE_SYSTEM                */ AttributeSystem             = 0x00000004,
            /* FILE_ATTRIBUTE_DIRECTORY             */ AttributeDirectory          = 0x00000010,
            /* FILE_ATTRIBUTE_ARCHIVE               */ AttributeArchive            = 0x00000020,
            /* FILE_ATTRIBUTE_DEVICE                */ AttributeDevice             = 0x00000040,
            /* FILE_ATTRIBUTE_NORMAL                */ AttributeNormal             = 0x00000080,
            /* FILE_ATTRIBUTE_TEMPORARY             */ AttributeTemporary          = 0x00000100,
            /* FILE_ATTRIBUTE_SPARSE_FILE           */ AttributeSparseFile         = 0x00000200,
            /* FILE_ATTRIBUTE_REPARSE_POINT         */ AttributeReparsePoint       = 0x00000400,
            /* FILE_ATTRIBUTE_COMPRESSED            */ AttributeCompressed         = 0x00000800,
            /* FILE_ATTRIBUTE_OFFLINE               */ AttributeOffline            = 0x00001000,
            /* FILE_ATTRIBUTE_NOT_CONTENT_INDEXED   */ AttributeNotContentIndexed  = 0x00002000,
            /* FILE_ATTRIBUTE_ENCRYPTED             */ AttributeEncrypted          = 0x00004000,
            /* FILE_ATTRIBUTE_INTEGRITY_STREAM      */ AttributeIntegrityStream    = 0x00008000,
            /* FILE_ATTRIBUTE_VIRTUAL               */ AttributeVirtual            = 0x00010000,
            /* FILE_ATTRIBUTE_NO_SCRUB_DATA         */ AttributeNoScrubData        = 0x00020000,
            /* FILE_ATTRIBUTE_RECALL_ON_OPEN        */ AttributeRecallOnOpen       = 0x00040000,
            /* FILE_FLAG_FIRST_PIPE_INSTANCE        */ FlagFirstPipeInstance       = 0x00080000,
            /* FILE_FLAG_OPEN_NO_RECALL             */ FlagOpenNoRecall            = 0x00100000,
            /* FILE_FLAG_OPEN_REPARSE_POINT         */ FlagOpenReparsePoint        = 0x00200000,
            /* FILE_ATTRIBUTE_RECALL_ON_DATA_ACCESS */ AttributeRecallOnDataAccess = 0x00400000,
            /* FILE_FLAG_SESSION_AWARE              */ FlagSessionAware            = 0x00800000,
            /* FILE_FLAG_POSIX_SEMANTICS            */ FlagPosixSemantics          = 0x01000000,
            /* FILE_FLAG_BACKUP_SEMANTICS           */ FlagBackupSemantics         = 0x02000000,
            /* FILE_FLAG_DELETE_ON_CLOSE            */ FlagDeleteOnClose           = 0x04000000,
            /* FILE_FLAG_SEQUENTIAL_SCAN            */ FlagSequentialScan          = 0x08000000,
            /* FILE_FLAG_RANDOM_ACCESS              */ FlagRandomAccess            = 0x10000000,
            /* FILE_FLAG_NO_BUFFERING               */ FlagNoBuffering             = 0x20000000,
            /* FILE_FLAG_OVERLAPPED                 */ FlagOverlapped              = 0x40000000,
            /* FILE_FLAG_WRITE_THROUGH              */ FlagWriteThrough            = 0x80000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew",
                Description = "CREATION_DISPOSITION enumeration")]
        internal enum FileCreationDisposition : uint
        {
            /* CREATE_NEW        */ CreateNew        = 1,
            /* CREATE_ALWAYS     */ CreateAlways     = 2,
            /* OPEN_EXISTING     */ OpenExisting     = 3,
            /* OPEN_ALWAYS       */ OpenAlways       = 4,
            /* TRUNCATE_EXISTING */ TruncateExisting = 5
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew",
                Description = "FILE_SHARE enumeration")]
        [Flags]
        internal enum FileShareModes : uint
        {
            /* FILE_SHARE_NONE   */ None   = 0x00000000,
            /* FILE_SHARE_READ   */ Read   = 0x00000001,
            /* FILE_SHARE_WRITE  */ Write  = 0x00000002,
            /* FILE_SHARE_DELETE */ Delete = 0x00000004
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/generic-access-rights",
                Description = "GENERIC enumeration")]
        [Flags]
        internal enum GenericAccessRights : uint
        {
            /* GENERIC_ALL     */ All     = 0x10000000,
            /* GENERIC_EXECUTE */ Execute = 0x20000000,
            /* GENERIC_WRITE   */ Write   = 0x40000000,
            /* GENERIC_READ    */ Read    = 0x80000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/seccrypto/common-hresult-values")]
        internal enum HResult : uint
        {
            /* S_OK                                              */ SOk                        =        0x0,
            /* S_FALSE                                           */ SFalse                     =        0x1,
            /* E_NOTIMPL                                         */ ENotImpl                   = 0x80004001,
            /* E_POINTER                                         */ EPointer                   = 0x80004003,
            /* E_FAIL                                            */ EFail                      = 0x80004005,
            /* E_UNEXPECTED                                      */ EUnexpected                = 0x8000ffff,
            /* E_ACCESSDENIED                                    */ EAccessDenied              = 0x80070000
                                                                                               | Error.AccessDenied,
            /* E_HANDLE                                          */ EHandle                    = 0x80070000
                                                                                               | Error.InvalidHandle,
            /* HRESULT_FROM_WIN32(ERROR_INVALID_DATA)            */ EWin32InvalidData          = 0x80070000
                                                                                               | Error.InvalidData,
            /* E_OUTOFMEMORY                                     */ EOutOfMemory               = 0x80070000
                                                                                               | Error.OutOfMemory,
            /* HRESULT_FROM_WIN32(ERROR_HANDLE_DISK_FULL)        */ EWin32HandleDiskFull       = 0x80070000
                                                                                               | Error.HandleDiskFull,
            /* E_INVALIDARG                                      */ EInvalidArg                = 0x80070000
                                                                                               | Error.InvalidParameter,
            /* HRESULT_FROM_WIN32(ERROR_DISK_FULL)               */ EWin32DiskFull             = 0x80070000
                                                                                               | Error.DiskFull,
            /* HRESULT_FROM_WIN32(ERROR_INSUFFICIENT_BUFFER)     */ EWin32InsufficientBuffer   = 0x80070000
                                                                                               | Error.InsufficientBuffer,
            /* HRESULT_FROM_WIN32(ERROR_NOT_FOUND)               */ EWin32NotFound             = 0x80070000
                                                                                               | Error.NotFound,
            /* HRESULT_FROM_WIN32(ERROR_RESOURCE_LANG_NOT_FOUND) */ EWin32ResourceLangNotFound = 0x80070000
                                                                                               | Error.ResourceLangNotFound
        }

        [ExternalReference("https://docs.microsoft.com/zh-tw/windows/desktop/SysInfo/image-file-machine-constants")]
        internal enum ImageFileMachine : ushort
        {
            /* IMAGE_FILE_MACHINE_UNKNOWN     */ Unknown    =      0,
            /* IMAGE_FILE_MACHINE_TARGET_HOST */ TargetHost = 0x0001,
            /* IMAGE_FILE_MACHINE_I386        */ I386       = 0x014c,
            /* IMAGE_FILE_MACHINE_R3000       */ R3000      = 0x0162,
            /* IMAGE_FILE_MACHINE_R4000       */ R4000      = 0x0166,
            /* IMAGE_FILE_MACHINE_R10000      */ R10000     = 0x0168,
            /* IMAGE_FILE_MACHINE_WCEMIPSV2   */ WceMipsV2  = 0x0169,
            /* IMAGE_FILE_MACHINE_ALPHA       */ Alpha      = 0x0184,
            /* IMAGE_FILE_MACHINE_SH3         */ Sh3        = 0x01a2,
            /* IMAGE_FILE_MACHINE_SH3DSP      */ Sh3Dsp     = 0x01a3,
            /* IMAGE_FILE_MACHINE_SH3E        */ Sh3E       = 0x01a4,
            /* IMAGE_FILE_MACHINE_SH4         */ Sh4        = 0x01a6,
            /* IMAGE_FILE_MACHINE_SH5         */ Sh5        = 0x01a8,
            /* IMAGE_FILE_MACHINE_ARM         */ Arm        = 0x01c0,
            /* IMAGE_FILE_MACHINE_THUMB       */ Thumb      = 0x01c2,
            /* IMAGE_FILE_MACHINE_ARMNT       */ ArmNT      = 0x01c4,
            /* IMAGE_FILE_MACHINE_AM33        */ Am33       = 0x01d3,
            /* IMAGE_FILE_MACHINE_POWERPC     */ PowerPC    = 0x01F0,
            /* IMAGE_FILE_MACHINE_POWERPCFP   */ PowerPCFp  = 0x01f1,
            /* IMAGE_FILE_MACHINE_IA64        */ Ia64       = 0x0200,
            /* IMAGE_FILE_MACHINE_MIPS16      */ Mips16     = 0x0266,
            /* IMAGE_FILE_MACHINE_ALPHA64     */ Alpha64    = 0x0284,
            /* IMAGE_FILE_MACHINE_MIPSFPU     */ MipsFpu    = 0x0366,
            /* IMAGE_FILE_MACHINE_MIPSFPU16   */ MipsFpu16  = 0x0466,
            /* IMAGE_FILE_MACHINE_AXP64       */ Axp64      = Alpha64,
            /* IMAGE_FILE_MACHINE_TRICORE     */ TriCore    = 0x0520,
            /* IMAGE_FILE_MACHINE_CEF         */ Cef        = 0x0cef,
            /* IMAGE_FILE_MACHINE_EBC         */ Ebc        = 0x0ebc,
            /* IMAGE_FILE_MACHINE_AMD64       */ Amd64      = 0x8664,
            /* IMAGE_FILE_MACHINE_M32R        */ M32R       = 0x9041,
            /* IMAGE_FILE_MACHINE_ARM64       */ Arm64      = 0xaa64,
            /* IMAGE_FILE_MACHINE_CEE         */ Cee        = 0xc0ee
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createprocesswithtokenw")]
        internal enum LogonFlag : uint
        {
            /*                            */ None               =          0,
            /* LOGON_WITH_PROFILE         */ WithProfile        = 0x00000001,
            /* LOGON_NETCREDENTIALS_ONLY  */ NetCredentialsOnly = 0x00000002,
            /* LOGON_ZERO_PASSWORD_BUFFER */ ZeroPasswordBuffer = 0x80000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-messagebox")]
        internal enum MessageBoxStyle : uint
        {
            /* MB_OK                   */ Ok                  = 0x00000000,
            /* MB_OKCANCEL             */ OkCancel            = 0x00000001,
            /* MB_ABORTRETRYIGNORE     */ AbortRetryIgnore    = 0x00000002,
            /* MB_YESNOCANCEL          */ YesNoCancel         = 0x00000003,
            /* MB_YESNO                */ YesNo               = 0x00000004,
            /* MB_RETRYCANCEL          */ RetryCancel         = 0x00000005,
            /* MB_CANCELTRYCONTINUE    */ CancelTryContinue   = 0x00000006,
            /* MB_ICONHAND             */ IconHand            = 0x00000010,
            /* MB_ICONERROR            */ IconError           = IconHand,
            /* MB_ICONSTOP             */ IconStop            = IconError,
            /* MB_ICONQUESTION         */ IconQuestion        = 0x00000020,
            /* MB_ICONEXCLAMATION      */ IconExclamation     = 0x00000030,
            /* MB_ICONWARNING          */ IconWarning         = IconExclamation,
            /* MB_ICONASTERISK         */ IconAsterisk        = 0x00000040,
            /* MB_ICONINFORMATION      */ IconInformation     = IconAsterisk,
            /* MB_USERICON             */ UserIcon            = 0x00000080,
            /* MB_DEFBUTTON1           */ DefButton1          = Ok,
            /* MB_DEFBUTTON2           */ DefButton2          = 0x00000100,
            /* MB_DEFBUTTON3           */ DefButton3          = 0x00000200,
            /* MB_DEFBUTTON4           */ DefButton4          = 0x00000300,
            /* MB_APPLMODAL            */ ApplModal           = DefButton1,
            /* MB_SYSTEMMODAL          */ SystemModal         = 0x00001000,
            /* MB_TASKMODAL            */ TaskModal           = 0x00002000,
            /* MB_HELP                 */ Help                = 0x00004000,
            /* MB_NOFOCUS              */ NoFocus             = 0x00008000,
            /* MB_SETFOREGROUND        */ SetForeground       = 0x00010000,
            /* MB_DEFAULT_DESKTOP_ONLY */ DefaultDesktopOnly  = 0x00020000,
            /* MB_TOPMOST              */ TopMost             = 0x00040000,
            /* MB_RIGHT                */ Right               = 0x00080000,
            /* MB_RTLREADING           */ RtlReading          = 0x00100000,
            /* MB_SERVICE_NOTIFICATION */ ServiceNotification = 0x00200000,
        }

        internal enum NtStatus : uint
        {
            /* STATUS_WAIT_0                       */ StatusWait0                    =          0,
            /* HIDP_STATUS_SUCCESS                 */ HidpStatusSuccess              = 0x00110000,
            /* HIDP_STATUS_NULL                    */ HidpStatusNull                 = 0x80110001,
            /* HIDP_STATUS_INVALID_PREPARSED_DATA  */ HidpStatusInvalidPreparsedData = 0xc0110001,
            /* HIDP_STATUS_INVALID_REPORT_TYPE     */ HidpStatusInvalidReportType    = 0xc0110002,
            /* HIDP_STATUS_INVALID_REPORT_LENGTH   */ HidpStatusInvalidReportLength  = 0xc0110003,
            /* HIDP_STATUS_USAGE_NOT_FOUND         */ HidpStatusUsageNotFound        = 0xc0110004,
            /* HIDP_STATUS_VALUE_OUT_OF_RANGE      */ HidpStatusValueOutOfRange      = 0xc0110005,
            /* HIDP_STATUS_BAD_LOG_PHY_VALUES      */ HidpStatusBadLogPhyValues      = 0xc0110006,
            /* HIDP_STATUS_BUFFER_TOO_SMALL        */ HidpStatusBufferTooSmall       = 0xc0110007,
            /* HIDP_STATUS_INTERNAL_ERROR          */ HidpStatusInternalError        = 0xc0110008,
            /* HIDP_STATUS_I8042_TRANS_UNKNOWN     */ HidpStatusI8042TransUnknown    = 0xc0110009,
            /* HIDP_STATUS_INCOMPATIBLE_REPORT_ID  */ HidpStatusIncompatibleReportId = 0xc011000a,
            /* HIDP_STATUS_NOT_VALUE_ARRAY         */ HidpStatusNotValueArray        = 0xc011000b,
            /* HIDP_STATUS_IS_VALUE_ARRAY          */ HidpStatusIsValueArray         = 0xc011000c,
            /* HIDP_STATUS_DATA_INDEX_NOT_FOUND    */ HidpStatusDataIndexNotFound    = 0xc011000d,
            /* HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE */ HidpStatusDataIndexOutOfRange  = 0xc011000e,
            /* HIDP_STATUS_BUTTON_NOT_PRESSED      */ HidpStatusButtonNotPressed     = 0xc011000f,
            /* HIDP_STATUS_REPORT_DOES_NOT_EXIST   */ HidpStatusReportDoesNotExist   = 0xc0110010,
            /* HIDP_STATUS_NOT_IMPLEMENTED         */ HidpStatusNotImplemented       = 0xc0110020
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createnamedpipea")]
        [Flags]
        internal enum PipeModes : uint
        {
            /* PIPE_WAIT                  */ Wait                = 0x00000000,
            /* PIPE_READMODE_BYTE         */ ReadModeByte        = Wait,
            /* PIPE_TYPE_BYTE             */ TypeByte            = ReadModeByte,
            /* PIPE_ACCEPT_REMOTE_CLIENTS */ AcceptRemoteClients = TypeByte,
            /* PIPE_NOWAIT                */ NoWait              = 0x00000001,
            /* PIPE_READMODE_MESSAGE      */ ReadModeMessage     = 0x00000002,
            /* PIPE_TYPE_MESSAGE          */ TypeMessage         = 0x00000004,
            /* PIPE_REJECT_REMOTE_CLIENTS */ RejectRemoteClients = 0x00000008
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winbase/nf-winbase-createnamedpipea")]
        [Flags]
        internal enum PipeOpenModes : uint
        {
            /*                               */ None                  =          0,
            /* PIPE_ACCESS_INBOUND           */ AccessInbound         = 0x00000001,
            /* PIPE_ACCESS_OUTBOUND          */ AccessOutbound        = 0x00000002,
            /* PIPE_ACCESS_DUPLEX            */ AccessDuplex          = 0x00000003,
            /* FILE_FLAG_FIRST_PIPE_INSTANCE */ FlagFirstPipeInstance = FileAttributeFlags.FlagFirstPipeInstance,
            /* FILE_FLAG_OVERLAPPED          */ FlagOverlapped        = FileAttributeFlags.FlagOverlapped,
            /* FILE_FLAG_WRITE_THROUGH       */ FlagWriteThrough      = FileAttributeFlags.FlagWriteThrough,
            /* WRITE_DAC                     */ WriteDac              = StandardAccessRights.WriteDac,
            /* WRITE_OWNER                   */ WriteOwner            = StandardAccessRights.WriteOwner,
            /* ACCESS_SYSTEM_SECURITY        */ AccessSystemSecurity  = 0x01000000,
            /*                               */ CurrentUserOnly       = 0x20000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights",
                Description = "Process access right enumeration")]
        [Flags]
        internal enum ProcessAccessRights : uint
        {
            /* PROCESS_TERMINATE                 */ Terminate                = 0x0001,
            /* PROCESS_CREATE_THREAD             */ CreateThread             = 0x0002,
            /* PROCESS_SET_SESSIONID             */ SetSessionId             = 0x0004,
            /* PROCESS_VM_OPERATION              */ VMOperation              = 0x0008,
            /* PROCESS_VM_READ                   */ VMRead                   = 0x0010,
            /* PROCESS_VM_WRITE                  */ VMWrite                  = 0x0020,
            /* PROCESS_DUP_HANDLE                */ DupHandle                = 0x0040,
            /* PROCESS_CREATE_PROCESS            */ CreateProcess            = 0x0080,
            /* PROCESS_SET_QUOTA                 */ SetQuota                 = 0x0100,
            /* PROCESS_SET_INFORMATION           */ SetInformation           = 0x0200,
            /* PROCESS_QUERY_INFORMATION         */ QueryInformation         = 0x0400,
            /* PROCESS_SUSPEND_RESUME            */ SuspendResume            = 0x0800,
            /* PROCESS_QUERY_LIMITED_INFORMATION */ QueryLimitedInformation  = 0x1000,
            /* PROCESS_SET_LIMITED_INFORMATION   */ SetLimitedInformation    = 0x2000,
            /* PROCESS_ALL_ACCESS                */ AllAccess                = StandardAccessRights.StandardRightsRequired
                                                                             | StandardAccessRights.Synchronize
                                                                             | 0xFFFF
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/ProcThread/process-creation-flags",
                Description = "Process Creation enumeration")]
        [Flags]
        internal enum ProcessCreationFlags : uint
        {
            /*                                  */ None                         =          0,
            /* DEBUG_PROCESS                    */ DebugProcess                 = 0x00000001,
            /* DEBUG_ONLY_THIS_PROCESS          */ DebugOnlyThisProcess         = 0x00000002,
            /* CREATE_SUSPENDED                 */ CreateSuspended              = 0x00000004,
            /* DETACHED_PROCESS                 */ DetachedProcess              = 0x00000008,
            /* CREATE_NEW_CONSOLE               */ CreateNewConsole             = 0x00000010,
            /* NORMAL_PRIORITY_CLASS            */ NormalPriorityClass          = 0x00000020,
            /* IDLE_PRIORITY_CLASS              */ IdlePriorityClass            = 0x00000040,
            /* HIGH_PRIORITY_CLASS              */ HighPriorityClass            = 0x00000080,
            /* REALTIME_PRIORITY_CLASS          */ RealtimePriorityClass        = 0x00000100,
            /* CREATE_NEW_PROCESS_GROUP         */ CreateNewProcessGroup        = 0x00000200,
            /* CREATE_UNICODE_ENVIRONMENT       */ CreateUnicodeEnvironment     = 0x00000400,
            /* CREATE_SEPARATE_WOW_VDM          */ CreateSeparateWowVdm         = 0x00000800,
            /* CREATE_SHARED_WOW_VDM            */ CreateSharedWowVdm           = 0x00001000,
            /* CREATE_FORCEDOS                  */ CreateForceDos               = 0x00002000,
            /* BELOW_NORMAL_PRIORITY_CLASS      */ BelowNormalPriorityClass     = 0x00004000,
            /* ABOVE_NORMAL_PRIORITY_CLASS      */ AboveNormalPriorityClass     = 0x00008000,
            /* INHERIT_PARENT_AFFINITY          */ InheritParentAffinity        = 0x00010000,
            /* INHERIT_CALLER_PRIORITY          */ InheritCallerPriority        = 0x00020000,
            /* CREATE_PROTECTED_PROCESS         */ CreateProtectedProcess       = 0x00040000,
            /* EXTENDED_STARTUPINFO_PRESENT     */ ExtendedStartupInfoPresent   = 0x00080000,
            /* PROCESS_MODE_BACKGROUND_BEGIN    */ ProcessModeBackgroundBegin   = 0x00100000,
            /* PROCESS_MODE_BACKGROUND_END      */ ProcessModeBackgroundEnd     = 0x00200000,
            /* CREATE_SECURE_PROCESS            */ CreateSecureProcess          = 0x00400000,
            /* CREATE_BREAKAWAY_FROM_JOB        */ CreateBreakawayFromJob       = 0x01000000,
            /* CREATE_PRESERVE_CODE_AUTHZ_LEVEL */ CreatePreserveCodeAuthzLevel = 0x02000000,
            /* CREATE_DEFAULT_ERROR_MODE        */ CreateDefaultErrorMode       = 0x04000000,
            /* CREATE_NO_WINDOW                 */ CreateNoWindow               = 0x08000000,
            /* PROFILE_USER                     */ ProfileUser                  = 0x10000000,
            /* PROFILE_KERNEL                   */ ProfileKernel                = 0x20000000,
            /* PROFILE_SERVER                   */ ProfileServer                = 0x40000000,
            /* CREATE_IGNORE_SYSTEM_DEFAULT     */ CreateIgnoreSystemDefault    = 0x80000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/sysinfo/predefined-keys")]
        internal enum RegistryKey
        {
            /* HKEY_CLASSES_ROOT                */ ClassesRoot              = unchecked((int)0x80000000),
            /* HKEY_CURRENT_USER                */ CurrentUser              = unchecked((int)0x80000001),
            /* HKEY_LOCAL_MACHINE               */ LocalMachine             = unchecked((int)0x80000002),
            /* HKEY_USERS                       */ Users                    = unchecked((int)0x80000003),
            /* HKEY_PERFORMANCE_DATA            */ PerformanceData          = unchecked((int)0x80000004),
            /* HKEY_CURRENT_CONFIG              */ CurrentConfig            = unchecked((int)0x80000005),
            /* HKEY_DYN_DATA                    */ DynData                  = unchecked((int)0x80000006),
            /* HKEY_CURRENT_USER_LOCAL_SETTINGS */ CurrentUserLocalSettings = unchecked((int)0x80000007),
            /* HKEY_PERFORMANCE_TEXT            */ PerformanceText          = unchecked((int)0x80000050),
            /* HKEY_PERFORMANCE_NLSTEXT         */ PerformanceNlsText       = unchecked((int)0x80000060)
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/sysinfo/registry-key-security-and-access-rights",
                Description = "Registry Key Access Right enumeration")]
        [Flags]
        internal enum RegistryKeyAccessRights : uint
        {
            /*                        */ None             = 0x0000,
            /* KEY_QUERY_VALUE        */ QueryValue       = 0x0001,
            /* KEY_SET_VALUE          */ SetValue         = 0x0002,
            /* KEY_CREATE_SUB_KEY     */ CreateSubKey     = 0x0004,
            /* KEY_ENUMERATE_SUB_KEYS */ EnumerateSubKeys = 0x0008,
            /* KEY_NOTIFY             */ Notify           = 0x0010,
            /* KEY_CREATE_LINK        */ CreateLink       = 0x0020,
            /* KEY_WOW64_64KEY        */ Wow6464Key       = 0x0100,
            /* KEY_WOW64_32KEY        */ Wow6432Key       = 0x0200,
            /* KEY_WOW64_RES          */ Wow64Res         = Wow6464Key
                                                          | Wow6432Key,
            /* KEY_READ               */ Read             = ( StandardAccessRights.StandardRightsRead | QueryValue
                                                                                                      | EnumerateSubKeys
                                                                                                      | Notify )
                                                          & ~StandardAccessRights.Synchronize,
            /* KEY_WRITE              */ Write            = ( StandardAccessRights.StandardRightsWrite | SetValue
                                                                                                       | CreateSubKey )
                                                          & ~StandardAccessRights.Synchronize,
            /* KEY_EXECUTE            */ Execute          = Read
                                                          & ~StandardAccessRights.Synchronize,
            /* KEY_ALL_ACCESS         */ AllAccess        = ( StandardAccessRights.StandardRightsAll | QueryValue
                                                                                                     | SetValue
                                                                                                     | CreateSubKey
                                                                                                     | EnumerateSubKeys
                                                                                                     | Notify
                                                                                                     | CreateLink )
                                                          & ~StandardAccessRights.Synchronize
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/sysinfo/registry-value-types")]
        internal enum RegistryValueType : uint
        {
            /* REG_NONE                       */ None                     =  0,
            /* REG_SZ                         */ String                   =  1,
            /* REG_EXPAND_SZ                  */ ExpandString             =  2,
            /* REG_BINARY                     */ Binary                   =  3,
            /* REG_DWORD                      */ DWord                    =  4,
            /* REG_DWORD_LITTLE_ENDIAN        */ DWordLittleEndian        = DWord,
            /* REG_DWORD_BIG_ENDIAN           */ DWordBigEndian           =  5,
            /* REG_LINK                       */ Link                     =  6,
            /* REG_MULTI_SZ                   */ MultiString              =  7,
            /* REG_RESOURCE_LIST              */ ResourceList             =  8,
            /* REG_FULL_RESOURCE_DESCRIPTOR   */ FullResourceDescriptor   =  9,
            /* REG_RESOURCE_REQUIREMENTS_LIST */ ResourceRequirementsList = 10,
            /* REG_QWORD                      */ QWord                    = 11,
            /* REG_QWORD_LITTLE_ENDIAN        */ QWordLittleEndian        = QWord
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_security_impersonation_level",
                Description = "SECURITY_IMPERSONATION_LEVEL enumeration")]
        internal enum SecurityImpersonationLevel : uint
        {
                /* SecurityAnonymous      */ SecurityAnonymous      = 0,
                /* SecurityIdentification */ SecurityIdentification = 1,
                /* SecurityImpersonation  */ SecurityImpersonation  = 2,
                /* SecurityDelegation     */ SecurityDelegation     = 3
        }

        [Flags]
        internal enum SePrivileges : uint
        {
            /* SE_PRIVILEGE_ENABLED_BY_DEFAULT */ EnabledByDefault = 0x00000001,
            /* SE_PRIVILEGE_ENABLED            */ Enabled          = 0x00000002,
            /* SE_PRIVILEGE_REMOVED            */ Removed          = 0x00000004,
            /* SE_PRIVILEGE_USED_FOR_ACCESS    */ UsedForAccess    = 0x80000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status",
                Description = "CONTROL_ACCEPTED enumeration")]
        [Flags]
        internal enum ServiceAcceptedControls : uint
        {
            /* SERVICE_ACCEPT_STOP                  */ Stop                  = 0x00000001,
            /* SERVICE_ACCEPT_PAUSE_CONTINUE        */ PauseContinue         = 0x00000002,
            /* SERVICE_ACCEPT_SHUTDOWN              */ Shutdown              = 0x00000004,
            /* SERVICE_ACCEPT_PARAMCHANGE           */ ParamChange           = 0x00000008,
            /* SERVICE_ACCEPT_NETBINDCHANGE         */ NetBindChange         = 0x00000010,
            /* SERVICE_ACCEPT_HARDWAREPROFILECHANGE */ HardwareProfileChange = 0x00000020,
            /* SERVICE_ACCEPT_POWEREVENT            */ PowerEvent            = 0x00000040,
            /* SERVICE_ACCEPT_SESSIONCHANGE         */ SessionChange         = 0x00000080,
            /* SERVICE_ACCEPT_PRESHUTDOWN           */ PreShutdown           = 0x00000100,
            /* SERVICE_ACCEPT_TIMECHANGE            */ TimeChange            = 0x00000200,
            /* SERVICE_ACCEPT_TRIGGEREVENT          */ TriggerEvent          = 0x00000400,
            /* SERVICE_ACCEPT_USER_LOGOFF           */ UserLogoff            = 0x00000800,
            /* SERVICE_ACCEPT_LOWRESOURCES          */ LowResources          = 0x00002000,
            /* SERVICE_ACCEPT_SYSTEMLOWRESOURCES    */ SystemLowResources    = 0x00004000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/Services/service-security-and-access-rights",
                Description = "SERVICE_ACCESS_RIGHT enumeration")]
        [Flags]
        internal enum ServiceAccessRights : uint
        {
            /* STANDARD_RIGHTS_REQUIRED     */ StandardRightsRequired = StandardAccessRights.StandardRightsRequired,
            /* SERVICE_QUERY_CONFIG         */ QueryConfig            = 0x0001,
            /* SERVICE_CHANGE_CONFIG        */ ChangeConfig           = 0x0002,
            /* SERVICE_QUERY_STATUS         */ QueryStatus            = 0x0004,
            /* SERVICE_ENUMERATE_DEPENDENTS */ EnumerateDependents    = 0x0008,
            /* SERVICE_START                */ Start                  = 0x0010,
            /* SERVICE_STOP                 */ Stop                   = 0x0020,
            /* SERVICE_PAUSE_CONTINUE       */ PauseContinue          = 0x0040,
            /* SERVICE_INTERROGATE          */ Interrogate            = 0x0080,
            /* SERVICE_USER_DEFINED_CONTROL */ UserDefinedControl     = 0x0100,
            /* SERVICE_ALL_ACCESS           */ AllAccess              = StandardRightsRequired
                                                                      | QueryConfig
                                                                      | ChangeConfig
                                                                      | QueryStatus
                                                                      | EnumerateDependents
                                                                      | Start
                                                                      | Stop
                                                                      | PauseContinue
                                                                      | Interrogate
                                                                      | UserDefinedControl
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-queryserviceconfig2w",
                Description = "SERVICE_CONFIG enumeration")]
        internal enum ServiceConfig
        {
            /* SERVICE_CONFIG_DESCRIPTION              */ Description            =  1,
            /* SERVICE_CONFIG_FAILURE_ACTIONS          */ FailureActions         =  2,
            /* SERVICE_CONFIG_DELAYED_AUTO_START_INFO  */ DelayedAutoStartInfo   =  3,
            /* SERVICE_CONFIG_FAILURE_ACTIONS_FLAG     */ FailureActionsFlag     =  4,
            /* SERVICE_CONFIG_SERVICE_SID_INFO         */ ServiceSidInfo         =  5,
            /* SERVICE_CONFIG_REQUIRED_PRIVILEGES_INFO */ RequiredPrivilegesInfo =  6,
            /* SERVICE_CONFIG_PRESHUTDOWN_INFO         */ PreshutdownInfo        =  7,
            /* SERVICE_CONFIG_TRIGGER_INFO             */ TriggerInfo            =  8,
            /* SERVICE_CONFIG_PREFERRED_NODE           */ PreferredNode          =  9,
            /* SERVICE_CONFIG_LAUNCH_PROTECTED         */ LaunchProtected        = 12
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/Services/service-security-and-access-rights",
                Description = "SC_MANAGER_ACCESS_RIGHT enumeration")]
        [Flags]
        internal enum ServiceControlManagerAccessRights : uint
        {
            /* STANDARD_RIGHTS_REQUIRED      */ StandardRightsRequired = StandardAccessRights.StandardRightsRequired,
            /* SC_MANAGER_CONNECT            */ Connect                = 0x0001,
            /* SC_MANAGER_CREATE_SERVICE     */ CreateService          = 0x0002,
            /* SC_MANAGER_ENUMERATE_SERVICE  */ EnumerateService       = 0x0004,
            /* SC_MANAGER_LOCK               */ Lock                   = 0x0008,
            /* SC_MANAGER_QUERY_LOCK_STATUS  */ QueryLockStatus        = 0x0010,
            /* SC_MANAGER_MODIFY_BOOT_CONFIG */ ModifyBootConfig       = 0x0020,
            /* SC_MANAGER_ALL_ACCESS         */ AllAccess              = StandardRightsRequired
                                                                       | Connect
                                                                       | CreateService
                                                                       | EnumerateService
                                                                       | Lock
                                                                       | QueryLockStatus
                                                                       | ModifyBootConfig
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status",
                Description = "CURRENT_STATE enumeration")]
        internal enum ServiceCurrentState : uint
        {
            /* SERVICE_STOPPED          */ Stopped         = 0x00000001,
            /* SERVICE_START_PENDING    */ StartPending    = 0x00000002,
            /* SERVICE_STOP_PENDING     */ StopPending     = 0x00000003,
            /* SERVICE_RUNNING          */ Running         = 0x00000004,
            /* SERVICE_CONTINUE_PENDING */ ContinuePending = 0x00000005,
            /* SERVICE_PAUSE_PENDING    */ PausePending    = 0x00000006,
            /* SERVICE_PAUSED           */ Paused          = 0x00000007
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-changeserviceconfigw",
                Description = "ERROR_CONTROL_TYPE enumeration")]
        internal enum ServiceErrorControl : uint
        {
            /* SERVICE_ERROR_IGNORE   */ Ignore   = 0x00000000,
            /* SERVICE_ERROR_NORMAL   */ Normal   = 0x00000001,
            /* SERVICE_ERROR_SEVERE   */ Severe   = 0x00000002,
            /* SERVICE_ERROR_CRITICAL */ Critical = 0x00000003,
            /* SERVICE_NO_CHANGE      */ NoChange = 0xffffffff
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-changeserviceconfigw",
                Description = "START_TYPE enumeration")]
        internal enum ServiceStartType : uint
        {
            /* SERVICE_BOOT_START   */ BootStart   = 0x00000000,
            /* SERVICE_SYSTEM_START */ SystemStart = 0x00000001,
            /* SERVICE_AUTO_START   */ AutoStart   = 0x00000002,
            /* SERVICE_DEMAND_START */ DemandStart = 0x00000003,
            /* SERVICE_DISABLED     */ Disabled    = 0x00000004,
            /* SERVICE_NO_CHANGE    */ NoChange    = 0xffffffff
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status",
                Description = "SERVICE_TYPE enumeration")]
        [Flags]
        internal enum ServiceTypes : uint
        {
            /* SERVICE_KERNEL_DRIVER        */ KernelDriver        = 0x00000001,
            /* SERVICE_FILE_SYSTEM_DRIVER   */ FileSystemDriver    = 0x00000002,
            /* SERVICE_ADAPTER              */ Adapter             = 0x00000004,
            /* SERVICE_RECOGNIZER_DRIVER    */ RecognizerDriver    = 0x00000008,
            /* SERVICE_DRIVER               */ Driver              = KernelDriver
                                                                   | FileSystemDriver
                                                                   | RecognizerDriver,
            /* SERVICE_WIN32_OWN_PROCESS    */ Win32OwnProcess     = 0x00000010,
            /* SERVICE_WIN32_SHARE_PROCESS  */ Win32ShareProcess   = 0x00000020,
            /* SERVICE_WIN32                */ Win32               = Win32OwnProcess
                                                                   | Win32ShareProcess,
            /* SERVICE_USER_SERVICE         */ UserService         = 0x00000040,
            /* SERVICE_USERSERVICE_INSTANCE */ UserServiceInstance = 0x00000080,
            /* SERVICE_USER_SHARE_PROCESS   */ UserShareProcess    = UserService
                                                                   | Win32ShareProcess,
            /* SERVICE_USER_OWN_PROCESS     */ UserOwnProcess      = UserService
                                                                   | Win32OwnProcess,
            /* SERVICE_INTERACTIVE_PROCESS  */ InteractiveProcess  = 0x00000100,
            /* SERVICE_PKG_SERVICE          */ PkgService          = 0x00000200,
            /* SERVICE_TYPE_ALL             */ All                 = Win32
                                                                   | Adapter
                                                                   | Driver
                                                                   | InteractiveProcess
                                                                   | UserService
                                                                   | UserServiceInstance
                                                                   | PkgService,
            /* SERVICE_NO_CHANGE            */ NoChange            = 0xffffffff
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceregistrypropertyw",
                Description = "SPDRP enumeration")]
        internal enum SetupDeviceRegistryProperty : uint
        {
            /* SPDRP_DEVICEDESC                  */ DeviceDesc               =  0,
            /* SPDRP_HARDWAREID                  */ HardwareId               =  1,
            /* SPDRP_COMPATIBLEIDS               */ CompatibleIds            =  2,
            /* SPDRP_UNUSED0                     */ Unused0                  =  3,
            /* SPDRP_SERVICE                     */ Service                  =  4,
            /* SPDRP_UNUSED1                     */ Unused1                  =  5,
            /* SPDRP_UNUSED2                     */ Unused2                  =  6,
            /* SPDRP_CLASS                       */ Class                    =  7,
            /* SPDRP_CLASSGUID                   */ ClassGuid                =  8,
            /* SPDRP_DRIVER                      */ Driver                   =  9,
            /* SPDRP_CONFIGFLAGS                 */ ConfigFlags              = 10,
            /* SPDRP_MFG                         */ Mfg                      = 11,
            /* SPDRP_FRIENDLYNAME                */ FriendlyName             = 12,
            /* SPDRP_LOCATION_INFORMATION        */ LocationInformation      = 13,
            /* SPDRP_PHYSICAL_DEVICE_OBJECT_NAME */ PhysicalDeviceObjectName = 14,
            /* SPDRP_CAPABILITIES                */ Capabilities             = 15,
            /* SPDRP_UI_NUMBER                   */ UiNumber                 = 16,
            /* SPDRP_UPPERFILTERS                */ UpperFilters             = 17,
            /* SPDRP_LOWERFILTERS                */ LowerFilters             = 18,
            /* SPDRP_BUSTYPEGUID                 */ BusTypeGuid              = 19,
            /* SPDRP_LEGACYBUSTYPE               */ LegacyBusType            = 20,
            /* SPDRP_BUSNUMBER                   */ BusNumber                = 21,
            /* SPDRP_ENUMERATOR_NAME             */ EnumeratorName           = 22,
            /* SPDRP_SECURITY                    */ Security                 = 23,
            /* SPDRP_SECURITY_SDS                */ SecuritySds              = 24,
            /* SPDRP_DEVTYPE                     */ DevType                  = 25,
            /* SPDRP_EXCLUSIVE                   */ Exclusive                = 26,
            /* SPDRP_CHARACTERISTICS             */ Characteristics          = 27,
            /* SPDRP_ADDRESS                     */ Address                  = 28,
            /* SPDRP_UI_NUMBER_DESC_FORMAT       */ UiNumberDescFormat       = 29,
            /* SPDRP_DEVICE_POWER_DATA           */ PowerData                = 30,
            /* SPDRP_REMOVAL_POLICY              */ RemovalPolicy            = 31,
            /* SPDRP_REMOVAL_POLICY_HW_DEFAULT   */ RemovalPolicyHwDefault   = 32,
            /* SPDRP_REMOVAL_POLICY_OVERRIDE     */ RemovalPolicyOverride    = 33,
            /* SPDRP_INSTALL_STATE               */ InstallState             = 34,
            /* SPDRP_LOCATION_PATHS              */ LocationPaths            = 35,
            /* SPDRP_BASE_CONTAINERID            */ BaseContainerId          = 36
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify",
                Description = "SHCNE enumeration")]
        [Flags]
        internal enum ShellChangeNotifyEventIds : uint
        {
            /* SHCNE_RENAMEITEM       */ RenameItem         = 0x00000001,
            /* SHCNE_CREATE           */ Create             = 0x00000002,
            /* SHCNE_DELETE           */ Delete             = 0x00000004,
            /* SHCNE_MKDIR            */ MakeDirectory      = 0x00000008,
            /* SHCNE_RMDIR            */ RemoveDirectory    = 0x00000010,
            /* SHCNE_MEDIAINSERTED    */ MediaInserted      = 0x00000020,
            /* SHCNE_MEDIAREMOVED     */ MediaRemoved       = 0x00000040,
            /* SHCNE_DRIVEREMOVED     */ DriveRemoved       = 0x00000080,
            /* SHCNE_DRIVEADD         */ DriveAdd           = 0x00000100,
            /* SHCNE_NETSHARE         */ NetShare           = 0x00000200,
            /* SHCNE_NETUNSHARE       */ NetUnshare         = 0x00000400,
            /* SHCNE_ATTRIBUTES       */ Attributes         = 0x00000800,
            /* SHCNE_UPDATEDIR        */ UpdateDirectory    = 0x00001000,
            /* SHCNE_UPDATEITEM       */ UpdateItem         = 0x00002000,
            /* SHCNE_SERVERDISCONNECT */ ServerDisconnect   = 0x00004000,
            /* SHCNE_UPDATEIMAGE      */ UpdateImage        = 0x00008000,
            /* SHCNE_DRIVEADDGUI      */ DriveAddGui        = 0x00010000,
            /* SHCNE_RENAMEFOLDER     */ RenameFolder       = 0x00020000,
            /* SHCNE_FREESPACE        */ FreeSpace          = 0x00040000,
            /* SHCNE_EXTENDED_EVENT   */ ExtendedEvent      = 0x04000000,
            /* SHCNE_ASSOCCHANGED     */ AssociationChanged = 0x08000000,
            /* SHCNE_DISKEVENTS       */ DiskEvents         = 0x0002381f,
            /* SHCNE_GLOBALEVENTS     */ GlobalEvents       = 0x0c0581e0,
            /* SHCNE_ALLEVENTS        */ AllEvents          = 0x7fffffff,
            /* SHCNE_INTERRUPT        */ Interrupt          = 0x80000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify",
                Description = "SHCNE enumeration")]
        internal enum ShellChangeNotifyFlags : uint
        {
            /* SHCNF_IDLIST      */ IdList      = 0x0000,
            /* SHCNF_PATHA       */ PathA       = 0x0001,
            /* SHCNF_PRINTERA    */ PrinterA    = 0x0002,
            /* SHCNF_DWORD       */ Dword       = 0x0003,
            /* SHCNF_PATHW       */ PathW       = 0x0005,
            /* SHCNF_PRINTERW    */ PrinterW    = 0x0006,
            /* SHCNF_TYPE        */ Type        = 0x00FF,
            /* SHCNF_FLUSH       */ Flush       = 0x1000,
            /* SHCNF_FLUSHNOWAIT */ FlushNoWait = 0x3000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow")]
        internal enum ShowWindowCommand
        {
            /* SW_HIDE            */ Hide            =  0,
            /* SW_SHOWNORMAL      */ ShowNormal      =  1,
            /* SW_NORMAL          */ Normal          = ShowNormal,
            /* SW_SHOWMINIMIZED   */ ShowMinimized   =  2,
            /* SW_SHOWMAXIMIZED   */ ShowMaximized   =  3,
            /* SW_MAXIMIZE        */ Maximize        = ShowMaximized,
            /* SW_SHOWNOACTIVATE  */ ShowNoActivate  =  4,
            /* SW_SHOW            */ Show            =  5,
            /* SW_MINIMIZE        */ Minimize        =  6,
            /* SW_SHOWMINNOACTIVE */ ShowMinNoActive =  7,
            /* SW_SHOWNA          */ ShowNA          =  8,
            /* SW_RESTORE         */ Restore         =  9,
            /* SW_SHOWDEFAULT     */ ShowDefault     = 10,
            /* SW_FORCEMINIMIZE   */ ForceMinimize   = 11,
            /* SW_MAX             */ Max             = ForceMinimize
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes")]
        internal enum ShutdownReason : uint
        {
            /* SHTDN_REASON_UNKNOWN    */ Unknown   = ShutdownReasonMinor.None,
            /* SHTDN_REASON_LEGACY_API */ LegacyApi = ShutdownReasonMajor.LegacyApi
                                                    | ShutdownReasonFlags.Planned
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes")]
        [Flags]
        internal enum ShutdownReasonFlags : uint
        {
            /* SHTDN_REASON_FLAG_COMMENT_REQUIRED          */ CommentRequired        = 0x01000000,
            /* SHTDN_REASON_FLAG_DIRTY_PROBLEM_ID_REQUIRED */ DirtyProblemIdRequired = 0x02000000,
            /* SHTDN_REASON_FLAG_CLEAN_UI                  */ CleanUI                = 0x04000000,
            /* SHTDN_REASON_FLAG_DIRTY_UI                  */ DirtyUI                = 0x08000000,
            /* SHTDN_REASON_FLAG_MOBILE_UI_RESERVED        */ MobileUIReserved       = 0x10000000,
            /* SHTDN_REASON_FLAG_USER_DEFINED              */ UserDefined            = 0x40000000,
            /* SHTDN_REASON_FLAG_PLANNED                   */ Planned                = 0x80000000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes")]
        internal enum ShutdownReasonMajor : uint
        {
            /* SHTDN_REASON_MAJOR_OTHER           */ Other           = 0x00000000,
            /* SHTDN_REASON_MAJOR_NONE            */ None            = 0x00000000,
            /* SHTDN_REASON_MAJOR_HARDWARE        */ Hardware        = 0x00010000,
            /* SHTDN_REASON_MAJOR_OPERATINGSYSTEM */ OperatingSystem = 0x00020000,
            /* SHTDN_REASON_MAJOR_SOFTWARE        */ Software        = 0x00030000,
            /* SHTDN_REASON_MAJOR_APPLICATION     */ Application     = 0x00040000,
            /* SHTDN_REASON_MAJOR_SYSTEM          */ System          = 0x00050000,
            /* SHTDN_REASON_MAJOR_POWER           */ Power           = 0x00060000,
            /* SHTDN_REASON_MAJOR_POWER           */ LegacyApi       = 0x00070000
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes")]
        internal enum ShutdownReasonMinor : uint
        {
            /* SHTDN_REASON_MINOR_OTHER                 */ Oth                  = 0x00000000,
            /* SHTDN_REASON_MINOR_MAINTENANCE           */ Maintenance          = 0x00000001,
            /* SHTDN_REASON_MINOR_INSTALLATION          */ Installation         = 0x00000002,
            /* SHTDN_REASON_MINOR_UPGRADE               */ Upgrade              = 0x00000003,
            /* SHTDN_REASON_MINOR_RECONFIG              */ Reconfig             = 0x00000004,
            /* SHTDN_REASON_MINOR_HUNG                  */ Hung                 = 0x00000005,
            /* SHTDN_REASON_MINOR_UNSTABLE              */ Unstable             = 0x00000006,
            /* SHTDN_REASON_MINOR_DISK                  */ Disk                 = 0x00000007,
            /* SHTDN_REASON_MINOR_PROCESSOR             */ Processor            = 0x00000008,
            /* SHTDN_REASON_MINOR_NETWORKCARD           */ NetworkCard          = 0x00000009,
            /* SHTDN_REASON_MINOR_POWER_SUPPLY          */ PowerSupply          = 0x0000000a,
            /* SHTDN_REASON_MINOR_CORDUNPLUGGED         */ CordUnplugged        = 0x0000000b,
            /* SHTDN_REASON_MINOR_ENVIRONMENT           */ Environment          = 0x0000000c,
            /* SHTDN_REASON_MINOR_HARDWARE_DRIVER       */ HardwareDriver       = 0x0000000d,
            /* SHTDN_REASON_MINOR_OTHERDRIVER           */ OtherDriver          = 0x0000000e,
            /* SHTDN_REASON_MINOR_BLUESCREEN            */ BlueScreen           = 0x0000000f,
            /* SHTDN_REASON_MINOR_SERVICEPACK           */ ServicePack          = 0x00000010,
            /* SHTDN_REASON_MINOR_HOTFIX                */ Hotfix               = 0x00000011,
            /* SHTDN_REASON_MINOR_SECURITYFIX           */ SecurityFix          = 0x00000012,
            /* SHTDN_REASON_MINOR_SECURITY              */ Security             = 0x00000013,
            /* SHTDN_REASON_MINOR_NETWORK_CONNECTIVITY  */ NetworkConnectivity  = 0x00000014,
            /* SHTDN_REASON_MINOR_WMI                   */ Wmi                  = 0x00000015,
            /* SHTDN_REASON_MINOR_SERVICEPACK_UNINSTALL */ ServicePackUninstall = 0x00000016,
            /* SHTDN_REASON_MINOR_HOTFIX_UNINSTALL      */ HotfixUninstall      = 0x00000017,
            /* SHTDN_REASON_MINOR_SECURITYFIX_UNINSTALL */ SecurityFixUninstall = 0x00000018,
            /* SHTDN_REASON_MINOR_MMC                   */ Mmc                  = 0x00000019,
            /* SHTDN_REASON_MINOR_SYSTEMRESTORE         */ SystemRestore        = 0x0000001a,
            /* SHTDN_REASON_MINOR_TERMSRV               */ TermSrv              = 0x00000020,
            /* SHTDN_REASON_MINOR_DC_PROMOTION          */ DcPromotion          = 0x00000021,
            /* SHTDN_REASON_MINOR_DC_DEMOTION           */ DcDemotion           = 0x00000022,
            /* SHTDN_REASON_MINOR_NONE                  */ None                 = 0x000000ff
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_sid_name_use",
                Description = "SID_NAME_USE enumeration")]
        internal enum SidType
        {
            /* SidTypeUser           */ User           =  1,
            /* SidTypeGroup          */ Group          =  2,
            /* SidTypeDomain         */ Domain         =  3,
            /* SidTypeAlias          */ Alias          =  4,
            /* SidTypeWellKnownGroup */ WellKnownGroup =  5,
            /* SidTypeDeletedAccount */ DeletedAccount =  6,
            /* SidTypeInvalid        */ Invalid        =  7,
            /* SidTypeUnknown        */ Unknown        =  8,
            /* SidTypeComputer       */ Computer       =  9,
            /* SidTypeLabel          */ Label          = 10
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/standard-access-rights")]
        [Flags]
        internal enum StandardAccessRights : uint
        {
            /* DELETE                   */ Delete                 = 0x00010000,
            /* READ_CONTROL             */ ReadControl            = 0x00020000,
            /* WRITE_DAC                */ WriteDac               = 0x00040000,
            /* WRITE_OWNER              */ WriteOwner             = 0x00080000,
            /* STANDARD_RIGHTS_REQUIRED */ StandardRightsRequired = Delete
                                                                  | ReadControl
                                                                  | WriteDac
                                                                  | WriteOwner,
            /* STANDARD_RIGHTS_READ     */ StandardRightsRead     = ReadControl,
            /* STANDARD_RIGHTS_WRITE    */ StandardRightsWrite    = ReadControl,
            /* STANDARD_RIGHTS_EXECUTE  */ StandardRightsExecute  = ReadControl,
            /* SYNCHRONIZE              */ Synchronize            = 0x00100000,
            /* STANDARD_RIGHTS_ALL      */ StandardRightsAll      = StandardRightsRequired
                                                                  | Synchronize
        }

        internal enum SystemInformationClass : uint
        {
            /* SystemBasicInformation                        */ SystemBasicInformation,
            /* SystemProcessorInformation                    */ SystemProcessorInformation,
            /* SystemPerformanceInformation                  */ SystemPerformanceInformation,
            /* SystemTimeOfDayInformation                    */ SystemTimeOfDayInformation,
            /* SystemPathInformation                         */ SystemPathInformation,
            /* SystemProcessInformation                      */ SystemProcessInformation,
            /* SystemCallCountInformation                    */ SystemCallCountInformation,
            /* SystemDeviceInformation                       */ SystemDeviceInformation,
            /* SystemProcessorPerformanceInformation         */ SystemProcessorPerformanceInformation,
            /* SystemFlagsInformation                        */ SystemFlagsInformation,
            /* SystemCallTimeInformation                     */ SystemCallTimeInformation,
            /* SystemModuleInformation                       */ SystemModuleInformation,
            /* SystemLocksInformation                        */ SystemLocksInformation,
            /* SystemStackTraceInformation                   */ SystemStackTraceInformation,
            /* SystemPagedPoolInformation                    */ SystemPagedPoolInformation,
            /* SystemNonPagedPoolInformation                 */ SystemNonPagedPoolInformation,
            /* SystemHandleInformation                       */ SystemHandleInformation,
            /* SystemObjectInformation                       */ SystemObjectInformation,
            /* SystemPageFileInformation                     */ SystemPageFileInformation,
            /* SystemVdmInstemulInformation                  */ SystemVdmInstemulInformation,
            /* SystemVdmBopInformation                       */ SystemVdmBopInformation,
            /* SystemFileCacheInformation                    */ SystemFileCacheInformation,
            /* SystemPoolTagInformation                      */ SystemPoolTagInformation,
            /* SystemInterruptInformation                    */ SystemInterruptInformation,
            /* SystemDpcBehaviorInformation                  */ SystemDpcBehaviorInformation,
            /* SystemFullMemoryInformation                   */ SystemFullMemoryInformation,
            /* SystemLoadGdiDriverInformation                */ SystemLoadGdiDriverInformation,
            /* SystemUnloadGdiDriverInformation              */ SystemUnloadGdiDriverInformation,
            /* SystemTimeAdjustmentInformation               */ SystemTimeAdjustmentInformation,
            /* SystemSummaryMemoryInformation                */ SystemSummaryMemoryInformation,
            /* SystemMirrorMemoryInformation                 */ SystemMirrorMemoryInformation,
            /* SystemPerformanceTraceInformation             */ SystemPerformanceTraceInformation,
            /* SystemObsolete0                               */ SystemObsolete0,
            /* SystemExceptionInformation                    */ SystemExceptionInformation,
            /* SystemCrashDumpStateInformation               */ SystemCrashDumpStateInformation,
            /* SystemKernelDebuggerInformation               */ SystemKernelDebuggerInformation,
            /* SystemContextSwitchInformation                */ SystemContextSwitchInformation,
            /* SystemRegistryQuotaInformation                */ SystemRegistryQuotaInformation,
            /* SystemExtendServiceTableInformation           */ SystemExtendServiceTableInformation,
            /* SystemPrioritySeperation                      */ SystemPrioritySeperation,
            /* SystemVerifierAddDriverInformation            */ SystemVerifierAddDriverInformation,
            /* SystemVerifierRemoveDriverInformation         */ SystemVerifierRemoveDriverInformation,
            /* SystemProcessorIdleInformation                */ SystemProcessorIdleInformation,
            /* SystemLegacyDriverInformation                 */ SystemLegacyDriverInformation,
            /* SystemCurrentTimeZoneInformation              */ SystemCurrentTimeZoneInformation,
            /* SystemLookasideInformation                    */ SystemLookasideInformation,
            /* SystemTimeSlipNotification                    */ SystemTimeSlipNotification,
            /* SystemSessionCreate                           */ SystemSessionCreate,
            /* SystemSessionDetach                           */ SystemSessionDetach,
            /* SystemSessionInformation                      */ SystemSessionInformation,
            /* SystemRangeStartInformation                   */ SystemRangeStartInformation,
            /* SystemVerifierInformation                     */ SystemVerifierInformation,
            /* SystemVerifierThunkExtend                     */ SystemVerifierThunkExtend,
            /* SystemSessionProcessInformation               */ SystemSessionProcessInformation,
            /* SystemLoadGdiDriverInSystemSpace              */ SystemLoadGdiDriverInSystemSpace,
            /* SystemNumaProcessorMap                        */ SystemNumaProcessorMap,
            /* SystemPrefetcherInformation                   */ SystemPrefetcherInformation,
            /* SystemExtendedProcessInformation              */ SystemExtendedProcessInformation,
            /* SystemRecommendedSharedDataAlignment          */ SystemRecommendedSharedDataAlignment,
            /* SystemComPlusPackage                          */ SystemComPlusPackage,
            /* SystemNumaAvailableMemory                     */ SystemNumaAvailableMemory,
            /* SystemProcessorPowerInformation               */ SystemProcessorPowerInformation,
            /* SystemEmulationBasicInformation               */ SystemEmulationBasicInformation,
            /* SystemEmulationProcessorInformation           */ SystemEmulationProcessorInformation,
            /* SystemExtendedHandleInformation               */ SystemExtendedHandleInformation,
            /* SystemLostDelayedWriteInformation             */ SystemLostDelayedWriteInformation,
            /* SystemBigPoolInformation                      */ SystemBigPoolInformation,
            /* SystemSessionPoolTagInformation               */ SystemSessionPoolTagInformation,
            /* SystemSessionMappedViewInformation            */ SystemSessionMappedViewInformation,
            /* SystemHotpatchInformation                     */ SystemHotpatchInformation,
            /* SystemObjectSecurityMode                      */ SystemObjectSecurityMode,
            /* SystemWatchdogTimerHandler                    */ SystemWatchdogTimerHandler,
            /* SystemWatchdogTimerInformation                */ SystemWatchdogTimerInformation,
            /* SystemLogicalProcessorInformation             */ SystemLogicalProcessorInformation,
            /* SystemWow64SharedInformation                  */ SystemWow64SharedInformation,
            /* SystemRegisterFirmwareTableInformationHandler */ SystemRegisterFirmwareTableInformationHandler,
            /* SystemFirmwareTableInformation                */ SystemFirmwareTableInformation,
            /* SystemModuleInformationEx                     */ SystemModuleInformationEx,
            /* SystemVerifierTriageInformation               */ SystemVerifierTriageInformation,
            /* SystemSuperfetchInformation                   */ SystemSuperfetchInformation,
            /* SystemMemoryListInformation                   */ SystemMemoryListInformation,
            /* SystemFileCacheInformationEx                  */ SystemFileCacheInformationEx,
            /* MaxSystemInfoClass                            */ MaxSystemInfoClass
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/access-rights-for-access-token-objects")]
        [Flags]
        internal enum TokenAccessRights : uint
        {
            /* TOKEN_ASSIGN_PRIMARY    */ AssignPrimary    =  0x0001,
            /* TOKEN_DUPLICATE         */ Duplicate        =  0x0002,
            /* TOKEN_IMPERSONATE       */ Impersonate      =  0x0004,
            /* TOKEN_QUERY             */ Query            =  0x0008,
            /* TOKEN_QUERY_SOURCE      */ QuerySource      =  0x0010,
            /* TOKEN_ADJUST_PRIVILEGES */ AdjustPrivileges =  0x0020,
            /* TOKEN_ADJUST_GROUPS     */ AdjustGroups     =  0x0040,
            /* TOKEN_ADJUST_DEFAULT    */ AdjustDefault    =  0x0080,
            /* TOKEN_ADJUST_SESSIONID  */ AdjustSessionId  =  0x0100,
            /* TOKEN_EXECUTE           */ Execute          = 0x20000,
            /* TOKEN_READ              */ Read             = 0x20000
                                                           | Query,
            /* TOKEN_WRITE             */ Write            = 0x20000
                                                           | AdjustPrivileges
                                                           | AdjustGroups
                                                           | AdjustDefault,
            /* TOKEN_ALL_ACCESS        */ AllAccess        = 0xf0000
                                                           | AssignPrimary
                                                           | Duplicate
                                                           | Impersonate
                                                           | Query
                                                           | QuerySource
                                                           | AdjustPrivileges
                                                           | AdjustGroups
                                                           | AdjustDefault
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_token_information_class",
                Description = "TOKEN_INFORMATION_CLASS enumeration")]
        internal enum TokenInformationClass : uint
        {
            /* TokenUser                            */ User = 1,
            /* TokenGroups                          */ Groups,
            /* TokenPrivileges                      */ Privileges,
            /* TokenOwner                           */ Owner,
            /* TokenPrimaryGroup                    */ PrimaryGroup,
            /* TokenDefaultDacl                     */ DefaultDacl,
            /* TokenSource                          */ Source,
            /* TokenType                            */ Type,
            /* TokenImpersonationLevel              */ ImpersonationLevel,
            /* TokenStatistics                      */ Statistics,
            /* TokenRestrictedSids                  */ RestrictedSids,
            /* TokenSessionId                       */ SessionId,
            /* TokenGroupsAndPrivileges             */ GroupsAndPrivileges,
            /* TokenSessionReference                */ SessionReference,
            /* TokenSandBoxInert                    */ SandBoxInert,
            /* TokenAuditPolicy                     */ AuditPolicy,
            /* TokenOrigin                          */ Origin,
            /* TokenElevationType                   */ ElevationType,
            /* TokenLinkedToken                     */ LinkedToken,
            /* TokenElevation                       */ Elevation,
            /* TokenHasRestrictions                 */ HasRestrictions,
            /* TokenAccessInformation               */ AccessInformation,
            /* TokenVirtualizationAllowed           */ VirtualizationAllowed,
            /* TokenVirtualizationEnabled           */ VirtualizationEnabled,
            /* TokenIntegrityLevel                  */ IntegrityLevel,
            /* TokenUIAccess                        */ UIAccess,
            /* TokenMandatoryPolicy                 */ MandatoryPolicy,
            /* TokenLogonSid                        */ LogonSid,
            /* TokenIsAppContainer                  */ IsAppContainer,
            /* TokenCapabilities                    */ Capabilities,
            /* TokenAppContainerSid                 */ AppContainerSid,
            /* TokenAppContainerNumber              */ AppContainerNumber,
            /* TokenUserClaimAttributes             */ UserClaimAttributes,
            /* TokenDeviceClaimAttributes           */ DeviceClaimAttributes,
            /* TokenRestrictedUserClaimAttributes   */ RestrictedUserClaimAttributes,
            /* TokenRestrictedDeviceClaimAttributes */ RestrictedDeviceClaimAttributes,
            /* TokenDeviceGroups                    */ DeviceGroups,
            /* TokenRestrictedDeviceGroups          */ RestrictedDeviceGroups,
            /* TokenSecurityAttributes              */ SecurityAttributes,
            /* TokenIsRestricted                    */ IsRestricted,
            /* TokenProcessTrustLevel               */ ProcessTrustLevel,
            /* TokenPrivateNameSpace                */ PrivateNameSpace,
            /* TokenSingletonAttributes             */ SingletonAttributes,
            /* TokenBnoIsolation                    */ BnoIsolation,
            /* TokenChildProcessFlags               */ ChildProcessFlags,
            /* TokenIsLessPrivilegedAppContainer    */ IsLessPrivilegedAppContainer,
            /* MaxTokenInfoClass                    */ MaxTokenInfoClass
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_token_type",
                Description = "TOKEN_TYPE enumeration")]
        internal enum TokenType : uint
        {
            /* TokenPrimary       */ TokenPrimary       = 1,
            /* TokenImpersonation */ TokenImpersonation = 2
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wintrust/nf-wintrust-winverifytrust")]
        internal enum TrustError : uint
        {
            /* TRUST_E_PROVIDER_UNKNOWN     */ ProviderUnknown    = 0x800B0001,
            /* TRUST_E_ACTION_UNKNOWN       */ ActionUnknown      = 0x800B0002,
            /* TRUST_E_SUBJECT_FORM_UNKNOWN */ SubjectFormUnknown = 0x800B0003,
            /* TRUST_E_SUBJECT_NOT_TRUSTED  */ SubjectNotTrusted  = 0x800B0004
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ne-wtsapi32-_wts_connectstate_class",
                Description = "WTS_CONNECTSTATE_CLASS enumeration")]
        internal enum WindowsTerminalServiceConnectState
        {
            /* WTSActive       */ Active       = 0,
            /* WTSConnected    */ Connected    = 1,
            /* WTSConnectQuery */ ConnectQuery = 2,
            /* WTSShadow       */ Shadow       = 3,
            /* WTSDisconnected */ Disconnected = 4,
            /* WTSIdle         */ Idle         = 5,
            /* WTSListen       */ Listen       = 6,
            /* WTSReset        */ Reset        = 7,
            /* WTSDown         */ Down         = 8,
            /* WTSInit         */ Init         = 9
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ne-wtsapi32-_wts_info_class",
                Description = "WTS_INFO_CLASS enumeration")]
        internal enum WindowsTerminalServiceInfo
        {
            /* WTSInitialProgram     */ InitialProgram     =  0,
            /* WTSApplicationName    */ ApplicationName    =  1,
            /* WTSWorkingDirectory   */ WorkingDirectory   =  2,
            /* WTSOEMId              */ OemId              =  3,
            /* WTSSessionId          */ SessionId          =  4,
            /* WTSUserName           */ UserName           =  5,
            /* WTSWinStationName     */ WinStationName     =  6,
            /* WTSDomainName         */ DomainName         =  7,
            /* WTSConnectState       */ ConnectState       =  8,
            /* WTSClientBuildNumber  */ ClientBuildNumber  =  9,
            /* WTSClientName         */ ClientName         = 10,
            /* WTSClientDirectory    */ ClientDirectory    = 11,
            /* WTSClientProductId    */ ClientProductId    = 12,
            /* WTSClientHardwareId   */ ClientHardwareId   = 13,
            /* WTSClientAddress      */ ClientAddress      = 14,
            /* WTSClientDisplay      */ ClientDisplay      = 15,
            /* WTSClientProtocolType */ ClientProtocolType = 16,
            /* WTSIdleTime           */ IdleTime           = 17,
            /* WTSLogonTime          */ LogonTime          = 18,
            /* WTSIncomingBytes      */ IncomingBytes      = 19,
            /* WTSOutgoingBytes      */ OutgoingBytes      = 20,
            /* WTSIncomingFrames     */ IncomingFrames     = 21,
            /* WTSOutgoingFrames     */ OutgoingFrames     = 22,
            /* WTSClientInfo         */ ClientInfo         = 23,
            /* WTSSessionInfo        */ SessionInfo        = 24,
            /* WTSSessionInfoEx      */ SessionInfoEx      = 25,
            /* WTSConfigInfo         */ ConfigInfo         = 26,
            /* WTSValidationInfo     */ ValidationInfo     = 27,
            /* WTSSessionAddressV4   */ SessionAddressV4   = 28,
            /* WTSIsRemoteSession    */ IsRemoteSession    = 29
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WTD_CHOICE enumeration")]
        internal enum WinTrustDataChoice : uint
        {
            /* WTD_CHOICE_FILE    */ File    = 1,
            /* WTD_CHOICE_CATALOG */ Catalog = 2,
            /* WTD_CHOICE_BLOB    */ Blob    = 3,
            /* WTD_CHOICE_SIGNER  */ Signer  = 4,
            /* WTD_CHOICE_CERT    */ Cert    = 5
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WTD_PROVIDERFLAG enumeration")]
        [Flags]
        internal enum WinTrustDataProviderFlags : uint
        {
            /* WTD_USE_IE4_TRUST_FLAG                  */ UseIe4TrustFlag                 =     1,
            /* WTD_NO_IE4_CHAIN_FLAG                   */ NoIe4ChainFlag                  =     2,
            /* WTD_NO_POLICY_USAGE_FLAG                */ NoPolicyUsageFlag               =     4,
            /* WTD_REVOCATION_CHECK_NONE               */ RevocationCheckNone             =    16,
            /* WTD_REVOCATION_CHECK_END_CERT           */ RevocationCheckEndCert          =    32,
            /* WTD_REVOCATION_CHECK_CHAIN              */ RevocationCheckChain            =    64,
            /* WTD_REVOCATION_CHECK_CHAIN_EXCLUDE_ROOT */ RevocationCheckChainExcludeRoot =   128,
            /* WTD_SAFER_FLAG                          */ SaferFlag                       =   256,
            /* WTD_HASH_ONLY_FLAG                      */ HashOnlyFlag                    =   512,
            /* WTD_USE_DEFAULT_OSVER_CHECK             */ UseDefaultOsVerCheck            =  1024,
            /* WTD_LIFETIME_SIGNING_FLAG               */ LifetimeSigningFlag             =  2048,
            /* WTD_CACHE_ONLY_URL_RETRIEVAL            */ CacheOnlyUrlRetrieval           =  4096,
            /* WTD_DISABLE_MD2_MD4                     */ DisableMd2Md4                   =  8192,
            /* WTD_MOTW                                */ MarkOfTheWeb                    = 16384
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WTD_REVOKE enumeration")]
        internal enum WinTrustDataRevoke : uint
        {
            /* WTD_REVOKE_NONE       */ None       = 0,
            /* WTD_REVOKE_WHOLECHAIN */ WholeChain = 1
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WTD_STATEACTION enumeration")]
        internal enum WinTrustDataStateAction : uint
        {
            /* WTD_STATEACTION_IGNORE           */ Ignore         = 0,
            /* WTD_STATEACTION_VERIFY           */ Verify         = 1,
            /* WTD_STATEACTION_CLOSE            */ Close          = 2,
            /* WTD_STATEACTION_AUTO_CACHE       */ AutoCache      = 3,
            /* WTD_STATEACTION_AUTO_CACHE_FLUSH */ AutoCacheFlush = 4
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WTD_UI enumeration")]
        internal enum WinTrustDataUI : uint
        {
            /* WTD_UI_ALL    */ All    = 1,
            /* WTD_UI_NONE   */ None   = 2,
            /* WTD_UI_NOBAD  */ NoBad  = 3,
            /* WTD_UI_NOGOOD */ NoGood = 4
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WTD_UICONTEXT enumeration")]
        internal enum WinTrustDataUIContext : uint
        {
            /* WTD_UICONTEXT_EXECUTE */ Execute = 0,
            /* WTD_UICONTEXT_INSTALL */ Install = 1
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/ns-bits1_5-bg_auth_credentials",
                Description = "BG_AUTH_CREDENTIALS structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct BitsAuthCredentials
        {
            internal /* BG_AUTH_TARGET            */ BitsAuthTarget Target;
            internal /* BG_AUTH_SCHEME            */ BitsAuthScheme Scheme;
            internal /* BG_AUTH_CREDENTIALS_UNION */ BitsAuthCredentialsUnion Credentials;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/ns-bits1_5-bg_auth_credentials_union",
                Description = "BG_AUTH_CREDENTIALS_UNION structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct BitsAuthCredentialsUnion
        {
            internal /* BG_BASIC_CREDENTIALS */ BitsBasicCredentials Basic;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/ns-bits1_5-bg_basic_credentials",
                Description = "BG_BASIC_CREDENTIALS structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct BitsBasicCredentials
        {
            internal /* LPWSTR */ string UserName;
            internal /* LPWSTR */ string Password;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ns-bits-bg_file_info",
                Description = "BG_FILE_INFO structure")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct BitsFileInfo
        {
            internal /* LPWSTR */ string RemoteName;
            internal /* LPWSTR */ string LocalName;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ns-bits-bg_file_progress",
                Description = "BG_FILE_PROGRESS structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct BitsFileProgress : IEquatable<BitsFileProgress>
        {
            internal readonly /* UINT64 */ ulong BytesTotal;
            internal readonly /* UINT64 */ ulong BytesTransferred;
            internal readonly /* BOOL   */ bool Completed;

            public static bool operator ==(BitsFileProgress left, BitsFileProgress right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(BitsFileProgress left, BitsFileProgress right)
            {
                return !Equals(left, right);
            }

            public bool Equals(BitsFileProgress other)
            {
                return BytesTotal == other.BytesTotal
                        && BytesTransferred == other.BytesTransferred
                        && Completed == other.Completed;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is BitsFileProgress && Equals((BitsFileProgress) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = BytesTotal.GetHashCode();
                    hashCode = (hashCode * 397) ^ BytesTransferred.GetHashCode();
                    hashCode = (hashCode * 397) ^ Completed.GetHashCode();
                    return hashCode;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ns-bits-bg_job_progress",
                Description = "BG_JOB_PROGRESS structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct BitsJobProgress : IEquatable<BitsJobProgress>
        {
            internal readonly /* UINT64 */ ulong BytesTotal;
            internal readonly /* UINT64 */ ulong BytesTransferred;
            internal readonly /* ULONG  */ uint FilesTotal;
            internal readonly /* ULONG  */ uint FilesTransferred;

            public static bool operator ==(BitsJobProgress left, BitsJobProgress right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(BitsJobProgress left, BitsJobProgress right)
            {
                return !Equals(left, right);
            }

            public bool Equals(BitsJobProgress other)
            {
                return BytesTotal == other.BytesTotal
                        && BytesTransferred == other.BytesTransferred
                        && FilesTotal == other.FilesTotal
                        && FilesTransferred == other.FilesTransferred;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is BitsJobProgress && Equals((BitsJobProgress) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = BytesTotal.GetHashCode();
                    hashCode = (hashCode * 397) ^ BytesTransferred.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) FilesTotal;
                    hashCode = (hashCode * 397) ^ (int) FilesTransferred;
                    return hashCode;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits1_5/ns-bits1_5-bg_job_reply_progress",
                Description = "BG_JOB_REPLY_PROGRESS structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct BitsJobReplyProgress : IEquatable<BitsJobReplyProgress>
        {
            internal readonly /* UINT64 */ ulong BytesTotal;
            internal readonly /* UINT64 */ ulong BytesTransferred;

            public static bool operator ==(BitsJobReplyProgress left, BitsJobReplyProgress right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(BitsJobReplyProgress left, BitsJobReplyProgress right)
            {
                return !Equals(left, right);
            }

            public bool Equals(BitsJobReplyProgress other)
            {
                return BytesTotal == other.BytesTotal
                        && BytesTransferred == other.BytesTransferred;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is BitsJobReplyProgress && Equals((BitsJobReplyProgress) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (BytesTotal.GetHashCode() * 397) ^ BytesTransferred.GetHashCode();
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/bits/ns-bits-bg_job_times",
                Description = "BG_JOB_TIMES structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct BitsJobTimes
        {
            internal /* FILETIME */ FileTime CreationTime;
            internal /* FILETIME */ FileTime ModificationTime;
            internal /* FILETIME */ FileTime TransferCompletionTime;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-_display_devicew",
                Description = "DISPLAY_DEVICEW structure")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DisplayDeviceW : IEquatable<DisplayDeviceW>
        {
                                                                  internal          /* DWORD      */ int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]  internal readonly /* WCHAR[32]  */ string deviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal readonly /* WCHAR[128] */ string deviceString;
                                                                  internal readonly /* DWORD      */ DisplayDeviceStateFlags stateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal readonly /* WCHAR[128] */ string deviceId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal readonly /* WCHAR[128] */ string deviceKey;

            public static bool operator ==(DisplayDeviceW left, DisplayDeviceW right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(DisplayDeviceW left, DisplayDeviceW right)
            {
                return !Equals(left, right);
            }

            public bool Equals(DisplayDeviceW other)
            {
                return deviceName == other.deviceName
                        && deviceString == other.deviceString
                        && stateFlags == other.stateFlags
                        && deviceId == other.deviceId
                        && deviceKey == other.deviceKey;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is DisplayDeviceW && Equals((DisplayDeviceW) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (deviceName != null ? deviceName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (deviceString != null ? deviceString.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int) stateFlags;
                    hashCode = (hashCode * 397) ^ (deviceId != null ? deviceId.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (deviceKey != null ? deviceKey.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/ns-dxgi-dxgi_adapter_desc",
                Description = "DXGI_ADAPTER_DESC structure")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DxgiAdapterDescription : IEquatable<DxgiAdapterDescription>
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal readonly /* WCHAR[128] */ string description;
                                                                  internal readonly /* UINT       */ uint vendorId;
                                                                  internal readonly /* UINT       */ uint deviceId;
                                                                  internal readonly /* UINT       */ uint subSysId;
                                                                  internal readonly /* UINT       */ uint revision;
                                                                  internal readonly /* SIZE_T     */ UIntPtr dedicatedVideoMemory;
                                                                  internal readonly /* SIZE_T     */ UIntPtr dedicatedSystemMemory;
                                                                  internal readonly /* SIZE_T     */ UIntPtr sharedSystemMemory;
                                                                  internal readonly /* LUID       */ long adapterLuid;

            public static bool operator ==(DxgiAdapterDescription left, DxgiAdapterDescription right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(DxgiAdapterDescription left, DxgiAdapterDescription right)
            {
                return !Equals(left, right);
            }

            public bool Equals(DxgiAdapterDescription other)
            {
                return description == other.description
                        && vendorId == other.vendorId
                        && deviceId == other.deviceId
                        && subSysId == other.subSysId
                        && revision == other.revision
                        && dedicatedVideoMemory.Equals(other.dedicatedVideoMemory)
                        && dedicatedSystemMemory.Equals(other.dedicatedSystemMemory)
                        && sharedSystemMemory.Equals(other.sharedSystemMemory)
                        && adapterLuid == other.adapterLuid;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is DxgiAdapterDescription && Equals((DxgiAdapterDescription) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (description != null ? description.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (int) vendorId;
                    hashCode = (hashCode * 397) ^ (int) deviceId;
                    hashCode = (hashCode * 397) ^ (int) subSysId;
                    hashCode = (hashCode * 397) ^ (int) revision;
                    hashCode = (hashCode * 397) ^ dedicatedVideoMemory.GetHashCode();
                    hashCode = (hashCode * 397) ^ dedicatedSystemMemory.GetHashCode();
                    hashCode = (hashCode * 397) ^ sharedSystemMemory.GetHashCode();
                    hashCode = (hashCode * 397) ^ adapterLuid.GetHashCode();
                    return hashCode;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/ns-dxgi-dxgi_output_desc",
                Description = "DXGI_OUTPUT_DESC structure")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DxgiOutputDescription : IEquatable<DxgiOutputDescription>
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal readonly /* WCHAR[32]          */ string deviceName;
                                                                 internal readonly /* RECT               */ Rectangle desktopCoordinates;
                                                                 internal readonly /* BOOL               */ bool attachedToDesktop;
                                                                 internal readonly /* DXGI_MODE_ROTATION */ DxgiModeRotation rotation;
                                                                 internal readonly /* HMONITOR           */ IntPtr monitor;

            public static bool operator ==(DxgiOutputDescription left, DxgiOutputDescription right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(DxgiOutputDescription left, DxgiOutputDescription right)
            {
                return !Equals(left, right);
            }

            public bool Equals(DxgiOutputDescription other)
            {
                return deviceName == other.deviceName
                        && desktopCoordinates.Equals(other.desktopCoordinates)
                        && attachedToDesktop == other.attachedToDesktop
                        && rotation == other.rotation
                        && monitor.Equals(other.monitor);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is DxgiOutputDescription && Equals((DxgiOutputDescription) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (deviceName != null ? deviceName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ desktopCoordinates.GetHashCode();
                    hashCode = (hashCode * 397) ^ attachedToDesktop.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) rotation;
                    hashCode = (hashCode * 397) ^ monitor.GetHashCode();
                    return hashCode;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-filetime",
                Description = "FILETIME structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct FileTime : IEquatable<FileTime>
        {
            internal readonly /* DWORD */ uint dwLowDateTime;
            internal readonly /* DWORD */ uint dwHighDateTime;

            public static bool operator ==(FileTime left, FileTime right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(FileTime left, FileTime right)
            {
                return !Equals(left, right);
            }

            public bool Equals(FileTime other)
            {
                return dwLowDateTime == other.dwLowDateTime
                        && dwHighDateTime == other.dwHighDateTime;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is FileTime && Equals((FileTime) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return ((int) dwLowDateTime * 397) ^ (int) dwHighDateTime;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidpi/ns-hidpi-_hidp_caps")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct HidDeviceCapability
        {
                                                                  internal /* USAGE      */ ushort usage;
                                                                  internal /* USAGE      */ ushort usagePage;
                                                                  internal /* USHORT     */ ushort inputReportByteLength;
                                                                  internal /* USHORT     */ ushort outputReportByteLength;
                                                                  internal /* USHORT     */ ushort featureReportByteLength;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 17)] internal /* USHORT[17] */ ushort[] reserved;
                                                                  internal /* USHORT     */ ushort numberLinkCollectionNodes;
                                                                  internal /* USHORT     */ ushort numberInputButtonCaps;
                                                                  internal /* USHORT     */ ushort numberInputValueCaps;
                                                                  internal /* USHORT     */ ushort numberInputDataIndices;
                                                                  internal /* USHORT     */ ushort numberOutputButtonCaps;
                                                                  internal /* USHORT     */ ushort numberOutputValueCaps;
                                                                  internal /* USHORT     */ ushort numberOutputDataIndices;
                                                                  internal /* USHORT     */ ushort numberFeatureButtonCaps;
                                                                  internal /* USHORT     */ ushort numberFeatureValueCaps;
                                                                  internal /* USHORT     */ ushort numberFeatureDataIndices;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfoexw",
                Description = "MONITORINFOEXW structure")]
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct MonitorInfoExW : IEquatable<MonitorInfoExW>
        {
                                                                 internal          /* DWORD     */ int size;
                                                                 internal readonly /* RECT      */ Rectangle rcMonitor;
                                                                 internal readonly /* RECT      */ Rectangle rcWork;
                                                                 internal readonly /* DWORD     */ uint flags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal readonly /* WCHAR[32] */ string DeviceName;

            public static bool operator ==(MonitorInfoExW left, MonitorInfoExW right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(MonitorInfoExW left, MonitorInfoExW right)
            {
                return !Equals(left, right);
            }

            public bool Equals(MonitorInfoExW other)
            {
                return rcMonitor.Equals(other.rcMonitor)
                        && rcWork.Equals(other.rcWork)
                        && flags == other.flags
                        && DeviceName == other.DeviceName;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is MonitorInfoExW && Equals((MonitorInfoExW) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = rcMonitor.GetHashCode();
                    hashCode = (hashCode * 397) ^ rcWork.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) flags;
                    hashCode = (hashCode * 397) ^ (DeviceName != null ? DeviceName.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/ns-processthreadsapi-process_information",
                Description = "PROCESS_INFORMATION structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct ProcessInformation
        {
            internal /* HANDLE */ IntPtr hProcess;
            internal /* HANDLE */ IntPtr hThread;
            internal /* DWORD  */ int dwProcessID;
            internal /* DWORD  */ int dwThreadID;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/wtypes/ns-wtypes-propertykey",
                Description = "PROPERTYKEY structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct PropertyKey : IEquatable<PropertyKey>
        {
            internal readonly /* GUID  */ Guid fmtid;
            internal readonly /* DWORD */ uint pid;

            internal PropertyKey(
                    Guid fmtid,
                    uint pid)
            {
                this.fmtid = fmtid;
                this.pid = pid;
            }

            public static bool operator ==(PropertyKey left, PropertyKey right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(PropertyKey left, PropertyKey right)
            {
                return !Equals(left, right);
            }

            public bool Equals(PropertyKey other)
            {
                return fmtid.Equals(other.fmtid)
                        && pid == other.pid;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is PropertyKey && Equals((PropertyKey) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (fmtid.GetHashCode() * 397) ^ (int) pid;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/propidlbase/ns-propidlbase-propvariant",
                Description = "PROPVARIANT structure")]
        [StructLayout(LayoutKind.Explicit)]
        internal struct PropVariant : IEquatable<PropVariant>
        {
            [FieldOffset(0)]
            internal readonly /* VARTYPE */ ushort vt;
            [FieldOffset(8)]
            internal readonly /* union   */ IntPtr unionMember;

            internal PropVariant(Guid data)
            {
                vt = (ushort) VarEnum.VT_CLSID;
                var dataInBytes = data.ToByteArray();
                unionMember = Marshal.AllocCoTaskMem(dataInBytes.Length);
                Marshal.Copy(dataInBytes, 0, unionMember, dataInBytes.Length);
            }

            internal PropVariant(string data)
            {
                vt = (ushort) VarEnum.VT_LPWSTR;
                unionMember = Marshal.StringToCoTaskMemUni(data);
            }

            public static bool operator ==(PropVariant left, PropVariant right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(PropVariant left, PropVariant right)
            {
                return !Equals(left, right);
            }

            public bool Equals(PropVariant other)
            {
                return vt == other.vt
                        && unionMember.Equals(other.unionMember);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is PropVariant && Equals((PropVariant) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    return (vt.GetHashCode() * 397) ^ unionMember.GetHashCode();
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-query_service_configw",
                Description = "QUERY_SERVICE_CONFIG structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct QueryServiceConfig
        {
            internal /* DWORD  */ ServiceTypes dwServiceType;
            internal /* DWORD  */ ServiceStartType dwStartType;
            internal /* DWORD  */ ServiceErrorControl dwErrorControl;
            internal /* LPTSTR */ string lpBinaryPathName;
            internal /* LPTSTR */ string lpLoadOrderGroup;
            internal /* DWORD  */ uint dwTagId;
            internal /* LPTSTR */ string lpDependencies;
            internal /* LPTSTR */ string lpServiceStartName;
            internal /* LPTSTR */ string lpDisplayName;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect",
                Description = "RECT structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct Rectangle
        {
            internal /* LONG */ int left;
            internal /* LONG */ int top;
            internal /* LONG */ int right;
            internal /* LONG */ int bottom;
        }

        [ExternalReference("https://msdn.microsoft.com/en-us/library/windows/desktop/aa379560.aspx",
                Description = "SECURITY_ATTRIBUTES")]
        [StructLayout(LayoutKind.Sequential)]
        internal class SecurityAttributes
        {
            internal /* DWORD  */ int nLength;
            internal /* LPVOID */ IntPtr lpSecurityDescriptor;
            internal /* BOOL   */ bool bInheritHandle;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winsvc/ns-winsvc-service_delayed_auto_start_info",
                Description = "SERVICE_DELAYED_AUTO_START_INFO structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct ServiceDelayedAutoStartInfo
        {
            internal /* BOOL */ bool fDelayedAutostart;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status",
                Description = "SERVICE_STATUS structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct ServiceStatus
        {
            internal /* DWORD */ ServiceTypes dwServiceType;
            internal /* DWORD */ ServiceCurrentState dwCurrentState;
            internal /* DWORD */ ServiceAcceptedControls dwControlAccepted;
            internal /* DWORD */ uint dwWin32ExitCode;
            internal /* DWORD */ uint dwServiceSpecificExitCode;
            internal /* DWORD */ uint dwCheckPoint;
            internal /* DWORD */ uint dwWaitHint;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/ns-setupapi-_sp_devinfo_data",
                Description = "SP_DEVINFO_DATA structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct SetupDeviceInfoData
        {
            internal /* DWORD     */ uint cbSize;
            internal /* GUID      */ Guid classGuid;
            internal /* DWORD     */ uint devInst;
            internal /* ULONG_PTR */ IntPtr reserved;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/ns-setupapi-_sp_device_interface_data",
                Description = "SP_DEVICE_INTERFACE_DATA structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct SetupDeviceInterfaceData
        {
            internal /* DWORD     */ uint cbSize;
            internal /* GUID      */ Guid interfaceClassGuid;
            internal /* DWORD     */ uint flags;
            internal /* ULONG_PTR */ IntPtr reserved;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-sid_and_attributes",
                Description = "SID_AND_ATTRIBUTES structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct SidAndAttributes
        {
            internal /* PSID  */ IntPtr Sid;
            internal /* DWORD */ uint Attributes;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/ns-processthreadsapi-_startupinfow",
                Description = "STARTUPINFO structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct StartupInfo
        {
            internal /* DWORD  */ int cb;
            internal /* LPWSTR */ string lpReserved;
            internal /* LPWSTR */ string lpDesktop;
            internal /* LPWSTR */ string lpTitle;
            internal /* DWORD  */ int dwX;
            internal /* DWORD  */ int dwY;
            internal /* DWORD  */ int dwXSize;
            internal /* DWORD  */ int dwXCountChars;
            internal /* DWORD  */ int dwYCountChars;
            internal /* DWORD  */ int dwFillAttribute;
            internal /* DWORD  */ int dwFlags;
            internal /* WORD   */ short wShowWindow;
            internal /* WORD   */ short cbReserved2;
            internal /* LPBYTE */ IntPtr lpReserved2;
            internal /* HANDLE */ IntPtr hStdInput;
            internal /* HANDLE */ IntPtr hStdOutput;
            internal /* HANDLE */ IntPtr hStdError;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-token_elevation",
                Description = "TOKEN_ELEVATION structure")]
        internal struct TokenElevation
        {
            internal /* DWORD */ uint TokenIsElevated;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_token_privileges",
                Description = "TOKEN_PRIVILEGES structure")]
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokenPrivileges
        {
            internal /* DWORD */ int Count;
            internal /* LUID  */ long Luid;
            internal /* DWORD */ SePrivileges Attr;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-_token_user",
                Description = "TOKEN_USER structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct TokenUser
        {
            internal /* SID_AND_ATTRIBUTES */ SidAndAttributes User;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_process_infow",
                Description = "WTS_PROCESS_INFO structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowsTerminalServiceProcessInfo
        {
                                              internal /* DWORD  */ uint sessionId;
                                              internal /* DWORD  */ uint processId;
            [MarshalAs(UnmanagedType.LPWStr)] internal /* LPTSTR */ string pProcessName;
                                              internal /* PSID   */ IntPtr pUserSid;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_session_infow",
                Description = "WTS_SESSION_INFO structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowsTerminalServiceSessionInfo
        {
                                              internal /* DWORD                  */ uint sessionId;
            [MarshalAs(UnmanagedType.LPWStr)] internal /* LPTSTR                 */ string pWinStationName;
                                              internal /* WTS_CONNECTSTATE_CLASS */ WindowsTerminalServiceConnectState state;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WINTRUST_DATA structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WinTrustData
        {
            internal /* DWORD                        */ uint cbStruct;
            internal /* LPVOID                       */ IntPtr pPolicyCallbackData;
            internal /* LPVOID                       */ IntPtr pSIPCallbackData;
            internal /* DWORD                        */ WinTrustDataUI dwUIChoice;
            internal /* DWORD                        */ WinTrustDataRevoke fdwRevocationChecks;
            internal /* DWORD                        */ WinTrustDataChoice dwUnionChoice;
            internal /* union                        */ WinTrustDataUnionChoice infoUnion;
            internal /* DWORD                        */ WinTrustDataStateAction dwStateAction;
            internal /* HANDLE                       */ IntPtr hWVTStateData;
            internal /* WCHAR*                       */ IntPtr pwszURLReference;
            internal /* DWORD                        */ WinTrustDataProviderFlags dwProvFlags;
            internal /* DWORD                        */ WinTrustDataUIContext dwUIContext;
            internal /* WINTRUST_SIGNATURE_SETTINGS* */ IntPtr pSignatureSettings;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data",
                Description = "WTD_UNION_CHOICE union")]
        [StructLayout(LayoutKind.Explicit)]
        internal struct WinTrustDataUnionChoice
        {
            [FieldOffset(0)] internal /* struct WINTRUST_FILE_INFO_    */ IntPtr pFile;
            [FieldOffset(0)] internal /* struct WINTRUST_CATALOG_INFO_ */ IntPtr pCatalog;
            [FieldOffset(0)] internal /* struct WINTRUST_BLOB_INFO_    */ IntPtr pBlob;
            [FieldOffset(0)] internal /* struct WINTRUST_SGNR_INFO_    */ IntPtr pSgnr;
            [FieldOffset(0)] internal /* struct WINTRUST_CERT_INFO_    */ IntPtr pCert;
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/minwinbase/ns-minwinbase-win32_find_dataw",
                Description = "WIN32_FIND_DATAW structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32FindDataW : IEquatable<Win32FindDataW>
        {
                                                                      internal readonly /* DWORD           */ FileAttributeFlags dwFileAttributes;
                                                                      internal readonly /* FILETIME        */ FileTime ftCreationTime;
                                                                      internal readonly /* FILETIME        */ FileTime ftLastAccessTime;
                                                                      internal readonly /* FILETIME        */ FileTime ftLastWriteTime;
                                                                      internal readonly /* DWORD           */ uint nFileSizeHigh;
                                                                      internal readonly /* DWORD           */ uint nFileSizeLow;
                                                                      internal readonly /* DWORD           */ uint dwReserved0;
                                                                      internal readonly /* DWORD           */ uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MaxPath)] internal readonly /* WCHAR[MAX_PATH] */ string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]      internal readonly /* WCHAR[14]       */ string cAlternateFileName;

            public static bool operator ==(Win32FindDataW left, Win32FindDataW right)
            {
                return Equals(left, right);
            }

            public static bool operator !=(Win32FindDataW left, Win32FindDataW right)
            {
                return !Equals(left, right);
            }

            public bool Equals(Win32FindDataW other)
            {
                return dwFileAttributes == other.dwFileAttributes
                        && ftCreationTime.Equals(other.ftCreationTime)
                        && ftLastAccessTime.Equals(other.ftLastAccessTime)
                        && ftLastWriteTime.Equals(other.ftLastWriteTime)
                        && nFileSizeHigh == other.nFileSizeHigh
                        && nFileSizeLow == other.nFileSizeLow
                        && cFileName == other.cFileName
                        && cAlternateFileName == other.cAlternateFileName;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj))
                {
                    return false;
                }
                return obj is Win32FindDataW && Equals((Win32FindDataW) obj);
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    var hashCode = (int) dwFileAttributes;
                    hashCode = (hashCode * 397) ^ ftCreationTime.GetHashCode();
                    hashCode = (hashCode * 397) ^ ftLastAccessTime.GetHashCode();
                    hashCode = (hashCode * 397) ^ ftLastWriteTime.GetHashCode();
                    hashCode = (hashCode * 397) ^ (int) nFileSizeHigh;
                    hashCode = (hashCode * 397) ^ (int) nFileSizeLow;
                    hashCode = (hashCode * 397) ^ (cFileName != null ? cFileName.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (cAlternateFileName != null ? cAlternateFileName.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        [ExternalReference("https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-wintrust_file_info_",
                Description = "WINTRUST_FILE_INFO structure")]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WinTrustFileInfo
        {
                                              internal /* DWORD   */ uint cbStruct;
            [MarshalAs(UnmanagedType.LPWStr)] internal /* LPCWSTR */ string pcwszFilePath;
                                              internal /* HANDLE  */ IntPtr hFile;
                                              internal /* GUID*   */ IntPtr pgKnownSubject;
        }
    }
}
