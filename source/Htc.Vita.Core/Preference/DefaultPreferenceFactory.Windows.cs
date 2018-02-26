using System;
using System.IO;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        internal static class Windows
        {
            internal static string GetAppDataPath()
            {
                var path = Environment.GetEnvironmentVariable("LocalAppData");
                if (string.IsNullOrWhiteSpace(path))
                {
                    return "";
                }
                return Path.Combine(path, "HTC");
            }
        }
    }
}
