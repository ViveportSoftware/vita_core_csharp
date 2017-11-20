using System;
using System.Drawing;
using System.IO;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.Util
{
    public static class Extract
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(Extract));

        public static bool FromFileToIcon(FileInfo fromFile, FileInfo toIcon)
        {
            if (!Platform.IsWindows)
            {
                return false;
            }
            return FromFileToIconInWindows(fromFile, toIcon);
        }

        private static bool FromFileToIconInWindows(FileInfo fromFile, FileInfo toIcon)
        {
            if (fromFile == null || !fromFile.Exists || toIcon == null)
            {
                return false;
            }

            var targetPathDir = toIcon.Directory;
            if (!targetPathDir.Exists)
            {
                targetPathDir.Create();
            }

            Icon icon = null;
            var filePath = fromFile.FullName;
            try
            {
                icon = Icon.ExtractAssociatedIcon(filePath);
            }
            catch (Exception e)
            {
                Log.Error("Can not extract icon from path \"" + filePath + "\"" + e.Message);
            }
            if (icon == null)
            {
                return false;
            }

            filePath = toIcon.FullName;
            try
            {
                using (var stream = new FileStream(filePath, FileMode.CreateNew))
                {
                    icon.Save(stream);
                }
                return true;
            }
            catch (Exception e)
            {
                Log.Error("Can not dump icon to path \"" + filePath + "\"" + e.Message);
            }
            return false;
        }
    }
}
