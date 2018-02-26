using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public partial class UserManager
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(UserManager));

        public static string GetFirstActiveUser()
        {
            return Windows.GetFirstActiveUser(null);
        }
    }
}
