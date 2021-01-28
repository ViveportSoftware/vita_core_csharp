using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class ShellLink : IDisposable
        {
            private static PropertyKey _appUserModelActivatorId = new PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 26);
            private static PropertyKey _appUserModelId = new PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 5);

            private bool _disposed;
            private IShellLinkW _shellLink;

            internal ShellLink(IShellLinkW iShellLinkW)
            {
                _shellLink = iShellLinkW;
            }

            ~ShellLink()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }

                if (disposing)
                {
                }

                if (_shellLink == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_shellLink))
                {
                    Marshal.ReleaseComObject(_shellLink);
                }
                _shellLink = null;

                _disposed = true;
            }

            internal static ShellLink GetInstance()
            {
                var iShellLinkW = Activator.CreateInstance(typeof(ClsidShellLink)) as IShellLinkW;
                if (iShellLinkW != null)
                {
                    return new ShellLink(iShellLinkW);
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot create new {nameof(IShellLinkW)}");
                return null;
            }

            internal bool Save(FileInfo targetPath)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var persistFile = _shellLink as IPersistFile;
                if (persistFile == null)
                {
                    Logger.GetInstance(typeof(ShellLink)).Error("Cannot get persist file.");
                    return false;
                }

                persistFile.Save(
                        targetPath.FullName + ".lnk",
                        false
                );
                return true;
            }

            internal bool SetDescription(string description)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetDescription(description);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set description. error: {error}");
                return false;
            }

            internal bool SetSourceActivatorId(Guid activatorId)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var propertyStore = _shellLink as IPropertyStore;
                if (propertyStore == null)
                {
                    Logger.GetInstance(typeof(ShellLink)).Error("Cannot get property store.");
                    return false;
                }

                var pv = new PropVariant(activatorId);
                var error = propertyStore.SetValue(
                        ref _appUserModelActivatorId,
                        ref pv
                );
                if (error == HResult.SOk)
                {
                    error = propertyStore.Commit();
                }
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set source activator id. error: {error}");
                return false;
            }

            internal bool SetSourceAppId(string sourceAppId)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var propertyStore = _shellLink as IPropertyStore;
                if (propertyStore == null)
                {
                    Logger.GetInstance(typeof(ShellLink)).Error("Cannot get property store.");
                    return false;
                }

                var pv = new PropVariant(sourceAppId);
                var error = propertyStore.SetValue(
                        ref _appUserModelId,
                        ref pv
                );
                if (error == HResult.SOk)
                {
                    error = propertyStore.Commit();
                }
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set source app id. error: {error}");
                return false;
            }

            internal bool SetSourceArguments(string sourceArguments)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetArguments(sourceArguments);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set source arguments. error: {error}");
                return false;
            }

            internal bool SetSourcePath(FileInfo sourcePath)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetPath(sourcePath.FullName);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set source path. error: {error}");
                return false;
            }

            internal bool SetSourceShowWindowCommand(ShowWindowCommand showWindowCommand)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetShowCmd(showWindowCommand);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set source show window command. error: {error}");
                return false;
            }

            internal bool SetSourceWorkingPath(DirectoryInfo workingDirectory)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetWorkingDirectory(workingDirectory.FullName);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set source working path. error: {error}");
                return false;
            }

            internal bool SetTargetIcon(
                    FileInfo targetIconPath,
                    int targetIconIndex)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLink), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetIconLocation(
                        targetIconPath.FullName,
                        targetIconIndex
                );
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLink)).Error($"Cannot set target icon. error: {error}");
                return false;
            }
        }
    }
}
