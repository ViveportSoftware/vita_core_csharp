using System;
using System.Net;

namespace Htc.Vita.Core.Net
{
    public class SecurityProtocolManager
    {
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
