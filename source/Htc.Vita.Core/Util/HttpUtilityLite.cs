using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class HttpUtilityLite.
    /// </summary>
    public static class HttpUtilityLite
    {
        /// <summary>
        /// Parses the query string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>NameValueCollection.</returns>
        public static NameValueCollection ParseQueryString(string data)
        {
            var result = new HttpValueCollection();
            var normalizedData = data;
            if (normalizedData.Contains("?"))
            {
                normalizedData = normalizedData.Substring(normalizedData.IndexOf('?') + 1);
            }
            foreach (var token in Regex.Split(normalizedData, "&"))
            {
                var pair = Regex.Split(token, "=");
                if (pair.Length >= 2)
                {
                    result.Add(pair[0], pair[1]);
                    continue;
                }

                if (string.IsNullOrEmpty(pair[0]))
                {
                    continue;
                }
                result.Add(pair[0], string.Empty);
            }
            return result;
        }

        internal class HttpValueCollection : NameValueCollection
        {
            public override string ToString()
            {
                var list = new List<string>();
                var keys = AllKeys;
                foreach (var key in keys)
                {
                    list.Add($"{key}={this[key]}");
                }
                return string.Join("&", list);
            }
        }
    }
}
