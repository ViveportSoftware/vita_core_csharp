using System;
using System.IO;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Net;

namespace Htc.Vita.Core.TestService
{
    internal class LoggerImpl : Logger
    {
        private readonly string _path;

        public LoggerImpl(string name) : base(name)
        {
            _path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            _path = Path.Combine(_path, "HTC", "Vita", "Logs");
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }

            _path = Path.Combine(_path, WebUserAgent.GetModuleInstanceName() + ".log");
        }

        protected override void OnDebug(string tag, string message)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            File.AppendAllText(_path, "[" + Name + "][Debug][" + tag + "] " + message + Environment.NewLine);
        }

        protected override void OnDebug(string tag, string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            if (exception == null)
            {
                File.AppendAllText(_path, "[" + Name + "][Debug][" + tag + "] " + message + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(_path, "[" + Name + "][Debug][" + tag + "] " + message + ", " + exception.StackTrace + Environment.NewLine);
            }
        }

        protected override void OnError(string tag, string message)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            File.AppendAllText(_path, "[" + Name + "][Error][" + tag + "] " + message + Environment.NewLine);
        }

        protected override void OnError(string tag, string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            if (exception == null)
            {
                File.AppendAllText(_path, "[" + Name + "][Error][" + tag + "] " + message + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(_path, "[" + Name + "][Error][" + tag + "] " + message + ", " + exception.StackTrace + Environment.NewLine);
            }
        }

        protected override void OnFatal(string tag, string message)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            File.AppendAllText(_path, "[" + Name + "][Fatal][" + tag + "] " + message + Environment.NewLine);
        }

        protected override void OnFatal(string tag, string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            if (exception == null)
            {
                File.AppendAllText(_path, "[" + Name + "][Fatal][" + tag + "] " + message + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(_path, "[" + Name + "][Fatal][" + tag + "] " + message + ", " + exception.StackTrace + Environment.NewLine);
            }
        }

        protected override void OnInfo(string tag, string message)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            File.AppendAllText(_path, "[" + Name + "][Info][" + tag + "] " + message + Environment.NewLine);
        }

        protected override void OnInfo(string tag, string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            if (exception == null)
            {
                File.AppendAllText(_path, "[" + Name + "][Info][" + tag + "] " + message + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(_path, "[" + Name + "][Info][" + tag + "] " + message + ", " + exception.StackTrace + Environment.NewLine);
            }
        }

        protected override void OnShutdown()
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            File.AppendAllText(_path, "Shutdown the logger ..." + Environment.NewLine);
        }

        protected override void OnTrace(string tag, string message)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            File.AppendAllText(_path, "[" + Name + "][Trace][" + tag + "] " + message + Environment.NewLine);
        }

        protected override void OnTrace(string tag, string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            if (exception == null)
            {
                File.AppendAllText(_path, "[" + Name + "][Trace][" + tag + "] " + message + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(_path, "[" + Name + "][Trace][" + tag + "] " + message + ", " + exception.StackTrace + Environment.NewLine);
            }
        }

        protected override void OnWarn(string tag, string message)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            File.AppendAllText(_path, "[" + Name + "][Warn][" + tag + "] " + message + Environment.NewLine);
        }

        protected override void OnWarn(string tag, string message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(_path))
            {
                return;
            }

            if (exception == null)
            {
                File.AppendAllText(_path, "[" + Name + "][Warn][" + tag + "] " + message + Environment.NewLine);
            }
            else
            {
                File.AppendAllText(_path, "[" + Name + "][Warn][" + tag + "] " + message + ", " + exception.StackTrace + Environment.NewLine);
            }
        }
    }
}
