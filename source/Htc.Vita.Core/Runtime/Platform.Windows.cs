using System;
using System.Management;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public static partial class Platform
    {
        internal static class Windows
        {
            internal static string GetProductNameInPlatform()
            {
                try
                {
                    using (var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
                    {
                        foreach (var o in searcher.Get())
                        {
                            var managementObject = o as ManagementObject;
                            if (managementObject == null)
                            {
                                continue;
                            }
                            return (string)managementObject.GetPropertyValue("Caption");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Platform)).Error("Can not detect product name", e);
                }
                return "UNKNOWN";
            }
        }
    }
}
