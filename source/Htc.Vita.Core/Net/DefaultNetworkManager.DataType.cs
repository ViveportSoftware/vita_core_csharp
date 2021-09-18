namespace Htc.Vita.Core.Net
{
    public partial class DefaultNetworkManager
    {
        internal enum NtpLeapIndicator
        {
            NoWarning          = 0,
            LastMinuteHas61Sec = 1,
            LastMinuteHas59Sec = 2,
            AlarmCondition     = 3
        }

        internal enum NtpMode
        {
            Reserved                     = 0,
            SymmetricActive              = 1,
            SymmetricPassive             = 2,
            Client                       = 3,
            Server                       = 4,
            Broadcast                    = 5,
            ReservedForNtpControlMessage = 6,
            ReservedForPrivateUse        = 7
        }

        internal enum NtpVersionNumber
        {
            IPv4Only    = 3,
            IPv4AndIPv6 = 4
        }
    }
}
