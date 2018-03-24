using System;
using System.Collections.Generic;
using System.Management;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public class WmiProcessWatcher : ProcessWatcher
    {
        private readonly List<ManagementEventWatcher> _eventWatchers;
        private string _targetProcessName;

        protected Dictionary<EventType, string> WmiInstanceEvents { get; }

        public WmiProcessWatcher()
        {
            _eventWatchers = new List<ManagementEventWatcher>();
            WmiInstanceEvents = new Dictionary<EventType, string>
            {
                    { EventType.Created, "__InstanceCreationEvent" },
                    { EventType.Deleted, "__InstanceDeletionEvent" },
                    { EventType.Modified, "__InstanceModificationEvent" }
            };
        }

        protected string GetTargetProcessName()
        {
            return _targetProcessName;
        }

        protected virtual string OnGetWmiQuery(EventType eventType)
        {
            var queryTarget = string.Empty;
            var targetProcessName = GetTargetProcessName();
            if (!string.IsNullOrWhiteSpace(targetProcessName))
            {
                queryTarget = $" and TargetInstance.Name = '{targetProcessName}'";
            }
            return $@"SELECT * FROM {WmiInstanceEvents[eventType]} WITHIN 5 WHERE TargetInstance ISA 'Win32_Process'{queryTarget}";
        }

        protected override bool OnIsRunning()
        {
            return _eventWatchers.Count > 0;
        }

        private void OnManagementEventArrived(object sender, EventArrivedEventArgs e)
        {
            using (var managementBaseObject = e.NewEvent)
            {
                if (managementBaseObject == null)
                {
                    return;
                }

                EventHandler eventHandler = null;
                var eventType = managementBaseObject.ClassPath.ClassName;
                foreach (var wmiInstanceEvent in WmiInstanceEvents)
                {
                    if (wmiInstanceEvent.Value.Equals(eventType))
                    {
                        eventHandler = EventHandlers[wmiInstanceEvent.Key];
                    }
                }
                if (eventHandler == null)
                {
                    return;
                }

                ProcessInfo processInfo;
                using (var targetInstance = managementBaseObject["TargetInstance"] as ManagementBaseObject)
                {
                    if (targetInstance == null)
                    {
                        return;
                    }

                    var id = (int) (targetInstance["ProcessId"] as uint? ?? 0);
                    var name = targetInstance["Name"] as string;
                    var path = targetInstance["ExecutablePath"] as string;
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        path = ProcessManager.GetProcessPathById(id);
                    }

                    processInfo = new ProcessInfo
                    {
                            Id = id,
                            Name = name,
                            Path = path
                    };
                }

                eventHandler(processInfo);
            }
        }

        protected override bool OnStart()
        {
            if (_eventWatchers.Count > 0)
            {
                OnStop();
            }

            var eventTypeValues = Enum.GetValues(typeof(EventType));
            foreach (var eventTypeValue in eventTypeValues)
            {
                var eventHandler = EventHandlers[(EventType) eventTypeValue];
                if (eventHandler == null)
                {
                    continue;
                }

                var wmiQuery = OnGetWmiQuery((EventType) eventTypeValue);
                Logger.GetInstance(typeof(WmiProcessWatcher)).Info("WMI query: " + wmiQuery);
                var eventWatcher = new ManagementEventWatcher(wmiQuery);
                eventWatcher.EventArrived += OnManagementEventArrived;
                try
                {
                    eventWatcher.Start();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(WmiProcessWatcher)).Error("Can not start management event watcher: " + e.Message);
                    return false;
                }
                _eventWatchers.Add(eventWatcher);
            }

            return true;
        }

        protected override bool OnStop()
        {
            foreach (var eventWatcher in _eventWatchers)
            {
                try
                {
                    eventWatcher.Stop();
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(WmiProcessWatcher)).Error("Can not stop management event watcher: " + e.Message);
                }
                eventWatcher.Dispose();
            }

            _eventWatchers.Clear();
            return true;
        }

        public WmiProcessWatcher SetTargetProcessName(string targetProcessName)
        {
            _targetProcessName = targetProcessName;
            return this;
        }
    }
}
