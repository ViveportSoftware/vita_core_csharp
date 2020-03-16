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
        protected override void OnDebug(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Debug][" + tag + "] " + message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnError(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Error][" + tag + "] " + message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnFatal(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Fatal][" + tag + "] " + message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnInfo(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Info][" + tag + "] " + message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnShutdown()
        {
            Console.Error.WriteLine("Shutdown the logger ...");
        }

        /// <inheritdoc />
        protected override void OnTrace(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Trace][" + tag + "] " + message);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        protected override void OnWarn(string tag, string message)
        {
            Console.WriteLine("[" + Name + "][Warn][" + tag + "] " + message);
        }

        /// <inheritdoc />
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
