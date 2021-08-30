using System;
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

            var map2 = new Dictionary<string, object>();
            map2.ApplyIfNotNullAndNotWhiteSpace("key1", null)
                    .ApplyIfNotNullAndNotWhiteSpace("key2", "")
                    .ApplyIfNotNullAndNotWhiteSpace("key3", " ")
                    .ApplyIfNotNullAndNotWhiteSpace("key4", "1");
            Assert.False(map2.ContainsKey("key1"));
            Assert.False(map2.ContainsKey("key2"));
            Assert.False(map2.ContainsKey("key3"));
            Assert.True(map2.ContainsKey("key4"));
        }

        [Fact]
        public static void Default_2_ToEncodedUriQueryParameters()
        {
            var map = new Dictionary<string, string>
            {
                    ["a"] = "1",
                    ["b"] = "2",
                    ["c"] = "3"
            };
            var encodedUriQueryParameters = map.ToEncodedUriQueryParameters();
            Assert.Contains("a=1", encodedUriQueryParameters);
            Assert.Contains("b=2", encodedUriQueryParameters);
            Assert.Contains("c=3", encodedUriQueryParameters);
            Assert.Contains("&", encodedUriQueryParameters);
        }

        [Fact]
        public static void Default_3_ParseString()
        {
            var map = new Dictionary<string, string>
            {
                    ["a"] = "1",
                    ["b"] = "2",
                    ["c"] = "3"
            };
            Assert.Equal("1", map.ParseString("a"));
            Assert.Equal("2", map.ParseString("b"));
            Assert.Equal("3", map.ParseString("c"));
            Assert.Null(map.ParseString("d"));
            Assert.Equal("5", map.ParseString("e", "5"));
        }

        [Fact]
        public static void Default_3_ParseUri()
        {
            var map = new Dictionary<string, string>
            {
                    ["a"] = "1",
                    ["b"] = "https://www.microsoft.com",
                    ["c"] = "http://www.google.com"
            };
            Assert.Null(map.ParseUri("a"));
            Assert.NotNull(map.ParseUri("b"));
            Assert.Equal(new Uri("http://www.google.com"), map.ParseUri("c"));
            Assert.NotNull(map.ParseUri("d", new Uri("https://www.apple.com")));

            var map2 = new Dictionary<string, object>
            {
                    ["a"] = "1",
                    ["b"] = JsonFactory.GetInstance().CreateJsonObject(),
                    ["c"] = new Uri("https://www.microsoft.com"),
                    ["d"] = "http://www.google.com"
            };
            Assert.Null(map2.ParseUri("a"));
            Assert.Null(map2.ParseUri("b"));
            Assert.Equal(new Uri("https://www.microsoft.com"), map2.ParseUri("c"));
            Assert.Equal(new Uri("http://www.google.com"), map2.ParseUri("d"));
            Assert.NotNull(map2.ParseUri("e", new Uri("https://www.apple.com")));
        }

        [Fact]
        public static void Default_4_ParseData()
        {
            var map = new Dictionary<string, object>
            {
                    ["a"] = "1",
                    ["b"] = JsonFactory.GetInstance().CreateJsonObject(),
                    ["c"] = new Uri("https://www.microsoft.com"),
                    ["d"] = null
            };
            Assert.NotNull(map.ParseData("a"));
            Assert.NotNull(map.ParseData("b"));
            Assert.NotNull(map.ParseData("c"));
            Assert.Null(map.ParseData("d"));
            Assert.NotNull(map.ParseData("e", new Uri("https://www.apple.com")));
        }
    }
}
