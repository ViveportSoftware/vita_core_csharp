using System.ComponentModel;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class Mime.
    /// </summary>
    public static class Mime
    {
        /// <summary>
        /// Enum ContentType
        /// </summary>
        public enum ContentType
        {
            /// <summary>
            /// application/octet-stream
            /// </summary>
            [Description("application/octet-stream")]
            Application_OctetStream,
            /// <summary>
            /// application/json
            /// </summary>
            [Description("application/json")]
            Application_Json,
            /// <summary>
            /// application/x-www-form-urlencoded
            /// </summary>
            [Description("application/x-www-form-urlencoded")]
            Application_XWwwFormUrlencoded,
            /// <summary>
            /// text/plain
            /// </summary>
            [Description("text/plain")]
            Text_Plain,
            /// <summary>
            /// Any content type
            /// </summary>
            [Description("*/*")]
            Any_Any,
            /// <summary>
            /// image/vnd.microsoft.icon
            /// </summary>
            [Description("image/vnd.microsoft.icon")]
            Image_VndMicrosoftIcon,
            /// <summary>
            /// image/x-icon
            /// </summary>
            [Description("image/x-icon")]
            Image_XIcon,
            /// <summary>
            /// text/html
            /// </summary>
            [Description("text/html")]
            Text_Html
        }
    }
}
