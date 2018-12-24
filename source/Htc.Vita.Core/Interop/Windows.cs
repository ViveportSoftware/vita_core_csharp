using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static readonly IntPtr /* INVALID_HANDLE_VALUE */ InvalidHandleValue = new IntPtr(-1);

        internal enum Error
        {
            /* ERROR_NOT_SUPPORTED            (50,  0x32) */ NotSupported = 0x32,
            /* ERROR_INVALID_PARAMETER        (87,  0x57) */ InvalidParameter = 0x57,
            /* ERROR_INSUFFICIENT_BUFFER     (122,  0x7a) */ InsufficientBuffer = 0x7a,
            /* ERROR_INVALID_NAME            (123,  0x7b) */ InvalidName = 0x7b,
            /* ERROR_NO_MORE_ITEMS           (259, 0x103) */ NoMoreItems = 0x103,
            /* ERROR_SERVICE_DOES_NOT_EXIST (1060, 0x424) */ ServiceDoesNotExist = 0x424
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
