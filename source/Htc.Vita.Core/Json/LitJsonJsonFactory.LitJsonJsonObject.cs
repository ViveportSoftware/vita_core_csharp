using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Htc.Vita.Core.Log;
using LitJson;

namespace Htc.Vita.Core.Json
{
    public partial class LitJsonJsonFactory
    {
        public class LitJsonJsonObject : JsonObject
        {
            private readonly JsonData _jsonData;
            private readonly Logger _logger;

            public LitJsonJsonObject(JsonData jsonData)
            {
                _jsonData = jsonData;
                _logger = Logger.GetInstance();
            }

            public JsonData GetInnerInstance()
            {
                return _jsonData;
            }

            protected override ICollection<string> OnAllKeys()
            {
                return _jsonData?.Keys;
            }

            protected override bool OnHasKey(string key)
            {
                if (_jsonData == null)
                {
                    return false;
                }
                return _jsonData.Keys.Any(key.Equals);
            }

            protected override bool OnParseBool(string key, bool defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null)
                {
                    return result;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data.IsBoolean)
                        {
                            result = (bool)data;
                        }
                        else if (data.IsString)
                        {
                            result = Util.Convert.ToBool((string)data);
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse bool value by key: " + key);
                }
                return result;
            }

            protected override double OnParseDouble(string key, double defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null)
                {
                    return result;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data.IsDouble)
                        {
                            result = (double)data;
                        }
                        else if (data.IsString)
                        {
                            result = Util.Convert.ToDouble((string)data);
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse double value by key: " + key);
                }
                return result;
            }

            protected override float OnParseFloat(string key, float defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null)
                {
                    return result;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data.IsDouble)
                        {
                            result = (float)(double)data;
                        }
                        else if (data.IsString)
                        {
                            result = (float)Util.Convert.ToDouble((string)data);
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse float value by key: " + key);
                }
                return result;
            }

            protected override int OnParseInt(string key, int defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null)
                {
                    return result;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data.IsInt)
                        {
                            result = (int)data;
                        }
                        else if (data.IsString)
                        {
                            result = Util.Convert.ToInt32((string)data);
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse int value by key: " + key);
                }
                return result;
            }

            protected override long OnParseLong(string key, long defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null)
                {
                    return result;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data.IsLong)
                        {
                            result = (long)data;
                        }
                        else if (data.IsString)
                        {
                            result = Util.Convert.ToInt64((string)data);
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse long value by key: " + key);
                }
                return result;
            }

            protected override string OnParseString(string key, string defaultValue)
            {
                var result = defaultValue;
                if (_jsonData == null)
                {
                    return result;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data.IsString)
                        {
                            result = (string)data;
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse string value by key: " + key);
                }
                return result;
            }

            protected override JsonArray OnParseJsonArray(string key)
            {
                if (_jsonData == null)
                {
                    return null;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data != null && data.IsArray)
                        {
                            return new LitJsonJsonArray(data);
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse JsonArray by key: " + key);
                }
                return null;
            }

            protected override JsonObject OnParseJsonObject(string key)
            {
                if (_jsonData == null)
                {
                    return null;
                }
                try
                {
                    foreach (var k in _jsonData.Keys)
                    {
                        if (!k.Equals(key))
                        {
                            continue;
                        }
                        var data = _jsonData[k];
                        if (data != null && data.IsObject)
                        {
                            return new LitJsonJsonObject(data);
                        }
                    }
                }
                catch (Exception)
                {
                    _logger.Error("Can not parse JsonObject by key: " + key);
                }
                return null;
            }

            protected override JsonObject OnPutBool(string key, bool value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            protected override JsonObject OnPutDouble(string key, double value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            protected override JsonObject OnPutFloat(string key, float value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            protected override JsonObject OnPutInt(string key, int value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            protected override JsonObject OnPutLong(string key, long value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            protected override JsonObject OnPutString(string key, string value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            protected override JsonObject OnPutJsonArray(string key, JsonArray value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = ((LitJsonJsonArray)value).GetInnerInstance();
                return this;
            }

            protected override JsonObject OnPutJsonObject(string key, JsonObject value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = ((LitJsonJsonObject)value).GetInnerInstance();
                return this;
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
