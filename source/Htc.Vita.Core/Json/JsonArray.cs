using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    /// <summary>
    /// Class JsonArray.
    /// </summary>
    public abstract class JsonArray
    {
        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(bool value)
        {
            var result = this;
            try
            {
                result = OnAppendBool(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(double value)
        {
            var result = this;
            try
            {
                result = OnAppendDouble(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(float value)
        {
            var result = this;
            try
            {
                result = OnAppendFloat(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(int value)
        {
            var result = this;
            try
            {
                result = OnAppendInt(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(long value)
        {
            var result = this;
            try
            {
                result = OnAppendLong(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(string value)
        {
            var result = this;
            try
            {
                result = OnAppendString(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(JsonArray value)
        {
            var result = this;
            try
            {
                result = OnAppendJsonArray(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Append(JsonObject value)
        {
            var result = this;
            try
            {
                result = OnAppendJsonObject(value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Appends the specific value if it is not null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray AppendIfNotNull(string value)
        {
            if (value == null)
            {
                return this;
            }
            return Append(value);
        }

        /// <summary>
        /// Appends the specific value if it is not null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray AppendIfNotNull(JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            return Append(value);
        }

        /// <summary>
        /// Appends the specific value if it is not null.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray AppendIfNotNull(JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            return Append(value);
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, bool value)
        {
            var result = this;
            try
            {
                result = OnInsertBool(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, double value)
        {
            var result = this;
            try
            {
                result = OnInsertDouble(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, float value)
        {
            var result = this;
            try
            {
                result = OnInsertFloat(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, int value)
        {
            var result = this;
            try
            {
                result = OnInsertInt(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, long value)
        {
            var result = this;
            try
            {
                result = OnInsertLong(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, string value)
        {
            var result = this;
            try
            {
                result = OnInsertString(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, JsonArray value)
        {
            var result = this;
            try
            {
                result = OnInsertJsonArray(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray Insert(int index, JsonObject value)
        {
            var result = this;
            try
            {
                result = OnInsertJsonObject(index, value);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Inserts the specific value by index if it is not null.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray InsertIfNotNull(int index, string value)
        {
            if (value == null)
            {
                return this;
            }
            return Insert(index, value);
        }

        /// <summary>
        /// Inserts the specific value by index if it is not null.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray InsertIfNotNull(int index, JsonArray value)
        {
            if (value == null)
            {
                return this;
            }
            return Insert(index, value);
        }

        /// <summary>
        /// Inserts the specific value by index if it is not null.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray InsertIfNotNull(int index, JsonObject value)
        {
            if (value == null)
            {
                return this;
            }
            return Insert(index, value);
        }

        /// <summary>
        /// Parses the bool value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Boolean.</returns>
        public bool ParseBool(int index)
        {
            return ParseBool(index, false);
        }

        /// <summary>
        /// Parses the bool value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Boolean.</returns>
        public bool ParseBool(int index, bool defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseBool(index, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the double value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Double.</returns>
        public double ParseDouble(int index)
        {
            return ParseDouble(index, 0.0D);
        }

        /// <summary>
        /// Parses the double value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public double ParseDouble(int index, double defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseDouble(index, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the float value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Single.</returns>
        public float ParseFloat(int index)
        {
            return ParseFloat(index, 0.0F);
        }

        /// <summary>
        /// Parses the float value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
        public float ParseFloat(int index, float defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseFloat(index, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the int value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Int32.</returns>
        public int ParseInt(int index)
        {
            return ParseInt(index, 0);
        }

        /// <summary>
        /// Parses the int value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public int ParseInt(int index, int defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseInt(index, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the long value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.Int64.</returns>
        public long ParseLong(int index)
        {
            return ParseLong(index, 0L);
        }

        /// <summary>
        /// Parses the long value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public long ParseLong(int index, long defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseLong(index, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the string value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>System.String.</returns>
        public string ParseString(int index)
        {
            return ParseString(index, null);
        }

        /// <summary>
        /// Parses the string value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string ParseString(int index, string defaultValue)
        {
            var result = defaultValue;
            try
            {
                result = OnParseString(index, defaultValue);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the URI value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>Uri.</returns>
        public Uri ParseUri(int index)
        {
            var data = ParseString(index);
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
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the JsonArray value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>JsonArray.</returns>
        public JsonArray ParseJsonArray(int index)
        {
            JsonArray result = null;
            try
            {
                result = OnParseJsonArray(index);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Parses the JsonObject value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>JsonObject.</returns>
        public JsonObject ParseJsonObject(int index)
        {
            JsonObject result = null;
            try
            {
                result = OnParseJsonObject(index);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Gets the element size of this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        public int Size()
        {
            var result = 0;
            try
            {
                result = OnSize();
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            if (result >= 0)
            {
                return result;
            }
            Logger.GetInstance(typeof(JsonArray)).Fatal($"Size abnormal: {result}");
            return 0;
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
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        /// <summary>
        /// Called when appending the bool value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendBool(bool value);
        /// <summary>
        /// Called when appending the double value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendDouble(double value);
        /// <summary>
        /// Called when appending the float value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendFloat(float value);
        /// <summary>
        /// Called when appending the int value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendInt(int value);
        /// <summary>
        /// Called when appending the long value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendLong(long value);
        /// <summary>
        /// Called when appending the string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendString(string value);
        /// <summary>
        /// Called when appending the JsonArray value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendJsonArray(JsonArray value);
        /// <summary>
        /// Called when appending the JsonObject value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnAppendJsonObject(JsonObject value);
        /// <summary>
        /// Called when inserting the specific bool value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertBool(int index, bool value);
        /// <summary>
        /// Called when inserting the specific double value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertDouble(int index, double value);
        /// <summary>
        /// Called when inserting the specific float value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertFloat(int index, float value);
        /// <summary>
        /// Called when inserting the specific int value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertInt(int index, int value);
        /// <summary>
        /// Called when inserting the specific long value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertLong(int index, long value);
        /// <summary>
        /// Called when inserting the specific string value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertString(int index, string value);
        /// <summary>
        /// Called when inserting the specific JsonArray value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertJsonArray(int index, JsonArray value);
        /// <summary>
        /// Called when inserting the specific JsonObject value by index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="value">The value.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnInsertJsonObject(int index, JsonObject value);
        /// <summary>
        /// Called when parsing the bool value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">if set to <c>true</c> [default value].</param>
        /// <returns>System.Boolean.</returns>
        protected abstract bool OnParseBool(int index, bool defaultValue);
        /// <summary>
        /// Called when parsing the double value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        protected abstract double OnParseDouble(int index, double defaultValue);
        /// <summary>
        /// Called when parsing the float value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Single.</returns>
        protected abstract float OnParseFloat(int index, float defaultValue);
        /// <summary>
        /// Called when parsing the int value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        protected abstract int OnParseInt(int index, int defaultValue);
        /// <summary>
        /// Called when parsing the long value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        protected abstract long OnParseLong(int index, long defaultValue);
        /// <summary>
        /// Called when parsing the string value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        protected abstract string OnParseString(int index, string defaultValue);
        /// <summary>
        /// Called when parsing the JsonArray value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>JsonArray.</returns>
        protected abstract JsonArray OnParseJsonArray(int index);
        /// <summary>
        /// Called when parsing the JsonObject value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>JsonObject.</returns>
        protected abstract JsonObject OnParseJsonObject(int index);
        /// <summary>
        /// Called when getting the element size of this instance.
        /// </summary>
        /// <returns>System.Int32.</returns>
        protected abstract int OnSize();
        /// <summary>
        /// Called when converting to pretty-print string.
        /// </summary>
        /// <returns>System.String.</returns>
        protected abstract string OnToPrettyString();
    }
}
