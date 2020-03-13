using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class UsbManager
    {
        public class DeviceInfo
        {
            public string Description { get; set; } = "";
            public string Manufacturer { get; set; } = "";
            public string Path { get; set; } = "";
            public string Product { get; set; } = "";
            public string ProductId { get; set; } = "";
            public string SerialNumber { get; set; } = "";
            public string VendorId { get; set; } = "";
            public Dictionary<string, string> Optional { get; set; } = new Dictionary<string, string>();
        }
    }
}
