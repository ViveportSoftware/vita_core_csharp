namespace Htc.Vita.Core.Runtime
{
    public static partial class ProcessManager
    {
        public class ProcessInfo
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public string User { get; set; }
        }
    }
}
