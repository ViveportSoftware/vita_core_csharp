namespace Htc.Vita.Core.IO
{
    public static partial class GraphicsDeviceManager
    {
        /// <summary>
        /// Class GraphicsMonitorInfo.
        /// </summary>
        public class GraphicsMonitorInfo
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
            /// Gets or sets the device key.
            /// </summary>
            /// <value>The device key.</value>
            public string DeviceKey { get; set; } = "";
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; } = "";
        }
    }
}