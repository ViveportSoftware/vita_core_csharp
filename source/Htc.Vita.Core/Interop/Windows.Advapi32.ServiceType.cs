﻿using System;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        public static partial class Advapi32
        {
            /**
             * https://msdn.microsoft.com/en-us/library/ms681987.aspx
             * https://msdn.microsoft.com/en-us/library/windows/desktop/ms685996.aspx
             */
            [Flags]
            public enum SERVICE_TYPE : uint
            {
                SERVICE_KERNEL_DRIVER = 0x00000001,
                SERVICE_FILE_SYSTEM_DRIVER = 0x00000002,
                SERVICE_ADAPTER = 0x00000004,
                SERVICE_RECOGNIZER_DRIVER = 0x00000008,
                SERVICE_DRIVER = SERVICE_KERNEL_DRIVER
                                 | SERVICE_FILE_SYSTEM_DRIVER
                                 | SERVICE_RECOGNIZER_DRIVER,
                SERVICE_WIN32_OWN_PROCESS = 0x00000010,
                SERVICE_WIN32_SHARE_PROCESS = 0x00000020,
                SERVICE_WIN32 = SERVICE_WIN32_OWN_PROCESS
                                | SERVICE_WIN32_SHARE_PROCESS,
                SERVICE_USER_SERVICE = 0x00000040,
                SERVICE_USERSERVICE_INSTANCE = 0x00000080,
                SERVICE_USER_SHARE_PROCESS = SERVICE_USER_SERVICE
                                             | SERVICE_WIN32_SHARE_PROCESS,
                SERVICE_USER_OWN_PROCESS = SERVICE_USER_SERVICE
                                           | SERVICE_WIN32_OWN_PROCESS,
                SERVICE_INTERACTIVE_PROCESS = 0x00000100,
                SERVICE_PKG_SERVICE = 0x00000200,
                SERVICE_TYPE_ALL = SERVICE_WIN32
                                   | SERVICE_ADAPTER
                                   | SERVICE_DRIVER
                                   | SERVICE_INTERACTIVE_PROCESS
                                   | SERVICE_USER_SERVICE
                                   | SERVICE_USERSERVICE_INSTANCE
                                   | SERVICE_PKG_SERVICE,
                SERVICE_NO_CHANGE = 0xffffffff
            }
        }
    }
}
