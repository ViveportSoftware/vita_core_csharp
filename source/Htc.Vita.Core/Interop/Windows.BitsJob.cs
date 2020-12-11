using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsJob : IDisposable
        {
            private readonly IBackgroundCopyJob _backgroundCopyJob;

            internal BitsJob(IBackgroundCopyJob backgroundCopyJob)
            {
                _backgroundCopyJob = backgroundCopyJob;
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
                if (_backgroundCopyJob == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_backgroundCopyJob))
                {
                    Marshal.ReleaseComObject(_backgroundCopyJob);
                }
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

            internal BitsNotifyFlag GetNotifyFlags()
            {
                if (_backgroundCopyJob == null)
                {
                    throw new ObjectDisposedException(nameof(BitsJob), $"Cannot access a closed {nameof(IBackgroundCopyJob)}.");
                }

                BitsNotifyFlag notifyFlag;
                var bitsResult = _backgroundCopyJob.GetNotifyFlags(out notifyFlag);
                if (bitsResult == BitsResult.SOk)
                {
                    return notifyFlag;
                }
                Logger.GetInstance(typeof(BitsJob)).Error($"Cannot get job notify flags. error: {bitsResult}");
                return BitsNotifyFlag.None;
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

            internal bool SetNotifyFlags(BitsNotifyFlag notifyFlags)
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
    }
}
