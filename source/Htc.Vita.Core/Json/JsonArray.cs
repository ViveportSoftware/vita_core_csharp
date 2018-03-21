using System;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    public abstract class JsonArray
    {
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

        public bool ParseBool(int index)
        {
            return ParseBool(index, false);
        }

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

        public double ParseDouble(int index)
        {
            return ParseDouble(index, 0.0D);
        }

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

        public float ParseFloat(int index)
        {
            return ParseFloat(index, 0.0F);
        }

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

        public int ParseInt(int index)
        {
            return ParseInt(index, 0);
        }

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

        public long ParseLong(int index)
        {
            return ParseLong(index, 0L);
        }

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

        public string ParseString(int index)
        {
            return ParseString(index, null);
        }

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
            Logger.GetInstance(typeof(JsonArray)).Fatal("Size abnormal: " + result);
            return 0;
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
                Logger.GetInstance(typeof(JsonArray)).Error(e.ToString());
            }
            return result;
        }

        protected abstract JsonArray OnAppendBool(bool value);
        protected abstract JsonArray OnAppendDouble(double value);
        protected abstract JsonArray OnAppendFloat(float value);
        protected abstract JsonArray OnAppendInt(int value);
        protected abstract JsonArray OnAppendLong(long value);
        protected abstract JsonArray OnAppendString(string value);
        protected abstract JsonArray OnAppendJsonArray(JsonArray value);
        protected abstract JsonArray OnAppendJsonObject(JsonObject value);
        protected abstract JsonArray OnInsertBool(int index, bool value);
        protected abstract JsonArray OnInsertDouble(int index, double value);
        protected abstract JsonArray OnInsertFloat(int index, float value);
        protected abstract JsonArray OnInsertInt(int index, int value);
        protected abstract JsonArray OnInsertLong(int index, long value);
        protected abstract JsonArray OnInsertString(int index, string value);
        protected abstract JsonArray OnInsertJsonArray(int index, JsonArray value);
        protected abstract JsonArray OnInsertJsonObject(int index, JsonObject value);
        protected abstract bool OnParseBool(int index, bool defaultValue);
        protected abstract double OnParseDouble(int index, double defaultValue);
        protected abstract float OnParseFloat(int index, float defaultValue);
        protected abstract int OnParseInt(int index, int defaultValue);
        protected abstract long OnParseLong(int index, long defaultValue);
        protected abstract string OnParseString(int index, string defaultValue);
        protected abstract JsonArray OnParseJsonArray(int index);
        protected abstract JsonObject OnParseJsonObject(int index);
        protected abstract int OnSize();
        protected abstract string OnToPrettyString();
    }
}
