using System;
using System.Text;
using Htc.Vita.Core.Json.LitJson;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    public partial class LitJsonJsonFactory
    {
        public class LitJsonJsonArray : JsonArray
        {
            private readonly JsonData _jsonData;

            public LitJsonJsonArray(JsonData jsonData)
            {
                _jsonData = jsonData;
            }

            public JsonData GetInnerInstance()
            {
                return _jsonData;
            }

            protected override JsonArray OnAppendBool(bool value)
            {
                _jsonData?.Add(value);
                return this;
            }

            protected override JsonArray OnAppendDouble(double value)
            {
                _jsonData?.Add(value);
                return this;
            }

            protected override JsonArray OnAppendFloat(float value)
            {
                _jsonData?.Add((double)value);
                return this;
            }

            protected override JsonArray OnAppendInt(int value)
            {
                _jsonData?.Add(value);
                return this;
            }

            protected override JsonArray OnAppendLong(long value)
            {
                _jsonData?.Add(value);
                return this;
            }

            protected override JsonArray OnAppendString(string value)
            {
                _jsonData?.Add(value);
                return this;
            }

            protected override JsonArray OnAppendJsonArray(JsonArray value)
            {
                if (value == null)
                {
                    return this;
                }
                _jsonData?.Add(((LitJsonJsonArray)value).GetInnerInstance());
                return this;
            }

            protected override JsonArray OnAppendJsonObject(JsonObject value)
            {
                if (value == null)
                {
                    return this;
                }
                _jsonData?.Add(((LitJsonJsonObject)value).GetInnerInstance());
                return this;
            }

            protected override JsonArray OnInsertBool(int index, bool value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add(value);
                }
                else
                {
                    _jsonData.Add(value);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = new JsonData(value);
                }
                return this;
            }

            protected override JsonArray OnInsertDouble(int index, double value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add(value);
                }
                else
                {
                    _jsonData.Add(value);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = new JsonData(value);
                }
                return this;
            }

            protected override JsonArray OnInsertFloat(int index, float value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add((double)value);
                }
                else
                {
                    _jsonData.Add((double)value);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = new JsonData(value);
                }
                return this;
            }

            protected override JsonArray OnInsertInt(int index, int value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add(value);
                }
                else
                {
                    _jsonData.Add(value);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = new JsonData(value);
                }
                return this;
            }

            protected override JsonArray OnInsertLong(int index, long value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add(value);
                }
                else
                {
                    _jsonData.Add(value);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = new JsonData(value);
                }
                return this;
            }

            protected override JsonArray OnInsertString(int index, string value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add(value);
                }
                else
                {
                    _jsonData.Add(value);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = value;
                }
                return this;
            }

            protected override JsonArray OnInsertJsonArray(int index, JsonArray value)
            {
                if (_jsonData == null || value == null)
                {
                    return this;
                }
                var data = ((LitJsonJsonArray)value).GetInnerInstance();
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add(data);
                }
                else
                {
                    _jsonData.Add(data);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = data;
                }
                return this;
            }

            protected override JsonArray OnInsertJsonObject(int index, JsonObject value)
            {
                if (_jsonData == null || value == null)
                {
                    return this;
                }
                var data = ((LitJsonJsonObject)value).GetInnerInstance();
                if (_jsonData.Count <= index)
                {
                    _jsonData.Add(data);
                }
                else
                {
                    _jsonData.Add(data);
                    var count = _jsonData.Count;
                    for (var i = count - 1; i > index; i--)
                    {
                        _jsonData[i] = _jsonData[i - 1];
                    }
                    _jsonData[index] = data;
                }
                return this;
            }

            protected override bool OnParseBool(int index, bool defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return result;
                }
                try
                {
                    var data = _jsonData[index];
                    if (data.IsBoolean)
                    {
                        result = (bool)data;
                    }
                    else if (data.IsString)
                    {
                        result = Util.Convert.ToBool((string)data);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse bool value by index: " + index);
                }
                return result;
            }

            protected override double OnParseDouble(int index, double defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return result;
                }
                try
                {
                    var data = _jsonData[index];
                    if (data.IsInt)
                    {
                        result = Convert.ToDouble((int)data);
                    }
                    if (data.IsLong)
                    {
                        result = Convert.ToDouble((long)data);
                    }
                    if (data.IsDouble)
                    {
                        result = (double)data;
                    }
                    else if (data.IsString)
                    {
                        result = Util.Convert.ToDouble((string)data);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse double value by index: " + index);
                }
                return result;
            }

            protected override float OnParseFloat(int index, float defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return result;
                }
                try
                {
                    var data = _jsonData[index];
                    if (data.IsInt)
                    {
                        result = (float)Convert.ToDouble((int)data);
                    }
                    if (data.IsLong)
                    {
                        result = (float)Convert.ToDouble((long)data);
                    }
                    if (data.IsDouble)
                    {
                        result = (float)(double)data;
                    }
                    else if (data.IsString)
                    {
                        result = (float)Util.Convert.ToDouble((string)data);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse float value by index: " + index);
                }
                return result;
            }

            protected override int OnParseInt(int index, int defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return result;
                }
                try
                {
                    var data = _jsonData[index];
                    if (data.IsInt || data.IsLong)
                    {
                        result = (int)data;
                    }
                    else if (data.IsString)
                    {
                        result = Util.Convert.ToInt32((string)data);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse int value by index: " + index);
                }
                return result;
            }

            protected override long OnParseLong(int index, long defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return result;
                }
                try
                {
                    var data = _jsonData[index];
                    if (data.IsInt || data.IsLong)
                    {
                        result = (long)data;
                    }
                    else if (data.IsString)
                    {
                        result = Util.Convert.ToInt64((string)data);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse long value by index: " + index);
                }
                return result;
            }

            protected override string OnParseString(int index, string defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return result;
                }
                try
                {
                    result = (string)_jsonData[index];
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse string value by index: " + index);
                }
                return result;
            }

            protected override JsonArray OnParseJsonArray(int index)
            {
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return null;
                }
                try
                {
                    var data = _jsonData[index];
                    if (data != null && data.IsArray)
                    {
                        return new LitJsonJsonArray(data);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse JsonArray by index: " + index);
                }
                return null;
            }

            protected override JsonObject OnParseJsonObject(int index)
            {
                if (_jsonData == null || _jsonData.Count <= index)
                {
                    return null;
                }
                try
                {
                    var data = _jsonData[index];
                    if (data != null && data.IsObject)
                    {
                        return new LitJsonJsonObject(data);
                    }
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonArray)).Error("Can not parse JsonObject by index: " + index);
                }
                return null;
            }

            protected override int OnSize()
            {
                if (_jsonData != null && _jsonData.IsArray)
                {
                    return _jsonData.Count;
                }
                return 0;
            }

            protected override string OnToPrettyString()
            {
                var builder = new StringBuilder();
                var writer = new JsonWriter(builder)
                {
                        PrettyPrint = true,
                        IndentValue = 2
                };
                JsonMapper.ToJson(_jsonData, writer);
                return builder.ToString();
            }

            public override string ToString()
            {
                if (_jsonData != null)
                {
                    return _jsonData.ToJson();
                }
                return "";
            }
        }
    }
}
