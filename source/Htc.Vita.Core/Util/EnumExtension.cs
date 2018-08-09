using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Htc.Vita.Core.Util
{
    public static class EnumExtension
    {
        public static T ApplyFlags<T>(this T value, T flags) where T : struct, IConvertible, IComparable, IFormattable
        {
            return value.SetFlagsStatus(flags, true);
        }

        private static void CheckIsEnum<T>(bool withFlags)
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException($"Type '{typeof(T).FullName}' is not an enum");
            }

            if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
            {
                throw new ArgumentException($"Type '{typeof(T).FullName}' doesn't have the 'Flags' attribute");
            }
        }

        public static T ClearFlags<T>(this T value, T flags) where T : struct, IConvertible, IComparable, IFormattable
        {
            return value.SetFlagsStatus(flags, false);
        }

        public static IEnumerable<T> GetAppliedFlags<T>(this T value) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            foreach (var flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (value.IsFlagApplied(flag))
                {
                    yield return flag;
                }
            }
        }

        public static string GetDescription<T>(this T value) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(false);
            var name = Enum.GetName(typeof(T), value);
            if (name == null)
            {
                return null;
            }
            var field = typeof(T).GetField(name);
            if (field == null)
            {
                return null;
            }
            var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attr?.Description;
        }

        public static bool IsFlagApplied<T>(this T value, T flag) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlag = System.Convert.ToInt64(flag);
            return (longValue & longFlag) != 0 && ((longValue | longFlag) == longFlag || (longValue | longFlag) == longValue);
        }

        public static bool IsFlagAppliedAny<T>(this T value, T flag) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlag = System.Convert.ToInt64(flag);
            return (longValue & longFlag) != 0;
        }

        public static bool IsFlagAppliedOnly<T>(this T value, T flag) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlag = System.Convert.ToInt64(flag);
            return (longValue & longFlag) != 0 && (longValue & ~longFlag) == 0;
        }

        public static T KeepFlags<T>(this T value, T flags) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlags = System.Convert.ToInt64(flags);
            return (T)Enum.ToObject(typeof(T), longValue & longFlags);
        }

        public static T SetFlagsStatus<T>(this T value, T flags, bool on) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlag = System.Convert.ToInt64(flags);
            if (on)
            {
                longValue |= longFlag;
            }
            else
            {
                longValue &= (~longFlag);
            }
            return (T)Enum.ToObject(typeof(T), longValue);
        }

        /*
        public static T SetValueKeepingFlags<T>(this T value, T valueFlags, T maskingFlags) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var lValue = System.Convert.ToInt64(value);
            var lValueFlags = System.Convert.ToInt64(valueFlags);
            var lMaskingFlags = System.Convert.ToInt64(maskingFlags);
            var lMaskingValue = (lValue & lMaskingFlags);
            lValue = lValueFlags | lMaskingValue;
            return (T)Enum.ToObject(typeof(T), lValue);
        }
        */
    }
}
