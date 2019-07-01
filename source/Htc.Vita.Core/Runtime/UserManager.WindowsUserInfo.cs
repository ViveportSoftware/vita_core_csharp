namespace Htc.Vita.Core.Runtime
{
    public static partial class UserManager
    {
        internal class WindowsUserInfo
        {
            public Interop.Windows.WindowsTerminalServiceConnectState State { get; set; }
            public string Domain { get; set; }
            public string Username { get; set; }
        }
    }
}
