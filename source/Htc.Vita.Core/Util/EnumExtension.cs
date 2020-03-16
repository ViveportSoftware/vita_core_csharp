using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class EnumExtension.
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Applies the flags into the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>T.</returns>
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

        /// <summary>
        /// Clears the flags from the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>T.</returns>
        public static T ClearFlags<T>(this T value, T flags) where T : struct, IConvertible, IComparable, IFormattable
        {
            return value.SetFlagsStatus(flags, false);
        }

        /// <summary>
        /// Gets the applied flags from the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>IEnumerable&lt;T&gt;.</returns>
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

        /// <summary>
        /// Gets the description from the enumeration value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Determines whether the specified flags are applied into the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="flag">The flag.</param>
        /// <returns><c>true</c> if [is flag applied] [the specified flag]; otherwise, <c>false</c>.</returns>
        public static bool IsFlagApplied<T>(this T value, T flag) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlag = System.Convert.ToInt64(flag);
            return (longValue & longFlag) != 0 && ((longValue | longFlag) == longFlag || (longValue | longFlag) == longValue);
        }

        /// <summary>
        /// Determines whether any of the specified flags is applied into the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="flag">The flag.</param>
        /// <returns><c>true</c> if [is flag applied any] [the specified flag]; otherwise, <c>false</c>.</returns>
        public static bool IsFlagAppliedAny<T>(this T value, T flag) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlag = System.Convert.ToInt64(flag);
            return (longValue & longFlag) != 0;
        }

        /// <summary>
        /// Determines whether the specified flags are only applied into the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="flag">The flag.</param>
        /// <returns><c>true</c> if [is flag applied only] [the specified flag]; otherwise, <c>false</c>.</returns>
        public static bool IsFlagAppliedOnly<T>(this T value, T flag) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlag = System.Convert.ToInt64(flag);
            return (longValue & longFlag) != 0 && (longValue & ~longFlag) == 0;
        }

        /// <summary>
        /// Keeps the flags from the value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="flags">The flags.</param>
        /// <returns>T.</returns>
        public static T KeepFlags<T>(this T value, T flags) where T : struct, IConvertible, IComparable, IFormattable
        {
            CheckIsEnum<T>(true);
            var longValue = System.Convert.ToInt64(value);
            var longFlags = System.Convert.ToInt64(flags);
            return (T)Enum.ToObject(typeof(T), longValue & longFlags);
        }

        /// <summary>
        /// Sets the flags status.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="flags">The flags.</param>
        /// <param name="on">if set to <c>true</c> [on].</param>
        /// <returns>T.</returns>
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
