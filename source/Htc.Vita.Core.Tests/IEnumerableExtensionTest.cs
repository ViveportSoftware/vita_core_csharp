using System.Collections.Generic;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class IEnumerableExtensionTest
    {
        [Fact]
        public static void Default_0_ToJsonArray_bool()
        {
            var items = new List<bool>
            {
                    true,
                    false,
                    true
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item2 = jsonArray.ParseBool(i);
                    if (!item.Equals(item2))
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
        public static void Default_0_ToJsonArray_double()
        {
            var items = new List<double>
            {
                    1.1d,
                    2.2d,
                    3.3d
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item2 = jsonArray.ParseDouble(i);
                    if (!item.Equals(item2))
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
        public static void Default_0_ToJsonArray_float()
        {
            var items = new List<float>
            {
                    1.1f,
                    2.2f,
                    3.3f
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item2 = jsonArray.ParseFloat(i);
                    if (!item.Equals(item2))
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
        public static void Default_0_ToJsonArray_int()
        {
            var items = new List<int>
            {
                    1,
                    2,
                    3
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item2 = jsonArray.ParseInt(i);
                    if (!item.Equals(item2))
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
        public static void Default_0_ToJsonArray_long()
        {
            var items = new List<long>
            {
                    1L,
                    2L,
                    3L
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item2 = jsonArray.ParseLong(i);
                    if (!item.Equals(item2))
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
        public static void Default_0_ToJsonArray_string()
        {
            var items = new List<string>
            {
                    "a",
                    "b",
                    "c"
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item2 = jsonArray.ParseString(i);
                    if (!item.Equals(item2))
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
        public static void Default_0_ToJsonArray_JsonArray()
        {
            var items = new List<JsonArray>
            {
                    JsonFactory.GetInstance().GetJsonArray("[]"),
                    JsonFactory.GetInstance().GetJsonArray("[1,2,3]"),
                    JsonFactory.GetInstance().GetJsonArray("[\"1\",\"2\"]")
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item1 = item.ToString();
                    var item2 = jsonArray.ParseJsonArray(i).ToString();
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
        public static void Default_0_ToJsonArray_JsonObject()
        {
            var items = new List<JsonObject>
            {
                    JsonFactory.GetInstance().GetJsonObject("{}"),
                    JsonFactory.GetInstance().GetJsonObject("{\"b\":\"2\"}"),
                    JsonFactory.GetInstance().GetJsonObject("{\"c\":\"3\"}")
            };
            var jsonArray = items.ToJsonArray();
            var jsonArraySize = jsonArray.Size();
            Assert.Equal(items.Count, jsonArraySize);
            foreach (var item in items)
            {
                var matched = false;
                for (var i = 0; i < jsonArraySize; i++)
                {
                    var item1 = item.ToString();
                    var item2 = jsonArray.ParseJsonObject(i).ToString();
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
    }
}
