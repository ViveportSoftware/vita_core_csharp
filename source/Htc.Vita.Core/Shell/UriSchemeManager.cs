using System.Linq;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Shell
{
    public partial class UriSchemeManager
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(UriSchemeManager));

        public static UriSchemeInfo GetSystemUriScheme(string name)
        {
            if (!name.All(c => char.IsLetterOrDigit(c) || c == '+' || c == '-' || c == '.'))
            {
                Log.Error("Do not find valid scheme name: \"" + name + "\"");
                return new UriSchemeInfo
                {
                        Name = name
                };
            }

            return Windows.GetSystemUriSchemeInPlatform(name);
        }
    }
}
