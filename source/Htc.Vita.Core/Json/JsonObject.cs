using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    /// <summary>
    /// Class JsonObject.
    /// </summary>
    public abstract class JsonObject
    {
        /// <summary>
        /// Gets all the keys in JsonObject.
        /// </summary>
        /// <returns>ICollection&lt;System.String&gt;.</returns>
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

        /// <summary>
        /// Determines whether JsonObject has the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if JsonObject has the specified key ; otherwise, <c>false</c>.</returns>
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

        /// <summary>
        /// Parses the bool value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Boolean.</returns>
        public bool ParseBool(string key)
        {
            return ParseBool(key, false);
        }

        /// <summary>
        /// Parses the bool value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Boolean.</returns>
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

        /// <summary>
        /// Parses the bool if key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Boolean.</returns>
        public bool ParseBoolIfKeyExist(string key)
        {
            return ParseBoolIfKeyExist(key, false);
        }

        /// <summary>
        /// Parses the bool if key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Boolean.</returns>
        public bool ParseBoolIfKeyExist(string key, bool defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }
            return ParseBool(key, defaultValue);
        }

        /// <summary>
        /// Parses the double value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Double.</returns>
        public double ParseDouble(string key)
        {
            return ParseDouble(key, 0.0D);
        }

        /// <summary>
        /// Parses the double value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
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

        /// <summary>
        /// Parses the double value if key exist.
        /// </summary>
        /// <param name="key">The key if key exist.</param>
        /// <returns>System.Double.</returns>
        public double ParseDoubleIfKeyExists(string key)
        {
            return ParseDoubleIfKeyExists(key, 0.0D);
        }

        /// <summary>
        /// Parses the double value if key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public double ParseDoubleIfKeyExists(string key, double defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }
            return ParseDouble(key, defaultValue);
        }

        /// <summary>
        /// Parses the float value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Single.</returns>
        public float ParseFloat(string key)
        {
            return ParseFloat(key, 0.0F);
        }

        /// <summary>
        /// Parses the float value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
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

        /// <summary>
        /// Parses the float value if key exist.
        /// </summary>
        /// <param name="key">The key if key exist.</param>
        /// <returns>System.Single.</returns>
        public float ParseFloatIfKeyExists(string key)
        {
            return ParseFloatIfKeyExists(key, 0.0F);
        }

        /// <summary>
        /// Parses the float value if key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
        public float ParseFloatIfKeyExists(string key, float defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }
            return ParseFloat(key, defaultValue);
        }

        /// <summary>
        /// Parses the int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Int32.</returns>
        public int ParseInt(string key)
        {
            return ParseInt(key, 0);
        }

        /// <summary>
        /// Parses the int value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
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

        /// <summary>
        /// Parses the int value if key exist.
        /// </summary>
        /// <param name="key">The key if key exist.</param>
        /// <returns>System.Int32.</returns>
        public int ParseIntIfKeyExists(string key)
        {
            return ParseIntIfKeyExists(key, 0);
        }

        /// <summary>
        /// Parses the int value if key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public int ParseIntIfKeyExists(string key, int defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }
            return ParseInt(key, defaultValue);
        }

        /// <summary>
        /// Parses the long value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.Int64.</returns>
        public long ParseLong(string key)
        {
            return ParseLong(key, 0L);
        }

        /// <summary>
        /// Parses the long value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
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

        /// <summary>
        /// Parses the long value if key exist.
        /// </summary>
        /// <param name="key">The key if key exist.</param>
        /// <returns>System.Int64.</returns>
        public long ParseLongIfKeyExists(string key)
        {
            return ParseLongIfKeyExists(key, 0L);
        }

        /// <summary>
        /// Parses the long value if key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public long ParseLongIfKeyExists(string key, long defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }
            return ParseLong(key, defaultValue);
        }

        /// <summary>
        /// Parses the string value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public string ParseString(string key)
        {
            return ParseString(key, null);
        }

        /// <summary>
        /// Parses the string value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Parses the string value if key exist.
        /// </summary>
        /// <param name="key">The key if key exist.</param>
        /// <returns>System.String.</returns>
        public string ParseStringIfKeyExists(string key)
        {
            return ParseStringIfKeyExists(key, null);
        }

        /// <summary>
        /// Parses the string value if key exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string ParseStringIfKeyExists(string key, string defaultValue)
        {
            if (!HasKey(key))
            {
                return defaultValue;
            }
            return ParseString(key, defaultValue);
        }

        /// <summary>
        /// Parses the URI value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Uri.</returns>
        public Uri ParseUri(string key)
        {
            var data = ParseString(key);
            if (string.IsNullOrWhiteSpace(data))
            {
                return null;
            }

            Uri result = null;
            try
            {
                result = new Uri(data);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonObject)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the URI value if key exists.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Uri.</returns>
        public Uri ParseUriIfKeyExists(string key)
        {
            if (!HasKey(key))
            {
                return null;
            }
            return ParseUri(key);
        }

        /// <summary>
        /// Parses the JsonArray value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>JsonArray.</returns>
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

        /// <summary>
        /// Parses the JsonArray value if key exist.
        /// </summary>
        /// <param name="key">The key if key exist.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray ParseJsonArrayIfKeyExists(string key)
        {
            if (!HasKey(key))
            {
                return null;
            }
            return ParseJsonArray(key);
        }

        /// <summary>
        /// Parses the JsonObject value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Parses the JsonObject value if key exist.
        /// </summary>
        /// <param name="key">The key if key exist.</param>
        /// <returns>JsonArray.</returns>
        public JsonObject ParseJsonObjectIfKeyExists(string key)
        {
            if (!HasKey(key))
            {
                return null;
            }
            return ParseJsonObject(key);
        }

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
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

        /// <summary>
        /// Puts the value if it is not null.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject PutIfNotNull(string key, string value)
        {
            if (value == null)
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>
        /// Puts the value if it is not null.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject PutIfNotNull(string key, JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>
        /// Puts the value if it is not null.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject PutIfNotNull(string key, JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>
        /// Puts the value if it is not null and not white space.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject PutIfNotNullAndNotWhiteSpace(string key, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>
        /// Puts the value if it is not null and not empty.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject PutIfNotNullAndNotEmpty(string key, JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            if (value.Size() <= 0)
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>
        /// Puts the value if it is not null and not empty.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject PutIfNotNullAndNotEmpty(string key, JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            if (value.AllKeys().Count <= 0)
            {
                return this;
            }
            return Put(key, value);
        }

        /// <summary>
        /// Converts to pretty-print string.
        /// </summary>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Called when [getting all the keys in JsonObject].
        /// </summary>
        /// <returns>ICollection&lt;System.String&gt;.</returns>
        protected abstract ICollection<string> OnAllKeys();
        /// <summary>
        /// Called when [determining whether JsonObject has the specified key].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if JsonObject has the specified key ; otherwise, <c>false</c>.</returns>
        protected abstract bool OnHasKey(string key);
        /// <summary>
        /// Called when [parsing the bool value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Boolean.</returns>
        protected abstract bool OnParseBool(string key, bool defaultValue);
        /// <summary>
        /// Called when [parsing the double value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        protected abstract double OnParseDouble(string key, double defaultValue);
        /// <summary>
        /// Called when [parsing the float value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
        protected abstract float OnParseFloat(string key, float defaultValue);
        /// <summary>
        /// Called when [parsing the int value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        protected abstract int OnParseInt(string key, int defaultValue);
        /// <summary>
        /// Called when [parsing the long value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        protected abstract long OnParseLong(string key, long defaultValue);
        /// <summary>
        /// Called when [parsing the string value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnParseString(string key, string defaultValue);
        /// <summary>
        /// Called when [parsing the JsonArray value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnParseJsonArray(string key);
        /// <summary>
        /// Called when [parsing the JsonObject value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnParseJsonObject(string key);
        /// <summary>
        /// Called when [putting the bool value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutBool(string key, bool value);
        /// <summary>
        /// Called when [putting the double value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutDouble(string key, double value);
        /// <summary>
        /// Called when [putting the float value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutFloat(string key, float value);
        /// <summary>
        /// Called when [putting the int value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutInt(string key, int value);
        /// <summary>
        /// Called when [putting the long value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutLong(string key, long value);
        /// <summary>
        /// Called when [putting the string value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutString(string key, string value);
        /// <summary>
        /// Called when [putting the JsonArray value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutJsonArray(string key, JsonArray value);
        /// <summary>
        /// Called when [putting the JsonObject value].
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnPutJsonObject(string key, JsonObject value);
        /// <summary>
        /// Called when [converting to pretty-print string].
        /// </summary>
        /// <returns>System.String.</returns>
        protected abstract string OnToPrettyString();
    }
}
