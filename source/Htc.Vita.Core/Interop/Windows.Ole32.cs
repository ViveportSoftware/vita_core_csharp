using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/combaseapi/nf-combaseapi-propvariantclear")]
        [DllImport(Libraries.WindowsOle32,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern HResult PropVariantClear(
                /* _Inout_ PROPVARIANT* */ [In] PropVariant pvar
        );
    }
}
