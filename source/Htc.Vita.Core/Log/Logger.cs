using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Htc.Vita.Core.Log
{
    /// <summary>
    /// Class Logger.
    /// </summary>
    public abstract class Logger
    {
        private static Dictionary<string, Logger> Instances { get; } = new Dictionary<string, Logger>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(ConsoleLogger);

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; } = string.Empty;

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : Logger
        {
            var type = typeof(T);
            if (_defaultType == type)
            {
                return;
            }

            _defaultType = type;
            Console.Error.WriteLine($"Registered default {nameof(Logger)} type to {_defaultType}");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>Logger.</returns>
        public static Logger GetInstance(Type type)
        {
            var name = string.Empty;
            if (type != null)
            {
                name = type.Name;
            }
            return GetInstance(name);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>Logger.</returns>
        public static Logger GetInstance()
        {
            return GetInstance("");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>Logger.</returns>
        public static Logger GetInstance(string name)
        {
            Logger instance;
            try
            {
                instance = DoGetInstance(_defaultType, name);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.GetInstance] {e}");
                Console.Error.WriteLine($"Initializing {typeof(ConsoleLogger).FullName}...");
                instance = new ConsoleLogger(name);
            }
            return instance;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">The type.</param>
        /// <returns>Logger.</returns>
        public static Logger GetInstance<T>(Type type)
                where T : Logger
        {
            var name = string.Empty;
            if (type != null)
            {
                name = type.FullName;
            }
            return GetInstance<T>(name);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>Logger.</returns>
        public static Logger GetInstance<T>()
                where T : Logger
        {
            return GetInstance<T>("");
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">The name.</param>
        /// <returns>Logger.</returns>
        public static Logger GetInstance<T>(string name)
                where T : Logger
        {
            Logger instance;
            try
            {
                instance = DoGetInstance(typeof(T), name);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.GetInstance<T>] {e}");
                Console.Error.WriteLine($"Initializing {typeof(ConsoleLogger).FullName}...");
                instance = new ConsoleLogger(name);
            }
            return instance;
        }

        private static Logger DoGetInstance(
                Type type,
                string name)
        {
            if (type == null || name == null)
            {
                throw new ArgumentException("Invalid arguments to get logger instance");
            }

            var key = $"{type.FullName}_{name}";
            Logger instance = null;
            lock (InstancesLock)
            {
                if (Instances.ContainsKey(key))
                {
                    instance = Instances[key];
                }
                if (instance == null)
                {
                    Console.Error.WriteLine($"Initializing {key}...");
                    var constructor = type.GetConstructor(new[] { typeof(string) });
                    if (constructor != null)
                    {
                        instance = (Logger)constructor.Invoke(new object[] { name });
                    }
                }
                if (instance == null)
                {
                    Console.Error.WriteLine($"Initializing {typeof(ConsoleLogger).FullName}[{name}]...");
                    instance = new ConsoleLogger(name);
                }
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Logger"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        protected Logger(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
        }

        /// <summary>
        /// Dump the specified message in debug level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public void Debug(
                string message,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnDebug(
                        tag,
                        message
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Debug] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in debug level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="tag">The tag.</param>
        public void Debug(
                string message,
                Exception exception,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnDebug(
                        tag,
                        message,
                        exception
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Debug] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in error level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public void Error(
                string message,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnError(
                        tag,
                        message
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Error] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in error level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="tag">The tag.</param>
        public void Error(
                string message,
                Exception exception,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnError(
                        tag,
                        message,
                        exception
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Error] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public void Fatal(
                string message,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnFatal(
                        tag,
                        message
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Fatal] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in fatal level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="tag">The tag.</param>
        public void Fatal(
                string message,
                Exception exception,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnFatal(
                        tag,
                        message,
                        exception
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Fatal] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in information level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public void Info(
                string message,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnInfo(
                        tag,
                        message
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Info] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in information level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="tag">The tag.</param>
        public void Info(
                string message,
                Exception exception,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnInfo(
                        tag,
                        message,
                        exception
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Info] {e}");
            }
        }

        /// <summary>
        /// Shuts down this instance.
        /// </summary>
        public void Shutdown()
        {
            try
            {
                OnShutdown();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Shutdown] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in trace level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public void Trace(
                string message,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnTrace(
                        tag,
                        message
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Trace] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in trace level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="tag">The tag.</param>
        public void Trace(
                string message,
                Exception exception,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnTrace(
                        tag,
                        message,
                        exception
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Trace] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in warning level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="tag">The tag.</param>
        public void Warn(
                string message,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnWarn(
                        tag,
                        message
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Warn] {e}");
            }
        }

        /// <summary>
        /// Dump the specified message in warning level.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="tag">The tag.</param>
        public void Warn(
                string message,
                Exception exception,
                [CallerMemberName] string tag = "")
        {
            try
            {
                OnWarn(
                        tag,
                        message,
                        exception
                );
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"[Fatal][Logger.Warn] {e}");
            }
        }

        /// <summary>
        /// Called when dumping in debug level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        protected abstract void OnDebug(
                string tag,
                string message
        );
        /// <summary>
        /// Called when dumping in debug level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        protected abstract void OnDebug(
                string tag,
                string message,
                Exception exception
        );
        /// <summary>
        /// Called when dumping in error level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        protected abstract void OnError(
                string tag,
                string message
        );
        /// <summary>
        /// Called when dumping in error level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        protected abstract void OnError(
                string tag,
                string message,
                Exception exception
        );
        /// <summary>
        /// Called when dumping in fatal level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        protected abstract void OnFatal(
                string tag,
                string message
        );
        /// <summary>
        /// Called when dumping in fatal level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        protected abstract void OnFatal(
                string tag,
                string message,
                Exception exception
        );
        /// <summary>
        /// Called when dumping in information level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        protected abstract void OnInfo(
                string tag,
                string message
        );
        /// <summary>
        /// Called when dumping in information level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        protected abstract void OnInfo(
                string tag,
                string message,
                Exception exception
        );
        /// <summary>
        /// Called when shutting down.
        /// </summary>
        protected abstract void OnShutdown();
        /// <summary>
        /// Called when dumping in trace level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        protected abstract void OnTrace(
                string tag,
                string message
        );
        /// <summary>
        /// Called when dumping in trace level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        protected abstract void OnTrace(
                string tag,
                string message,
                Exception exception
        );
        /// <summary>
        /// Called when dumping in warning level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        protected abstract void OnWarn(
                string tag,
                string message
        );
        /// <summary>
        /// Called when dumping in warning level.
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        protected abstract void OnWarn(
                string tag,
                string message,
                Exception exception
        );
    }
}
