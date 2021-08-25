using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Text;
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

        /// <summary>
        /// Converts to encoded query string.
        /// </summary>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>System.String.</returns>
        public static string ToEncodedQueryString(Dictionary<string, string> queryParams)
        {
            var stringBuilder = new StringBuilder();
            var isFirst = true;
            foreach (var key in queryParams.Keys)
            {
                if (!isFirst)
                {
                    stringBuilder.Append("&");
                }

                var param = queryParams[key];
                if (string.IsNullOrEmpty(param))
                {
                    continue;
                }

                stringBuilder.Append(WebUtility.UrlEncode(key))
                        .Append("=")
                        .Append(WebUtility.UrlEncode(param));
                isFirst = false;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Class HttpValueCollection.
        /// Implements the <see cref="NameValueCollection" />
        /// </summary>
        /// <seealso cref="NameValueCollection" />
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
