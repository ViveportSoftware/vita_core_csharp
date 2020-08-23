namespace Htc.Vita.Core.Runtime
{
    public abstract partial class ProcessWatcher
    {
        /// <summary>
        /// Class ProcessInfo.
        /// </summary>
        public class ProcessInfo
        {
            /// <summary>
            /// Gets or sets the process identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public int Id { get; set; }
            /// <summary>
            /// Gets or sets the process name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }
            /// <summary>
            /// Gets or sets the process path.
            /// </summary>
            /// <value>The path.</value>
            public string Path { get; set; }
        }
    }
}
