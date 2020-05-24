using System;
using System.Linq;
using System.Text;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class Convert.
    /// </summary>
    public static class Convert
    {
        /// <summary>
        /// Converts data from the Base64 string to byte array.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] FromBase64String(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            byte[] result = null;
            try
            {
                result = System.Convert.FromBase64String(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Convert)).Error($"Fail converting base64 string \"{content}\" to byte array: {e}");
            }
            return result;
        }

        /// <summary>
        /// Converts data from the hexadecimal string to byte array.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.Byte[].</returns>
        public static byte[] FromHexString(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return null;
            }

            var result = new byte[content.Length / 2];
            for (var i = 0; i < result.Length; i++)
            {
                result[i] = System.Convert.ToByte(content.Substring(i * 2, 2), 16);
            }
            return result;
        }

        /// <summary>
        /// Converts data from string to bool.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.Boolean.</returns>
        public static bool ToBool(string content)
        {
            return ToBool(content, false);
        }

        /// <summary>
        /// Converts data from string to bool.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Boolean.</returns>
        public static bool ToBool(string content, bool defaultValue)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                result = System.Convert.ToBoolean(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Convert)).Error($"Fail parsing \"{content}\" to bool: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Converts data from string to double.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(string content)
        {
            return ToDouble(content, 0.0D);
        }

        /// <summary>
        /// Converts data from string to double.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Double.</returns>
        public static double ToDouble(string content, double defaultValue)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                result = System.Convert.ToDouble(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Convert)).Error($"Fail parsing \"{content}\" to double: {e.Message}");
            }

            return result;
        }

        /// <summary>
        /// Converts data from string to int.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt32(string content)
        {
            return ToInt32(content, 0);
        }

        /// <summary>
        /// Converts data from string to int.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int32.</returns>
        public static int ToInt32(string content, int defaultValue)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                result = System.Convert.ToInt32(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Convert)).Error($"Fail parsing \"{content}\" to int/int32: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Converts data from string to long.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <returns>System.Int64.</returns>
        public static long ToInt64(string content)
        {
            return ToInt64(content, 0L);
        }

        /// <summary>
        /// Converts data from string to long.
        /// </summary>
        /// <param name="content">The content.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.Int64.</returns>
        public static long ToInt64(string content, long defaultValue)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                return defaultValue;
            }

            var result = defaultValue;
            try
            {
                result = System.Convert.ToInt64(content);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Convert)).Error($"Fail parsing \"{content}\" to long/int64: {e.Message}");
            }
            return result;
        }

        /// <summary>
        /// Converts data from byte array to Base64 string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string ToBase64String(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

            var result = string.Empty;
            try
            {
                result = System.Convert.ToBase64String(bytes);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Convert)).Error($"Fail converting byte array to base64 string: {e}");
            }
            return result;
        }

        /// <summary>
        /// Converts data from byte array to hexadecimal string.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns>System.String.</returns>
        public static string ToHexString(byte[] bytes)
        {
            if (bytes == null)
            {
                return string.Empty;
            }

            var buffer = new StringBuilder(bytes.Length * 2);
            foreach (var b in bytes)
            {
                // x2 for lower case
                buffer.Append(b.ToString("x2"));
            }
            return buffer.ToString();
        }

        /// <summary>
        /// Converts data from timestamp string in millisecond to datetime.
        /// </summary>
        /// <param name="timestampStringInMilli">The timestamp string in millisecond.</param>
        /// <returns>System.DateTime.</returns>
        public static DateTime ToDateTime(string timestampStringInMilli)
        {
            var timestamp = ToDouble(timestampStringInMilli);
            return new DateTime(1970, 1, 1).AddMilliseconds(timestamp);
        }

        /// <summary>
        /// Converts data from datetime to timestamp in millisecond.
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns>System.Int64.</returns>
        public static long ToTimestampInMilli(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        /// <summary>
        /// Converts data by description.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>T.</returns>
        public static T ToTypeByDescription<T>(string data) where T : struct, IConvertible, IComparable, IFormattable
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return default(T);
            }

            return ((T[])Enum.GetValues(typeof(T)))
                    .FirstOrDefault(item => item.GetDescription().Equals(data));
        }

        /// <summary>
        /// Converts data by name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <returns>T.</returns>
        public static T ToTypeByName<T>(string data) where T : struct, IConvertible, IComparable, IFormattable
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                return default(T);
            }

            return ((T[])Enum.GetValues(typeof(T)))
                    .FirstOrDefault(item => item.ToString().Equals(data));
        }
    }
}
