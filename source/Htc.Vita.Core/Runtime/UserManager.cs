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
            var result = Windows.GetFirstActiveUserInPlatform(null);
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

        /// <summary>
        /// Determines whether shell user is elevated.
        /// </summary>
        /// <returns><c>true</c> if shell user is elevated; otherwise, <c>false</c>.</returns>
        public static bool IsShellUserElevated()
        {
            var result = false;
            try
            {
                result = Windows.IsShellUserElevatedInPlatform();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(UserManager)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Sends the message to first active user.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="timeout">The timeout.</param>
        /// <returns><c>true</c> if sending message to the first active user successfully, <c>false</c> otherwise.</returns>
        public static bool SendMessageToFirstActiveUser(
                string title,
                string message,
                uint timeout)
        {
            return Windows.SendMessageToFirstActiveUserInPlatform(
                    title,
                    message,
                    timeout,
                    null
            );
        }
    }
}
