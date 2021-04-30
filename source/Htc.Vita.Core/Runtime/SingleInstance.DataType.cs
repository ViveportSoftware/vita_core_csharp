using System;
using System.ComponentModel;

namespace Htc.Vita.Core.Runtime
{
    public partial class SingleInstance
    {
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

        /// <summary>
        /// Enum MessageVerificationPolicy
        /// </summary>
        [Flags]
        public enum MessageVerificationPolicy : uint
        {
            /// <summary>
            /// Verify none
            /// </summary>
            None = 0,
            /// <summary>
            /// Verify if the sender and the receiver are the same binary
            /// </summary>
            SameBinary = 1,
            /// <summary>
            /// Verify if the sender and the receiver are in the same location
            /// </summary>
            SameLocation = 2
        }
    }
}
