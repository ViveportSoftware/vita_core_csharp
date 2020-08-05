using System;

namespace Htc.Vita.Core.Runtime
{
    public partial class EventBus
    {
        /// <summary>
        /// Class EventData.
        /// Implements the <see cref="IEventData" />
        /// </summary>
        /// <seealso cref="IEventData" />
        public class EventData : IEventData
        {
            /// <summary>
            /// Gets or sets the source.
            /// </summary>
            /// <value>The source.</value>
            public object Source { get; set; }
            /// <summary>
            /// Gets or sets the time in UTC.
            /// </summary>
            /// <value>The time in UTC.</value>
            public DateTime TimeInUtc { get; set; } = DateTime.UtcNow;
        }
    }
}
