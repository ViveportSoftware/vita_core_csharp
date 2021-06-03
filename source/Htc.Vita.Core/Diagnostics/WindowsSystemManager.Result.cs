using System;
using System.Collections.Generic;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class WindowsSystemManager
    {
        /// <summary>
        /// Class CheckResult.
        /// </summary>
        public class CheckResult
        {
            /// <summary>
            /// Gets or sets the FIPS status.
            /// </summary>
            /// <value>The FIPS status.</value>
            public WindowsFipsStatus FipsStatus { get; set; }
            /// <summary>
            /// Gets or sets the product name.
            /// </summary>
            /// <value>The product name.</value>
            public string ProductName { get; set; }
            /// <summary>
            /// Gets or sets the product type.
            /// </summary>
            /// <value>The product type.</value>
            public WindowsProductType ProductType { get; set; } = WindowsProductType.Unknown;
            /// <summary>
            /// Gets or sets the product version.
            /// </summary>
            /// <value>The product version.</value>
            public Version ProductVersion { get; set; }
            /// <summary>
            /// Gets or sets the secure boot status.
            /// </summary>
            /// <value>The secure boot status.</value>
            public WindowsSecureBootStatus SecureBootStatus { get; set; }
        }

        /// <summary>
        /// Class GetInstalledApplicationListResult.
        /// </summary>
        public class GetInstalledApplicationListResult
        {
            /// <summary>
            /// Gets or sets the installed application list.
            /// </summary>
            /// <value>The installed application list.</value>
            public List<WindowsApplicationInfo> InstalledApplicationList { get; set; } = new List<WindowsApplicationInfo>();
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetInstalledApplicationListStatus Status { get; set; }
        }

        /// <summary>
        /// Class GetInstalledUpdateListResult.
        /// </summary>
        public class GetInstalledUpdateListResult
        {
            /// <summary>
            /// Gets or sets the installed update list.
            /// </summary>
            /// <value>The installed update list.</value>
            public List<WindowsUpdateInfo> InstalledUpdateList { get; set; } = new List<WindowsUpdateInfo>();
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetInstalledUpdateListStatus Status { get; set; }
        }
    }
}
