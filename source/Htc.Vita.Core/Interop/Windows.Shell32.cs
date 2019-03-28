using System;
using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        /**
        * SHCNE enumeration
        * https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify
        */
        [Flags]
        internal enum ShellChangeNotifyEventId
        {
            /* SHCNE_RENAMEITEM       */ RenameItem = 0x00000001,
            /* SHCNE_CREATE           */ Create = 0x00000002,
            /* SHCNE_DELETE           */ Delete = 0x00000004,
            /* SHCNE_MKDIR            */ MakeDirectory = 0x00000008,
            /* SHCNE_RMDIR            */ RemoveDirectory = 0x00000010,
            /* SHCNE_MEDIAINSERTED    */ MediaInserted = 0x00000020,
            /* SHCNE_MEDIAREMOVED     */ MediaRemoved = 0x00000040,
            /* SHCNE_DRIVEREMOVED     */ DriveRemoved = 0x00000080,
            /* SHCNE_DRIVEADD         */ DriveAdd = 0x00000100,
            /* SHCNE_NETSHARE         */ NetShare = 0x00000200,
            /* SHCNE_NETUNSHARE       */ NetUnshare = 0x00000400,
            /* SHCNE_ATTRIBUTES       */ Attributes = 0x00000800,
            /* SHCNE_UPDATEDIR        */ UpdateDirectory = 0x00001000,
            /* SHCNE_UPDATEITEM       */ UpdateItem = 0x00002000,
            /* SHCNE_SERVERDISCONNECT */ ServerDisconnect = 0x00004000,
            /* SHCNE_UPDATEIMAGE      */ UpdateImage = 0x00008000,
            /* SHCNE_DRIVEADDGUI      */ DriveAddGui = 0x00010000,
            /* SHCNE_RENAMEFOLDER     */ RenameFolder = 0x00020000,
            /* SHCNE_FREESPACE        */ FreeSpace = 0x00040000,
            /* SHCNE_EXTENDED_EVENT   */ ExtendedEvent = 0x04000000,
            /* SHCNE_ASSOCCHANGED     */ AssociationChanged = 0x08000000,
            /* SHCNE_DISKEVENTS       */ DiskEvents = 0x0002381F,
            /* SHCNE_GLOBALEVENTS     */ GlobalEvents = 0x0C0581E0,
            /* SHCNE_ALLEVENTS        */ AllEvents = 0x7FFFFFFF,
            /* SHCNE_INTERRUPT        */ Interrupt = unchecked((int) 0x80000000)
        }

        /**
        * SHCNE enumeration
        * https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify
        */
        internal enum ShellChangeNotifyFlags : uint
        {
            /* SHCNF_IDLIST      */ IdList = 0x0000,
            /* SHCNF_PATHA       */ PathA = 0x0001,
            /* SHCNF_PRINTERA    */ PrinterA = 0x0002,
            /* SHCNF_DWORD       */ Dword = 0x0003,
            /* SHCNF_PATHW       */ PathW = 0x0005,
            /* SHCNF_PRINTERW    */ PrinterW = 0x0006,
            /* SHCNF_TYPE        */ Type = 0x00FF,
            /* SHCNF_FLUSH       */ Flush = 0x1000,
            /* SHCNF_FLUSHNOWAIT */ FlushNoWait = 0x3000
        }

        /**
         * https://docs.microsoft.com/en-us/windows/desktop/api/shlobj_core/nf-shlobj_core-shchangenotify
         */
        [DllImport(Libraries.WindowsShell32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern void SHChangeNotify(
                /*          LONG    */ [In] ShellChangeNotifyEventId wEventId,
                /*          UINT    */ [In] ShellChangeNotifyFlags uFlags,
                /* _In_opt_ LPCVOID */ [In] IntPtr dwItem1,
                /* _In_opt_ LPCVOID */ [In] IntPtr dwItem2
        );
    }
}
