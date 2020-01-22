using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    public abstract class JsonObject
    {
        public ICollection<string> AllKeys()
        {
            ICollection<string> result = null;
            try
            {
                result = OnAllKeys();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result ?? new List<string>();
        }

        public bool HasKey(string key)
        {
            var result = false;
            try
            {
                result = OnHasKey(key);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public bool ParseBool(string key)
        {
            return ParseBool(key, false);
        }

        public bool ParseBool(string key, bool defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseBool(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public double ParseDouble(string key)
        {
            return ParseDouble(key, 0.0D);
        }

        public double ParseDouble(string key, double defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseDouble(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public float ParseFloat(string key)
        {
            return ParseFloat(key, 0.0F);
        }

        public float ParseFloat(string key, float defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseFloat(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public int ParseInt(string key)
        {
            return ParseInt(key, 0);
        }

        public int ParseInt(string key, int defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseInt(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public long ParseLong(string key)
        {
            return ParseLong(key, 0L);
        }

        public long ParseLong(string key, long defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseLong(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public string ParseString(string key)
        {
            return ParseString(key, null);
        }

        public string ParseString(string key, string defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseString(key, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonArray ParseJsonArray(string key)
        {
            JsonArray result = null;
            try
            {
                result = OnParseJsonArray(key);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject ParseJsonObject(string key)
        {
            JsonObject result = null;
            try
            {
                result = OnParseJsonObject(key);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, bool value)
        {
            var result = this;
            try
            {
                result = OnPutBool(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, double value)
        {
            var result = this;
            try
            {
                result = OnPutDouble(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, float value)
        {
            var result = this;
            try
            {
                result = OnPutFloat(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, int value)
        {
            var result = this;
            try
            {
                result = OnPutInt(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, long value)
        {
            var result = this;
            try
            {
                result = OnPutLong(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, string value)
        {
            var result = this;
            try
            {
                result = OnPutString(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, JsonArray value)
        {
            var result = this;
            try
            {
                result = OnPutJsonArray(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        public JsonObject Put(string key, JsonObject value)
        {
            var result = this;
            try
            {
                result = OnPutJsonObject(key, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>Puts the value if it is not null.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public JsonObject PutIfNotNull(string key, string value)
        {
            if (value == null)
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>Puts the value if it is not null.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public JsonObject PutIfNotNull(string key, JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>Puts the value if it is not null.</summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public JsonObject PutIfNotNull(string key, JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            return Put(key, value);
        }

        public string ToPrettyString()
        {
            var result = string.Empty;
            try
            {
                result = OnToPrettyString();
                result = result?.Trim() ?? string.Empty;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        protected abstract ICollection<string> OnAllKeys();
        protected abstract bool OnHasKey(string key);
        protected abstract bool OnParseBool(string key, bool defaultValue);
        protected abstract double OnParseDouble(string key, double defaultValue);
        protected abstract float OnParseFloat(string key, float defaultValue);
        protected abstract int OnParseInt(string key, int defaultValue);
        protected abstract long OnParseLong(string key, long defaultValue);
        protected abstract string OnParseString(string key, string defaultValue);
        protected abstract JsonArray OnParseJsonArray(string key);
        protected abstract JsonObject OnParseJsonObject(string key);
        protected abstract JsonObject OnPutBool(string key, bool value);
        protected abstract JsonObject OnPutDouble(string key, double value);
        protected abstract JsonObject OnPutFloat(string key, float value);
        protected abstract JsonObject OnPutInt(string key, int value);
        protected abstract JsonObject OnPutLong(string key, long value);
        protected abstract JsonObject OnPutString(string key, string value);
        protected abstract JsonObject OnPutJsonArray(string key, JsonArray value);
        protected abstract JsonObject OnPutJsonObject(string key, JsonObject value);
        protected abstract string OnToPrettyString();
    }
}
