using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Htc.Vita.Core.Json;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class DictionaryExtension.
    /// </summary>
    public static class DictionaryExtension
    {
        /// <summary>
        /// Applies the value if it is not null and not white space.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>Dictionary&lt;System.String, System.String&gt;.</returns>
        public static Dictionary<string, string> ApplyIfNotNullAndNotWhiteSpace(
                this Dictionary<string, string> data,
                string key,
                string value)
        {
            if (data == null)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(value))
            {
                return data;
            }

            data[key] = value;
            return data;
        }

        /// <summary>
        /// Parses the value to string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public static string ParseString(
                this Dictionary<string, string> data,
                string key)
        {
            return ParseString(
                    data,
                    key,
                    null
            );
        }

        /// <summary>
        /// Parses the value to string.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public static string ParseString(
                this Dictionary<string, string> data,
                string key,
                string defaultValue)
        {
            if (data == null)
            {
                return defaultValue;
            }

            if (!data.ContainsKey(key))
            {
                return defaultValue;
            }

            return data[key];
        }

        /// <summary>
        /// Parses the value to URI.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns>Uri.</returns>
        public static Uri ParseUri(
                this Dictionary<string, object> data,
                string key)
        {
            return ParseUri(
                    data,
                    key,
                    null
            );
        }

        /// <summary>
        /// Parses the value to URI.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Uri.</returns>
        public static Uri ParseUri(
                this Dictionary<string, object> data,
                string key,
                Uri defaultValue)
        {
            if (data == null)
            {
                return defaultValue;
            }

            if (!data.ContainsKey(key))
            {
                return defaultValue;
            }

            var result = data[key] as Uri;
            if (result != null)
            {
                return result;
            }

            var item = data[key] as string;
            return item.ToUri() ?? defaultValue;
        }

        /// <summary>
        /// Parses the value to URI.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <returns>Uri.</returns>
        public static Uri ParseUri(
                this Dictionary<string, string> data,
                string key)
        {
            return ParseUri(
                    data,
                    key,
                    null
            );
        }

        /// <summary>
        /// Parses the value to URI.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>Uri.</returns>
        public static Uri ParseUri(
                this Dictionary<string, string> data,
                string key,
                Uri defaultValue)
        {
            if (data == null)
            {
                return defaultValue;
            }

            if (!data.ContainsKey(key))
            {
                return defaultValue;
            }

            return data[key].ToUri();
        }

        /// <summary>
        /// Converts to encoded URI query parameters.
        /// </summary>
        /// <param name="queryParams">The query parameters.</param>
        /// <returns>System.String.</returns>
        public static string ToEncodedUriQueryParameters(this Dictionary<string, string> queryParams)
        {
            if (queryParams == null)
            {
                return null;
            }

            var stringBuilder = new StringBuilder();
            var isFirst = true;
            foreach (var key in queryParams.Keys)
            {
                var param = queryParams[key];
                if (string.IsNullOrEmpty(param))
                {
                    continue;
                }

                if (!isFirst)
                {
                    stringBuilder.Append('&');
                }

                stringBuilder.Append(WebUtility.UrlEncode(key))
                        .Append('=')
                        .Append(WebUtility.UrlEncode(param));
                isFirst = false;
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts to JsonObject with bool properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, bool> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }

        /// <summary>
        /// Converts to JsonObject with double properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, double> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }

        /// <summary>
        /// Converts to JsonObject with float properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, float> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }

        /// <summary>
        /// Converts to JsonObject with int properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, int> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }

        /// <summary>
        /// Converts to JsonObject with long properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, long> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }

        /// <summary>
        /// Converts to JsonObject with string properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, string> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }

        /// <summary>
        /// Converts to JsonObject with JsonArray properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, JsonArray> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }

        /// <summary>
        /// Converts to JsonObject with JsonObject properties.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <returns>JsonObject.</returns>
        public static JsonObject ToJsonObject(this Dictionary<string, JsonObject> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonObject();
            foreach (var key in data.Keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                {
                    continue;
                }

                result.Put(key, data[key]);
            }
            return result;
        }
    }
}
