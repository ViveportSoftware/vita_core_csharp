namespace Htc.Vita.Core.Net
{
    public partial class NetworkManager
    {
        /// <summary>
        /// Enum PortStatus
        /// </summary>
        public enum PortStatus
        {
            /// <summary>
            /// Unknown port status
            /// </summary>
            Unknown,
            /// <summary>
            /// The port is in use
            /// </summary>
            InUse,
            /// <summary>
            /// The port is available
            /// </summary>
            Available
        }
    }
}
