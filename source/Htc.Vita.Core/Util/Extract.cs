using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.Util
{
    public static partial class Extract
    {
        public static bool FromFileToIcon(
                FileInfo fromFile,
                FileInfo toIcon)
        {
            if (!Platform.IsWindows)
            {
                return false;
            }
            return Windows.FromFileToIconInPlatform(fromFile, toIcon);
        }

        public static bool FromAssemblyToFileByResourceName(
                string byResourceName,
                FileInfo toFile,
                CompressionType compressionType)
        {
            if (string.IsNullOrWhiteSpace(byResourceName))
            {
                return false;
            }

            if (toFile == null)
            {
                return false;
            }

            try
            {
                var binaryDirectory = toFile.Directory;
                if (binaryDirectory != null && !binaryDirectory.Exists)
                {
                    binaryDirectory.Create();
                }

                var assembly = Assembly.GetCallingAssembly();
                var doesResourceExist = false;
                foreach (var name in assembly.GetManifestResourceNames())
                {
                    if (byResourceName.Equals(name))
                    {
                        doesResourceExist = true;
                    }
                }

                if (!doesResourceExist)
                {
                    Logger.GetInstance(typeof(Extract)).Error("Can not find resource \"" + byResourceName);
                    return false;
                }

                using (var stream = assembly.GetManifestResourceStream(byResourceName))
                {
                    if (stream == null)
                    {
                        return false;
                    }

                    if (compressionType == CompressionType.Gzip)
                    {
                        using (var gZipStream = new GZipStream(stream, CompressionMode.Decompress))
                        {
                            using (var fileStream = toFile.OpenWrite())
                            {
                                gZipStream.CopyTo(fileStream);
                            }
                        }
                    }
                    else
                    {
                        using (var fileStream = toFile.OpenWrite())
                        {
                            stream.CopyTo(fileStream);
                        }
                    }
                }

                return true;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Extract)).Error("Can not extract resource \"" + byResourceName + "\" to \"" + toFile + "\": " + e.Message);
            }

            return false;
        }

        public enum CompressionType
        {
            None,
            Gzip
        }
    }
}
