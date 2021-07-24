using System.ComponentModel;

namespace Htc.Vita.Core.Runtime
{
    public partial class SingleInstance
    {
        /// <summary>
        /// Class SendMessageResult.
        /// </summary>
        public class SendMessageResult
        {
            /// <summary>
            /// Gets or sets the status.
            /// </summary>
            /// <value>The status.</value>
            public SendMessageStatus Status { get; set; } = SendMessageStatus.Unknown;
        }

        /// <summary>
        /// Enum SendMessageStatus
        /// </summary>
        public enum SendMessageStatus
        {
            /// <summary>
            /// Unknown
            /// </summary>
            [Description("unknown")]
            Unknown,
            /// <summary>
            /// Ok
            /// </summary>
            [Description("ok")]
            Ok,
            /// <summary>
            /// Invalid data
            /// </summary>
            [Description("invalid_data")]
            InvalidData,
            /// <summary>
            /// Connection not ready
            /// </summary>
            [Description("connection_not_ready")]
            ConnectionNotReady,
            /// <summary>
            /// Insufficient permission
            /// </summary>
            [Description("insufficient_permission")]
            InsufficientPermission
        }
    }
}
