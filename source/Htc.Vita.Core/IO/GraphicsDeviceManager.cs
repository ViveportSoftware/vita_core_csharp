using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public partial class GraphicsDeviceManager
    {
        public static List<GraphicsAdapterInfo> GetGraphicsAdapterList()
        {
            if (!Runtime.Platform.IsWindows)
            {
                return new List<GraphicsAdapterInfo>();
            }

            return Windows.GetGraphicsAdapterListInPlatform();
        }
    }
}
