using System;
using System.Collections.Generic;
using System.Text;
using Htc.Vita.Core.Json.LitJson;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Json
{
    public partial class LitJsonJsonFactory
    {
        /// <summary>
        /// Class LitJsonJsonObject.
        /// Implements the <see cref="JsonObject" />
        /// </summary>
        /// <seealso cref="JsonObject" />
        public class LitJsonJsonObject : JsonObject
        {
            private readonly JsonData _jsonData;

            /// <summary>
            /// Initializes a new instance of the <see cref="LitJsonJsonObject"/> class.
            /// </summary>
            /// <param name="jsonData">The json data.</param>
            public LitJsonJsonObject(JsonData jsonData)
            {
                _jsonData = jsonData;
            }

            /// <summary>
            /// Gets the inner instance.
            /// </summary>
            /// <returns>JsonData.</returns>
            public JsonData GetInnerInstance()
            {
                return _jsonData;
            }

            /// <inheritdoc />
            protected override ICollection<string> OnAllKeys()
            {
                return _jsonData?.Keys;
            }

            /// <inheritdoc />
            protected override bool OnHasKey(string key)
            {
                if (_jsonData == null)
                {
                    return false;
                }
                return _jsonData.ContainsKey(key);
            }

            /// <inheritdoc />
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
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse bool value by key: " + key);
                }
                return result;
            }

            /// <inheritdoc />
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
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse double value by key: " + key);
                }
                return result;
            }

            /// <inheritdoc />
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
                }
                catch (Exception)
                {
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse float value by key: " + key);
                }
                return result;
            }

            /// <inheritdoc />
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
                        if (data.IsInt || data.IsLong)
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
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse int value by key: " + key);
                }
                return result;
            }

            /// <inheritdoc />
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
                        if (data.IsInt || data.IsLong)
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
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse long value by key: " + key);
                }
                return result;
            }

            /// <inheritdoc />
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
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse string value by key: " + key);
                }
                return result;
            }

            /// <inheritdoc />
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
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse JsonArray by key: " + key);
                }
                return null;
            }

            /// <inheritdoc />
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
                    Logger.GetInstance(typeof(LitJsonJsonObject)).Error("Can not parse JsonObject by key: " + key);
                }
                return null;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutBool(string key, bool value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutDouble(string key, double value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutFloat(string key, float value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutInt(string key, int value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutLong(string key, long value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutString(string key, string value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = value;
                return this;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutJsonArray(string key, JsonArray value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = ((LitJsonJsonArray)value).GetInnerInstance();
                return this;
            }

            /// <inheritdoc />
            protected override JsonObject OnPutJsonObject(string key, JsonObject value)
            {
                if (_jsonData == null)
                {
                    return this;
                }
                _jsonData[key] = ((LitJsonJsonObject)value).GetInnerInstance();
                return this;
            }

            /// <inheritdoc />
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

            /// <inheritdoc />
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
