namespace Htc.Vita.Core.Runtime
{
    public partial class EventBus
    {
        /// <summary>
        /// Interface IEventListener
        /// </summary>
        public interface IEventListener
        {
        }

        /// <summary>
        /// Interface IEventListener
        /// Implements the <see cref="IEventListener" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <seealso cref="IEventListener" />
        public interface IEventListener<T> : IEventListener where T : IEventData
        {
            /// <summary>
            /// Processes the event.
            /// </summary>
            /// <param name="eventData">The event data.</param>
            void ProcessEvent(T eventData);
        }
    }
}
