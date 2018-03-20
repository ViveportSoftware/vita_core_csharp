namespace Htc.Vita.Core.Shell
{
    public partial class UriSchemeManager
    {
        public class UriSchemeInfo
        {
            public string CommandPath { get; set; } = string.Empty;
            public string CommandParameter { get; set; } = string.Empty;
            public string DefaultIcon { get; set; } = string.Empty;
            public string Name { get; set; } = string.Empty;
        }
    }
}
