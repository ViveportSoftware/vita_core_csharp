using System;

namespace Htc.Vita.Core.Runtime
{
    public partial class SingleInstance
    {
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
