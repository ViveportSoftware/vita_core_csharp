using System.Net;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    public class DefaultWebProxyFactory : WebProxyFactory
    {
        protected override IWebProxy OnGetWebProxy()
        {
            var webProxyUri = ParseWebProxyUri(
                    Registry.GetStringValue(
                            Registry.Hive.CurrentUser,
                            "SOFTWARE\\Vita\\Core\\Net",
                            "Proxy"
                    )
            ) ?? ParseWebProxyUri(
                    Registry.GetStringValue(
                            Registry.Hive.LocalMachine,
                            "SOFTWARE\\Vita\\Core\\Net",
                            "Proxy"
                    )
            );
            var result = WebRequest.GetSystemWebProxy();
            if (webProxyUri != null)
            {
                result = new WebProxy(webProxyUri);
            }
            return result;
        }
    }
}
