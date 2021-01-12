using System.IO;
using System.Text;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Util
{
    /// <summary>
    /// Class Win32Ini.
    /// </summary>
    public class Win32Ini
    {
        private const int ReadBufferSize = 1024;

        private readonly string _fullPath;

        private Win32Ini(string fullPath)
        {
            _fullPath = fullPath;
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        public string ReadString(
                string section,
                string key)
        {
            return ReadString(
                    section,
                    key,
                    null
            );
        }

        /// <summary>
        /// Reads the string.
        /// </summary>
        /// <param name="section">The section.</param>
        /// <param name="key">The key.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>System.String.</returns>
        public string ReadString(
                string section,
                string key,
                string defaultValue)
        {
            var builder = new StringBuilder(ReadBufferSize);
            var length = Windows.GetPrivateProfileStringW(
                    section,
                    key,
                    defaultValue,
                    builder,
                    ReadBufferSize,
                    _fullPath
            );

            return builder.ToString(0, (int)length);
        }

        /// <summary>
        /// Loads Win32Ini instance.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <returns>Win32Ini.</returns>
        /// <exception cref="DirectoryNotFoundException"></exception>
        public static Win32Ini LoadFrom(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                return null;
            }

            var parent = fileInfo.Directory;
            if (parent == null || !parent.Exists)
            {
                Logger.GetInstance(typeof(Win32Ini)).Error("Can not find parent directory to place file.");
                throw new DirectoryNotFoundException();
            }

            return new Win32Ini(fileInfo.FullName);
        }
    }
}
