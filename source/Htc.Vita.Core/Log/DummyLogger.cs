using System;
using System.Text;

namespace Htc.Vita.Core.Log
{
    public class DummyLogger : Logger
    {
        private readonly StringBuilder _buffer;

        public DummyLogger(string label) : base(label)
        {
            _buffer = new StringBuilder();
        }

        public string GetBuffer()
        {
            return _buffer.ToString();
        }

        protected override void OnDebug(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        protected override void OnDebug(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        protected override void OnError(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        protected override void OnError(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        protected override void OnFatal(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        protected override void OnFatal(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        protected override void OnInfo(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        protected override void OnInfo(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        protected override void OnShutdown()
        {
            // do nothing
        }

        protected override void OnTrace(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        protected override void OnTrace(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }

        protected override void OnWarn(string tag, string message)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message);
        }

        protected override void OnWarn(string tag, string message, Exception exception)
        {
            _buffer.Clear().Append(Name).Append(tag).Append(message).Append(exception);
        }
    }
}
