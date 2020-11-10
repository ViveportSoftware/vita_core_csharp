using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class WpdManager
    {
        /// <summary>
        /// Class DeviceInfo.
        /// </summary>
        public class DeviceInfo
        {
            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            public string Description { get; set; } = "";
            /// <summary>
            /// Gets or sets the manufacturer name.
            /// </summary>
            /// <value>The manufacturer name.</value>
            public string Manufacturer { get; set; } = "";
            /// <summary>
            /// Gets or sets the path.
            /// </summary>
            /// <value>The path.</value>
            public string Path { get; set; } = "";
            /// <summary>
            /// Gets or sets the product name.
            /// </summary>
            /// <value>The product name.</value>
            public string Product { get; set; } = "";
            /// <summary>
            /// Gets or sets the serial number.
            /// </summary>
            /// <value>The serial number.</value>
            public string SerialNumber { get; set; } = "";
            /// <summary>
            /// Gets or sets the optional values.
            /// </summary>
            /// <value>The optional values.</value>
            public Dictionary<string, string> Optional { get; set; } = new Dictionary<string, string>();
        }
    }
}
