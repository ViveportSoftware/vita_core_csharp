using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class GraphicsDeviceManager
    {
        /// <summary>
        /// Class GraphicsDisplayInfo.
        /// </summary>
        public class GraphicsDisplayInfo
        {
            /// <summary>
            /// Gets or sets the monitor list.
            /// </summary>
            /// <value>The monitor list.</value>
            public List<GraphicsMonitorInfo> MonitorList { get; set; } = new List<GraphicsMonitorInfo>();
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; } = "";
        }
    }
}
