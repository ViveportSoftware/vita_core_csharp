using System;
using System.Security.Principal;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class UserManager.
    /// </summary>
    public static partial class UserManager
    {
        /// <summary>
        /// Gets the first active user.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetFirstActiveUser()
        {
            var result = Windows.GetFirstActiveUser(null);
            if (string.IsNullOrWhiteSpace(result))
            {
                result = GetCurrentUser();
            }

            if (!"NT AUTHORITY\\SYSTEM".Equals(result))
            {
                return result;
            }

            Logger.GetInstance(typeof(UserManager)).Error("Only system account found, no user account");
            return null;
        }

        private static string GetCurrentUser()
        {
            string result = null;
            try
            {
                using (var identity = WindowsIdentity.GetCurrent())
                {
                    result = identity.Name;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UserManager)).Error("Can not get current Windows username: " + e.Message);
            }
            return result;
        }
    }
}
