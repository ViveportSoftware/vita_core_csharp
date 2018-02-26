namespace Htc.Vita.Core.Runtime
{
    public partial class ServiceManager
    {
        public class ServiceInfo
        {
            public string ServiceName { get; set; }
            public CurrentState CurrentState { get; set; } = CurrentState.Unknown;
            public StartType StartType { get; set; } = StartType.Unknown;
            public int ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
