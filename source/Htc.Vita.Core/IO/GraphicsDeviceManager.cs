using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class GraphicsDeviceManager.
    /// </summary>
    public static partial class GraphicsDeviceManager
    {
        /// <summary>
        /// Gets the graphics adapter list.
        /// </summary>
        /// <returns>List&lt;GraphicsAdapterInfo&gt;.</returns>
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
