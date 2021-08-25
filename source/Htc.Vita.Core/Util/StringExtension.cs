using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class StringExtension.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Splits to set.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="separator">The separator.</param>
        /// <returns>ISet&lt;System.String&gt;.</returns>
        public static ISet<string> SplitToSet(this string data, params char[] separator)
        {
            if (data == null)
            {
                return null;
            }

            var items = data.Split(separator);
            var result = new HashSet<string>();
            foreach (var item in items)
            {
                result.Add(item);
            }
            return result;
        }

        /// <summary>
        /// Converts data by description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>T.</returns>
        public static T ToTypeByDescription<T>(this string data)
                where T : struct, IConvertible, IComparable, IFormattable
        {
            return Convert.ToTypeByDescription<T>(data);
        }

        /// <summary>
        /// Converts data by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>T.</returns>
        public static T ToTypeByName<T>(this string data)
                where T : struct, IConvertible, IComparable, IFormattable
        {
            return Convert.ToTypeByName<T>(data);
        }

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
