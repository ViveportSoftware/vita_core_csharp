using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;
using Convert = Htc.Vita.Core.Util.Convert;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
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
                if (Marshal.IsComObject(_backgroundCopyError))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_backgroundCopyError);
#pragma warning restore CA1416
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
                if (Marshal.IsComObject(_backgroundCopyFile))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_backgroundCopyFile);
#pragma warning restore CA1416
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
                if (Marshal.IsComObject(_enumBackgroundCopyFiles))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_enumBackgroundCopyFiles);
#pragma warning restore CA1416
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
                if (Marshal.IsComObject(_backgroundCopyJob))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_backgroundCopyJob);
#pragma warning restore CA1416
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
                if (Marshal.IsComObject(_backgroundCopyManager))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_backgroundCopyManager);
#pragma warning restore CA1416
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
                                    if (Marshal.IsComObject(iBackgroundCopyJob))
                                    {
#pragma warning disable CA1416
                                        Marshal.ReleaseComObject(iBackgroundCopyJob);
#pragma warning restore CA1416
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
                        if (Marshal.IsComObject(iEnumBackgroundCopyJobs))
                        {
#pragma warning disable CA1416
                            Marshal.ReleaseComObject(iEnumBackgroundCopyJobs);
#pragma warning restore CA1416
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
                if (Marshal.IsComObject(_dxgiAdapter))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_dxgiAdapter);
#pragma warning restore CA1416
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
                if (Marshal.IsComObject(_dxgiFactory))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_dxgiFactory);
#pragma warning restore CA1416
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
                if (Marshal.IsComObject(_dxgiOutput))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_dxgiOutput);
#pragma warning restore CA1416
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
    }
}
