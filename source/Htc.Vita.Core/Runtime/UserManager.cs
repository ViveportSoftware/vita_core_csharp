using System;
using System.Security.Principal;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    public static partial class UserManager
    {
        public static string GetFirstActiveUser()
        {
            return Windows.GetFirstActiveUser(null) ?? GetCurrentUser();
        }

        private static string GetCurrentUser()
        {
            string result = null;
            try
            {
                result = WindowsIdentity.GetCurrent().Name;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UserManager)).Error("Can not get current Windows username: " + e.Message);
            }
            return result;
        }
    }
}
