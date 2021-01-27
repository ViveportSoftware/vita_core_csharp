using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.Shell
{
    /// <summary>
    /// Class ShellLink.
    /// </summary>
    public static partial class ShellLink
    {
        private static Windows.ShowWindowCommand ConvertFrom(ShellLinkWindowState windowState)
        {
            if (windowState == ShellLinkWindowState.Maximized)
            {
                return Windows.ShowWindowCommand.ShowMaximized;
            }

            if (windowState == ShellLinkWindowState.Minimized)
            {
                return Windows.ShowWindowCommand.ShowMinNoActive;
            }

            return Windows.ShowWindowCommand.ShowNormal;
        }

        /// <summary>
        /// Creates the specified file link.
        /// </summary>
        /// <param name="fileLinkInfo">The file link information.</param>
        /// <returns><c>true</c> if creating the file link successfully, <c>false</c> otherwise.</returns>
        public static bool Create(FileLinkInfo fileLinkInfo)
        {
            if (!Platform.IsWindows)
            {
                return false;
            }
            return CreateInWindows(fileLinkInfo);
        }

        /// <summary>
        /// Creates the specified shell link.
        /// </summary>
        /// <param name="shellLinkInfo">The shell link information.</param>
        /// <returns><c>true</c> if creating the shell link successfully, <c>false</c> otherwise.</returns>
        public static bool Create(ShellLinkInfo shellLinkInfo)
        {
            if (!Platform.IsWindows)
            {
                return false;
            }
            return CreateInWindows(shellLinkInfo);
        }

        private static bool CreateInWindows(FileLinkInfo fileLinkInfo)
        {
            if (fileLinkInfo == null)
            {
                return false;
            }

            var sourcePath = fileLinkInfo.SourcePath;
            var targetPath = fileLinkInfo.TargetPath;
            var targetIconPath = fileLinkInfo.TargetIconPath;
            var targetIconIndex = fileLinkInfo.TargetIconIndex;
            if (sourcePath == null || !sourcePath.Exists || targetPath == null)
            {
                return false;
            }

            var targetPathDir = targetPath.Directory;
            if (targetPathDir == null)
            {
                return false;
            }
            if (!targetPathDir.Exists)
            {
                targetPathDir.Create();
            }

            var guid = new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8");
            var type = Type.GetTypeFromCLSID(guid);
            if (type == null)
            {
                Logger.GetInstance(typeof(ShellLink)).Error($"Can not find type class from system with CLSID: {guid}");
                return false;
            }

            object wshShell = null;
            try
            {
                wshShell = Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ShellLink)).Error($"Can not create wshShell class from system with CLSID: {guid}, {e.Message}");
            }
            if (wshShell == null)
            {
                return false;
            }

            object wshShortcut;
            try
            {
                wshShortcut = type.InvokeMember(
                        "CreateShortcut",
                        BindingFlags.InvokeMethod,
                        null,
                        wshShell,
                        new object[] {targetPath.FullName + ".lnk"}
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ShellLink)).Error($"Can not create wshShortcut class from wshShell: {e.Message}");
                return false;
            }
            finally
            {
                Marshal.FinalReleaseComObject(wshShell);
            }

            try
            {
                type.InvokeMember(
                        "TargetPath",
                        BindingFlags.SetProperty,
                        null,
                        wshShortcut,
                        new object[] { sourcePath.FullName }
                );
                type.InvokeMember(
                        "IconLocation",
                        BindingFlags.SetProperty,
                        null,
                        wshShortcut,
                        new object[] { targetIconPath.FullName + ", " + targetIconIndex }
                );
                type.InvokeMember(
                        "Save",
                        BindingFlags.InvokeMethod,
                        null,
                        wshShortcut,
                        null
                );
                return true;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ShellLink)).Error($"Can not create shortcut: {e.Message}");
            }
            finally
            {
                Marshal.FinalReleaseComObject(wshShortcut);
            }
            return false;
        }

        private static bool CreateInWindows(ShellLinkInfo shellLinkInfo)
        {
            if (shellLinkInfo == null)
            {
                return false;
            }

            var sourcePath = shellLinkInfo.SourcePath;
            var targetPath = shellLinkInfo.TargetPath;
            var targetIconPath = shellLinkInfo.TargetIconPath;
            var targetIconIndex = shellLinkInfo.TargetIconIndex;
            if (sourcePath == null || !sourcePath.Exists || targetPath == null)
            {
                return false;
            }

            var sourceWorkingPath = shellLinkInfo.SourceWorkingPath ?? sourcePath.Directory;
            if (sourceWorkingPath == null)
            {
                return false;
            }

            var targetParent = targetPath.Directory;
            if (targetParent != null && !targetParent.Exists)
            {
                Directory.CreateDirectory(targetParent.FullName);
            }

            var description = shellLinkInfo.Description;
            var sourceActivatorId = shellLinkInfo.SourceActivatorId;
            var sourceAppId = shellLinkInfo.SourceAppId;
            var sourceArguments = shellLinkInfo.SourceArguments;
            var sourceShowWindowCommand = ConvertFrom(shellLinkInfo.SourceWindowState);

            using (var windowShellLink = Windows.ShellLink.GetInstance())
            {
                var success = windowShellLink.SetSourcePath(sourcePath);
                if (!success)
                {
                    return false;
                }

                success = windowShellLink.SetSourceShowWindowCommand(sourceShowWindowCommand);
                if (!success)
                {
                    return false;
                }

                success = windowShellLink.SetSourceWorkingPath(sourceWorkingPath);
                if (!success)
                {
                    return false;
                }

                if (targetIconPath != null && File.Exists(targetIconPath.FullName))
                {
                    success = windowShellLink.SetTargetIcon(
                            targetIconPath,
                            targetIconIndex
                    );
                    if (!success)
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrWhiteSpace(description))
                {
                    success = windowShellLink.SetDescription(description);
                    if (!success)
                    {
                        return false;
                    }
                }

                if (sourceActivatorId != Guid.Empty)
                {
                    success = windowShellLink.SetSourceActivatorId(sourceActivatorId);
                    if (!success)
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrWhiteSpace(sourceAppId))
                {
                    success = windowShellLink.SetSourceAppId(sourceAppId);
                    if (!success)
                    {
                        return false;
                    }
                }

                if (!string.IsNullOrWhiteSpace(sourceArguments))
                {
                    success = windowShellLink.SetSourceArguments(sourceArguments);
                    if (!success)
                    {
                        return false;
                    }
                }

                success = windowShellLink.Save(targetPath);
                if (!success)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Enum ShellLinkWindowState
        /// </summary>
        public enum ShellLinkWindowState
        {
            /// <summary>
            /// Normal Window state
            /// </summary>
            Normal = 0,
            /// <summary>
            /// Window maximized
            /// </summary>
            Maximized = 1,
            /// <summary>
            /// Window minimized 
            /// </summary>
            Minimized = 2
        }
    }
}
