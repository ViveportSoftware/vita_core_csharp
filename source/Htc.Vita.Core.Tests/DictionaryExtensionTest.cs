using System.Collections.Generic;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class DictionaryExtensionTest
    {
        [Fact]
        public static void Default_0_ToJsonObject_bool()
        {
            var map = new Dictionary<string, bool>
            {
                    ["a"] = true,
                    ["b"] = false,
                    ["c"] = true
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value;
                    var item2 = jsonObject.ParseBool(key);
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_0_ToJsonObject_double()
        {
            var map = new Dictionary<string, double>
            {
                    ["a"] = 1.1d,
                    ["b"] = 2.2d,
                    ["c"] = 3.3d
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value;
                    var item2 = jsonObject.ParseDouble(key);
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_0_ToJsonObject_float()
        {
            var map = new Dictionary<string, float>
            {
                    ["a"] = 1.1f,
                    ["b"] = 2.2f,
                    ["c"] = 3.3f
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value;
                    var item2 = jsonObject.ParseFloat(key);
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_0_ToJsonObject_int()
        {
            var map = new Dictionary<string, int>
            {
                    ["a"] = 1,
                    ["b"] = 2,
                    ["c"] = 3
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value;
                    var item2 = jsonObject.ParseInt(key);
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_0_ToJsonObject_long()
        {
            var map = new Dictionary<string, long>
            {
                    ["a"] = 1L,
                    ["b"] = 2L,
                    ["c"] = 3L
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value;
                    var item2 = jsonObject.ParseLong(key);
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_0_ToJsonObject_string()
        {
            var map = new Dictionary<string, string>
            {
                    ["a"] = "1",
                    ["b"] = "2",
                    ["c"] = "3"
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value;
                    var item2 = jsonObject.ParseString(key);
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_0_ToJsonObject_JsonArray()
        {
            var map = new Dictionary<string, JsonArray>
            {
                    ["a"] = JsonFactory.GetInstance().GetJsonArray("[]"),
                    ["b"] = JsonFactory.GetInstance().GetJsonArray("[1,2,3]"),
                    ["c"] = JsonFactory.GetInstance().GetJsonArray("[\"1\",\"2\"]")
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value.ToString();
                    var item2 = jsonObject.ParseJsonArray(key).ToString();
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_0_ToJsonObject_JsonObject()
        {
            var map = new Dictionary<string, JsonObject>
            {
                    ["a"] = JsonFactory.GetInstance().GetJsonObject("{}"),
                    ["b"] = JsonFactory.GetInstance().GetJsonObject("{\"b\":\"2\"}"),
                    ["c"] = JsonFactory.GetInstance().GetJsonObject("{\"c\":\"3\"}")
            };
            var jsonObject = map.ToJsonObject();
            var jsonObjectCount = jsonObject.AllKeys().Count;
            Assert.Equal(map.Count, jsonObjectCount);
            foreach (var item in map)
            {
                var matched = false;
                foreach (var key in jsonObject.AllKeys())
                {
                    var item1 = item.Value.ToString();
                    var item2 = jsonObject.ParseJsonObject(key).ToString();
                    if (!item1.Equals(item2))
                    {
                        continue;
                    }
                    matched = true;
                    break;
                }
                Assert.True(matched);
            }
        }

        [Fact]
        public static void Default_1_ApplyIfNotNullAndNotWhiteSpace()
        {
            var map = new Dictionary<string, string>();
            map.ApplyIfNotNullAndNotWhiteSpace("key1", null)
                    .ApplyIfNotNullAndNotWhiteSpace("key2", "")
                    .ApplyIfNotNullAndNotWhiteSpace("key3", " ")
                    .ApplyIfNotNullAndNotWhiteSpace("key4", "1");
            Assert.False(map.ContainsKey("key1"));
            Assert.False(map.ContainsKey("key2"));
            Assert.False(map.ContainsKey("key3"));
            Assert.True(map.ContainsKey("key4"));
        }
    }
}
