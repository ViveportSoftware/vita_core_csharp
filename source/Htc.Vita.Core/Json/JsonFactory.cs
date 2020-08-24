using System;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Json
{
    /// <summary>
    /// Class JsonFactory.
    /// </summary>
    public abstract class JsonFactory
    {
        static JsonFactory()
        {
            TypeRegistry.RegisterDefault<JsonFactory, LitJsonJsonFactory>();
        }

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : JsonFactory, new()
        {
            TypeRegistry.Register<JsonFactory, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>JsonFactory.</returns>
        public static JsonFactory GetInstance()
        {
            return TypeRegistry.GetInstance<JsonFactory>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>JsonFactory.</returns>
        public static JsonFactory GetInstance<T>()
                where T : JsonFactory, new()
        {
            return TypeRegistry.GetInstance<JsonFactory, T>();
        }

        /// <summary>
        /// Creates an empty JsonArray.
        /// </summary>
        /// <returns>JsonArray.</returns>
        public JsonArray CreateJsonArray()
        {
            return OnCreateJsonArray();
        }

        /// <summary>
        /// Creates an empty JsonObject.
        /// </summary>
        /// <returns>JsonObject.</returns>
        public JsonObject CreateJsonObject()
        {
            return OnCreateJsonObject();
        }

        /// <summary>
        /// Deserializes the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <returns>T.</returns>
        public T DeserializeObject<T>(string content)
        {
            var result = default(T);
            if (string.IsNullOrWhiteSpace(content))
            {
                return result;
            }

            try
            {
                result = OnDeserializeObject<T>(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonFactory)).Fatal($"Deserializing object error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Serializes the object.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.String.</returns>
        public string SerializeObject(object content)
        {
            if (content == null)
            {
                return null;
            }

            string result = null;
            try
            {
                result = OnSerializeObject(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonFactory)).Fatal($"Serializing object error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Gets the JsonArray.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray GetJsonArray(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            JsonArray result = null;
            try
            {
                result = OnGetJsonArray(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonFactory)).Fatal($"Getting json array error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Gets the JsonObject.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject GetJsonObject(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return null;
            }

            JsonObject result = null;
            try
            {
                result = OnGetJsonObject(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonFactory)).Fatal($"Getting json object error: {e}");
            }
            return result;
        }

        /// <summary>
        /// Called when creating an empty JsonArray.
        /// </summary>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnCreateJsonArray();
        /// <summary>
        /// Called when creating an empty JsonObject.
        /// </summary>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnCreateJsonObject();
        /// <summary>
        /// Called when deserializing object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <returns>T.</returns>
        protected abstract T OnDeserializeObject<T>(string content);
        /// <summary>
        /// Called when getting the JsonArray.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnGetJsonArray(string content);
        /// <summary>
        /// Called when getting the JsonObject.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnGetJsonObject(string content);
        /// <summary>
        /// Called when serializing object.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnSerializeObject(object content);
    }
}
