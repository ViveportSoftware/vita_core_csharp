using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/dxgi/nf-dxgi-createdxgifactory")]
        [DllImport(Libraries.WindowsDxgi,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern DxgiError CreateDXGIFactory(
                /* _In_  REFIID */ [In] ref Guid riid,
                /* _Out_ void** */ [Out] out IDxgiFactory factory
        );
    }
}
