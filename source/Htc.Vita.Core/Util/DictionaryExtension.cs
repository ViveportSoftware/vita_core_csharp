using System.Collections.Generic;
using Htc.Vita.Core.Json;

namespace Htc.Vita.Core.Util
{
    public static class DictionaryExtension
    {
        /// <summary>Applies the value if it is not null and not white space.</summary>
        /// <param name="data">The data.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
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
