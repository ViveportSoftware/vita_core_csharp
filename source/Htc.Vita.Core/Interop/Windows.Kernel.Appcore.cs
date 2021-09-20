using System.Runtime.InteropServices;
using System.Text;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/appmodel/nf-appmodel-getcurrentpackagepath2",
                File = Headers.WindowsAppmodel)]
        [DllImport(Libraries.WindowsKernelAppcore,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error GetCurrentPackagePath2(
                /*_In_              PackagePathType */ [In] PackagePathType packagePathType,
                /* _Inout_          UINT32*         */ [In][Out] ref int pathLength,
                /* _Out_writes_opt_ PWSTR           */ [In][Out] StringBuilder path
        );

        [ExternalReference("https://docs.microsoft.com/en-us/windows/win32/api/appmodel/nf-appmodel-getpackagepathbyfullname2",
                File = Headers.WindowsAppmodel)]
        [DllImport(Libraries.WindowsKernelAppcore,
                CallingConvention = CallingConvention.Winapi,
                CharSet = CharSet.Unicode,
                ExactSpelling = true,
                SetLastError = true)]
        internal static extern Error GetPackagePathByFullName2(
                /* _In_             PCWSTR          */ [In] string packageFullName,
                /* _In_             PackagePathType */ [In] PackagePathType packagePathType,
                /* _Inout_          UINT32*         */ [In][Out] ref int pathLength,
                /* _Out_writes_opt_ PWSTR           */ [In][Out] StringBuilder path
        );
    }
}
