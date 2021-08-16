using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;
using Convert = Htc.Vita.Core.Util.Convert;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal static class ComWrapperMarshal
        {
            internal static bool IsComObject(object o)
            {
                return Marshal.IsComObject(o);
            }

            internal static int ReleaseComObject(object o)
            {
#pragma warning disable CA1416
                return Marshal.ReleaseComObject(o);
#pragma warning restore CA1416
            }
        }

        internal class BitsCallback : IBackgroundCopyCallback
        {
            private readonly BitsManager _bitsManager;

            internal BitsCallback(BitsManager bitsManager)
            {
                _bitsManager = bitsManager;
            }

            public BitsResult JobTransferred(IBackgroundCopyJob pJob)
            {
                if (pJob == null)
                {
                    return BitsResult.SOk;
                }

                if (_bitsManager == null)
                {
                    return BitsResult.SOk;
                }

                Guid guid;
                var bitsResult = pJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    _bitsManager.NotifyJobTransferred(guid.ToString());
                }

                return BitsResult.SOk;
            }

            public BitsResult JobError(
                    IBackgroundCopyJob pJob,
                    IBackgroundCopyError pError)
            {
                if (pJob == null)
                {
                    return BitsResult.SOk;
                }

                if (_bitsManager == null)
                {
                    return BitsResult.SOk;
                }

                Guid guid;
                var bitsResult = pJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    _bitsManager.NotifyJobError(guid.ToString());
                }

                return BitsResult.SOk;
            }

            public BitsResult JobModification(
                    IBackgroundCopyJob pJob,
                    uint dwReserved)
            {
                if (pJob == null)
                {
                    return BitsResult.SOk;
                }

                if (_bitsManager == null)
                {
                    return BitsResult.SOk;
                }

                Guid guid;
                var bitsResult = pJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    _bitsManager.NotifyJobModification(guid.ToString());
                }

                return BitsResult.SOk;
            }
        }

        internal class BitsError : IDisposable
        {
            private IBackgroundCopyError _backgroundCopyError;
            private bool _disposed;

            internal BitsError(IBackgroundCopyError backgroundCopyError)
            {
                _backgroundCopyError = backgroundCopyError;
            }

            ~BitsError()
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
                    // Disposing managed resource
                }

                if (_backgroundCopyError == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_backgroundCopyError))
                {
                    ComWrapperMarshal.ReleaseComObject(_backgroundCopyError);
                }
                _backgroundCopyError = null;

                _disposed = true;
            }

            internal BitsErrorContext GetErrorContext()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                BitsErrorContext errorContext;
                HResult hResult;
                var bitsResult = _backgroundCopyError.GetError(
                        out errorContext,
                        out hResult
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return errorContext;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get error context. error: {bitsResult}");
                return BitsErrorContext.Unknown;
            }

            internal string GetErrorContextDescription()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                string errorContextDescription;
                var bitsResult = _backgroundCopyError.GetErrorContextDescription(
                        0, // 0 for language neutral
                        out errorContextDescription
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return errorContextDescription;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get error context description. error: {bitsResult}");
                return null;
            }

            internal string GetErrorDescription()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                string errorDescription;
                var bitsResult = _backgroundCopyError.GetErrorDescription(
                        0, // 0 for language neutral
                        out errorDescription
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return errorDescription;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get error description. error: {bitsResult}");
                return null;
            }

            internal BitsFile GetFile()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                IBackgroundCopyFile iBackgroundCopyFile;
                var bitsResult = _backgroundCopyError.GetFile(out iBackgroundCopyFile);
                if (bitsResult == BitsResult.SOk)
                {
                    return new BitsFile(iBackgroundCopyFile);
                }

                if (bitsResult != BitsResult.EFileNotAvailable)
                {
                    Logger.GetInstance(typeof(BitsError)).Error($"Cannot get file. error: {bitsResult}");
                }
                return null;
            }

            internal string GetProtocol()
            {
                if (_backgroundCopyError == null)
                {
                    throw new ObjectDisposedException(nameof(BitsError), $"Cannot access a closed {nameof(IBackgroundCopyError)}.");
                }

                string protocol;
                var bitsResult = _backgroundCopyError.GetProtocol(out protocol);
                if (bitsResult == BitsResult.SOk)
                {
                    return protocol;
                }
                Logger.GetInstance(typeof(BitsError)).Error($"Cannot get protocol. error: {bitsResult}");
                return null;
            }
        }

        internal class BitsFile : IDisposable
        {
            private IBackgroundCopyFile _backgroundCopyFile;
            private bool _disposed;

            internal BitsFile(IBackgroundCopyFile backgroundCopyFile)
            {
                _backgroundCopyFile = backgroundCopyFile;
            }

            ~BitsFile()
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
                    // Disposing managed resource
                }

                if (_backgroundCopyFile == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_backgroundCopyFile))
                {
                    ComWrapperMarshal.ReleaseComObject(_backgroundCopyFile);
                }
                _backgroundCopyFile = null;

                _disposed = true;
            }

            internal string GetLocalName()
            {
                if (_backgroundCopyFile == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFile), $"Cannot access a closed {nameof(IBackgroundCopyFile)}.");
                }

                string localName;
                var bitsResult = _backgroundCopyFile.GetLocalName(out localName);
                if (bitsResult == BitsResult.SOk)
                {
                    return localName;
                }
                Logger.GetInstance(typeof(BitsFile)).Error($"Cannot get local name. error: {bitsResult}");
                return null;
            }

            internal BitsFileProgress GetProgress()
            {
                if (_backgroundCopyFile == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFile), $"Cannot access a closed {nameof(IBackgroundCopyFile)}.");
                }

                BitsFileProgress fileProgress;
                var bitsResult = _backgroundCopyFile.GetProgress(out fileProgress);
                if (bitsResult == BitsResult.SOk)
                {
                    return fileProgress;
                }
                Logger.GetInstance(typeof(BitsFile)).Error($"Cannot get progress. error: {bitsResult}");
                return new BitsFileProgress();
            }

            internal string GetRemoteName()
            {
                if (_backgroundCopyFile == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFile), $"Cannot access a closed {nameof(IBackgroundCopyFile)}.");
                }

                string remoteName;
                var bitsResult = _backgroundCopyFile.GetRemoteName(out remoteName);
                if (bitsResult == BitsResult.SOk)
                {
                    return remoteName;
                }
                Logger.GetInstance(typeof(BitsFile)).Error($"Cannot get remote name. error: {bitsResult}");
                return null;
            }
        }

        internal class BitsFiles : IDisposable
        {
            private IEnumBackgroundCopyFiles _enumBackgroundCopyFiles;
            private bool _disposed;

            internal BitsFiles(IEnumBackgroundCopyFiles enumBackgroundCopyFiles)
            {
                _enumBackgroundCopyFiles = enumBackgroundCopyFiles;
            }

            ~BitsFiles()
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
                    // Disposing managed resource
                }

                if (_enumBackgroundCopyFiles == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_enumBackgroundCopyFiles))
                {
                    ComWrapperMarshal.ReleaseComObject(_enumBackgroundCopyFiles);
                }
                _enumBackgroundCopyFiles = null;

                _disposed = true;
            }

            internal uint GetCount()
            {
                if (_enumBackgroundCopyFiles == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFiles), $"Cannot access a closed {nameof(IEnumBackgroundCopyFiles)}.");
                }

                uint count;
                var bitsResult = _enumBackgroundCopyFiles.GetCount(out count);
                if (bitsResult == BitsResult.SOk)
                {
                    return count;
                }
                Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot get file count. error: {bitsResult}");
                return 0;
            }

            internal BitsFile GetFile(uint index)
            {
                if (_enumBackgroundCopyFiles == null)
                {
                    throw new ObjectDisposedException(nameof(BitsFiles), $"Cannot access a closed {nameof(IEnumBackgroundCopyFiles)}.");
                }

                var bitsResult = _enumBackgroundCopyFiles.Reset();
                if (bitsResult != BitsResult.SOk)
                {
                    Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot reset file index. error: {bitsResult}");
                    return null;
                }

                if (index > 0)
                {
                    bitsResult = _enumBackgroundCopyFiles.Skip(index);
                    if (bitsResult != BitsResult.SOk)
                    {
                        Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot skip file items. error: {bitsResult}");
                        return null;
                    }
                }

                IBackgroundCopyFile iBackgroundCopyFile;
                uint fetchedFileCount;
                bitsResult = _enumBackgroundCopyFiles.Next(
                        1,
                        out iBackgroundCopyFile,
                        out fetchedFileCount
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return new BitsFile(iBackgroundCopyFile);
                }

                Logger.GetInstance(typeof(BitsFiles)).Error($"Cannot get file item. error: {bitsResult}");
                return null;
            }
        }

        internal class BitsJob : IDisposable
        {
            private IBackgroundCopyJob _backgroundCopyJob;
            private bool _disposed;

            internal BitsJob(IBackgroundCopyJob backgroundCopyJob)
            {
                _backgroundCopyJob = backgroundCopyJob;
            }

            ~BitsJob()
            {
                Dispose(false);
            }

            internal bool AddFile(BitsFileInfo file)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var localName = file.LocalName;
                if (string.IsNullOrWhiteSpace(localName))
                {
                    return false;
                }

                var remoteName = file.RemoteName;
                if (string.IsNullOrWhiteSpace(remoteName))
                {
                    return false;
                }

                var bitsResult = _backgroundCopyJob.AddFile(
                        remoteName,
                        localName
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot add single file. error: {bitsResult}");
                return false;
            }

            internal bool Cancel()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.Cancel();
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot cancel job. error: {bitsResult}");
                return false;
            }

            internal bool Complete()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.Complete();
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot complete job. error: {bitsResult}");
                return false;
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
                    // Disposing managed resource
                }

                if (_backgroundCopyJob == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_backgroundCopyJob))
                {
                    ComWrapperMarshal.ReleaseComObject(_backgroundCopyJob);
                }
                _backgroundCopyJob = null;

                _disposed = true;
            }

            internal string GetDescription()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                string description;
                var bitsResult = _backgroundCopyJob.GetDescription(out description);
                if (bitsResult == BitsResult.SOk)
                {
                    return description;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job description. error: {bitsResult}");
                return null;
            }

            internal string GetDisplayName()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                string displayName;
                var bitsResult = _backgroundCopyJob.GetDisplayName(out displayName);
                if (bitsResult == BitsResult.SOk)
                {
                    return displayName;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job display name. error: {bitsResult}");
                return null;
            }

            internal BitsError GetError()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                IBackgroundCopyError iBackgroundCopyError;
                var bitsResult = _backgroundCopyJob.GetError(out iBackgroundCopyError);
                if (bitsResult == BitsResult.SOk)
                {
                    return new BitsError(iBackgroundCopyError);
                }

                if (bitsResult != BitsResult.EErrorInformationUnavailable)
                {
                    Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job error. error: {bitsResult}");
                }
                return null;
            }

            internal uint GetErrorCount()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                uint errorCount;
                var bitsResult = _backgroundCopyJob.GetErrorCount(out errorCount);
                if (bitsResult == BitsResult.SOk)
                {
                    return errorCount;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job error count. error: {bitsResult}");
                return 0;
            }

            internal BitsFiles GetFiles()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                IEnumBackgroundCopyFiles iEnumBackgroundCopyFiles;
                var bitsResult = _backgroundCopyJob.EnumFiles(out iEnumBackgroundCopyFiles);
                if (bitsResult == BitsResult.SOk)
                {
                    return new BitsFiles(iEnumBackgroundCopyFiles);
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job files. error: {bitsResult}");
                return null;
            }

            internal string GetId()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                Guid guid;
                var bitsResult = _backgroundCopyJob.GetId(out guid);
                if (bitsResult == BitsResult.SOk)
                {
                    return guid.ToString();
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job id. error: {bitsResult}");
                return null;
            }

            internal uint GetMinimumRetryDelay()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                uint minimumRetryDelay;
                var bitsResult = _backgroundCopyJob.GetMinimumRetryDelay(out minimumRetryDelay);
                if (bitsResult == BitsResult.SOk)
                {
                    return minimumRetryDelay;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job minimum retry delay. error: {bitsResult}");
                return 0;
            }

            internal uint GetNoProgressTimeout()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                uint noProgressTimeout;
                var bitsResult = _backgroundCopyJob.GetNoProgressTimeout(out noProgressTimeout);
                if (bitsResult == BitsResult.SOk)
                {
                    return noProgressTimeout;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job no progress timeout. error: {bitsResult}");
                return 0;
            }

            internal BitsNotifyFlags GetNotifyFlags()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsNotifyFlags notifyFlags;
                var bitsResult = _backgroundCopyJob.GetNotifyFlags(out notifyFlags);
                if (bitsResult == BitsResult.SOk)
                {
                    return notifyFlags;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job notify flags. error: {bitsResult}");
                return BitsNotifyFlags.None;
            }

            internal string GetOwner()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                string owner;
                var bitsResult = _backgroundCopyJob.GetOwner(out owner);
                if (bitsResult == BitsResult.SOk)
                {
                    return owner;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job owner. error: {bitsResult}");
                return null;
            }

            internal BitsJobPriority GetPriority()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsJobPriority jobPriority;
                var bitsResult = _backgroundCopyJob.GetPriority(out jobPriority);
                if (bitsResult == BitsResult.SOk)
                {
                    return jobPriority;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job priority. error: {bitsResult}");
                return BitsJobPriority.Foreground;
            }

            internal BitsJobProgress GetProgress()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsJobProgress jobProgress;
                var bitsResult = _backgroundCopyJob.GetProgress(out jobProgress);
                if (bitsResult == BitsResult.SOk)
                {
                    return jobProgress;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job progress. error: {bitsResult}");
                return new BitsJobProgress();
            }

            internal BitsJobProxySettings GetProxySettings()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsJobProxyUsage usage;
                string proxyList;
                string proxyPassList;
                var bitsResult = _backgroundCopyJob.GetProxySettings(
                        out usage,
                        out proxyList,
                        out proxyPassList
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return new BitsJobProxySettings
                    {
                        ProxyList = proxyList,
                        ProxyBypassList = proxyPassList,
                        Usage = usage
                    };
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job proxy settings. error: {bitsResult}");
                return null;
            }

            internal BitsJobState GetState()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsJobState jobState;
                var bitsResult = _backgroundCopyJob.GetState(out jobState);
                if (bitsResult == BitsResult.SOk)
                {
                    return jobState;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job state. error: {bitsResult}");
                return BitsJobState.Error;
            }

            internal BitsJobTimes GetTimes()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsJobTimes jobTimes;
                var bitsResult = _backgroundCopyJob.GetTimes(out jobTimes);
                if (bitsResult == BitsResult.SOk)
                {
                    return jobTimes;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job times. error: {bitsResult}");
                return new BitsJobTimes();
            }

            internal new BitsJobType GetType()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsJobType jobType;
                var bitsResult = _backgroundCopyJob.GetType(out jobType);
                if (bitsResult == BitsResult.SOk)
                {
                    return jobType;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job type. error: {bitsResult}");
                return BitsJobType.Download;
            }

            internal bool Resume()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.Resume();
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot resume job. error: {bitsResult}");
                return false;
            }

            internal bool SetDescription(string description)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var realDescription = description ?? string.Empty;
                var bitsResult = _backgroundCopyJob.SetDescription(realDescription);
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job description to \"{realDescription}\". error: {bitsResult}");
                return false;
            }

            internal bool SetDisplayName(string displayName)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var realDisplayName = displayName ?? string.Empty;
                var bitsResult = _backgroundCopyJob.SetDisplayName(realDisplayName);
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job display name to \"{realDisplayName}\". error: {bitsResult}");
                return false;
            }

            internal bool SetMinimumRetryDelay(uint minimumRetryDelay)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.SetMinimumRetryDelay(minimumRetryDelay);
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job minimum retry delay to \"{minimumRetryDelay}\". error: {bitsResult}");
                return false;
            }

            internal bool SetNoProgressTimeout(uint noProgressTimeout)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.SetNoProgressTimeout(noProgressTimeout);
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job no progress timeout to \"{noProgressTimeout}\". error: {bitsResult}");
                return false;
            }

            internal bool SetNotifyFlags(BitsNotifyFlags notifyFlags)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.SetNotifyFlags(notifyFlags);
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job notify flags to \"{notifyFlags}\". error: {bitsResult}");
                return false;
            }

            internal bool SetNotifyInterface(BitsCallback callback)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.SetNotifyInterface(callback);
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job notify interface. error: {bitsResult}");
                return false;
            }

            internal bool SetPriority(BitsJobPriority priority)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.SetPriority(priority);
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job priority to \"{priority}\". error: {bitsResult}");
                return false;
            }

            internal bool SetProxySettings(BitsJobProxySettings proxySettings)
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.SetProxySettings(
                        proxySettings.Usage,
                        proxySettings.ProxyList,
                        proxySettings.ProxyBypassList
                );
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot set job proxy settings. error: {bitsResult}");
                return false;
            }

            internal bool Suspend()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                var bitsResult = _backgroundCopyJob.Suspend();
                if (bitsResult == BitsResult.SOk)
                {
                    return true;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot suspend job. error: {bitsResult}");
                return false;
            }
        }

        internal class BitsJobProxySettings
        {
            internal string ProxyBypassList { get; set; }
            internal string ProxyList { get; set; }
            internal BitsJobProxyUsage Usage { get; set; }
        }

        internal class BitsManager : IDisposable
        {
            internal event Action<string> OnJobError;
            internal event Action<string> OnJobModification;
            internal event Action<string> OnJobTransferred;

            private IBackgroundCopyManager _backgroundCopyManager;
            private BitsCallback _bitsCallback;
            private bool _disposed;

            internal BitsManager(IBackgroundCopyManager backgroundCopyManager)
            {
                _backgroundCopyManager = backgroundCopyManager;
                _bitsCallback = new BitsCallback(this);
            }

            ~BitsManager()
            {
                Dispose(false);
            }

            internal BitsJob CreateJob(
                    string displayName,
                    BitsJobType jobType)
            {
                if (_backgroundCopyManager == null)
                {
                    throw new ObjectDisposedException(nameof(BitsManager), $"Cannot access a closed {nameof(IBackgroundCopyManager)}.");
                }

                var realDisplayName = displayName;
                if (string.IsNullOrEmpty(realDisplayName))
                {
                    realDisplayName = $"Untitled-{Convert.ToTimestampInMilli(DateTime.UtcNow)}";
                }

                Guid jobId;
                IBackgroundCopyJob job;
                var bitsError = _backgroundCopyManager.CreateJob(
                        realDisplayName,
                        jobType,
                        out jobId,
                        out job
                );
                if (bitsError != BitsResult.SOk)
                {
                    throw new InvalidOperationException($"Cannot create new {nameof(IBackgroundCopyJob)}");
                }

                return new BitsJob(job);
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
                    _bitsCallback = null;
                    OnJobError = null;
                    OnJobModification = null;
                    OnJobTransferred = null;
                }

                if (_backgroundCopyManager == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_backgroundCopyManager))
                {
                    ComWrapperMarshal.ReleaseComObject(_backgroundCopyManager);
                }
                _backgroundCopyManager = null;

                _disposed = true;
            }

            internal static BitsManager GetInstance()
            {
                var iBackgroundCopyManager = Activator.CreateInstance(typeof(ComBackgroundCopyManager)) as IBackgroundCopyManager;
                if (iBackgroundCopyManager != null)
                {
                    return new BitsManager(iBackgroundCopyManager);
                }

                Logger.GetInstance(typeof(BitsManager)).Error($"Cannot create new {nameof(IBackgroundCopyManager)}");
                return null;
            }

            internal BitsJob GetJob(string id)
            {
                Guid guid;
                var success = Guid.TryParse(
                        id,
                        out guid
                );
                return success ? GetJob(guid) : null;
            }

            internal BitsJob GetJob(Guid id)
            {
                IBackgroundCopyJob iBackgroundCopyJob;
                var bitsError = _backgroundCopyManager.GetJob(
                        ref id,
                        out iBackgroundCopyJob
                );
                if (bitsError == BitsResult.SOk)
                {
                    return new BitsJob(iBackgroundCopyJob);
                }
                if (bitsError == BitsResult.EAccessDenied)
                {
                    Logger.GetInstance(typeof(BitsManager)).Warn("Can not get job. Please check your running permission");
                }
                if (bitsError == BitsResult.ENotFound)
                {
                    Logger.GetInstance(typeof(BitsManager)).Error("Can not find available job");
                }

                return null;
            }

            internal List<string> GetJobIdList()
            {
                return GetJobIdList(false);
            }

            internal List<string> GetJobIdList(bool forAllUsers)
            {
                if (_backgroundCopyManager == null)
                {
                    throw new ObjectDisposedException(nameof(BitsManager), $"Cannot access a closed {nameof(IBackgroundCopyManager)}.");
                }

                var result = new List<string>();
                var ownerScope = forAllUsers
                               ? BitsJobEnumOwnerScope.AllUsers
                               : BitsJobEnumOwnerScope.CurrentUser;
                IEnumBackgroundCopyJobs iEnumBackgroundCopyJobs = null;
                try
                {
                    var bitsError = _backgroundCopyManager.EnumJobs(
                            ownerScope,
                            out iEnumBackgroundCopyJobs
                    );
                    if (bitsError == BitsResult.SOk && iEnumBackgroundCopyJobs != null)
                    {
                        uint jobCount;
                        bitsError = iEnumBackgroundCopyJobs.GetCount(out jobCount);
                        if (bitsError != BitsResult.SOk)
                        {
                            Logger.GetInstance(typeof(BitsManager)).Error($"Cannot get job list count. error: {bitsError}");
                            return result;
                        }

                        if (jobCount <= 0)
                        {
                            return result;
                        }

                        for (var i = 0; i < jobCount; i++)
                        {
                            IBackgroundCopyJob iBackgroundCopyJob = null;
                            try
                            {
                                uint fetchedJobCount;
                                bitsError = iEnumBackgroundCopyJobs.Next(
                                        1,
                                        out iBackgroundCopyJob,
                                        out fetchedJobCount
                                );
                                if (bitsError != BitsResult.SOk
                                        && bitsError != BitsResult.SFalse)
                                {
                                    continue;
                                }

                                Guid jobId;
                                bitsError = iBackgroundCopyJob.GetId(out jobId);
                                if (bitsError != BitsResult.SOk)
                                {
                                    Logger.GetInstance(typeof(BitsManager)).Error($"Cannot get job id. error: {bitsError}");
                                }
                                else
                                {
                                    result.Add(jobId.ToString());
                                }
                            }
                            catch (Exception e)
                            {
                                Logger.GetInstance(typeof(BitsManager)).Error($"Cannot get job. error: {e.Message}");
                            }
                            finally
                            {
                                if (iBackgroundCopyJob != null)
                                {
                                    if (ComWrapperMarshal.IsComObject(iBackgroundCopyJob))
                                    {
                                        ComWrapperMarshal.ReleaseComObject(iBackgroundCopyJob);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (bitsError == BitsResult.EAccessDenied
                                && ownerScope == BitsJobEnumOwnerScope.AllUsers)
                        {
                            Logger.GetInstance(typeof(BitsManager)).Warn("Cannot get job list for all users. Please check your running permission");
                        }

                        if (bitsError != BitsResult.SOk)
                        {
                            Logger.GetInstance(typeof(BitsManager)).Error($"Cannot get job list for all users. error: {bitsError}");
                        }
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(BitsManager)).Error($"Cannot get job id list. error: {e.Message}");
                }
                finally
                {
                    if (iEnumBackgroundCopyJobs != null)
                    {
                        if (ComWrapperMarshal.IsComObject(iEnumBackgroundCopyJobs))
                        {
                            ComWrapperMarshal.ReleaseComObject(iEnumBackgroundCopyJobs);
                        }
                    }
                }

                return result;
            }

            internal bool ListenJob(BitsJob job)
            {
                return job?.SetNotifyInterface(_bitsCallback) ?? false;
            }

            internal void NotifyJobError(string jobId)
            {
                if (string.IsNullOrWhiteSpace(jobId))
                {
                    return;
                }

                Task.Run(() =>
                {
                        try
                        {
                            OnJobError?.Invoke(jobId);
                        }
                        catch (Exception e)
                        {
                            Logger.GetInstance(typeof(BitsManager)).Error(e.ToString());
                        }
                });
            }

            internal void NotifyJobModification(string jobId)
            {
                if (string.IsNullOrWhiteSpace(jobId))
                {
                    return;
                }

                Task.Run(() =>
                {
                        try
                        {
                            OnJobModification?.Invoke(jobId);
                        }
                        catch (Exception e)
                        {
                            Logger.GetInstance(typeof(BitsManager)).Error(e.ToString());
                        }
                });
            }

            internal void NotifyJobTransferred(string jobId)
            {
                if (string.IsNullOrWhiteSpace(jobId))
                {
                    return;
                }

                Task.Run(() =>
                {
                        try
                        {
                            OnJobTransferred?.Invoke(jobId);
                        }
                        catch (Exception e)
                        {
                            Logger.GetInstance(typeof(BitsManager)).Error(e.ToString());
                        }
                });
            }
        }

        internal class DxgiAdapter : IDisposable
        {
            private IDxgiAdapter _dxgiAdapter;
            private bool _disposed;

            internal DxgiAdapter(IDxgiAdapter dxgiAdapter)
            {
                _dxgiAdapter = dxgiAdapter;
            }

            ~DxgiAdapter()
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
                    // Disposing managed resource
                }

                if (_dxgiAdapter == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_dxgiAdapter))
                {
                    ComWrapperMarshal.ReleaseComObject(_dxgiAdapter);
                }
                _dxgiAdapter = null;

                _disposed = true;
            }

            internal List<DxgiOutput> EnumerateOutputs()
            {
                var result = new List<DxgiOutput>();
                if (_dxgiAdapter == null)
                {
                    return result;
                }

                try
                {
                    var index = 0U;
                    while (true)
                    {
                        IDxgiOutput iDxgiOutput;
                        var dxgiError = _dxgiAdapter.EnumOutputs(
                                index,
                                out iDxgiOutput
                        );
                        if (dxgiError != DxgiError.SOk)
                        {
                            break;
                        }

                        result.Add(new DxgiOutput(iDxgiOutput));
                        index++;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiAdapter)).Error($"Can not enumerate DXGI outputs. error: {e.Message}");
                }

                return result;
            }

            internal DxgiAdapterDescription GetDescription()
            {
                if (_dxgiAdapter == null)
                {
                    return new DxgiAdapterDescription();
                }

                try
                {
                    DxgiAdapterDescription result;
                    var dxgiError = _dxgiAdapter.GetDesc(out result);
                    if (dxgiError == DxgiError.SOk)
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiAdapter)).Error($"Can not get DXGI adapter description. error: {e.Message}");
                }

                return new DxgiAdapterDescription();
            }
        }

        internal class DxgiFactory : IDisposable
        {
            private IDxgiFactory _dxgiFactory;
            private bool _disposed;

            internal DxgiFactory(IDxgiFactory dxgiFactory)
            {
                _dxgiFactory = dxgiFactory;
            }

            ~DxgiFactory()
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
                    // Disposing managed resource
                }

                if (_dxgiFactory == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_dxgiFactory))
                {
                    ComWrapperMarshal.ReleaseComObject(_dxgiFactory);
                }
                _dxgiFactory = null;

                _disposed = true;
            }

            internal List<DxgiAdapter> EnumerateAdapters()
            {
                var result = new List<DxgiAdapter>();
                if (_dxgiFactory == null)
                {
                    return result;
                }

                try
                {
                    var index = 0U;
                    while (true)
                    {
                        IDxgiAdapter iDxgiAdapter;
                        var dxgiError = _dxgiFactory.EnumAdapters(
                                index,
                                out iDxgiAdapter
                        );
                        if (dxgiError != DxgiError.SOk)
                        {
                            break;
                        }

                        result.Add(new DxgiAdapter(iDxgiAdapter));
                        index++;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiFactory)).Error($"Can not enumerate DXGI adapters. error: {e.Message}");
                }

                return result;
            }

            internal static DxgiFactory GetInstance()
            {
                IDxgiFactory iDxgiFactory;

                var guid = typeof(IDxgiFactory).GUID;
                var dxgiError = CreateDXGIFactory(
                        ref guid,
                        out iDxgiFactory
                );

                if (dxgiError != DxgiError.SOk)
                {
                    Logger.GetInstance(typeof(DxgiFactory)).Error($"Can not create DXGI factory. error: {dxgiError}");
                    return null;
                }

                return new DxgiFactory(iDxgiFactory);
            }
        }

        internal class DxgiOutput : IDisposable
        {
            private IDxgiOutput _dxgiOutput;
            private bool _disposed;

            internal DxgiOutput(IDxgiOutput dxgiOutput)
            {
                _dxgiOutput = dxgiOutput;
            }

            ~DxgiOutput()
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
                    // Disposing managed resource
                }

                if (_dxgiOutput == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_dxgiOutput))
                {
                    ComWrapperMarshal.ReleaseComObject(_dxgiOutput);
                }
                _dxgiOutput = null;

                _disposed = true;
            }

            internal DxgiOutputDescription GetDescription()
            {
                if (_dxgiOutput == null)
                {
                    return new DxgiOutputDescription();
                }

                try
                {
                    DxgiOutputDescription result;
                    var dxgiError = _dxgiOutput.GetDesc(out result);
                    if (dxgiError == DxgiError.SOk)
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiOutput)).Error($"Can not get DXGI output description. error: {e.Message}");
                }

                return new DxgiOutputDescription();
            }
        }

        internal class Location : IDisposable
        {
            private static readonly Guid ReportTypeICivicAddressReport = new Guid(ComInterfaceId.ICivicAddressReport);
            private static readonly Guid ReportTypeILatLongReport = new Guid(ComInterfaceId.ILatLongReport);

            private ILocation _location;
            private bool _disposed;

            internal Location(ILocation location)
            {
                _location = location;
            }

            ~Location()
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
                    // Disposing managed resource
                }

                if (_location == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_location))
                {
                    ComWrapperMarshal.ReleaseComObject(_location);
                }
                _location = null;

                _disposed = true;
            }

            internal static Location GetInstance()
            {
                var iLocation = Activator.CreateInstance(typeof(ComLocation)) as ILocation;
                if (iLocation != null)
                {
                    return new Location(iLocation);
                }

                Logger.GetInstance(typeof(Location)).Error($"Cannot create new {nameof(ILocation)}");
                return null;
            }

            internal LocationReport GetReport(LocationReportType reportType)
            {
                if (reportType == LocationReportType.Unknown)
                {
                    return null;
                }

                var reportTypeGuid = ToReportTypeGuid(reportType);
                ILocationReport report;
                var hResult = _location.GetReport(
                        ref reportTypeGuid,
                        out report
                );
                if (hResult == HResult.SOk)
                {
                    return new LocationReport(report);
                }

                Logger.GetInstance(typeof(Location)).Error($"Can not get report for report type: {reportType}. HResult: {hResult}");
                return null;
            }

            internal LocationReportStatus GetReportStatus(LocationReportType reportType)
            {
                if (reportType == LocationReportType.Unknown)
                {
                    return LocationReportStatus.NotSupported;
                }

                var reportTypeGuid = ToReportTypeGuid(reportType);
                LocationReportStatus status;
                var hResult = _location.GetReportStatus(
                        ref reportTypeGuid,
                        out status
                );
                if (hResult == HResult.SOk)
                {
                    return status;
                }
                if (hResult == HResult.EWin32NotSupported)
                {
                    return LocationReportStatus.NotSupported;
                }

                Logger.GetInstance(typeof(Location)).Error($"Can not get report status for report type: {reportType}. HResult: {hResult}");
                return LocationReportStatus.Error;
            }

            internal bool RequestPermission(LocationReportType reportType)
            {
                if (reportType == LocationReportType.Unknown)
                {
                    return false;
                }

                var reportTypeGuid = ToReportTypeGuid(reportType);
                var hResult = _location.RequestPermissions(
                        IntPtr.Zero,
                        ref reportTypeGuid,
                        1,
                        true
                );
                if (hResult == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(Location)).Error($"Can not request permission for report type: {reportType}. HResult: {hResult}");
                return false;
            }

            private static Guid ToReportTypeGuid(LocationReportType reportType)
            {
                if (reportType == LocationReportType.ICivicAddressReport)
                {
                    return ReportTypeICivicAddressReport;
                }
                if (reportType == LocationReportType.ILatLongReport)
                {
                    return ReportTypeILatLongReport;
                }

                return ReportTypeICivicAddressReport;
            }
        }

        internal class LocationReport : IDisposable
        {
            private readonly PropertyKey _sensorDataTypeCountryRegion = new PropertyKey(SensorDataTypeLocationGuid, 28);

            private bool _disposed;
            private ILocationReport _locationReport;

            internal LocationReport(ILocationReport locationReport)
            {
                _locationReport = locationReport;
            }

            ~LocationReport()
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
                    // Disposing managed resource
                }

                if (_locationReport == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_locationReport))
                {
                    ComWrapperMarshal.ReleaseComObject(_locationReport);
                }
                _locationReport = null;

                _disposed = true;
            }

            internal string GetCountryRegion()
            {
                return GetStringProperty(
                        _locationReport,
                        _sensorDataTypeCountryRegion
                );
            }

            private static string GetStringProperty(
                    ILocationReport locationReport,
                    PropertyKey propertyKey)
            {
                if (locationReport == null)
                {
                    return null;
                }

                using (var propVariant = new PropVariant())
                {
                    var hResult = locationReport.GetValue(
                            ref propertyKey,
                            propVariant
                    );
                    if (hResult == HResult.SOk)
                    {
                        return propVariant.GetValue() as string;
                    }

                    Logger.GetInstance(typeof(LocationReport)).Error($"Can not get string property for key: {propertyKey}. HResult: {hResult}");
                    return null;
                }
            }
        }

        internal class PortableDeviceManager : IDisposable
        {
            private IPortableDeviceManager _portableDeviceManager;
            private bool _disposed;

            internal PortableDeviceManager(IPortableDeviceManager portableDeviceManager)
            {
                _portableDeviceManager = portableDeviceManager;
            }

            ~PortableDeviceManager()
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
                    // Disposing managed resource
                }

                if (_portableDeviceManager == null)
                {
                    return;
                }
                if (ComWrapperMarshal.IsComObject(_portableDeviceManager))
                {
                    ComWrapperMarshal.ReleaseComObject(_portableDeviceManager);
                }
                _portableDeviceManager = null;

                _disposed = true;
            }

            internal string GetDeviceDescription(string deviceId)
            {
                var result = string.Empty;
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDeviceDescription(
                        deviceId,
                        null,
                        ref count
                );
                if (hResult != HResult.SOk
                        && hResult != HResult.EWin32InsufficientBuffer)
                {
                    if (hResult == HResult.EWin32InvalidData)
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Debug($"The device {deviceId} does not support description");
                    }
                    else
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device description (1). HResult: {hResult}");
                    }
                    return result;
                }

                var builder = new StringBuilder((int)count);
                hResult = _portableDeviceManager.GetDeviceDescription(
                        deviceId,
                        builder,
                        ref count
                );
                if (hResult == HResult.SOk)
                {
                    return builder.ToString();
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device description (2). HResult: {hResult}");
                return result;
            }

            internal string GetDeviceFriendlyName(string deviceId)
            {
                var result = string.Empty;
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDeviceFriendlyName(
                        deviceId,
                        null,
                        ref count
                );
                if (hResult != HResult.SOk
                        && hResult != HResult.EWin32InsufficientBuffer)
                {
                    if (hResult == HResult.EWin32InvalidData)
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Debug($"The device {deviceId} does not support friendly name");
                    }
                    else
                    {
                        Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device friendly name (1). HResult: {hResult}");
                    }
                    return result;
                }

                var builder = new StringBuilder((int)count);
                hResult = _portableDeviceManager.GetDeviceFriendlyName(
                        deviceId,
                        builder,
                        ref count
                );
                if (hResult == HResult.SOk)
                {
                    return builder.ToString();
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device friendly name (2). HResult: {hResult}");
                return result;
            }

            internal List<string> GetDeviceList()
            {
                var result = new List<string>();
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDevices(
                        null,
                        ref count
                );
                if (hResult != HResult.SOk)
                {
                    Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device list. HResult: {hResult}");
                    return result;
                }

                if (count <= 0)
                {
                    return result;
                }

                IntPtr[] intPtrArray = null;
                try
                {
                    intPtrArray = new IntPtr[count];
                    _portableDeviceManager.GetDevices(
                            intPtrArray,
                            ref count
                    );

                    foreach (var ptr in intPtrArray)
                    {
                        var id = Marshal.PtrToStringUni(ptr);
                        if (!string.IsNullOrEmpty(id))
                        {
                            result.Add(id);
                        }
                    }
                }
                finally
                {
                    if (intPtrArray != null)
                    {
                        for (uint i = 0; i < count; i++)
                        {
                            Marshal.FreeCoTaskMem(intPtrArray[i]);
                        }
                    }
                }

                return result;
            }

            internal string GetDeviceManufacturer(string deviceId)
            {
                var result = string.Empty;
                if (_portableDeviceManager == null)
                {
                    return result;
                }

                uint count = 0;
                var hResult = _portableDeviceManager.GetDeviceManufacturer(
                        deviceId,
                        null,
                        ref count
                );
                if (hResult != HResult.SOk
                        && hResult != HResult.EWin32InsufficientBuffer)
                {
                    Logger.GetInstance(typeof(PortableDeviceManager)).Error(
                        $"Can not get device manufacturer (1). HResult: {hResult}");
                    return result;
                }

                var builder = new StringBuilder((int)count);
                hResult = _portableDeviceManager.GetDeviceManufacturer(
                        deviceId,
                        builder,
                        ref count
                );
                if (hResult == HResult.SOk)
                {
                    return builder.ToString();
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error($"Can not get device manufacturer (2). HResult: {hResult}");
                return result;
            }

            internal static PortableDeviceManager GetInstance()
            {
                var iPortableDeviceManager = Activator.CreateInstance(typeof(ComPortableDeviceManager)) as IPortableDeviceManager;
                if (iPortableDeviceManager != null)
                {
                    return new PortableDeviceManager(iPortableDeviceManager);
                }

                Logger.GetInstance(typeof(PortableDeviceManager)).Error("Can not create Portable Device Manager");
                return null;
            }
        }

        internal class ShellLinkW : IDisposable
        {
            private static PropertyKey _systemAppUserModelActivatorId = new PropertyKey(SystemAppUserModelGuid, 26);
            private static PropertyKey _systemAppUserModelId = new PropertyKey(SystemAppUserModelGuid, 5);

            private bool _disposed;
            private IShellLinkW _shellLink;

            internal ShellLinkW(IShellLinkW iShellLinkW)
            {
                _shellLink = iShellLinkW;
            }

            ~ShellLinkW()
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
                if (ComWrapperMarshal.IsComObject(_shellLink))
                {
                    ComWrapperMarshal.ReleaseComObject(_shellLink);
                }
                _shellLink = null;

                _disposed = true;
            }

            internal static ShellLinkW GetInstance()
            {
                var iShellLinkW = Activator.CreateInstance(typeof(ComShellLink)) as IShellLinkW;
                if (iShellLinkW != null)
                {
                    return new ShellLinkW(iShellLinkW);
                }

                Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot create new {nameof(IShellLinkW)}");
                return null;
            }

            internal bool Save(FileInfo targetPath)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var persistFile = _shellLink as IPersistFile;
                if (persistFile == null)
                {
                    Logger.GetInstance(typeof(ShellLinkW)).Error("Cannot get persist file.");
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
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetDescription(description);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set description. error: {error}");
                return false;
            }

            internal bool SetSourceActivatorId(Guid activatorId)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var propertyStore = _shellLink as IPropertyStore;
                if (propertyStore == null)
                {
                    Logger.GetInstance(typeof(ShellLinkW)).Error("Cannot get property store.");
                    return false;
                }

                var propVariant = new PropVariant(activatorId);
                try
                {
                    var error = propertyStore.SetValue(
                            ref _systemAppUserModelActivatorId,
                            ref propVariant
                    );
                    if (error == HResult.SOk)
                    {
                        error = propertyStore.Commit();
                    }
                    if (error == HResult.SOk)
                    {
                        return true;
                    }

                    Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set source activator id. error: {error}");
                    return false;
                }
                finally
                {
                    propVariant?.Dispose();
                }
            }

            internal bool SetSourceAppId(string sourceAppId)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var propertyStore = _shellLink as IPropertyStore;
                if (propertyStore == null)
                {
                    Logger.GetInstance(typeof(ShellLinkW)).Error("Cannot get property store.");
                    return false;
                }

                var propVariant = new PropVariant(sourceAppId);
                try
                {
                    var error = propertyStore.SetValue(
                            ref _systemAppUserModelId,
                            ref propVariant
                    );
                    if (error == HResult.SOk)
                    {
                        error = propertyStore.Commit();
                    }

                    if (error == HResult.SOk)
                    {
                        return true;
                    }

                    Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set source app id. error: {error}");
                    return false;
                }
                finally
                {
                    propVariant?.Dispose();
                }
            }

            internal bool SetSourceArguments(string sourceArguments)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetArguments(sourceArguments);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set source arguments. error: {error}");
                return false;
            }

            internal bool SetSourcePath(FileInfo sourcePath)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetPath(sourcePath.FullName);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set source path. error: {error}");
                return false;
            }

            internal bool SetSourceShowWindowCommand(ShowWindowCommand showWindowCommand)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetShowCmd(showWindowCommand);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set source show window command. error: {error}");
                return false;
            }

            internal bool SetSourceWorkingPath(DirectoryInfo workingDirectory)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetWorkingDirectory(workingDirectory.FullName);
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set source working path. error: {error}");
                return false;
            }

            internal bool SetTargetIcon(
                    FileInfo targetIconPath,
                    int targetIconIndex)
            {
                if (_shellLink == null)
                {
                    throw new ObjectDisposedException(nameof(ShellLinkW), $"Cannot access a closed {nameof(IShellLinkW)}.");
                }

                var error = _shellLink.SetIconLocation(
                        targetIconPath.FullName,
                        targetIconIndex
                );
                if (error == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(ShellLinkW)).Error($"Cannot set target icon. error: {error}");
                return false;
            }
        }
    }
}
