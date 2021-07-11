using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class GraphicsDeviceManager
    {
        /// <summary>
        /// Class GraphicsAdapterInfo.
        /// </summary>
        public class GraphicsAdapterInfo
        {
            /// <summary>
            /// Gets or sets the description.
            /// </summary>
            /// <value>The description.</value>
            public string Description { get; set; } = "";
            /// <summary>
            /// Gets or sets the device identifier.
            /// </summary>
            /// <value>The device identifier.</value>
            public string DeviceId { get; set; } = "";
            /// <summary>
            /// Gets or sets the display list.
            /// </summary>
            /// <value>The display list.</value>
            public List<GraphicsDisplayInfo> DisplayList { get; set; } = new List<GraphicsDisplayInfo>();
            /// <summary>
            /// Gets or sets the revision identifier.
            /// </summary>
            /// <value>The revision identifier.</value>
            public string RevisionId { get; set; } = "";
            /// <summary>
            /// Gets or sets the subsystem device identifier.
            /// </summary>
            /// <value>The subsystem device identifier.</value>
            public string SubsystemDeviceId { get; set; } = "";
            /// <summary>
            /// Gets or sets the subsystem vendor identifier.
            /// </summary>
            /// <value>The subsystem vendor identifier.</value>
            public string SubsystemVendorId { get; set; } = "";
            /// <summary>
            /// Gets or sets the vendor identifier.
            /// </summary>
            /// <value>The vendor identifier.</value>
            public string VendorId { get; set; } = "";
        }
    }
}
