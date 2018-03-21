using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.Shell
{
    public static partial class ShellLink
    {
        public static bool Create(FileLinkInfo fileLinkInfo)
        {
            if (!Platform.IsWindows)
            {
                return false;
            }
            return CreateInWindows(fileLinkInfo);
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
                Logger.GetInstance(typeof(ShellLink)).Error("Can not find type class from system with CLSID: " + guid);
                return false;
            }

            var wshShell = Activator.CreateInstance(type);
            object wshShortcut = null;
            var success = false;
            try
            {
                wshShortcut = type.InvokeMember(
                        "CreateShortcut",
                        BindingFlags.InvokeMethod,
                        null,
                        wshShell,
                        new object[] {targetPath.FullName + ".lnk"}
                );
                success = true;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ShellLink)).Error("Can not create wshShell class from system with CLSID: " + guid + ", " + e.Message);
            }
            finally
            {
                Marshal.FinalReleaseComObject(wshShell);
            }
            if (!success)
            {
                return false;
            }

            success = false;
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
                success = true;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ShellLink)).Error("Can not create shortcut: " + e.Message);
            }
            finally
            {
                Marshal.FinalReleaseComObject(wshShortcut);
            }
            return success;
        }
    }
}
