using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    /// <summary>
    /// Class JsonFactory.
    /// </summary>
    public abstract class JsonFactory
    {
        private static Dictionary<string, JsonFactory> Instances { get; } = new Dictionary<string, JsonFactory>();

        private static readonly object InstancesLock = new object();

        private static Type _defaultType = typeof(LitJsonJsonFactory);

        /// <summary>
        /// Registers the instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>() where T : JsonFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance(typeof(JsonFactory)).Info("Registered default " + typeof(JsonFactory).Name + " type to " + _defaultType);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>JsonFactory.</returns>
        public static JsonFactory GetInstance()
        {
            JsonFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonFactory)).Fatal("Instance initialization error: " + e);
                Logger.GetInstance(typeof(JsonFactory)).Info("Initializing " + typeof(LitJsonJsonFactory).FullName + "...");
                instance = new LitJsonJsonFactory();
            }
            return instance;
        }

        private static JsonFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get " + typeof(JsonFactory).Name + " instance");
            }

            var key = type.FullName + "_";
            JsonFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(JsonFactory)).Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (JsonFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance(typeof(JsonFactory)).Info("Initializing " + typeof(LitJsonJsonFactory).FullName + "...");
                instance = new LitJsonJsonFactory();
            }
            lock (InstancesLock)
            {
                if (!Instances.ContainsKey(key))
                {
                    Instances.Add(key, instance);
                }
            }
            return instance;
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
                Logger.GetInstance(typeof(JsonFactory)).Fatal("Deserializing object error: " + e);
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
                Logger.GetInstance(typeof(JsonFactory)).Fatal("Serializing object error: " + e);
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
                Logger.GetInstance(typeof(JsonFactory)).Fatal("Getting json array error: " + e);
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
                Logger.GetInstance(typeof(JsonFactory)).Fatal("Getting json object error: " + e);
            }
            return result;
        }

        /// <summary>
        /// Called when [creating an empty JsonArray].
        /// </summary>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnCreateJsonArray();
        /// <summary>
        /// Called when [creating an empty JsonObject].
        /// </summary>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnCreateJsonObject();
        /// <summary>
        /// Called when [deserializing object].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content">The content.</param>
        /// <returns>T.</returns>
        protected abstract T OnDeserializeObject<T>(string content);
        /// <summary>
        /// Called when [getting the JsonArray].
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnGetJsonArray(string content);
        /// <summary>
        /// Called when [getting the JsonObject].
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnGetJsonObject(string content);
        /// <summary>
        /// Called when [serializing object].
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnSerializeObject(object content);
    }
}
