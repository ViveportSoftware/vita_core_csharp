using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class DefaultEventBus.
    /// Implements the <see cref="EventBus" />
    /// </summary>
    /// <seealso cref="EventBus" />
    public class DefaultEventBus : EventBus
    {
        private readonly Dictionary<Type, List<IEventListener>> listenerListMap = new Dictionary<Type, List<IEventListener>>();

        private bool DoRegisterListener(
                Type eventType,
                IEventListener eventListener)
        {
            List<IEventListener> listenerList = null;
            if (listenerListMap.ContainsKey(eventType))
            {
                listenerList = listenerListMap[eventType];
            }
            if (listenerList == null)
            {
                listenerList = new List<IEventListener>();
            }
            if (!listenerList.Contains(eventListener))
            {
                listenerList.Add(eventListener);
            }
            listenerListMap[eventType] = listenerList;

            return true;
        }

        private DefaultEventBus DoTrigger(
                Type eventType,
                IEventData eventData)
        {
            if (!listenerListMap.ContainsKey(eventType))
            {
                return this;
            }
            var listenerList = listenerListMap[eventType];
            if (listenerList == null)
            {
                return this;
            }

            MethodInfo methodInfo = null;
            foreach (var eventListener in listenerList.Where(eventListener => eventListener != null))
            {
                if (methodInfo == null)
                {
                    methodInfo = GetCandidateMethod(
                            eventListener,
                            eventData
                    );
                }

                if (methodInfo == null)
                {
                    Logger.GetInstance(typeof(DefaultEventBus)).Error("Can not find candidate method in event listener");
                    return this;
                }

                var methodInfoInTask = methodInfo;
                Task.Run(() =>
                {
                        methodInfoInTask.Invoke(
                                eventListener,
                                new object[] { eventData }
                        );
                });
            }

            return this;
        }

        private static MethodInfo GetCandidateMethod(
                IEventListener eventListener,
                IEventData eventData)
        {
            if (eventListener == null || eventData == null)
            {
                return null;
            }

            var eventType = eventData.GetType();
            foreach (var methodInfo in eventListener.GetType().GetMethods())
            {
                var parameterList = methodInfo.GetParameters();
                if (parameterList.Length != 1)
                {
                    continue;
                }

                if (parameterList[0].ParameterType.IsAssignableFrom(eventType))
                {
                    return methodInfo;
                }
            }

            return null;
        }

        private bool DoUnregisterListener(
                Type eventType,
                IEventListener eventListener)
        {
            if (!listenerListMap.ContainsKey(eventType))
            {
                return true;
            }
            var listenerList = listenerListMap[eventType];
            if (listenerList == null)
            {
                return true;
            }
            if (listenerList.Contains(eventListener))
            {
                listenerList.Remove(eventListener);
            }

            return true;
        }

        /// <inheritdoc />
        protected override bool OnRegisterListener<T>(IEventListener eventListener)
        {
            return DoRegisterListener(
                    typeof(T),
                    eventListener
            );
        }

        /// <inheritdoc />
        protected override EventBus OnTrigger<T>(IEventData eventData)
        {
            return DoTrigger(
                    typeof(T),
                    eventData
            );
        }

        /// <inheritdoc />
        protected override bool OnUnregisterListener<T>(IEventListener eventListener)
        {
            return DoUnregisterListener(
                    typeof(T),
                    eventListener
            );
        }
    }
}
