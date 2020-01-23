using System.Collections.Generic;
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
