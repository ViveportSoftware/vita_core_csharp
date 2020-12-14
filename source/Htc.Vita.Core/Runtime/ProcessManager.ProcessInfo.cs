namespace Htc.Vita.Core.Runtime
{
    public static partial class ProcessManager
    {
        /// <summary>
        /// Class ProcessInfo.
        /// </summary>
        public class ProcessInfo
        {
            /// <summary>
            /// Gets or sets the identifier.
            /// </summary>
            /// <value>The identifier.</value>
            public int Id { get; set; }
            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            /// <value>The name.</value>
            public string Name { get; set; }
            /// <summary>
            /// Gets or sets the path.
            /// </summary>
            /// <value>The path.</value>
            public string Path { get; set; }
            /// <summary>
            /// Gets or sets the user.
            /// </summary>
            /// <value>The user.</value>
            public string User { get; set; }
        }
    }
}
