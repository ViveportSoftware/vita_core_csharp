using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static readonly IntPtr /* INVALID_HANDLE_VALUE */ InvalidHandleValue = new IntPtr(-1);

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/SecAuthZ/standard-access-rights
         */
        internal enum AccessRight : uint
        {
            /* DELETE                   */ Delete = 0x00010000,
            /* READ_CONTROL             */ ReadControl = 0x00020000,
            /* WRITE_DAC                */ WriteDac = 0x00040000,
            /* WRITE_OWNER              */ WriteOwner = 0x00080000,
            /* STANDARD_RIGHTS_REQUIRED */ StandardRightsRequired = Delete
                                                                  | ReadControl
                                                                  | WriteDac
                                                                  | WriteOwner,
            /* STANDARD_RIGHTS_READ     */ StandardRightsRead = ReadControl,
            /* STANDARD_RIGHTS_WRITE    */ StandardRightsWrite = ReadControl,
            /* STANDARD_RIGHTS_EXECUTE  */ StandardRightsExecute = ReadControl,
            /* SYNCHRONIZE              */ Synchronize = 0x00100000,
            /* STANDARD_RIGHTS_ALL      */ StandardRightsAll = StandardRightsRequired
                                                             | Synchronize
        }

        internal enum Error
        {
            /* ERROR_FILE_NOT_FOUND            (2,   0x2) */ FileNotFound = 0x2,
            /* ERROR_INVALID_DATA             (13,   0xd) */ InvalidData = 0xd,
            /* ERROR_GEN_FAILURE              (31,  0x1f) */ GenFailure = 0x1f,
            /* ERROR_NOT_SUPPORTED            (50,  0x32) */ NotSupported = 0x32,
            /* ERROR_INVALID_PARAMETER        (87,  0x57) */ InvalidParameter = 0x57,
            /* ERROR_INSUFFICIENT_BUFFER     (122,  0x7a) */ InsufficientBuffer = 0x7a,
            /* ERROR_INVALID_NAME            (123,  0x7b) */ InvalidName = 0x7b,
            /* ERROR_NO_MORE_ITEMS           (259, 0x103) */ NoMoreItems = 0x103,
            /* ERROR_SERVICE_DOES_NOT_EXIST (1060, 0x424) */ ServiceDoesNotExist = 0x424,
            /* ERROR_DEVICE_NOT_CONNECTED   (1167, 0x48f) */ DeviceNotConnected = 0x48f
        }

        internal enum NtStatus
        {
            /* STATUS_WAIT_0                       */ StatusWait0 = 0,
            /* HIDP_STATUS_SUCCESS                 */ HidpStatusSuccess = 0x00110000,
            /* HIDP_STATUS_NULL                    */ HidpStatusNull = unchecked((int)0x80110001),
            /* HIDP_STATUS_INVALID_PREPARSED_DATA  */ HidpStatusInvalidPreparsedData = unchecked((int)0xc0110001),
            /* HIDP_STATUS_INVALID_REPORT_TYPE     */ HidpStatusInvalidReportType = unchecked((int)0xc0110002),
            /* HIDP_STATUS_INVALID_REPORT_LENGTH   */ HidpStatusInvalidReportLength = unchecked((int)0xc0110003),
            /* HIDP_STATUS_USAGE_NOT_FOUND         */ HidpStatusUsageNotFound = unchecked((int)0xc0110004),
            /* HIDP_STATUS_VALUE_OUT_OF_RANGE      */ HidpStatusValueOutOfRange = unchecked((int)0xc0110005),
            /* HIDP_STATUS_BAD_LOG_PHY_VALUES      */ HidpStatusBadLogPhyValues = unchecked((int)0xc0110006),
            /* HIDP_STATUS_BUFFER_TOO_SMALL        */ HidpStatusBufferTooSmall = unchecked((int)0xc0110007),
            /* HIDP_STATUS_INTERNAL_ERROR          */ HidpStatusInternalError = unchecked((int)0xc0110008),
            /* HIDP_STATUS_I8042_TRANS_UNKNOWN     */ HidpStatusI8042TransUnknown = unchecked((int)0xc0110009),
            /* HIDP_STATUS_INCOMPATIBLE_REPORT_ID  */ HidpStatusIncompatibleReportId = unchecked((int)0xc011000a),
            /* HIDP_STATUS_NOT_VALUE_ARRAY         */ HidpStatusNotValueArray = unchecked((int)0xc011000b),
            /* HIDP_STATUS_IS_VALUE_ARRAY          */ HidpStatusIsValueArray = unchecked((int)0xc011000c),
            /* HIDP_STATUS_DATA_INDEX_NOT_FOUND    */ HidpStatusDataIndexNotFound = unchecked((int)0xc011000d),
            /* HIDP_STATUS_DATA_INDEX_OUT_OF_RANGE */ HidpStatusDataIndexOutOfRange = unchecked((int)0xc011000e),
            /* HIDP_STATUS_BUTTON_NOT_PRESSED      */ HidpStatusButtonNotPressed = unchecked((int)0xc011000f),
            /* HIDP_STATUS_REPORT_DOES_NOT_EXIST   */ HidpStatusReportDoesNotExist = unchecked((int)0xc0110010),
            /* HIDP_STATUS_NOT_IMPLEMENTED         */ HidpStatusNotImplemented = unchecked((int)0xc0110020)
        }

        internal enum RegType : uint
        {
            /* REG_NONE                       */ None = 0,
            /* REG_SZ                         */ Sz = 1,
            /* REG_EXPAND_SZ                  */ ExpandSz = 2,
            /* REG_BINARY                     */ Binary = 3,
            /* REG_DWORD                      */ Dword = 4,
            /* REG_DWORD_LITTLE_ENDIAN        */ DwordLittleEndian = Dword,
            /* REG_DWORD_BIG_ENDIAN           */ DwordBigEndian = 5,
            /* REG_LINK                       */ Link = 6,
            /* REG_MULTI_SZ                   */ MultiSz = 7,
            /* REG_RESOURCE_LIST              */ ResourceList = 8,
            /* REG_FULL_RESOURCE_DESCRIPTOR   */ FullResourceDescriptor = 9,
            /* REG_RESOURCE_REQUIREMENTS_LIST */ ResourceRequirementsList = 10,
            /* REG_QWORD                      */ Qword = 11,
            /* REG_QWORD_LITTLE_ENDIAN        */ QwordLittleEndian = Qword
        }

        internal enum TrustError : uint
        {
            /* TRUST_E_PROVIDER_UNKNOWN     */ ProviderUnknown = 0x800B0001,
            /* TRUST_E_ACTION_UNKNOWN       */ ActionUnknown = 0x800B0002,
            /* TRUST_E_SUBJECT_FORM_UNKNOWN */ SubjectFormUnknown = 0x800B0003,
            /* TRUST_E_SUBJECT_NOT_TRUSTED  */ SubjectNotTrusted = 0x800B0004
        }
    }
}
