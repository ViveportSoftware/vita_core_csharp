using System.Collections.Generic;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class WebBrowserManager
    {
        /// <summary>
        /// Class GetInstalledWebBrowserListResult.
        /// </summary>
        public class GetInstalledWebBrowserListResult
        {
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetInstalledWebBrowserListStatus Status { get; set; }
            /// <summary>
            /// Gets or sets the WebBrowser list.
            /// </summary>
            /// <value>The WebBrowser list.</value>
            public List<WebBrowserInfo> WebBrowserList { get; set; } = new List<WebBrowserInfo>();
        }

        /// <summary>
        /// Enum GetInstalledWebBrowserListStatus
        /// </summary>
        public enum GetInstalledWebBrowserListStatus
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// Ok
            /// </summary>
            Ok,
            /// <summary>
            /// Unsupported platform
            /// </summary>
            UnsupportedPlatform
        }
    }
}
