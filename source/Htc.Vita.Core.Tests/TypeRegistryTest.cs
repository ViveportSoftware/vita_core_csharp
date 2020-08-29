using Htc.Vita.Core.Util;
using Xunit;

namespace Htc.Vita.Core.Tests
{
    public class TypeRegistryTest
    {
        [Fact]
        public static void Default_0_RegisterDefault()
        {
            TypeRegistry.RegisterDefault<BaseClass, SubClass1>();
        }

        [Fact]
        public static void Default_1_Register()
        {
            TypeRegistry.RegisterDefault<BaseClass, SubClass1>();
            TypeRegistry.Register<BaseClass, SubClass2>();
        }

        [Fact]
        public static void Default_2_GetInstance()
        {
            TypeRegistry.RegisterDefault<BaseClass, SubClass1>();
            TypeRegistry.Register<BaseClass, SubClass2>();
            var obj = TypeRegistry.GetInstance<BaseClass>();
            Assert.False(obj is SubClass1);
            Assert.True(obj is SubClass2);
        }
    }

    public abstract class BaseClass
    {
    }

    public class SubClass1 : BaseClass
    {
    }

    public class SubClass2 : BaseClass
    {
    }
}
