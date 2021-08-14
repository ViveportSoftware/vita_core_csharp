using System.Runtime.InteropServices;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class ComClassId
        {
            internal const string BackgroundCopyManager = "4991d34b-80a1-4291-83b6-3328366b9097";
            internal const string Location              = "e5b8e079-ee6d-4e33-a438-c87f2e959254";
            internal const string PortableDeviceManager = "0af10cec-2ecd-4b92-9581-34f6ae0637f3";
            internal const string ShellLink             = "00021401-0000-0000-c000-000000000046";
        }

        [ComImport]
        [Guid(ComClassId.BackgroundCopyManager)]
        internal class ComBackgroundCopyManager
        {
        }

        [ComImport]
        [Guid(ComClassId.Location)]
        internal class ComLocation
        {
        }

        [ComImport]
        [Guid(ComClassId.PortableDeviceManager)]
        internal class ComPortableDeviceManager
        {
        }

        [ComImport]
        [Guid(ComClassId.ShellLink)]
        internal class ComShellLink
        {
        }
    }
}
