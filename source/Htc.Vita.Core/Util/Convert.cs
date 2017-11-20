using System;
using System.Text;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Util
{
    public static class Convert
    {
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
                Logger.GetInstance().Error("Fail converting base64 string \"" + content + "\" to byte array: " + e);
            }
            return result;
        }

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

        public static bool ToBool(string content, bool defaultValue = false)
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
                Logger.GetInstance().Error("Fail parsing \"" + content + "\" to bool: " + e.Message);
            }
            return result;
        }

        public static double ToDouble(string content, double defaultValue = 0.0D)
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
                Logger.GetInstance().Error("Fail parsing \"" + content + "\" to double: " + e.Message);
            }

            return result;
        }

        public static int ToInt32(string content, int defaultValue = 0)
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
                Logger.GetInstance().Error("Fail parsing \"" + content + "\" to int/int32: " + e.Message);
            }
            return result;
        }

        public static long ToInt64(string content, long defaultValue = 0L)
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
                Logger.GetInstance().Error("Fail parsing \"" + content + "\" to long/int64: " + e.Message);
            }
            return result;
        }

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
                Logger.GetInstance().Error("Fail converting byte array to base64 string: " + e);
            }
            return result;
        }

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

        public static DateTime ToDateTime(string timestampStringInMilli)
        {
            var timestamp = ToDouble(timestampStringInMilli);
            return new DateTime(1970, 1, 1).AddMilliseconds(timestamp);
        }

        public static long ToTimestampInMilli(DateTime dateTime)
        {
            return (long)(dateTime - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }
    }
}
