using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class GraphicsDeviceManager
    {
        public class GraphicsDisplayInfo
        {
            public List<GraphicsMonitorInfo> MonitorList { get; set; } = new List<GraphicsMonitorInfo>();
            public string Name { get; set; } = "";
        }
    }
}
