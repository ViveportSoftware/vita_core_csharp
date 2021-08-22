using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class StringExtension.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Converts to URI.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>Uri.</returns>
        public static Uri ToUri(this string data)
        {
            if (data == null)
            {
                return null;
            }

            try
            {
                return new Uri(data);
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(StringExtension)).Warn($"Can not parse \"{data}\" to URI.");
            }
            return null;
        }
    }
}
