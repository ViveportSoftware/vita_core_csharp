using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Htc.Vita.Core.Log
{
    public abstract class Logger
    {
        private static Dictionary<string, Logger> Instances { get; } = new Dictionary<string, Logger>();

        private static readonly object InstancesLock = new object();

        private static Type defaultType = typeof(ConsoleLogger);

        public string Name { get; } = string.Empty;

        public static void Register<T>() where T : Logger
        {
            defaultType = typeof(T);
            Console.Error.WriteLine("Registered default logger type to " + defaultType);
        }

        public static Logger GetInstance(Type type)
        {
            var name = string.Empty;
            if (type != null)
            {
                name = type.Name;
            }
            return GetInstance(name);
        }

        public static Logger GetInstance()
        {
            return GetInstance("");
        }

        public static Logger GetInstance(string name)
        {
            Logger instance;
            try
            {
                instance = DoGetInstance(defaultType, name);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.GetInstance] " + e);
                Console.Error.WriteLine("Initializing " + typeof(ConsoleLogger).FullName + "...");
                instance = new ConsoleLogger(name);
            }
            return instance;
        }

        public static Logger GetInstance<T>(Type type) where T : Logger
        {
            var name = string.Empty;
            if (type != null)
            {
                name = type.FullName;
            }
            return GetInstance<T>(name);
        }

        public static Logger GetInstance<T>() where T : Logger
        {
            return GetInstance<T>("");
        }

        public static Logger GetInstance<T>(string name) where T : Logger
        {
            Logger instance;
            try
            {
                instance = DoGetInstance(typeof(T), name);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.GetInstance<T>] " + e);
                Console.Error.WriteLine("Initializing " + typeof(ConsoleLogger).FullName + "...");
                instance = new ConsoleLogger(name);
            }
            return instance;
        }

        private static Logger DoGetInstance(Type type, string name)
        {
            if (type == null || name == null)
            {
                throw new ArgumentException("Invalid arguments to get logger instance");
            }

            var key = type.FullName + "_" + name;
            Logger instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Console.Error.WriteLine("Initializing " + key + "...");
                var constructor = type.GetConstructor(new[] { typeof(string) });
                if (constructor != null)
                {
                    instance = (Logger)constructor.Invoke(new object[] { name });
                }
            }
            if (instance == null)
            {
                Console.Error.WriteLine("Initializing " + typeof(ConsoleLogger).FullName + "[" + name + "]...");
                instance = new ConsoleLogger(name);
            }
            lock (InstancesLock)
            {
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
        }

        protected Logger(string name)
        {
            if (!string.IsNullOrWhiteSpace(name))
            {
                Name = name;
            }
        }

        public void Debug(string message, [CallerMemberName] string tag = "")
        {
            try
            {
                OnDebug(tag, message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Debug] " + e);
            }
        }

        public void Debug(string message, Exception exception, [CallerMemberName] string tag = "")
        {
            try
            {
                OnDebug(tag, message, exception);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Debug] " + e);
            }
        }

        public void Error(string message, [CallerMemberName] string tag = "")
        {
            try
            {
                OnError(tag, message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Error] " + e);
            }
        }

        public void Error(string message, Exception exception, [CallerMemberName] string tag = "")
        {
            try
            {
                OnError(tag, message, exception);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Error] " + e);
            }
        }

        public void Fatal(string message, [CallerMemberName] string tag = "")
        {
            try
            {
                OnFatal(tag, message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Fatal] " + e);
            }
        }

        public void Fatal(string message, Exception exception, [CallerMemberName] string tag = "")
        {
            try
            {
                OnFatal(tag, message, exception);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Fatal] " + e);
            }
        }

        public void Info(string message, [CallerMemberName] string tag = "")
        {
            try
            {
                OnInfo(tag, message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Info] " + e);
            }
        }

        public void Info(string message, Exception exception, [CallerMemberName] string tag = "")
        {
            try
            {
                OnInfo(tag, message, exception);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Info] " + e);
            }
        }

        public void Shutdown()
        {
            try
            {
                OnShutdown();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Shutdown] " + e);
            }
        }

        public void Trace(string message, [CallerMemberName] string tag = "")
        {
            try
            {
                OnTrace(tag, message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Trace] " + e);
            }
        }

        public void Trace(string message, Exception exception, [CallerMemberName] string tag = "")
        {
            try
            {
                OnTrace(tag, message, exception);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Trace] " + e);
            }
        }

        public void Warn(string message, [CallerMemberName] string tag = "")
        {
            try
            {
                OnWarn(tag, message);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Warn] " + e);
            }
        }

        public void Warn(string message, Exception exception, [CallerMemberName] string tag = "")
        {
            try
            {
                OnWarn(tag, message, exception);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("[Fatal][Logger.Warn] " + e);
            }
        }

        protected abstract void OnDebug(string tag, string message);
        protected abstract void OnDebug(string tag, string message, Exception exception);
        protected abstract void OnError(string tag, string message);
        protected abstract void OnError(string tag, string message, Exception exception);
        protected abstract void OnFatal(string tag, string message);
        protected abstract void OnFatal(string tag, string message, Exception exception);
        protected abstract void OnInfo(string tag, string message);
        protected abstract void OnInfo(string tag, string message, Exception exception);
        protected abstract void OnShutdown();
        protected abstract void OnTrace(string tag, string message);
        protected abstract void OnTrace(string tag, string message, Exception exception);
        protected abstract void OnWarn(string tag, string message);
        protected abstract void OnWarn(string tag, string message, Exception exception);
    }
}
