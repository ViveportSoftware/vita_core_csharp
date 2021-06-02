using System;

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
        }
    }
}
