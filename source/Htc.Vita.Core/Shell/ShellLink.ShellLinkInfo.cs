using System;
using System.IO;

namespace Htc.Vita.Core.Shell
{
    public static partial class ShellLink
    {
        /// <summary>
        /// Class ShellLinkInfo.
        /// </summary>
        public class ShellLinkInfo
        {
            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            public string Description { get; set; }
            /// <summary>
            /// Gets or sets the source activator identifier.
            /// </summary>
            /// <value>The source activator identifier.</value>
            public Guid SourceActivatorId { get; set; }
            /// <summary>
            /// Gets or sets the source application identifier.
            /// </summary>
            /// <value>The source application identifier.</value>
            public string SourceAppId { get; set; }
            /// <summary>
            /// Gets or sets the source arguments.
            /// </summary>
            /// <value>The source arguments.</value>
            public string SourceArguments { get; set; }
            /// <summary>
            /// Gets or sets the state of the source window.
            /// </summary>
            /// <value>The state of the source window.</value>
            public ShellLinkWindowState SourceWindowState { get; set; }
            /// <summary>
            /// Gets or sets the source working path.
            /// </summary>
            /// <value>The source working path.</value>
            public DirectoryInfo SourceWorkingPath { get; set; }
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
