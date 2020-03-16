namespace Htc.Vita.Core.Runtime
{
    public static partial class ProcessManager
    {
        internal class WindowsProcessInfo
        {
            public int Id { get; set; }
            public int SessionId { get; set; }
            public string Name { get; set; }
            public string UserSid { get; set; }
        }
    }
}
