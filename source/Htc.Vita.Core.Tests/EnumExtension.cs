using System;
using System.ComponentModel;
using System.Linq;
using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public static class EnumExtension
    {
        [Fact]
        public static void Default_0_ApplyFlags()
        {
            var accumulateFlag = TestFlag.Flag0 | TestFlag.Flag1;
            var accumulateFlag2 = TestFlag.Flag0.ApplyFlags(TestFlag.Flag1);
            Assert.Equal(accumulateFlag, accumulateFlag2);
            var accumulateFlag3 = TestFlag.Flag0.ApplyFlags(TestFlag.Flag2);
            Assert.NotEqual(accumulateFlag, accumulateFlag3);
        }

        [Fact]
        public static void Default_1_ClearFlags()
        {
            var accumulateFlag = TestFlag.Flag0 | TestFlag.Flag1 | TestFlag.Flag2;
            var accumulateFlag2 = TestFlag.Flag0 | TestFlag.Flag1;
            var accumulateFlag3 = accumulateFlag.ClearFlags(TestFlag.Flag2);
            Assert.Equal(accumulateFlag2, accumulateFlag3);
            var accumulateFlag4 = accumulateFlag.ClearFlags(TestFlag.Flag2);
            Assert.Equal(accumulateFlag2, accumulateFlag4);
            var accumulateFlag5 = accumulateFlag4.ClearFlags(TestFlag.Flag1);
            Assert.NotEqual(accumulateFlag2, accumulateFlag5);
            Assert.Equal(TestFlag.Flag0, accumulateFlag5);
        }

        [Fact]
        public static void Default_2_GetAppliedFlags()
        {
            const TestFlag accumulateFlags = TestFlag.Flag0 | TestFlag.Flag1 | TestFlag.Flag2 | TestFlag.Flag3 | TestFlag.Flag4 | TestFlag.Flag5;
            var appliedFlagList = accumulateFlags.GetAppliedFlags().ToArray();
            Assert.Equal(6, appliedFlagList.Length);
            Assert.True(appliedFlagList.Contains(TestFlag.Flag0));
            Assert.True(appliedFlagList.Contains(TestFlag.Flag1));
            Assert.True(appliedFlagList.Contains(TestFlag.Flag2));
            Assert.True(appliedFlagList.Contains(TestFlag.Flag3));
            Assert.True(appliedFlagList.Contains(TestFlag.Flag4));
            Assert.True(appliedFlagList.Contains(TestFlag.Flag5));
        }

        [Fact]
        public static void Default_3_GetDescription()
        {
            foreach (TestType testType in Enum.GetValues(typeof(TestType)))
            {
                Assert.Equal(testType + " Description", testType.GetDescription());
            }
        }

        [Fact]
        public static void Default_3_GetDescription_WithoutDescriptionAtrribute()
        {
            foreach (AltTestType testType in Enum.GetValues(typeof(AltTestType)))
            {
                Assert.Null(testType.GetDescription());
            }
        }

        [Fact]
        public static void Default_4_IsFlagApplied()
        {
            var accumulateFlags = TestFlag.Flag0 | TestFlag.Flag1 | TestFlag.Flag2 | TestFlag.Flag3 | TestFlag.Flag4;
            Assert.True(accumulateFlags.IsFlagApplied(TestFlag.Flag0));
            Assert.True(accumulateFlags.IsFlagApplied(TestFlag.Flag1));
            Assert.True(accumulateFlags.IsFlagApplied(TestFlag.Flag2));
            Assert.True(accumulateFlags.IsFlagApplied(TestFlag.Flag3));
            Assert.True(accumulateFlags.IsFlagApplied(TestFlag.Flag4));
            Assert.False(accumulateFlags.IsFlagApplied(TestFlag.Flag5));
            accumulateFlags = TestFlag.Flag1;
            Assert.True(accumulateFlags.IsFlagApplied(TestFlag.Flag1));
        }

        [Fact]
        public static void Default_5_IsFlagAppliedOnly()
        {
            var accumulateFlags = TestFlag.Flag0 | TestFlag.Flag1 | TestFlag.Flag2 | TestFlag.Flag3 | TestFlag.Flag4;
            Assert.False(accumulateFlags.IsFlagAppliedOnly(TestFlag.Flag0));
            Assert.False(accumulateFlags.IsFlagAppliedOnly(TestFlag.Flag1));
            Assert.False(accumulateFlags.IsFlagAppliedOnly(TestFlag.Flag2));
            Assert.False(accumulateFlags.IsFlagAppliedOnly(TestFlag.Flag3));
            Assert.False(accumulateFlags.IsFlagAppliedOnly(TestFlag.Flag4));
            Assert.False(accumulateFlags.IsFlagAppliedOnly(TestFlag.Flag5));
            accumulateFlags = TestFlag.Flag1;
            Assert.True(accumulateFlags.IsFlagAppliedOnly(TestFlag.Flag1));
        }

        [Fact]
        public static void Default_6_IsFlagAppliedAny()
        {
            var accumulateFlags = TestFlag.Flag0 | TestFlag.Flag3 | TestFlag.Flag4;
            var accumulateFlags2 = TestFlag.Flag0 | TestFlag.Flag1;
            var accumulateFlags3 = TestFlag.Flag0 | TestFlag.Flag2;
            var accumulateFlags4 = TestFlag.Flag1 | TestFlag.Flag2;
            Assert.True(accumulateFlags.IsFlagAppliedAny(accumulateFlags2));
            Assert.True(accumulateFlags.IsFlagAppliedAny(accumulateFlags3));
            Assert.False(accumulateFlags.IsFlagAppliedAny(accumulateFlags4));
        }

        /*
        [Fact]
        public static void Default_7_SetValueKeepingFlags()
        {
            var flags = TestFlag.Flag0 | TestFlag.Flag2 | TestFlag.Flag4 | TestFlag.Flag6;
            var flags2 = flags.SetValueKeepingFlags(TestFlag.Flag3, TestFlag.Flag7);
            var flags3 = flags.KeepFlags(TestFlag.Flag7).ApplyFlags(TestFlag.Flag3);
            Assert.Equal(flags2, flags3);
        }
        */

        public enum AltTestType
        {
            AltTest0,
            AltTest1,
            AltTest2,
            AltTest3,
            AltTest4,
            AltTest5
        }

        [Flags]
        public enum TestFlag
        {
            Flag0 = 1 << 0,
            Flag1 = 1 << 1,
            Flag2 = 1 << 2,
            Flag3 = 1 << 3,
            Flag4 = 1 << 4,
            Flag5 = 1 << 5,
            Flag6 = 1 << 6,
            Flag7 = Flag0
                  | Flag1
                  | Flag5
                  | Flag6
        }

        public enum TestType
        {
            [Description("Test0 Description")] Test0,
            [Description("Test1 Description")] Test1,
            [Description("Test2 Description")] Test2,
            [Description("Test3 Description")] Test3,
            [Description("Test4 Description")] Test4,
            [Description("Test5 Description")] Test5
        }
    }
}
