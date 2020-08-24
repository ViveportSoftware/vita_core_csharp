using System;

namespace Htc.Vita.Core.Log
{
    /// <summary>
    /// Class ConsoleLogger.
    /// Implements the <see cref="Logger" />
    /// </summary>
    /// <seealso cref="Logger" />
    public class ConsoleLogger : Logger
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        public ConsoleLogger(string label) : base(label)
        {
        }

        /// <inheritdoc />
        protected override void OnDebug(
                string tag,
                string message)
        {
            OnDebug(
                    tag,
                    message,
                    null
            );
        }

        /// <inheritdoc />
        protected override void OnDebug(
                string tag,
                string message,
                Exception exception)
        {
            Console.WriteLine(exception == null
                    ? $"[{Name}][Debug][{tag}] {message}"
                    : $"[{Name}][Debug][{tag}] {message}, {exception.StackTrace}"
            );
        }

        /// <inheritdoc />
        protected override void OnError(
                string tag,
                string message)
        {
            OnError(
                    tag,
                    message,
                    null
            );
        }

        /// <inheritdoc />
        protected override void OnError(
                string tag,
                string message,
                Exception exception)
        {
            Console.WriteLine(exception == null
                    ? $"[{Name}][Error][{tag}] {message}"
                    : $"[{Name}][Error][{tag}] {message}, {exception.StackTrace}"
            );
        }

        /// <inheritdoc />
        protected override void OnFatal(
                string tag,
                string message)
        {
            OnFatal(
                    tag,
                    message,
                    null
            );
        }

        /// <inheritdoc />
        protected override void OnFatal(
                string tag,
                string message,
                Exception exception)
        {
            Console.WriteLine(exception == null
                    ? $"[{Name}][Fatal][{tag}] {message}"
                    : $"[{Name}][Fatal][{tag}] {message}, {exception.StackTrace}"
            );
        }

        /// <inheritdoc />
        protected override void OnInfo(
                string tag,
                string message)
        {
            OnInfo(
                    tag,
                    message,
                    null
            );
        }

        /// <inheritdoc />
        protected override void OnInfo(
                string tag,
                string message,
                Exception exception)
        {
            Console.WriteLine(exception == null
                    ? $"[{Name}][Info][{tag}] {message}"
                    : $"[{Name}][Info][{tag}] {message}, {exception.StackTrace}"
            );
        }

        /// <inheritdoc />
        protected override void OnShutdown()
        {
            Console.Error.WriteLine("Shutdown the logger ...");
        }

        /// <inheritdoc />
        protected override void OnTrace(
                string tag,
                string message)
        {
            OnTrace(
                    tag,
                    message,
                    null
            );
        }

        /// <inheritdoc />
        protected override void OnTrace(
                string tag,
                string message,
                Exception exception)
        {
            Console.WriteLine(exception == null
                    ? $"[{Name}][Trace][{tag}] {message}"
                    : $"[{Name}][Trace][{tag}] {message}, {exception.StackTrace}"
            );
        }

        /// <inheritdoc />
        protected override void OnWarn(
                string tag,
                string message)
        {
            OnWarn(
                    tag,
                    message,
                    null
            );
        }

        /// <inheritdoc />
        protected override void OnWarn(
                string tag,
                string message,
                Exception exception)
        {
            Console.WriteLine(exception == null
                    ? $"[{Name}][Warn][{tag}] {message}"
                    : $"[{Name}][Warn][{tag}] {message}, {exception.StackTrace}"
            );
        }
    }
}
