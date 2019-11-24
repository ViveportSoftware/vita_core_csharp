using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
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

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-certclosestore
         */
        internal enum CertCloseStoreFlag
        {
            /*                             */ Default =          0,
            /* CERT_CLOSE_STORE_FORCE_FLAG */ Force   = 0x00000001,
            /* CERT_CLOSE_STORE_CHECK_FLAG */ Check   = 0x00000002
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/privilege-constants
         */
        internal const string SeShutdownName = "SeShutdownPrivilege";

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptmsggetparam
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject
         */
        [Flags]
        internal enum CertQueryContentFlag : uint
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

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject
         */
        internal enum CertQueryFormat
        {
            /*                                         */ None               = 0,
            /* CERT_QUERY_FORMAT_BINARY                */ Binary             = 1,
            /* CERT_QUERY_FORMAT_BASE64_ENCODED        */ Base64Encoded      = 2,
            /* CERT_QUERY_FORMAT_ASN_ASCII_HEX_ENCODED */ AsnAsciiHexEncoded = 3
        }

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject
         */
        internal enum CertQueryFormatFlag : uint
        {
            /* CERT_QUERY_FORMAT_FLAG_BINARY                */ Binary             = 1 << CertQueryFormat.Binary,
            /* CERT_QUERY_FORMAT_FLAG_BASE64_ENCODED        */ Base64Encoded      = 1 << CertQueryFormat.Base64Encoded,
            /* CERT_QUERY_FORMAT_FLAG_ASN_ASCII_HEX_ENCODED */ AsnAsciiHexEncoded = 1 << CertQueryFormat.AsnAsciiHexEncoded,
            /* CERT_QUERY_FORMAT_FLAG_ALL                   */ All                = Binary
                                                                                  | Base64Encoded
                                                                                  | AsnAsciiHexEncoded
        }

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wincrypt/nf-wincrypt-cryptqueryobject
         */
        internal enum CertQueryObject
        {
            /*                        */ None = 0,
            /* CERT_QUERY_OBJECT_FILE */ File = 1,
            /* CERT_QUERY_OBJECT_BLOB */ Blob = 2
        }

        /**
         * DIGCF enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetclassdevsw
         */
        [Flags]
        internal enum DeviceInfoGetClassFlag : uint
        {
            /* DIGCF_DEFAULT         */ Default         = 0x00000001,
            /* DIGCF_PRESENT         */ Present         = 0x00000002,
            /* DIGCF_ALLCLASSES      */ AllClasses      = 0x00000004,
            /* DIGCF_PROFILE         */ Profile         = 0x00000008,
            /* DIGCF_DEVICEINTERFACE */ DeviceInterface = 0x00000010
        }

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-_display_devicew
         */
        [Flags]
        internal enum DisplayDeviceStateFlags : uint
        {
            /*                                    */ None              =          0,
            /* DISPLAY_DEVICE_ATTACHED_TO_DESKTOP */ AttachedToDesktop = 0x00000001,
            /* DISPLAY_DEVICE_MULTI_DRIVER        */ MultiDriver       = 0x00000002,
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

        /**
         * DXGI_ERROR enumeration
         * https://docs.microsoft.com/en-us/windows/win32/direct3ddxgi/dxgi-error
         */
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

        /**
         * DXGI_MODE_ROTATION enumeration
         * https://docs.microsoft.com/en-us/previous-versions/windows/desktop/legacy/bb173065(v=vs.85)
         */
        internal enum DxgiModeRotation
        {
            /* DXGI_MODE_ROTATION_UNSPECIFIED */ Unspecified = 0,
            /* DXGI_MODE_ROTATION_IDENTITY    */ Identity    = 1,
            /* DXGI_MODE_ROTATION_ROTATE90    */ Rotate90    = 2,
            /* DXGI_MODE_ROTATION_ROTATE180   */ Rotate180   = 3,
            /* DXGI_MODE_ROTATION_ROTATE270   */ Rotate270   = 4
        }

        internal enum Error
        {
            /* ERROR_SUCCESS                   (0,   0x0) */ Success             =   0x0,
            /* ERROR_FILE_NOT_FOUND            (2,   0x2) */ FileNotFound        =   0x2,
            /* ERROR_INVALID_DATA             (13,   0xd) */ InvalidData         =   0xd,
            /* ERROR_BAD_LENGTH               (24,  0x18) */ BadLength           =  0x18,
            /* ERROR_GEN_FAILURE              (31,  0x1f) */ GenFailure          =  0x1f,
            /* ERROR_NOT_SUPPORTED            (50,  0x32) */ NotSupported        =  0x32,
            /* ERROR_INVALID_PARAMETER        (87,  0x57) */ InvalidParameter    =  0x57,
            /* ERROR_INSUFFICIENT_BUFFER     (122,  0x7a) */ InsufficientBuffer  =  0x7a,
            /* ERROR_INVALID_NAME            (123,  0x7b) */ InvalidName         =  0x7b,
            /* ERROR_NO_MORE_ITEMS           (259, 0x103) */ NoMoreItems         = 0x103,
            /* ERROR_SERVICE_DOES_NOT_EXIST (1060, 0x424) */ ServiceDoesNotExist = 0x424,
            /* ERROR_DEVICE_NOT_CONNECTED   (1167, 0x48f) */ DeviceNotConnected  = 0x48f,
            /* ERROR_NO_SUCH_LOGON_SESSION  (1312, 0x520) */ NoSuchLogonSession  = 0x520
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/winuser/nf-winuser-exitwindowsex
         */
        [Flags]
        internal enum ExitType : uint
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

        /**
         * FILE_ATTRIBUTE_FLAG enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/FileIO/file-attribute-constants
         */
        [Flags]
        internal enum FileAttributeFlag : uint
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

        /**
         * CREATION_DISPOSITION enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew
         */
        internal enum FileCreationDisposition : uint
        {
            /* CREATE_NEW        */ CreateNew        = 1,
            /* CREATE_ALWAYS     */ CreateAlways     = 2,
            /* OPEN_EXISTING     */ OpenExisting     = 3,
            /* OPEN_ALWAYS       */ OpenAlways       = 4,
            /* TRUNCATE_EXISTING */ TruncateExisting = 5
        }

        /**
         * FILE_SHARE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/fileapi/nf-fileapi-createfilew
         */
        [Flags]
        internal enum FileShare : uint
        {
            /* FILE_SHARE_NONE   */ None   = 0x00000000,
            /* FILE_SHARE_READ   */ Read   = 0x00000001,
            /* FILE_SHARE_WRITE  */ Write  = 0x00000002,
            /* FILE_SHARE_DELETE */ Delete = 0x00000004
        }

        /**
         * GENERIC enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/generic-access-rights
         */
        [Flags]
        internal enum GenericAccessRight : uint
        {
            /* GENERIC_ALL     */ All     = 0x10000000,
            /* GENERIC_EXECUTE */ Execute = 0x20000000,
            /* GENERIC_WRITE   */ Write   = 0x40000000,
            /* GENERIC_READ    */ Read    = 0x80000000
        }

        /**
         * https://docs.microsoft.com/en-us/windows/win32/seccrypto/common-hresult-values
         */
        internal enum HResult : uint
        {
            /* S_OK           */ SOk           =          0,
            /* S_FALSE        */ SFalse        =          1,
            /* E_POINTER      */ EPointer      = 0x80004003,
            /* E_FAIL         */ EFail         = 0x80004005,
            /* E_UNEXPECTED   */ EUnexpected   = 0x8000ffff,
            /* E_ACCESSDENIED */ EAccessDenied = 0x80070005,
            /* E_OUTOFMEMORY  */ EOutOfMemory  = 0x8007000e,
            /* E_INVALIDARG   */ EInvalidArg   = 0x80070057
        }

        /**
         * https://docs.microsoft.com/zh-tw/windows/desktop/SysInfo/image-file-machine-constants
         */
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

        /**
         * Process access right enumeration
         * https://docs.microsoft.com/en-us/windows/win32/procthread/process-security-and-access-rights
         */
        [Flags]
        internal enum ProcessAccessRight : uint
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
            /* PROCESS_ALL_ACCESS                */ AllAccess                = StandardAccessRight.StandardRightsRequired
                                                                             | StandardAccessRight.Synchronize
                                                                             | 0xFFFF
        }

        /**
         * Process Creation enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/ProcThread/process-creation-flags
         */
        [Flags]
        internal enum ProcessCreationFlag : uint
        {
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/sysinfo/registry-value-types
         */
        internal enum RegType : uint
        {
            /* REG_NONE                       */ None                     =  0,
            /* REG_SZ                         */ Sz                       =  1,
            /* REG_EXPAND_SZ                  */ ExpandSz                 =  2,
            /* REG_BINARY                     */ Binary                   =  3,
            /* REG_DWORD                      */ Dword                    =  4,
            /* REG_DWORD_LITTLE_ENDIAN        */ DwordLittleEndian        = Dword,
            /* REG_DWORD_BIG_ENDIAN           */ DwordBigEndian           =  5,
            /* REG_LINK                       */ Link                     =  6,
            /* REG_MULTI_SZ                   */ MultiSz                  =  7,
            /* REG_RESOURCE_LIST              */ ResourceList             =  8,
            /* REG_FULL_RESOURCE_DESCRIPTOR   */ FullResourceDescriptor   =  9,
            /* REG_RESOURCE_REQUIREMENTS_LIST */ ResourceRequirementsList = 10,
            /* REG_QWORD                      */ Qword                    = 11,
            /* REG_QWORD_LITTLE_ENDIAN        */ QwordLittleEndian        = Qword
        }

        /**
         * SECURITY_IMPERSONATION_LEVEL enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_security_impersonation_level
         */
        internal enum SecurityImpersonationLevel : uint
        {
                /* SecurityAnonymous      */ SecurityAnonymous,
                /* SecurityIdentification */ SecurityIdentification,
                /* SecurityImpersonation  */ SecurityImpersonation,
                /* SecurityDelegation     */ SecurityDelegation
        }

        [Flags]
        internal enum SePrivilege : uint
        {
            /* SE_PRIVILEGE_ENABLED_BY_DEFAULT */ EnabledByDefault = 0x00000001,
            /* SE_PRIVILEGE_ENABLED            */ Enabled          = 0x00000002,
            /* SE_PRIVILEGE_REMOVED            */ Removed          = 0x00000004,
            /* SE_PRIVILEGE_USED_FOR_ACCESS    */ UsedForAccess    = 0x80000000
        }

        /**
         * CONTROL_ACCEPTED enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
        [Flags]
        internal enum ServiceAcceptedControl : uint
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

        /**
         * SERVICE_ACCESS_RIGHT enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/Services/service-security-and-access-rights
         */
        [Flags]
        internal enum ServiceAccessRight : uint
        {
            /* STANDARD_RIGHTS_REQUIRED     */ StandardRightsRequired = StandardAccessRight.StandardRightsRequired,
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

        /**
         * SC_MANAGER_ACCESS_RIGHT enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/Services/service-security-and-access-rights
         */
        [Flags]
        internal enum ServiceControlManagerAccessRight : uint
        {
            /* STANDARD_RIGHTS_REQUIRED      */ StandardRightsRequired = StandardAccessRight.StandardRightsRequired,
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

        /**
         * CURRENT_STATE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
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

        /**
         * ERROR_CONTROL_TYPE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-changeserviceconfigw
         */
        internal enum ServiceErrorControl : uint
        {
            /* SERVICE_ERROR_IGNORE   */ Ignore   = 0x00000000,
            /* SERVICE_ERROR_NORMAL   */ Normal   = 0x00000001,
            /* SERVICE_ERROR_SEVERE   */ Severe   = 0x00000002,
            /* SERVICE_ERROR_CRITICAL */ Critical = 0x00000003,
            /* SERVICE_NO_CHANGE      */ NoChange = 0xffffffff
        }

        /**
         * START_TYPE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/nf-winsvc-changeserviceconfigw
         */
        internal enum ServiceStartType : uint
        {
            /* SERVICE_BOOT_START   */ BootStart   = 0x00000000,
            /* SERVICE_SYSTEM_START */ SystemStart = 0x00000001,
            /* SERVICE_AUTO_START   */ AutoStart   = 0x00000002,
            /* SERVICE_DEMAND_START */ DemandStart = 0x00000003,
            /* SERVICE_DISABLED     */ Disabled    = 0x00000004,
            /* SERVICE_NO_CHANGE    */ NoChange    = 0xffffffff
        }

        /**
         * SERVICE_TYPE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
        [Flags]
        internal enum ServiceType : uint
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

        /**
         * SPDRP enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/setupapi/nf-setupapi-setupdigetdeviceregistrypropertyw
         */
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

        /**
         * SHCNE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify
         */
        [Flags]
        internal enum ShellChangeNotifyEventId :uint
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

        /**
         * SHCNE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes
         */
        internal enum ShutdownReason : uint
        {
            /* SHTDN_REASON_UNKNOWN    */ Unknown   = ShutdownReasonMinor.None,
            /* SHTDN_REASON_LEGACY_API */ LegacyApi = ShutdownReasonMajor.LegacyApi
                                                    | ShutdownReasonFlag.Planned
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes
         */
        [Flags]
        internal enum ShutdownReasonFlag : uint
        {
            /* SHTDN_REASON_FLAG_COMMENT_REQUIRED          */ CommentRequired        = 0x01000000,
            /* SHTDN_REASON_FLAG_DIRTY_PROBLEM_ID_REQUIRED */ DirtyProblemIdRequired = 0x02000000,
            /* SHTDN_REASON_FLAG_CLEAN_UI                  */ CleanUI                = 0x04000000,
            /* SHTDN_REASON_FLAG_DIRTY_UI                  */ DirtyUI                = 0x08000000,
            /* SHTDN_REASON_FLAG_MOBILE_UI_RESERVED        */ MobileUIReserved       = 0x10000000,
            /* SHTDN_REASON_FLAG_USER_DEFINED              */ UserDefined            = 0x40000000,
            /* SHTDN_REASON_FLAG_PLANNED                   */ Planned                = 0x80000000
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/Shutdown/system-shutdown-reason-codes
         */
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

        /**
         * SID_NAME_USE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_sid_name_use
         */
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/standard-access-rights
         */
        internal enum StandardAccessRight : uint
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

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/access-rights-for-access-token-objects
         */
        [Flags]
        internal enum TokenAccessRight : uint
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

        /**
         * TOKEN_INFORMATION_CLASS enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_token_information_class
         */
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

        /**
         * TOKEN_TYPE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ne-winnt-_token_type
         */
        internal enum TokenType : uint
        {
            /* TokenPrimary       */ TokenPrimary       = 1,
            /* TokenImpersonation */ TokenImpersonation = 2
        }

        /**
         * https://docs.microsoft.com/en-us/windows/win32/api/wintrust/nf-wintrust-winverifytrust
         */
        internal enum TrustError : uint
        {
            /* TRUST_E_PROVIDER_UNKNOWN     */ ProviderUnknown    = 0x800B0001,
            /* TRUST_E_ACTION_UNKNOWN       */ ActionUnknown      = 0x800B0002,
            /* TRUST_E_SUBJECT_FORM_UNKNOWN */ SubjectFormUnknown = 0x800B0003,
            /* TRUST_E_SUBJECT_NOT_TRUSTED  */ SubjectNotTrusted  = 0x800B0004
        }

        /**
         * WTS_CONNECTSTATE_CLASS enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ne-wtsapi32-_wts_connectstate_class
         */
        internal enum WindowsTerminalServiceConnectState
        {
            /* WTSActive       */ Active,
            /* WTSConnected    */ Connected,
            /* WTSConnectQuery */ ConnectQuery,
            /* WTSShadow       */ Shadow,
            /* WTSDisconnected */ Disconnected,
            /* WTSIdle         */ Idle,
            /* WTSListen       */ Listen,
            /* WTSReset        */ Reset,
            /* WTSDown         */ Down,
            /* WTSInit         */ Init
        }

        /**
         * WTS_INFO_CLASS enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ne-wtsapi32-_wts_info_class
         */
        internal enum WindowsTerminalServiceInfo
        {
            /* WTSInitialProgram     */ InitialProgram,
            /* WTSApplicationName    */ ApplicationName,
            /* WTSWorkingDirectory   */ WorkingDirectory,
            /* WTSOEMId              */ OemId,
            /* WTSSessionId          */ SessionId,
            /* WTSUserName           */ UserName,
            /* WTSWinStationName     */ WinStationName,
            /* WTSDomainName         */ DomainName,
            /* WTSConnectState       */ ConnectState,
            /* WTSClientBuildNumber  */ ClientBuildNumber,
            /* WTSClientName         */ ClientName,
            /* WTSClientDirectory    */ ClientDirectory,
            /* WTSClientProductId    */ ClientProductId,
            /* WTSClientHardwareId   */ ClientHardwareId,
            /* WTSClientAddress      */ ClientAddress,
            /* WTSClientDisplay      */ ClientDisplay,
            /* WTSClientProtocolType */ ClientProtocolType,
            /* WTSIdleTime           */ IdleTime,
            /* WTSLogonTime          */ LogonTime,
            /* WTSIncomingBytes      */ IncomingBytes,
            /* WTSOutgoingBytes      */ OutgoingBytes,
            /* WTSIncomingFrames     */ IncomingFrames,
            /* WTSOutgoingFrames     */ OutgoingFrames,
            /* WTSClientInfo         */ ClientInfo,
            /* WTSSessionInfo        */ SessionInfo,
            /* WTSSessionInfoEx      */ SessionInfoEx,
            /* WTSConfigInfo         */ ConfigInfo,
            /* WTSValidationInfo     */ ValidationInfo,
            /* WTSSessionAddressV4   */ SessionAddressV4,
            /* WTSIsRemoteSession    */ IsRemoteSession
        }

        /**
         * WTD_CHOICE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
        internal enum WinTrustDataChoice : uint
        {
            /* WTD_CHOICE_FILE    */ File    = 1,
            /* WTD_CHOICE_CATALOG */ Catalog = 2,
            /* WTD_CHOICE_BLOB    */ Blob    = 3,
            /* WTD_CHOICE_SIGNER  */ Signer  = 4,
            /* WTD_CHOICE_CERT    */ Cert    = 5
        }

        /**
         * WTD_PROVIDERFLAG enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
        [Flags]
        internal enum WinTrustDataProviderFlag : uint
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

        /**
         * WTD_REVOKE enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
        internal enum WinTrustDataRevoke : uint
        {
            /* WTD_REVOKE_NONE       */ None,
            /* WTD_REVOKE_WHOLECHAIN */ WholeChain
        }

        /**
         * WTD_STATEACTION enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
        internal enum WinTrustDataStateAction : uint
        {
            /* WTD_STATEACTION_IGNORE           */ Ignore,
            /* WTD_STATEACTION_VERIFY           */ Verify,
            /* WTD_STATEACTION_CLOSE            */ Close,
            /* WTD_STATEACTION_AUTO_CACHE       */ AutoCache,
            /* WTD_STATEACTION_AUTO_CACHE_FLUSH */ AutoCacheFlush
        }

        /**
         * WTD_UI enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
        internal enum WinTrustDataUI : uint
        {
            /* WTD_UI_ALL    */ All    = 1,
            /* WTD_UI_NONE   */ None   = 2,
            /* WTD_UI_NOBAD  */ NoBad  = 3,
            /* WTD_UI_NOGOOD */ NoGood = 4
        }

        /**
         * WTD_UICONTEXT enumeration
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
        internal enum WinTrustDataUIContext : uint
        {
            /* WTD_UICONTEXT_EXECUTE */ Execute,
            /* WTD_UICONTEXT_INSTALL */ Install
        }

        /**
         * DISPLAY_DEVICEW structure
         * https://docs.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-_display_devicew
         */
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DisplayDeviceW
        {
                                                                  internal /* DWORD      */ int cb;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]  internal /* WCHAR[32]  */ string deviceName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal /* WCHAR[128] */ string deviceString;
                                                                  internal /* DWORD      */ DisplayDeviceStateFlags stateFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal /* WCHAR[128] */ string deviceId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal /* WCHAR[128] */ string deviceKey;
        }

        /**
         * DXGI_ADAPTER_DESC structure
         * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/ns-dxgi-dxgi_adapter_desc
         */
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DxgiAdapterDescription
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] internal /* WCHAR[128] */ string description;
            internal /* UINT       */ uint vendorId;
            internal /* UINT       */ uint deviceId;
            internal /* UINT       */ uint subSysId;
            internal /* UINT       */ uint revision;
            internal /* SIZE_T     */ UIntPtr dedicatedVideoMemory;
            internal /* SIZE_T     */ UIntPtr dedicatedSystemMemory;
            internal /* SIZE_T     */ UIntPtr sharedSystemMemory;
            internal /* LUID       */ long adapterLuid;
        }

        /**
         * DXGI_OUTPUT_DESC structure
         * https://docs.microsoft.com/en-us/windows/win32/api/dxgi/ns-dxgi-dxgi_output_desc
         */
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct DxgiOutputDescription
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal /* WCHAR[32]          */ string deviceName;
                                                                 internal /* RECT               */ Rectangle desktopCoordinates;
                                                                 internal /* BOOL               */ bool attachedToDesktop;
                                                                 internal /* DXGI_MODE_ROTATION */ DxgiModeRotation rotation;
                                                                 internal /* HMONITOR           */ IntPtr monitor;
        }

        /**
         * https://docs.microsoft.com/en-us/windows-hardware/drivers/ddi/content/hidpi/ns-hidpi-_hidp_caps
         */
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

        /**
         * MONITORINFOEXW structure
         * https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-monitorinfoexw
         */
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct MonitorInfoExW
        {
                                                                 internal /* DWORD     */ int size;
                                                                 internal /* RECT      */ Rectangle rcMonitor;
                                                                 internal /* RECT      */ Rectangle rcWork;
                                                                 internal /* DWORD     */ uint flags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] internal /* WCHAR[32] */ string DeviceName;
        }

        /**
         * RECT structure
         * https://docs.microsoft.com/en-us/windows/win32/api/windef/ns-windef-rect
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct Rectangle
        {
            internal /* LONG */ int left;
            internal /* LONG */ int top;
            internal /* LONG */ int right;
            internal /* LONG */ int bottom;
        }

        /**
         * PROCESS_INFORMATION structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/ns-processthreadsapi-process_information
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct ProcessInformation
        {
            internal /* HANDLE */ IntPtr hProcess;
            internal /* HANDLE */ IntPtr hThread;
            internal /* DWORD  */ int dwProcessID;
            internal /* DWORD  */ int dwThreadID;
        }

        /**
         * QUERY_SERVICE_CONFIG structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-query_service_configw
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct QueryServiceConfig
        {
            internal /* DWORD  */ ServiceType dwServiceType;
            internal /* DWORD  */ ServiceStartType dwStartType;
            internal /* DWORD  */ ServiceErrorControl dwErrorControl;
            internal /* LPTSTR */ string lpBinaryPathName;
            internal /* LPTSTR */ string lpLoadOrderGroup;
            internal /* DWORD  */ uint dwTagId;
            internal /* LPTSTR */ string lpDependencies;
            internal /* LPTSTR */ string lpServiceStartName;
            internal /* LPTSTR */ string lpDisplayName;
        }

        /**
         * SECURITY_ATTRIBUTES
         * https://msdn.microsoft.com/en-us/library/windows/desktop/aa379560.aspx
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct SecurityAttributes
        {
            internal /* DWORD  */ int nLength;
            internal /* LPVOID */ IntPtr lpSecurityDescriptor;
            internal /* BOOL   */ bool bInheritHandle;
        }

        /**
         * SERVICE_STATUS structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/winsvc/ns-winsvc-_service_status
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct ServiceStatus
        {
            internal /* DWORD */ ServiceType dwServiceType;
            internal /* DWORD */ ServiceCurrentState dwCurrentState;
            internal /* DWORD */ ServiceAcceptedControl dwControlAccepted;
            internal /* DWORD */ uint dwWin32ExitCode;
            internal /* DWORD */ uint dwServiceSpecificExitCode;
            internal /* DWORD */ uint dwCheckPoint;
            internal /* DWORD */ uint dwWaitHint;
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
         * SID_AND_ATTRIBUTES structure
         * https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-sid_and_attributes
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct SidAndAttributes
        {
            internal /* PSID  */ IntPtr Sid;
            internal /* DWORD */ uint Attributes;
        }

        /**
         * STARTUPINFO structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/processthreadsapi/ns-processthreadsapi-_startupinfow
         */
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

        /**
         * TOKEN_PRIVILEGES structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/winnt/ns-winnt-_token_privileges
         */
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        internal struct TokenPrivileges
        {
            internal /* DWORD */ int Count;
            internal /* LUID  */ long Luid;
            internal /* DWORD */ SePrivilege Attr;
        }

        /**
         * TOKEN_USER structure
         * https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-_token_user
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct TokenUser
        {
            internal /* SID_AND_ATTRIBUTES */ SidAndAttributes User;
        }

        /**
         * WTS_PROCESS_INFO structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_process_infow
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowsTerminalServiceProcessInfo
        {
            internal /* DWORD  */ uint sessionId;
            internal /* DWORD  */ uint processId;
            internal /* LPTSTR */ string pProcessName;
            internal /* PSID   */ IntPtr pUserSid;
        }

        /**
         * WTS_SESSION_INFO structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/wtsapi32/ns-wtsapi32-wts_session_infow
         */
        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowsTerminalServiceSessionInfo
        {
            internal /* DWORD                  */ uint sessionId;
            internal /* LPTSTR                 */ string pWinStationName;
            internal /* WTS_CONNECTSTATE_CLASS */ WindowsTerminalServiceConnectState state;
        }

        /**
         * WINTRUST_DATA structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
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
            internal /* DWORD                        */ WinTrustDataProviderFlag dwProvFlags;
            internal /* DWORD                        */ WinTrustDataUIContext dwUIContext;
            internal /* WINTRUST_SIGNATURE_SETTINGS* */ IntPtr pSignatureSettings;
        }

        /**
         * WTD_UNION_CHOICE union
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-_wintrust_data
         */
        [StructLayout(LayoutKind.Explicit)]
        internal struct WinTrustDataUnionChoice
        {
            [FieldOffset(0)] internal /* struct WINTRUST_FILE_INFO_    */ IntPtr pFile;
            [FieldOffset(0)] internal /* struct WINTRUST_CATALOG_INFO_ */ IntPtr pCatalog;
            [FieldOffset(0)] internal /* struct WINTRUST_BLOB_INFO_    */ IntPtr pBlob;
            [FieldOffset(0)] internal /* struct WINTRUST_SGNR_INFO_    */ IntPtr pSgnr;
            [FieldOffset(0)] internal /* struct WINTRUST_CERT_INFO_    */ IntPtr pCert;
        }

        /**
         * WINTRUST_FILE_INFO structure
         * https://docs.microsoft.com/en-us/windows/desktop/api/wintrust/ns-wintrust-wintrust_file_info_
         */
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
