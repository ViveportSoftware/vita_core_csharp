namespace Htc.Vita.Core.Shell
{
    public abstract partial class UriSchemeManager
    {
        /// <summary>
        /// Class UriSchemeInfo.
        /// </summary>
        public class UriSchemeInfo
        {
            /// <summary>
            /// Gets or sets the command path.
            /// </summary>
            /// <value>The command path.</value>
            public string CommandPath { get; set; } = string.Empty;
            /// <summary>
            /// Gets or sets the command parameter.
            /// </summary>
            /// <value>The command parameter.</value>
            public string CommandParameter { get; set; } = string.Empty;
            /// <summary>
            /// Gets or sets the default icon.
            /// </summary>
            /// <value>The default icon.</value>
            public string DefaultIcon { get; set; } = string.Empty;
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; } = string.Empty;
        }
    }
}
