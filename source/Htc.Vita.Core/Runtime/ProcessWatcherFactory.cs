using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class ProcessWatcherFactory.
    /// </summary>
    public abstract class ProcessWatcherFactory
    {
        static ProcessWatcherFactory()
        {
            TypeRegistry.RegisterDefault<ProcessWatcherFactory, WmiProcessWatcherFactory>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : ProcessWatcherFactory, new()
        {
            TypeRegistry.Register<ProcessWatcherFactory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>ProcessWatcherFactory.</returns>
        public static ProcessWatcherFactory GetInstance()
        {
            return TypeRegistry.GetInstance<ProcessWatcherFactory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>ProcessWatcherFactory.</returns>
        public static ProcessWatcherFactory GetInstance<T>()
                where T : ProcessWatcherFactory, new()
        {
            return TypeRegistry.GetInstance<ProcessWatcherFactory, T>();
        }

        /// <summary>
        /// Creates the process watcher.
        /// </summary>
        /// <returns>ProcessWatcher.</returns>
        public ProcessWatcher CreateProcessWatcher()
        {
            return CreateProcessWatcher(null);
        }

        /// <summary>
        /// Creates the process watcher.
        /// </summary>
        /// <param name="targetProcessName">Name of the target process.</param>
        /// <returns>ProcessWatcher.</returns>
        public ProcessWatcher CreateProcessWatcher(string targetProcessName)
        {
            return OnCreateProcessWatcher(targetProcessName);
        }

        /// <summary>
        /// Called when creating process watcher.
        /// </summary>
        /// <param name="targetProcessName">Name of the target process.</param>
        /// <returns>ProcessWatcher.</returns>
        protected abstract ProcessWatcher OnCreateProcessWatcher(string targetProcessName);
    }
}
