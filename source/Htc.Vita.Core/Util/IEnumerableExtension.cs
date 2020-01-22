using System.Collections.Generic;
using Htc.Vita.Core.Json;

namespace Htc.Vita.Core.Util
{
    /// <summary>A helper class to provide some extension method to handle IEnumerable easily.</summary>
    public static class IEnumerableExtension
    {
        /// <summary>Converts to JSON Array with bool element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<bool> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }

        /// <summary>Converts to JSON Array with double element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<double> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }

        /// <summary>Converts to JSON Array with float element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<float> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }

        /// <summary>Converts to JSON Array with int element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<int> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }

        /// <summary>Converts to JSON Array with long element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<long> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }

        /// <summary>Converts to JSON Array with string element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<string> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }

        /// <summary>Converts to JSON Array with JsonArray element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<JsonArray> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }

        /// <summary>Converts to JSON Array with string element.</summary>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static JsonArray ToJsonArray(this IEnumerable<JsonObject> data)
        {
            if (data == null)
            {
                return null;
            }

            var result = JsonFactory.GetInstance().CreateJsonArray();
            foreach (var item in data)
            {
                result.Append(item);
            }
            return result;
        }
    }
}
