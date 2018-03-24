namespace Htc.Vita.Core.Runtime
{
    public class WmiProcessWatcherFactory : ProcessWatcherFactory
    {
        protected override ProcessWatcher OnCreateProcessWatcher(string targetProcessName)
        {
            return new WmiProcessWatcher().SetTargetProcessName(targetProcessName);
        }
    }
}
