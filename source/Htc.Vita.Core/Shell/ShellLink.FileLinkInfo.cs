using System.IO;

namespace Htc.Vita.Core.Shell
{
    public static partial class ShellLink
    {
        /// <summary>
        /// Class FileLinkInfo.
        /// </summary>
        public class FileLinkInfo
        {
            /// <summary>
            /// Gets or sets the source path.
            /// </summary>
            /// <value>The source path.</value>
            public FileInfo SourcePath { get; set; }
            /// <summary>
            /// Gets or sets the target path.
            /// </summary>
            /// <value>The target path.</value>
            public FileInfo TargetPath { get; set; }
            /// <summary>
            /// Gets or sets the target icon path.
            /// </summary>
            /// <value>The target icon path.</value>
            public FileInfo TargetIconPath { get; set; }
            /// <summary>
            /// Gets or sets the target icon index.
            /// </summary>
            /// <value>The target icon index.</value>
            public int TargetIconIndex { get; set; }
        }
    }
}
