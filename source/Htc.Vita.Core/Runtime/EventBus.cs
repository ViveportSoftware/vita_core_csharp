using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class EventBus.
    /// </summary>
    public abstract partial class EventBus
    {
        static EventBus()
        {
            TypeRegistry.RegisterDefault<EventBus, DefaultEventBus>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : EventBus, new()
        {
            TypeRegistry.Register<EventBus, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>EventBus.</returns>
        public static EventBus GetInstance()
        {
            return TypeRegistry.GetInstance<EventBus>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>EventBus.</returns>
        public static EventBus GetInstance<T>()
                where T : EventBus, new()
        {
            return TypeRegistry.GetInstance<EventBus, T>();
        }

        /// <summary>
        /// Registers the listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventListener">The event listener.</param>
        /// <returns><c>true</c> if registering the listener successfully, <c>false</c> otherwise.</returns>
        public bool RegisterListener<T>(IEventListener eventListener)
                where T : IEventData
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
        public EventBus Trigger<T>(IEventData eventData)
                where T : IEventData
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
        public bool UnregisterListener<T>(IEventListener eventListener)
                where  T : IEventData
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
        protected abstract bool OnRegisterListener<T>(IEventListener eventListener)
                where T : IEventData;
        /// <summary>
        /// Called when triggering event data.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventData">The event data.</param>
        /// <returns>EventBus.</returns>
        protected abstract EventBus OnTrigger<T>(IEventData eventData)
                where T : IEventData;
        /// <summary>
        /// Called when unregistering listener.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="eventListener">The event listener.</param>
        /// <returns><c>true</c> if registering the listener successfully, <c>false</c> otherwise.</returns>
        protected abstract bool OnUnregisterListener<T>(IEventListener eventListener)
                where T : IEventData;
    }
}
