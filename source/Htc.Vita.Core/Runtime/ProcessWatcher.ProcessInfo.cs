namespace Htc.Vita.Core.Runtime
{
    public abstract partial class ProcessWatcher
    {
        public class ProcessInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
        }
    }
}
