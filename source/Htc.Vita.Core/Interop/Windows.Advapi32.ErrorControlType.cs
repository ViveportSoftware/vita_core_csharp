using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             */
            public enum ERROR_CONTROL_TYPE : uint
            {
                SERVICE_ERROR_IGNORE = 0x00000000,
                SERVICE_ERROR_NORMAL = 0x00000001,
                SERVICE_ERROR_SEVERE = 0x00000002,
                SERVICE_ERROR_CRITICAL = 0x00000003,
                SERVICE_NO_CHANGE = 0xffffffff
            }
        }
    }
}
