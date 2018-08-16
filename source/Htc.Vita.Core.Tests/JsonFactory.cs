using System;
using System.Collections.Generic;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Json.LitJson;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class JsonFactory
    {
        [Fact]
        public static void Default_0_GetInstance()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
        }

        [Fact]
        public static void Default_1_CreateJsonArray()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
        }

        [Fact]
        public static void Default_2_CreateJsonObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
        }

        [Fact]
        public static void Default_3_DeserializeObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"}";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.NotNull(testClass1);
            Assert.Equal(true, testClass1.TestBool1);
            Assert.Equal(3, testClass1.TestInt1);
            Assert.Equal("test", testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_WithBoolAndInt()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{\"TestBool1\":true,\"TestInt1\":3}";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.NotNull(testClass1);
            Assert.Equal(true, testClass1.TestBool1);
            Assert.Equal(3, testClass1.TestInt1);
            Assert.Equal(null, testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_WithEmpty()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{}";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.NotNull(testClass1);
            Assert.Equal(false, testClass1.TestBool1);
            Assert.Equal(0, testClass1.TestInt1);
            Assert.Equal(null, testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_WithNull()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "";
            var testClass1 = jsonFactory.DeserializeObject<TestClass1>(content);
            Assert.Null(testClass1);
            testClass1 = jsonFactory.DeserializeObject<TestClass1>(null);
            Assert.Null(testClass1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_AsList()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "[{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"},{\"TestBool1\":false,\"TestInt1\":5,\"TestString1\":null}]";
            var classList = jsonFactory.DeserializeObject<List<TestClass1>>(content);
            Assert.NotNull(classList);
            Assert.True(classList.Count == 2);
            var testClass0 = classList[0];
            Assert.NotNull(testClass0);
            Assert.Equal(true, testClass0.TestBool1);
            Assert.Equal(3, testClass0.TestInt1);
            Assert.Equal("test", testClass0.TestString1);
            var testClass1 = classList[1];
            Assert.NotNull(testClass1);
            Assert.Equal(false, testClass1.TestBool1);
            Assert.Equal(5, testClass1.TestInt1);
            Assert.Equal(null, testClass1.TestString1);
        }

        [Fact]
        public static void Default_3_DeserializeObject_AsDictionary()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"}";
            var dict = jsonFactory.DeserializeObject<Dictionary<string, string>>(content);
            Assert.NotNull(dict);
            Assert.Equal("testValue0", dict["testKey0"]);
            Assert.Equal("testValue1", dict["testKey1"]);
            Assert.Equal("testValue2", dict["testKey2"]);
        }

        [Fact]
        public static void Default_3_DeserializeObject_AsListOfDictionary()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var content = "[{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"},{\"testKey0\":\"testValue3\",\"testKey2\":\"testValue4\",\"testKey4\":\"testValue5\"}]";
            var dictList = jsonFactory.DeserializeObject<List<Dictionary<string, string>>>(content);
            Assert.NotNull(dictList);
            Assert.True(dictList.Count == 2);
            var dict0 = dictList[0];
            Assert.NotNull(dict0);
            Assert.Equal("testValue0", dict0["testKey0"]);
            Assert.Equal("testValue1", dict0["testKey1"]);
            Assert.Equal("testValue2", dict0["testKey2"]);
            var dict1 = dictList[1];
            Assert.NotNull(dict1);
            Assert.Equal("testValue3", dict1["testKey0"]);
            Assert.Equal("testValue4", dict1["testKey2"]);
            Assert.Equal("testValue5", dict1["testKey4"]);
        }

        [Fact]
        public static void Default_4_GetJsonArray()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
        }

        [Fact]
        public static void Default_5_GetJsonObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
        }

        [Fact]
        public static void Default_6_SerializeObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var class1 = new TestClass1
            {
                TestBool1 = true,
                TestInt1 = 3,
                TestString1 = "test"
            };
            var result = jsonFactory.SerializeObject(class1);
            Assert.NotNull(result);
            Assert.Equal("{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"}", result);
        }

        [Fact]
        public static void Default_6_SerializeObject_WithList()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var classList = new List<TestClass1>();
            var class1 = new TestClass1
            {
                TestBool1 = true,
                TestInt1 = 3,
                TestString1 = "test"
            };
            classList.Add(class1);
            var class2 = new TestClass1
            {
                TestInt1 = 5
            };
            classList.Add(class2);
            var result = jsonFactory.SerializeObject(classList);
            Assert.NotNull(result);
            Assert.Equal("[{\"TestBool1\":true,\"TestInt1\":3,\"TestString1\":\"test\"},{\"TestBool1\":false,\"TestInt1\":5,\"TestString1\":null}]", result);
        }

        [Fact]
        public static void Default_6_SerializeObject_WithDictionary()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var dict = new Dictionary<string, string>
            {
                {"testKey0", "testValue0"},
                {"testKey1", "testValue1"},
                {"testKey2", "testValue2"}
            };
            var result = jsonFactory.SerializeObject(dict);
            Assert.NotNull(result);
            Assert.Equal("{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"}", result);
        }

        [Fact]
        public static void Default_6_SerializeObject_WithListOfDictionary()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var dict = new Dictionary<string, string>
            {
                {"testKey0", "testValue0"},
                {"testKey1", "testValue1"},
                {"testKey2", "testValue2"}
            };
            var dict2 = new Dictionary<string, string>
            {
                {"testKey0", "testValue3"},
                {"testKey2", "testValue4"},
                {"testKey4", "testValue5"}
            };
            var dictList = new List<Dictionary<string, string>>
            {
                dict,
                dict2
            };
            var result = jsonFactory.SerializeObject(dictList);
            Console.WriteLine("Serialized string: " + result);
            Assert.Equal("[{\"testKey0\":\"testValue0\",\"testKey1\":\"testValue1\",\"testKey2\":\"testValue2\"},{\"testKey0\":\"testValue3\",\"testKey2\":\"testValue4\",\"testKey4\":\"testValue5\"}]", result);
        }

        [Fact]
        public static void JsonArray_00_Size()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            Assert.Equal(0, jsonArray.Size());
        }

        [Fact]
        public static void JsonArray_01_ParseBool()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, true);
            var value = jsonArray.ParseBool(0);
            Assert.Equal(true, value);
            jsonArray.Insert(1, "true");
            var value2 = jsonArray.ParseBool(1);
            Assert.Equal(true, value2);
            jsonArray.Insert(1, false);
            var value3 = jsonArray.ParseBool(1);
            Assert.Equal(false, value3);
        }

        [Fact]
        public static void JsonArray_01_ParseBool_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, true);
            var value = jsonArray.ParseBool(0);
            Assert.Equal(true, value);
            jsonArray.Insert(1, "true");
            var value2 = jsonArray.ParseBool(1);
            Assert.Equal(true, value2);
            var value3 = jsonArray.ParseBool(2, true);
            Assert.Equal(true, value3);
        }

        [Fact]
        public static void JsonArray_02_ParseDouble()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 1.1D);
            var value = jsonArray.ParseDouble(0);
            Assert.Equal(1.1D, value);
            jsonArray.Insert(1, "2.2");
            var value2 = jsonArray.ParseDouble(1);
            Assert.Equal(2.2D, value2);
            jsonArray.Insert(1, "3.3");
            var value3 = jsonArray.ParseDouble(1);
            Assert.Equal(3.3D, value3);
        }

        [Fact]
        public static void JsonArray_02_ParseDouble_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 1.1D);
            var value = jsonArray.ParseDouble(0);
            Assert.Equal(1.1D, value);
            jsonArray.Insert(1, 2.2D);
            var value2 = jsonArray.ParseDouble(1);
            Assert.Equal(2.2D, value2);
            var value3 = jsonArray.ParseDouble(2, 3.3D);
            Assert.Equal(3.3D, value3);
        }

        [Fact]
        public static void JsonArray_03_ParseFloat()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 0.0F);
            var value = jsonArray.ParseFloat(0);
            Assert.Equal(0.0F, value);
            jsonArray.Insert(1, "1.1");
            var value2 = jsonArray.ParseFloat(1);
            Assert.Equal(1.1F, value2);
            jsonArray.Insert(1, 2.2F);
            var value3 = jsonArray.ParseFloat(1);
            Assert.Equal(2.2F, value3);
        }

        [Fact]
        public static void JsonArray_03_ParseFloat_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 0.0F);
            var value = jsonArray.ParseFloat(0);
            Assert.Equal(0.0F, value);
            jsonArray.Insert(1, "1.1");
            var value2 = jsonArray.ParseFloat(1);
            Assert.Equal(1.1F, value2);
            var value3 = jsonArray.ParseFloat(2, 2.2F);
            Assert.Equal(2.2F, value3);
        }

        [Fact]
        public static void JsonArray_04_ParseInt()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 0);
            var value = jsonArray.ParseInt(0);
            Assert.Equal(0, value);
            jsonArray.Insert(1, "1");
            var value2 = jsonArray.ParseInt(1);
            Assert.Equal(1, value2);
            jsonArray.Insert(1, 2);
            var value3 = jsonArray.ParseInt(1);
            Assert.Equal(2, value3);
        }

        [Fact]
        public static void JsonArray_04_ParseInt_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 0);
            var value = jsonArray.ParseInt(0);
            Assert.Equal(0, value);
            jsonArray.Insert(1, "1");
            var value2 = jsonArray.ParseInt(1);
            Assert.Equal(1, value2);
            var value3 = jsonArray.ParseInt(2, 2);
            Assert.Equal(2, value3);
        }

        [Fact]
        public static void JsonArray_05_ParseLong()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 100000000000L);
            var value = jsonArray.ParseLong(0);
            Assert.Equal(100000000000L, value);
            jsonArray.Insert(1, "200000000001");
            var value2 = jsonArray.ParseLong(1);
            Assert.Equal(200000000001L, value2);
            jsonArray.Insert(1, 300000000002L);
            var value3 = jsonArray.ParseLong(1);
            Assert.Equal(300000000002, value3);
            jsonArray.Insert(1, 1);
            var value4 = jsonArray.ParseLong(1);
            Assert.Equal(1L, value4);
        }

        [Fact]
        public static void JsonArray_05_ParseLong_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, 100000000000L);
            var value = jsonArray.ParseLong(0);
            Assert.Equal(100000000000L, value);
            jsonArray.Insert(1, "200000000001");
            var value2 = jsonArray.ParseLong(1);
            Assert.Equal(200000000001L, value2);
            var value3 = jsonArray.ParseLong(2, 300000000002L);
            Assert.Equal(300000000002L, value3);
        }

        [Fact]
        public static void JsonArray_06_ParseString()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, "test0");
            var value = jsonArray.ParseString(0);
            Assert.Equal("test0", value);
            jsonArray.Insert(1, "true1");
            var value2 = jsonArray.ParseString(1);
            Assert.Equal("true1", value2);
            jsonArray.Insert(1, "true2");
            var value3 = jsonArray.ParseString(1);
            Assert.Equal("true2", value3);
        }

        [Fact]
        public static void JsonArray_06_ParseString_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, "test0");
            var value = jsonArray.ParseString(0);
            Assert.Equal("test0", value);
            jsonArray.Insert(1, "test1");
            var value2 = jsonArray.ParseString(1);
            Assert.Equal("test1", value2);
            var value3 = jsonArray.ParseString(2, "test2");
            Assert.Equal("test2", value3);
        }

        [Fact]
        public static void JsonArray_07_ParseJsonArray()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonArray2 = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray2);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, jsonArray2);
            var value = jsonArray.ParseJsonArray(0);
            Assert.Equal("[]", value.ToString());
        }

        [Fact]
        public static void JsonArray_08_ParseJsonObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Insert(0, jsonObject);
            var value = jsonArray.ParseJsonObject(0);
            Assert.Equal("{}", value.ToString());
        }

        [Fact]
        public static void JsonArray_09_AppendBool()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append(true);
            var value = jsonArray.ParseBool(0);
            Assert.Equal(true, value);
            jsonArray.Append("true");
            var value2 = jsonArray.ParseBool(1);
            Assert.Equal(true, value2);
            jsonArray.Insert(1, false);
        }

        [Fact]
        public static void JsonArray_10_AppendDouble()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append(1.1D);
            var value = jsonArray.ParseDouble(0);
            Assert.Equal(1.1D, value);
            jsonArray.Append("2.2");
            var value2 = jsonArray.ParseDouble(1);
            Assert.Equal(2.2D, value2);
            jsonArray.Append("2");
            var value3 = jsonArray.ParseDouble(2);
            Assert.Equal(2.0D, value3);
        }

        [Fact]
        public static void JsonArray_11_AppendFloat()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append(0.0F);
            var value = jsonArray.ParseFloat(0);
            Assert.Equal(0.0F, value);
            jsonArray.Append("1.1");
            var value2 = jsonArray.ParseFloat(1);
            Assert.Equal(1.1F, value2);
        }

        [Fact]
        public static void JsonArray_12_AppendInt()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append(0);
            var value = jsonArray.ParseInt(0);
            Assert.Equal(0, value);
            jsonArray.Append("1");
            var value2 = jsonArray.ParseInt(1);
            Assert.Equal(1, value2);
        }

        [Fact]
        public static void JsonArray_13_AppendLong()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append(100000000000L);
            var value = jsonArray.ParseLong(0);
            Assert.Equal(100000000000L, value);
            jsonArray.Append("200000000001");
            var value2 = jsonArray.ParseLong(1);
            Assert.Equal(200000000001L, value2);
            jsonArray.Append(1);
            var value3 = jsonArray.ParseLong(2);
            Assert.Equal(1L, value3);
        }

        [Fact]
        public static void JsonArray_14_AppendString()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append("test0");
            var value = jsonArray.ParseString(0);
            Assert.Equal("test0", value);
            jsonArray.Append("true1");
            var value2 = jsonArray.ParseString(1);
            Assert.Equal("true1", value2);
        }

        [Fact]
        public static void JsonArray_15_AppendJsonArray()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonArray2 = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray2);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append(jsonArray2);
            var value = jsonArray.ParseJsonArray(0);
            Assert.Equal("[]", value.ToString());
        }

        [Fact]
        public static void JsonArray_16_AppendJsonObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.CreateJsonArray();
            Assert.NotNull(jsonArray);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("[]", jsonArray.ToString());
            jsonArray.Append(jsonObject);
            var value = jsonArray.ParseJsonObject(0);
            Assert.Equal("{}", value.ToString());
        }

        [Fact]
        public static void JsonArray_17_InsertBool()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, true);
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonArray_18_InsertDouble()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, 3.3D);
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonArray_19_InsertFloat()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, 2.2F);
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonArray_20_InsertInt()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, 1);
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonArray_21_InsertLong()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, 100000000000L);
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonArray_22_InsertString()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, "test");
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonArray_23_InsertJsonArray()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, new LitJsonJsonFactory.LitJsonJsonArray(JsonMapper.ToObject("[]")));
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonArray_24_InsertJsonObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonArray = jsonFactory.GetJsonArray("[]");
            Assert.NotNull(jsonArray);
            jsonArray = jsonArray.Insert(0, new LitJsonJsonFactory.LitJsonJsonArray(JsonMapper.ToObject("{}")));
            Assert.NotNull(jsonArray);
        }

        [Fact]
        public static void JsonObject_00_HasKey()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.CreateJsonObject();
            Assert.NotNull(jsonObject);
            Assert.Equal("{}", jsonObject.ToString());
            jsonObject.Put("key", true);
            Assert.Equal(true, jsonObject.HasKey("key"));
            Assert.Equal(false, jsonObject.HasKey("key2"));
        }

        [Fact]
        public static void JsonObject_01_ParseBool()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseBool("key");
            Assert.Equal(false, value);
        }

        [Fact]
        public static void JsonObject_01_ParseBool_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseBool("key", true);
            Assert.Equal(true, value);
        }

        [Fact]
        public static void JsonObject_02_ParseDouble()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseDouble("key");
            Assert.Equal(0.0D, value);
        }

        [Fact]
        public static void JsonObject_02_ParseDouble_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseDouble("key", 3.3D);
            Assert.Equal(3.3D, value);
        }

        [Fact]
        public static void JsonObject_03_ParseFloat()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseFloat("key");
            Assert.Equal(0.0F, value);
        }

        [Fact]
        public static void JsonObject_03_ParseFloat_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseFloat("key", 2.2F);
            Assert.Equal(2.2F, value);
        }

        [Fact]
        public static void JsonObject_04_ParseInt()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseInt("key");
            Assert.Equal(0, value);
        }

        [Fact]
        public static void JsonObject_04_ParseInt_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseInt("key", 1);
            Assert.Equal(1, value);
        }

        [Fact]
        public static void JsonObject_05_ParseLong()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseLong("key");
            Assert.Equal(0L, value);
        }

        [Fact]
        public static void JsonObject_05_ParseLong_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseLong("key", 100000000000L);
            Assert.Equal(100000000000L, value);
        }

        [Fact]
        public static void JsonObject_06_ParseString()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseString("key");
            Assert.Equal(null, value);
        }

        [Fact]
        public static void JsonObject_06_ParseString_WithDefault()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseString("key", "test");
            Assert.Equal("test", value);
        }

        [Fact]
        public static void JsonObject_07_ParseJsonArray()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseJsonArray("key");
            Assert.Null(value);
        }

        [Fact]
        public static void JsonObject_08_ParseJsonObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            var value = jsonObject.ParseJsonObject("key");
            Assert.Null(value);
        }

        [Fact]
        public static void JsonObject_09_PutBool()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", true);
            Assert.NotNull(jsonObject);
        }

        [Fact]
        public static void JsonObject_10_PutDouble()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", 3.3D);
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key2", 1.1D);
            var value = jsonObject.ParseDouble("key2");
            Assert.Equal(1.1D, value);
            jsonObject = jsonObject.Put("key3", 2);
            var value2 = jsonObject.ParseDouble("key3");
            Assert.Equal(2.0D, value2);
        }

        [Fact]
        public static void JsonObject_11_PutFloat()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", 2.2F);
            Assert.NotNull(jsonObject);
        }

        [Fact]
        public static void JsonObject_12_PutInt()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", 1);
            Assert.NotNull(jsonObject);
        }

        [Fact]
        public static void JsonObject_13_PutLong()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", 100000000000L);
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key2", 1L);
            var value = jsonObject.ParseLong("key2");
            Assert.Equal(1L, value);
        }

        [Fact]
        public static void JsonObject_14_PutString()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", "test");
            Assert.NotNull(jsonObject);
        }

        [Fact]
        public static void JsonObject_15_InsertJsonArray()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", new LitJsonJsonFactory.LitJsonJsonArray(JsonMapper.ToObject("[]")));
            Assert.NotNull(jsonObject);
        }

        [Fact]
        public static void JsonObject_16_InsertJsonObject()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject = jsonObject.Put("key", new LitJsonJsonFactory.LitJsonJsonArray(JsonMapper.ToObject("{}")));
            Assert.NotNull(jsonObject);
        }

        [Fact]
        public static void JsonObject_17_AllKeys()
        {
            var jsonFactory = Json.JsonFactory.GetInstance();
            Assert.NotNull(jsonFactory);
            var jsonObject = jsonFactory.GetJsonObject("{}");
            Assert.NotNull(jsonObject);
            jsonObject.Put("key1", 1);
            jsonObject.Put("key2", true);
            jsonObject.Put("key3", 3.3D);
            Assert.Equal(3, jsonObject.AllKeys().Count);
        }

        public class TestClass1
        {
            public bool TestBool1 { get; set; }
            public int TestInt1 { get; set; }
            public string TestString1 { get; set; }
        }
    }
}
