using System.ServiceProcess;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.TestService
{
    internal class DefaultService : ServiceBase
    {
        private static readonly object ServiceLock = new object();

        private static RunningStatus _runningStatus = RunningStatus.Unknown;

        public DefaultService()
        {
            ServiceName = DefaultInstaller.ServiceName;
        }

        internal static bool DoStart(string[] args)
        {
            var result = false;
            lock (ServiceLock)
            {
                if (_runningStatus == RunningStatus.Started)
                {
                    Logger.GetInstance(typeof(DefaultService)).Info("Service has already started. Skip");
                    return true;
                }

                Logger.GetInstance(typeof(DefaultService)).Info("Service starting");

                // TODO
            }

            Logger.GetInstance(typeof(DefaultService)).Info("Service started");
            return result;
        }

        internal static bool DoStop()
        {
            var result = false;
            lock (ServiceLock)
            {
                if (_runningStatus == RunningStatus.Stopped)
                {
                    Logger.GetInstance(typeof(DefaultService)).Info("Service has already stopped. Skip");
                    return true;
                }

                Logger.GetInstance(typeof(DefaultService)).Info("Service stopping");

                // TODO
            }

            Logger.GetInstance(typeof(DefaultService)).Info("Service stopped");
            return result;
        }

        protected override void OnStart(string[] args)
        {
            DoStart(args);
        }

        protected override void OnStop()
        {
            DoStop();
        }
    }

    internal enum RunningStatus
    {
        Unknown,
        Stopped,
        Started
    }
}
