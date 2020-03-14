using System;
using System.Text;

namespace Htc.Vita.Core.Log
{
    /// <summary>
    /// Class DummyLogger.
    /// Implements the <see cref="Logger" />
    /// </summary>
    /// <seealso cref="Logger" />
    public class DummyLogger : Logger
    {
        private readonly StringBuilder _buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DummyLogger"/> class.
        /// </summary>
        /// <param name="label">The label.</param>
        public DummyLogger(string label) : base(label)
        {
            _buffer = new StringBuilder();
        }

        /// <summary>
        /// Gets the buffer.
        /// </summary>
        /// <returns>System.String.</returns>
        public string GetBuffer()
        {
            return _buffer.ToString();
        }

        /// <inheritdoc />
        protected override void OnDebug(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        /// <inheritdoc />
        protected override void OnDebug(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        /// <inheritdoc />
        protected override void OnError(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        /// <inheritdoc />
        protected override void OnError(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        /// <inheritdoc />
        protected override void OnFatal(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        /// <inheritdoc />
        protected override void OnFatal(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        /// <inheritdoc />
        protected override void OnInfo(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        /// <inheritdoc />
        protected override void OnInfo(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        /// <inheritdoc />
        protected override void OnShutdown()
        {
            // do nothing
        }

        /// <inheritdoc />
        protected override void OnTrace(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        /// <inheritdoc />
        protected override void OnTrace(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        /// <inheritdoc />
        protected override void OnWarn(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        /// <inheritdoc />
        protected override void OnWarn(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }
    }
}
