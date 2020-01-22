using System.Collections.Generic;
using Htc.Vita.Core.Json;

namespace Htc.Vita.Core.Util
{
    public static class DictionaryExtension
    {
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
