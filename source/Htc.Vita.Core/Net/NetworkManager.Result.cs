namespace Htc.Vita.Core.Net
{
    public partial class NetworkManager
    {
        /// <summary>
        /// Class GetLocalPortStatusResult.
        /// </summary>
        public class GetLocalPortStatusResult
        {
            /// <summary>
            /// Gets or sets the local port status.
            /// </summary>
            /// <value>The local port status.</value>
            public PortStatus LocalPortStatus { get; set; } = PortStatus.Unknown;
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetLocalPortStatusStatus Status { get; set; } = GetLocalPortStatusStatus.Unknown;
        }

        /// <summary>
        /// Class GetUnusedLocalPortResult.
        /// </summary>
        public class GetUnusedLocalPortResult
        {
            /// <summary>
            /// Gets or sets the local port.
            /// </summary>
            /// <value>The local port.</value>
            public int LocalPort { get; set; }
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public GetUnusedLocalPortStatus Status { get; set; } = GetUnusedLocalPortStatus.Unknown;
        }

        /// <summary>
        /// Class VerifyLocalPortStatusResult.
        /// </summary>
        public class VerifyLocalPortStatusResult
        {
            /// <summary>
            /// Gets or sets the local port status.
            /// </summary>
            /// <value>The local port status.</value>
            public PortStatus LocalPortStatus { get; set; } = PortStatus.Unknown;
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public VerifyLocalPortStatusStatus Status { get; set; } = VerifyLocalPortStatusStatus.Unknown;
        }

        /// <summary>
        /// Enum GetLocalPortStatusStatus
        /// </summary>
        public enum GetLocalPortStatusStatus
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// Ok
            /// </summary>
            Ok,
            /// <summary>
            /// Invalid data
            /// </summary>
            InvalidData,
            /// <summary>
            /// Network error
            /// </summary>
            NetworkError
        }

        /// <summary>
        /// Enum GetUnusedLocalPortStatus
        /// </summary>
        public enum GetUnusedLocalPortStatus
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// Ok
            /// </summary>
            Ok,
            /// <summary>
            /// Network error
            /// </summary>
            NetworkError
        }

        /// <summary>
        /// Enum VerifyLocalPortStatusStatus
        /// </summary>
        public enum VerifyLocalPortStatusStatus
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown,
            /// <summary>
            /// Ok
            /// </summary>
            Ok,
            /// <summary>
            /// Invalid data
            /// </summary>
            InvalidData,
            /// <summary>
            /// Network error
            /// </summary>
            NetworkError
        }
    }
}
