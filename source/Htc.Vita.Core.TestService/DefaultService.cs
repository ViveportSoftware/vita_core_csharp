using System.Diagnostics;
using System.ServiceProcess;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

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
            lock (ServiceLock)
            {
                if (_runningStatus == RunningStatus.Started)
                {
                    Logger.GetInstance(typeof(DefaultService)).Info("Service has already started. Skip");
                    return true;
                }

                Logger.GetInstance(typeof(DefaultService)).Info("Service starting");

                // TODO
                var processAsUser = ProcessManager.LaunchProcessAsUser("C:\\Windows\\system32\\notepad.exe", "");
                if (processAsUser == null)
                {
                    Logger.GetInstance(typeof(DefaultService)).Info("Can not launch process as user");
                }
                else
                {
                    Logger.GetInstance(typeof(DefaultService)).Info("processAsUser.Id: " + processAsUser.Id);
                    Logger.GetInstance(typeof(DefaultService)).Info("processAsUser.Name: " + processAsUser.Name);
                    Logger.GetInstance(typeof(DefaultService)).Info("processAsUser.Path: " + processAsUser.Path);
                    Logger.GetInstance(typeof(DefaultService)).Info("processAsUser.User: " + processAsUser.User);
                }

                var success = UserManager.SendMessageToFirstActiveUser("unicode", "測試", 0);
                if (!success)
                {
                    Logger.GetInstance(typeof(DefaultService)).Info("Can not send message to active user");
                }

                _runningStatus = RunningStatus.Started;

                var isElevatedProcess = ProcessManager.IsElevatedProcess(Process.GetCurrentProcess());
                Logger.GetInstance(typeof(DefaultService)).Info("Service is running as an elevated process: " + isElevatedProcess);
            }

            Logger.GetInstance(typeof(DefaultService)).Info("Service started");
            return true;
        }

        internal static bool DoStop()
        {
            lock (ServiceLock)
            {
                if (_runningStatus == RunningStatus.Stopped)
                {
                    Logger.GetInstance(typeof(DefaultService)).Info("Service has already stopped. Skip");
                    return true;
                }

                Logger.GetInstance(typeof(DefaultService)).Info("Service stopping");

                // TODO

                _runningStatus = RunningStatus.Stopped;
            }

            Logger.GetInstance(typeof(DefaultService)).Info("Service stopped");
            return true;
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
