using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    public abstract class JsonFactory
    {
        private static Dictionary<string, JsonFactory> Instances { get; } = new Dictionary<string, JsonFactory>();
        private static Type _defaultType = typeof(LitJsonJsonFactory);

        private readonly Logger _logger;

        protected JsonFactory()
        {
            _logger = Logger.GetInstance();
        }

        public static void Register<T>() where T : JsonFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance().Info("Registered default json factory type to " + _defaultType);
        }

        public static JsonFactory GetInstance()
        {
            JsonFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error: " + e);
                Logger.GetInstance().Info("Initializing " + typeof(LitJsonJsonFactory).FullName + "...");
                instance = new LitJsonJsonFactory();
            }
            return instance;
        }

        private static JsonFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get json factory instance");
            }

            var key = type.FullName + "_";
            JsonFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (JsonFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + typeof(LitJsonJsonFactory).FullName + "...");
                instance = new LitJsonJsonFactory();
            }
            if (!Instances.ContainsKey(key))
            {
                Instances.Add(key, instance);
            }
            return instance;
        }

        public JsonArray CreateJsonArray()
        {
            return OnCreateJsonArray();
        }

        public JsonObject CreateJsonObject()
        {
            return OnCreateJsonObject();
        }

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
                _logger.Fatal("Deserializing object error: " + e);
            }
            return result;
        }

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
                _logger.Fatal("Serializing object error: " + e);
            }
            return result;
        }

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
                _logger.Fatal("Getting json array error: " + e);
            }
            return result;
        }

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
                _logger.Fatal("Getting json object error: " + e);
            }
            return result;
        }

        protected abstract JsonArray OnCreateJsonArray();
        protected abstract JsonObject OnCreateJsonObject();
        protected abstract T OnDeserializeObject<T>(string content);
        protected abstract JsonArray OnGetJsonArray(string content);
        protected abstract JsonObject OnGetJsonObject(string content);
        protected abstract string OnSerializeObject(object content);
    }
}
