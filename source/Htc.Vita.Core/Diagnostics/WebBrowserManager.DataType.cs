using System.IO;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class WebBrowserManager
    {
        /// <summary>
        /// Class WebBrowserInfo.
        /// </summary>
        public class WebBrowserInfo
        {
            /// <summary>
            /// Gets or sets the display name.
            /// </summary>
            /// <value>The display name.</value>
            public string DisplayName { get; set; }
            /// <summary>
            /// Gets or sets the launch parameters.
            /// </summary>
            /// <value>The launch parameters.</value>
            public string[] LaunchParams { get; set; }
            /// <summary>
            /// Gets or sets the launch path.
            /// </summary>
            /// <value>The launch path.</value>
            public FileInfo LaunchPath { get; set; }
            /// <summary>
            /// Gets or sets the scheme display name.
            /// </summary>
            /// <value>The scheme display name.</value>
            public string SchemeDisplayName { get; set; } = "Unknown";
            /// <summary>
            /// Gets or sets the supported scheme.
            /// </summary>
            /// <value>The supported scheme.</value>
            public Scheme SupportedScheme { get; set; } = Scheme.Unknown;
            /// <summary>
            /// Gets or sets the web browser type.
            /// </summary>
            /// <value>The web browser type.</value>
            public WebBrowserType Type { get; set; } = WebBrowserType.Unknown;
        }

        /// <summary>
        /// Enum WebBrowserType
        /// </summary>
        public enum WebBrowserType
        {
            /// <summary>
            /// Unknown web browser
            /// </summary>
            Unknown,
            /// <summary>
            /// Google Chrome
            /// </summary>
            GoogleChrome,
            /// <summary>
            /// Microsoft Edge
            /// </summary>
            MicrosoftEdge,
            /// <summary>
            /// Microsoft Edge (Chromium-based)
            /// </summary>
            MicrosoftEdgeChromium,
            /// <summary>
            /// Microsoft Internet Explorer
            /// </summary>
            MicrosoftInternetExplorer,
            /// <summary>
            /// Mozilla Firefox
            /// </summary>
            MozillaFirefox,
            /// <summary>
            /// Opera
            /// </summary>
            Opera,
            /// <summary>
            /// Qihoo 360 Extreme Browser
            /// </summary>
            Qihoo360ExtremeBrowser,
            /// <summary>
            /// Qihoo 360 Safe Browser
            /// </summary>
            Qihoo360SafeBrowser
        }

        /// <summary>
        /// Enum Scheme
        /// </summary>
        public enum Scheme
        {
            /// <summary>
            /// Unknown scheme
            /// </summary>
            Unknown,
            /// <summary>
            /// The HTTP scheme
            /// </summary>
            Http,
            /// <summary>
            /// The HTTPS scheme
            /// </summary>
            Https
        }
    }
}
