using System.Collections.Generic;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class WindowsStoreAppManager
    {
        /// <summary>
        /// Class WindowsStoreAppPackageInfo.
        /// </summary>
        public class WindowsStoreAppPackageInfo
        {
            /// <summary>
            /// Gets or sets the family name.
            /// </summary>
            /// <value>The family name.</value>
            public string FamilyName { get; set; }
            /// <summary>
            /// Gets or sets the full name list.
            /// </summary>
            /// <value>The full name list.</value>
            public List<string> FullNameList { get; set; } = new List<string>();
        }
    }
}
