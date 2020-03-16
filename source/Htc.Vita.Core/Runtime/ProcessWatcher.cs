using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public abstract partial class ProcessWatcher
    {
        public delegate void EventHandler(ProcessInfo processInfo);

        public event EventHandler ProcessCreated;
        public event EventHandler ProcessDeleted;
        public event EventHandler ProcessModified;

        protected Dictionary<EventType, EventHandler> EventHandlers { get; }

        protected ProcessWatcher()
        {
            EventHandlers = new Dictionary<EventType, EventHandler>();
        }

        public bool IsRunning()
        {
            var result = false;
            try
            {
                result = OnIsRunning();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ProcessWatcher)).Error(e.ToString());
            }
            return result;
        }

        public bool Start()
        {
            EventHandlers.Add(EventType.Created, ProcessCreated);
            EventHandlers.Add(EventType.Deleted, ProcessDeleted);
            EventHandlers.Add(EventType.Modified, ProcessModified);

            var result = false;
            try
            {
                result = OnStart();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ProcessWatcher)).Error(e.ToString());
            }
            return result;
        }

        public bool Stop()
        {
            var result = false;
            try
            {
                result = OnStop();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(ProcessWatcher)).Error(e.ToString());
            }

            EventHandlers.Clear();
            return result;
        }

        protected enum EventType
        {
            Created,
            Deleted,
            Modified
        }

        protected abstract bool OnIsRunning();
        protected abstract bool OnStart();
        protected abstract bool OnStop();
    }
}
