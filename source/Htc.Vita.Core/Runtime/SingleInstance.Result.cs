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
    }
}
