namespace Htc.Vita.Core.Util
{
    public partial class Win32Registry
    {
        public enum KeyPermissionCheck
        {
            Default          = 0,
            ReadSubTree      = 1,
            ReadWriteSubTree = 2,
        };
    }
}
