using System;

namespace Htc.Vita.Core.Runtime
{
    public partial class EventBus
    {
        /// <summary>
        /// Interface IEventData
        /// </summary>
        public interface IEventData
        {
            /// <summary>
            /// Gets or sets the source.
            /// </summary>
            /// <value>The source.</value>
            object Source { get; set; }
            /// <summary>
            /// Gets or sets the time in UTC.
            /// </summary>
            /// <value>The time in UTC.</value>
            DateTime TimeInUtc { get; set; }
        }
    }
}
