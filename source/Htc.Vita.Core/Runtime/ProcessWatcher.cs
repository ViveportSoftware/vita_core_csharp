using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class ProcessWatcher.
    /// </summary>
    public abstract partial class ProcessWatcher
    {
        /// <summary>
        /// EventHandler delegate
        /// </summary>
        /// <param name="processInfo">The process information.</param>
        public delegate void EventHandler(ProcessInfo processInfo);

        /// <summary>
        /// Occurs when process created.
        /// </summary>
        public event EventHandler ProcessCreated;
        /// <summary>
        /// Occurs when process deleted.
        /// </summary>
        public event EventHandler ProcessDeleted;
        /// <summary>
        /// Occurs when process modified.
        /// </summary>
        public event EventHandler ProcessModified;

        /// <summary>
        /// Gets the event handlers.
        /// </summary>
        /// <value>The event handlers.</value>
        protected Dictionary<EventType, EventHandler> EventHandlers { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessWatcher"/> class.
        /// </summary>
        protected ProcessWatcher()
        {
            EventHandlers = new Dictionary<EventType, EventHandler>();
        }

        /// <summary>
        /// Determines whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Starts this instance.
        /// </summary>
        /// <returns><c>true</c> if starting this instance successfully, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Stops this instance.
        /// </summary>
        /// <returns><c>true</c> if stopping this instance successfully, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Enum EventType
        /// </summary>
        protected enum EventType
        {
            /// <summary>
            /// Process-created event
            /// </summary>
            Created,
            /// <summary>
            /// Process-deleted event
            /// </summary>
            Deleted,
            /// <summary>
            /// Process-modified event
            /// </summary>
            Modified
        }

        /// <summary>
        /// Called when Determining whether this instance is running.
        /// </summary>
        /// <returns><c>true</c> if this instance is running, <c>false</c> otherwise.</returns>
        protected abstract bool OnIsRunning();
        /// <summary>
        /// Called when starting.
        /// </summary>
        /// <returns><c>true</c> if starting successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnStart();
        /// <summary>
        /// Called when stopping.
        /// </summary>
        /// <returns><c>true</c> if stopping successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnStop();
    }
}
