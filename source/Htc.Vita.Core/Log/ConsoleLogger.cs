using System;

namespace Htc.Vita.Core.Log
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger(string label) : base(label)
        {
        }

        protected override void OnDebug(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Debug][" + tag + "] " + message);
        }

        protected override void OnDebug(string tag, string message, Exception exception)
        {
            if (exception == null)
            {
                Console.WriteLine("[" + Name + "][Debug][" + tag + "] " + message);
            }
            else
            {
                Console.WriteLine("[" + Name + "][Debug][" + tag + "] " + message + ", " + exception.StackTrace);
            }
        }

        protected override void OnError(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Error][" + tag + "] " + message);
        }

        protected override void OnError(string tag, string message, Exception exception)
        {
            if (exception == null)
            {
                Console.WriteLine("[" + Name + "][Error][" + tag + "] " + message);
            }
            else
            {
                Console.WriteLine("[" + Name + "][Error][" + tag + "] " + message + ", " + exception.StackTrace);
            }
        }

        protected override void OnFatal(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Fatal][" + tag + "] " + message);
        }

        protected override void OnFatal(string tag, string message, Exception exception)
        {
            if (exception == null)
            {
                Console.WriteLine("[" + Name + "][Fatal][" + tag + "] " + message);
            }
            else
            {
                Console.WriteLine("[" + Name + "][Fatal][" + tag + "] " + message + ", " + exception.StackTrace);
            }
        }

        protected override void OnInfo(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Info][" + tag + "] " + message);
        }

        protected override void OnInfo(string tag, string message, Exception exception)
        {
            if (exception == null)
            {
                Console.WriteLine("[" + Name + "][Info][" + tag + "] " + message);
            }
            else
            {
                Console.WriteLine("[" + Name + "][Info][" + tag + "] " + message + ", " + exception.StackTrace);
            }
        }

        protected override void OnShutdown()
        {
            Console.Error.WriteLine("Shutdown the logger ...");
        }

        protected override void OnTrace(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Trace][" + tag + "] " + message);
        }

        protected override void OnTrace(string tag, string message, Exception exception)
        {
            if (exception == null)
            {
                Console.WriteLine("[" + Name + "][Trace][" + tag + "] " + message);
            }
            else
            {
                Console.WriteLine("[" + Name + "][Trace][" + tag + "] " + message + ", " + exception.StackTrace);
            }
        }

        protected override void OnWarn(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Warn][" + tag + "] " + message);
        }

        protected override void OnWarn(string tag, string message, Exception exception)
        {
            if (exception == null)
            {
                Console.WriteLine("[" + Name + "][Warn][" + tag + "] " + message);
            }
            else
            {
                Console.WriteLine("[" + Name + "][Warn][" + tag + "] " + message + ", " + exception.StackTrace);
            }
        }
    }
}
