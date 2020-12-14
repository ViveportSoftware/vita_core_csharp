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
                    Marshal.ReleaseComObject(_backgroundCopyManager);
                }
                _backgroundCopyManager = null;

                _disposed = true;
            }

            internal static BitsManager GetInstance()
            {
                var iBackgroundCopyManager = Activator.CreateInstance(typeof(ClsidBackgroundCopyManager)) as IBackgroundCopyManager;
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
                                        Marshal.ReleaseComObject(iBackgroundCopyJob);
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
                            Marshal.ReleaseComObject(iEnumBackgroundCopyJobs);
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
    }
}
