using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);

        public static int ERROR_INSUFFICIENT_BUFFER = 0x7a; // 122
        public static int ERROR_NO_MORE_ITEMS = 0x103; // 259

        public static int REG_NONE = 0;
        public static int REG_SZ = 1;
        public static int REG_EXPAND_SZ = 2;
        public static int REG_BINARY = 3;
        public static int REG_DWORD = 4;
        public static int REG_DWORD_LITTLE_ENDIAN = REG_DWORD;
        public static int REG_DWORD_BIG_ENDIAN = 5;
        public static int REG_LINK = 6;
        public static int REG_MULTI_SZ = 7;
        public static int REG_RESOURCE_LIST = 8;
        public static int REG_FULL_RESOURCE_DESCRIPTOR = 9;
        public static int REG_RESOURCE_REQUIREMENTS_LIST = 10;
        public static int REG_QWORD = 11;
        public static int REG_QWORD_LITTLE_ENDIAN = REG_QWORD;

        public static uint TRUST_E_PROVIDER_UNKNOWN = 0x800B0001;
        public static uint TRUST_E_ACTION_UNKNOWN = 0x800B0002;
        public static uint TRUST_E_SUBJECT_FORM_UNKNOWN = 0x800B0003;
        public static uint TRUST_E_SUBJECT_NOT_TRUSTED = 0x800B0004;
    }
}
