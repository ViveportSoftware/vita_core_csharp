using System;
using System.IO;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public class FileSystemManager
    {
        private static readonly Logger Log = Logger.GetInstance(typeof(FileSystemManager));

        public static DiskSpaceInfo GetDiskSpaceFor(DirectoryInfo directoryInfo)
        {
            return GetWindowsDiskSpace(directoryInfo);
        }

        public static long GetDiskFreeSpaceFor(DirectoryInfo directoryInfo)
        {
            return GetDiskSpaceFor(directoryInfo).FreeOfBytes;
        }

        internal static DiskSpaceInfo GetWindowsDiskSpace(DirectoryInfo directoryInfo)
        {
            var result = new DiskSpaceInfo();
            if (directoryInfo == null)
            {
                return result;
            }

            try
            {
                var path = directoryInfo.FullName;
                ulong freeBytesAvailable;
                ulong totalNumberOfBytes;
                ulong totalNumberOfFreeBytes;

                var success = Interop.Windows.Kernel32.GetDiskFreeSpaceExW(
                    path,
                    out freeBytesAvailable,
                    out totalNumberOfBytes,
                    out totalNumberOfFreeBytes
                );

                result.Path = path;

                if (success)
                {
                    result.FreeOfBytes = (long) freeBytesAvailable;
                    result.TotalOfBytes = (long) totalNumberOfBytes;
                    result.TotalFreeOfBytes = (long) totalNumberOfFreeBytes;
                }
            }
            catch (Exception e)
            {
                Log.Error("Can not get Windows disk free space: " + e.Message);
            }
            return result;
        }

        public class DiskSpaceInfo
        {
            public string Path { get; set; } = "";
            public long FreeOfBytes { get; set; } = -1;
            public long TotalOfBytes { get; set; } = -1;
            public long TotalFreeOfBytes { get; set; } = -1;
        }
    }
}
