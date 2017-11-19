using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        internal static int ERROR_INSUFFICIENT_BUFFER = 0x7a; // 122
        internal static int ERROR_INVALID_NAME = 0x7b; // 123
        internal static int ERROR_NO_MORE_ITEMS = 0x103; // 259
        internal static int ERROR_SERVICE_DOES_NOT_EXIST = 0x424; // 1060

        internal static uint REG_NONE = 0;
        internal static uint REG_SZ = 1;
        internal static uint REG_EXPAND_SZ = 2;
        internal static uint REG_BINARY = 3;
        internal static uint REG_DWORD = 4;
        internal static uint REG_DWORD_LITTLE_ENDIAN = REG_DWORD;
        internal static uint REG_DWORD_BIG_ENDIAN = 5;
        internal static uint REG_LINK = 6;
        internal static uint REG_MULTI_SZ = 7;
        internal static uint REG_RESOURCE_LIST = 8;
        internal static uint REG_FULL_RESOURCE_DESCRIPTOR = 9;
        internal static uint REG_RESOURCE_REQUIREMENTS_LIST = 10;
        internal static uint REG_QWORD = 11;
        internal static uint REG_QWORD_LITTLE_ENDIAN = REG_QWORD;

        internal static uint TRUST_E_PROVIDER_UNKNOWN = 0x800B0001;
        internal static uint TRUST_E_ACTION_UNKNOWN = 0x800B0002;
        internal static uint TRUST_E_SUBJECT_FORM_UNKNOWN = 0x800B0003;
        internal static uint TRUST_E_SUBJECT_NOT_TRUSTED = 0x800B0004;
    }
}
