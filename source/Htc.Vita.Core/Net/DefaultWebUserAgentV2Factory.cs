namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class DefaultWebUserAgentV2Factory.
    /// Implements the <see cref="WebUserAgentV2Factory" />
    /// </summary>
    /// <seealso cref="WebUserAgentV2Factory" />
    public partial class DefaultWebUserAgentV2Factory : WebUserAgentV2Factory
    {
        /// <inheritdoc />
        protected override WebUserAgentV2 OnGetWebUserAgent()
        {
            return new DefaultWebUserAgentV2();
        }
    }
}
