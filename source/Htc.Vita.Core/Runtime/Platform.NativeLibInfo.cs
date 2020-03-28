using System;

namespace Htc.Vita.Core.Runtime
{
    public static partial class Platform
    {
        /// <summary>
        /// Class NativeLibInfo.
        /// </summary>
        public class NativeLibInfo
        {
            /// <summary>
            /// Gets the inner exception.
            /// </summary>
            /// <value>The inner exception.</value>
            public Exception InnerException { get; internal set; }
            /// <summary>
            /// Gets the handle.
            /// </summary>
            /// <value>The handle.</value>
            public IntPtr Handle { get; internal set; }

            /// <summary>
            /// Check if there is no error.
            /// </summary>
            /// <returns><c>true</c> if there is no error; otherwise, <c>false</c>.</returns>
            public bool IsNoError()
            {
                return InnerException == null;
            }
        }
    }
}
