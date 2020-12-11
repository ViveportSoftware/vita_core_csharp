using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class BitsManager : IDisposable
        {
            private readonly IBackgroundCopyManager _backgroundCopyManager;

            internal BitsManager(IBackgroundCopyManager backgroundCopyManager)
            {
                _backgroundCopyManager = backgroundCopyManager;
            }

            public void Dispose()
            {
                if (_backgroundCopyManager == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_backgroundCopyManager))
                {
                    Marshal.ReleaseComObject(_backgroundCopyManager);
                }
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
        }
    }
}
