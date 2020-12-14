namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsJobProxySettings
        {
            internal string ProxyBypassList { get; set; }
            internal string ProxyList { get; set; }
            internal BitsJobProxyUsage Usage { get; set; }
        }
    }
}
