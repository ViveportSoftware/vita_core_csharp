using System.IO;

namespace Htc.Vita.Core.Shell
{
    public static partial class ShellLink
    {
        public class FileLinkInfo
        {
            public FileInfo SourcePath { get; set; }
            public FileInfo TargetPath { get; set; }
            public FileInfo TargetIconPath { get; set; }
            public int TargetIconIndex { get; set; }
        }
    }
}
