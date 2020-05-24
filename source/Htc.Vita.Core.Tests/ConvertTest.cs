using System;
using System.ComponentModel;
using System.Text;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class ConvertTest
    {
        [Fact]
        public static void Default_0_FromBase64String()
        {
            var bytes1 = Encoding.UTF8.GetBytes("123");
            Assert.NotNull(bytes1);
            var converted = Util.Convert.ToBase64String(bytes1);
            Assert.False(string.IsNullOrWhiteSpace(converted));
            var bytes2 = Util.Convert.FromBase64String(converted);
            Assert.Equal(bytes1, bytes2);
            Assert.Equal("123", Encoding.UTF8.GetString(bytes2));
        }

        [Fact]
        public static void Default_1_FromHexString()
        {
            var bytes1 = Encoding.UTF8.GetBytes("123");
            Assert.NotNull(bytes1);
            var converted = Util.Convert.ToHexString(bytes1);
            Assert.False(string.IsNullOrWhiteSpace(converted));
            var bytes2 = Util.Convert.FromHexString(converted);
            Assert.Equal(bytes1, bytes2);
            Assert.Equal("123", Encoding.UTF8.GetString(bytes2));
        }

        [Fact]
        public static void Default_2_ToBool()
        {
            Assert.False(Util.Convert.ToBool(null));
            Assert.False(Util.Convert.ToBool(""));
            Assert.False(Util.Convert.ToBool("0"));
            Assert.False(Util.Convert.ToBool("1"));
            Assert.False(Util.Convert.ToBool("12"));
            Assert.False(Util.Convert.ToBool("no"));
            Assert.False(Util.Convert.ToBool("No"));
            Assert.False(Util.Convert.ToBool("NO"));
            Assert.False(Util.Convert.ToBool("yes"));
            Assert.False(Util.Convert.ToBool("Yes"));
            Assert.False(Util.Convert.ToBool("YES"));
            Assert.False(Util.Convert.ToBool("false"));
            Assert.False(Util.Convert.ToBool("False"));
            Assert.False(Util.Convert.ToBool("FALSE"));
            Assert.False(Util.Convert.ToBool("FAlse"));
            Assert.True(Util.Convert.ToBool("true"));
            Assert.True(Util.Convert.ToBool("True"));
            Assert.True(Util.Convert.ToBool("TRUE"));
            Assert.True(Util.Convert.ToBool("TRue"));
            Assert.False(Util.Convert.ToBool("TRue1"));
        }

        [Fact]
        public static void Default_3_ToDateTime()
        {
            var dateTime = Util.Convert.ToDateTime("1503779659135.07");
            Assert.Equal(2017, dateTime.Year);
            Assert.Equal(8, dateTime.Month);
            Assert.Equal(26, dateTime.Day);
        }

        [Fact]
        public static void Default_4_ToTimestampInMilli()
        {
            var time = string.Empty + Util.Convert.ToTimestampInMilli(new DateTime(2017, 8, 26));
            Assert.True(time.Length == 13);
            Assert.StartsWith("15037", time);
        }

        [Fact]
        public static void Default_5_ToTypeByDescription()
        {
            const string data = "test_description_5";
            var type = Util.Convert.ToTypeByDescription<TestStatus>(data);
            Assert.Equal(TestStatus.TestStatus0, type);
        }

        [Fact]
        public static void Default_6_ToTypeByName()
        {
            const string data = "TestStatus1";
            var type = Util.Convert.ToTypeByName<TestStatus>(data);
            Assert.Equal(TestStatus.TestStatus1, type);
        }

        public enum TestStatus
        {
            [Description("test_description_5")]
            TestStatus0,
            [Description("test_description_4")]
            TestStatus1,
            [Description("test_description_3")]
            TestStatus2,
            [Description("test_description_2")]
            TestStatus3,
            [Description("test_description_1")]
            TestStatus4,
            [Description("test_description_0")]
            TestStatus5
        }
    }
}
