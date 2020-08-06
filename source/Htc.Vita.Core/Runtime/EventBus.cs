using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class EventBus.
    /// </summary>
    public abstract partial class EventBus
    {
        private static Dictionary<string, EventBus> Instances { get; } = new Dictionary<string, EventBus>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(DefaultEventBus);

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : EventBus
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(EventBus)).Info($"Registered default {nameof(EventBus)} type to {_defaultType}");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>EventBus.</returns>
        public static EventBus GetInstance()
        {
            EventBus instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(EventBus)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(EventBus)).Info($"Initializing {typeof(DefaultEventBus).FullName}...");
                instance = new DefaultEventBus();
            }
            return instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>EventBus.</returns>
        public static EventBus GetInstance<T>() where T : EventBus
        {
            EventBus instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(EventBus)).Fatal($"Instance initialization error: {e}");
                Logger.GetInstance(typeof(EventBus)).Info($"Initializing {typeof(DefaultEventBus).FullName}...");
                instance = new DefaultEventBus();
            }
            return instance;
        }

        private static EventBus DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException($"Invalid arguments to get {nameof(EventBus)} instance");
            }

            var key = $"{type.FullName}_";
            EventBus instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(EventBus)).Info($"Initializing {key}...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (EventBus)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(EventBus)).Info($"Initializing {typeof(DefaultEventBus).FullName}...");
                instance = new DefaultEventBus();
            }
            lock (InstancesLock)
            {
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
        }

        /// <summary>
        /// Registers the listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventListener">The event listener.</param>
        /// <returns><c>true</c> if registering the listener successfully, <c>false</c> otherwise.</returns>
        public bool RegisterListener<T>(IEventListener eventListener) where T : IEventData
        {
            if (eventListener == null)
            {
                return false;
            }

            var result = false;
            try
            {
                result = OnRegisterListener<T>(eventListener);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(EventBus)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Triggers the specified event data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData">The event data.</param>
        /// <returns>EventBus.</returns>
        public EventBus Trigger<T>(IEventData eventData) where T : IEventData
        {
            if (eventData == null)
            {
                return this;
            }

            if (!(eventData is T))
            {
                return this;
            }

            EventBus result = null;
            try
            {
                result = OnTrigger<T>(eventData);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(EventBus)).Error(e.ToString());
            }
            return result ?? this;
        }

        /// <summary>
        /// Unregisters the listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventListener">The event listener.</param>
        /// <returns><c>true</c> if unregistering the listener successfully, <c>false</c> otherwise.</returns>
        public bool UnregisterListener<T>(IEventListener eventListener) where  T : IEventData
        {
            if (eventListener == null)
            {
                return false;
            }

            var result = false;
            try
            {
                result = OnUnregisterListener<T>(eventListener);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(EventBus)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when registering listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventListener">The event listener.</param>
        /// <returns><c>true</c> if registering the listener successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnRegisterListener<T>(IEventListener eventListener) where T : IEventData;
        /// <summary>
        /// Called when triggering event data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData">The event data.</param>
        /// <returns>EventBus.</returns>
        protected abstract EventBus OnTrigger<T>(IEventData eventData) where T : IEventData;
        /// <summary>
        /// Called when unregistering listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventListener">The event listener.</param>
        /// <returns><c>true</c> if registering the listener successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnUnregisterListener<T>(IEventListener eventListener) where T : IEventData;
    }
}
