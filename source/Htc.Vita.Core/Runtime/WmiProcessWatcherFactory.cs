namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class WmiProcessWatcherFactory.
    /// Implements the <see cref="ProcessWatcherFactory" />
    /// </summary>
    /// <seealso cref="ProcessWatcherFactory" />
    public class WmiProcessWatcherFactory : ProcessWatcherFactory
    {
        /// <inheritdoc />
        protected override ProcessWatcher OnCreateProcessWatcher(string targetProcessName)
        {
            return new WmiProcessWatcher().SetTargetProcessName(targetProcessName);
        }
    }
}
