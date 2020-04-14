using System;
using System.Net;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class SecurityProtocolManager.
    /// </summary>
    public static class SecurityProtocolManager
    {
        /// <summary>
        /// Applies the available protocol.
        /// </summary>
        public static void ApplyAvailableProtocol()
        {
            var oldProtocol = ServicePointManager.SecurityProtocol;
            var newProtocol = GetAvailableProtocol();
            if (oldProtocol == newProtocol)
            {
                Logger.GetInstance(typeof(SecurityProtocolManager)).Info($"Current: {newProtocol}. All have been applied.");
                return;
            }

            ServicePointManager.SecurityProtocol = newProtocol;
            Logger.GetInstance(typeof(SecurityProtocolManager)).Info($"Current: {newProtocol}, new-applied: {newProtocol & ~oldProtocol}");
        }

        /// <summary>
        /// Gets the available protocol.
        /// </summary>
        /// <returns>SecurityProtocolType.</returns>
        public static SecurityProtocolType GetAvailableProtocol()
        {
            var result = (SecurityProtocolType) 0;
            foreach (var securityProtocolType in (SecurityProtocolType[]) Enum.GetValues(typeof(SecurityProtocolType)))
            {
                result |= securityProtocolType;
            }
            return result;
        }
    }
}
