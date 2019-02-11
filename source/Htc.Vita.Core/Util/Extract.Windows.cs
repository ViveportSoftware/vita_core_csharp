using System;
using System.Drawing;
using System.IO;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Util
{
    public static partial class Extract
    {
        internal static class Windows
        {
            internal static bool FromFileToIconInPlatform(FileInfo fromFile, FileInfo toIcon)
            {
                if (fromFile == null || !fromFile.Exists || toIcon == null)
                {
                    return false;
                }

                var targetPathDir = toIcon.Directory;
                if (targetPathDir != null && !targetPathDir.Exists)
                {
                    targetPathDir.Create();
                }

                try
                {
                    using (var icon = Icon.ExtractAssociatedIcon(fromFile.FullName))
                    {
                        if (icon == null)
                        {
                            return false;
                        }
                        using (var stream = new FileStream(toIcon.FullName, FileMode.CreateNew))
                        {
                            icon.Save(stream);
                        }
                        return true;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Extract)).Error("Can not extract icon to path: " + e.Message);
                }
                return false;
            }
        }
    }
}
