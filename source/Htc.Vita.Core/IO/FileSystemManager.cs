using System;
using System.IO;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.IO
{
    public static partial class FileSystemManager
    {
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
                var freeBytesAvailable = 0UL;
                var totalNumberOfBytes = 0UL;
                var totalNumberOfFreeBytes = 0UL;

                var success = Interop.Windows.GetDiskFreeSpaceExW(
                        path,
                        ref freeBytesAvailable,
                        ref totalNumberOfBytes,
                        ref totalNumberOfFreeBytes
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
                Logger.GetInstance(typeof(FileSystemManager)).Error("Can not get Windows disk free space: " + e.Message);
            }
            return result;
        }
    }
}
