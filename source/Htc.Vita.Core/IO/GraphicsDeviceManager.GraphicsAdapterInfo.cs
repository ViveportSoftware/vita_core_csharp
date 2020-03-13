using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class GraphicsDeviceManager
    {
        public class GraphicsAdapterInfo
        {
            public string Description { get; set; } = "";
            public string DeviceId { get; set; } = "";
            public List<GraphicsDisplayInfo> DisplayList { get; set; } = new List<GraphicsDisplayInfo>();
            public string RevisionId { get; set; } = "";
            public string SubsystemDeviceId { get; set; } = "";
            public string SubsystemVendorId { get; set; } = "";
            public string VendorId { get; set; } = "";
        }
    }
}
