namespace Htc.Vita.Core.Runtime
{
    public static partial class UserManager
    {
        public static string GetFirstActiveUser()
        {
            return Windows.GetFirstActiveUser(null);
        }
    }
}
