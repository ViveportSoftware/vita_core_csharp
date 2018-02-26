using System;
using System.IO;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        internal class Unix
        {
            internal static string GetAppDataPath()
            {
                var path = Environment.GetEnvironmentVariable("HOME");
                if (string.IsNullOrWhiteSpace(path))
                {
                    return "";
                }
                return Path.Combine(path, ".htc");
            }
        }
    }
}
