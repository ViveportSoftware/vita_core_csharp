namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Setupapi
        {
            /**
             * https://msdn.microsoft.com/en-us/library/windows/hardware/ff551967.aspx
             */
            public enum SPDRP
            {
                SPDRP_DEVICEDESC = 0,
                SPDRP_HARDWAREID = 1,
                SPDRP_COMPATIBLEIDS = 2,
                SPDRP_UNUSED0 = 3,
                SPDRP_SERVICE = 4,
                SPDRP_UNUSED1 = 5,
                SPDRP_UNUSED2 = 6,
                SPDRP_CLASS = 7,
                SPDRP_CLASSGUID = 8,
                SPDRP_DRIVER = 9,
                SPDRP_CONFIGFLAGS = 10,
                SPDRP_MFG = 11,
                SPDRP_FRIENDLYNAME = 12,
                SPDRP_LOCATION_INFORMATION = 13,
                SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 14,
                SPDRP_CAPABILITIES = 15,
                SPDRP_UI_NUMBER = 16,
                SPDRP_UPPERFILTERS = 17,
                SPDRP_LOWERFILTERS = 18,
                SPDRP_BUSTYPEGUID = 19,
                SPDRP_LEGACYBUSTYPE = 20,
                SPDRP_BUSNUMBER = 21,
                SPDRP_ENUMERATOR_NAME = 22,
                SPDRP_SECURITY = 23,
                SPDRP_SECURITY_SDS = 24,
                SPDRP_DEVTYPE = 25,
                SPDRP_EXCLUSIVE = 26,
                SPDRP_CHARACTERISTICS = 27,
                SPDRP_ADDRESS = 28,
                SPDRP_UI_NUMBER_DESC_FORMAT = 29,
                SPDRP_DEVICE_POWER_DATA = 30,
                SPDRP_REMOVAL_POLICY = 31,
                SPDRP_REMOVAL_POLICY_HW_DEFAULT = 32,
                SPDRP_REMOVAL_POLICY_OVERRIDE = 33,
                SPDRP_INSTALL_STATE = 34,
                SPDRP_LOCATION_PATHS = 35,
                SPDRP_BASE_CONTAINERID = 36
            }
        }
    }
}
