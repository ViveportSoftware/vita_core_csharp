namespace Htc.Vita.Core.IO
{
    public partial class GraphicsDeviceManager
    {
        public class GraphicsAdapterInfo
        {
            public string Description { get; set; } = "";
            public string DeviceId { get; set; } = "";
            public string RevisionId { get; set; } = "";
            public string SubsystemDeviceId { get; set; } = "";
            public string SubsystemVendorId { get; set; } = "";
            public string VendorId { get; set; } = "";
        }
    }
}
