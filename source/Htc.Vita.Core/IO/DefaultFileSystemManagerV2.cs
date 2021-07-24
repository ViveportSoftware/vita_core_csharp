using System;
using System.IO;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class DefaultFileSystemManagerV2.
    /// Implements the <see cref="FileSystemManagerV2" />
    /// </summary>
    /// <seealso cref="FileSystemManagerV2" />
    public class DefaultFileSystemManagerV2 : FileSystemManagerV2
    {
        private const int BasicWindowsFileNameLength = 8 + 1 + 3; // 8.3
        private const int MaxNtfsFolderNameLength = 255 - 1;      // removing null-terminated

        private static string[] GetSubPathArrayFrom(int depth)
        {
            var subPathArraySize = depth / (MaxNtfsFolderNameLength + 1) + 1;

            var pathArray = new string[subPathArraySize];
            for (var k = 0; k < subPathArraySize - 1; k++)
            {
                pathArray[k] = new string('a', MaxNtfsFolderNameLength);
            }
            pathArray[subPathArraySize - 1] = new string(
                    'a',
                    Math.Min(
                            depth % (MaxNtfsFolderNameLength + 1),
                            MaxNtfsFolderNameLength
                    )
            );
            return pathArray;
        }

        /// <inheritdoc />
        protected override GetDiskSpaceResult OnGetDiskSpace(DirectoryInfo directoryInfo)
        {
            if (!Platform.IsWindows)
            {
                return new GetDiskSpaceResult
                {
                        Status = GetDiskSpaceStatus.UnsupportedPlatform
                };
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

                if (success)
                {
                    return new GetDiskSpaceResult
                    {
                            DiskSpace = new DiskSpaceInfo
                            {
                                    FreeOfBytes = (long)freeBytesAvailable,
                                    Path = directoryInfo,
                                    TotalOfBytes = (long)totalNumberOfBytes,
                                    TotalFreeOfBytes = (long)totalNumberOfFreeBytes
                            },
                            Status = GetDiskSpaceStatus.Ok
                    };
                }

                Logger.GetInstance(typeof(DefaultFileSystemManagerV2)).Error($"Can not get disk space, error code: {Marshal.GetLastWin32Error()}");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultFileSystemManagerV2)).Error($"Can not get disk space: {e.Message}");
            }

            return new GetDiskSpaceResult();
        }

        /// <inheritdoc />
        [ExternalReference("https://docs.microsoft.com/en-us/archive/blogs/jeremykuhne/net-4-6-2-and-long-paths-on-windows-10")]
        protected override VerifyPathDepthResult OnVerifyPathDepth(
                DirectoryInfo basePath,
                int depth)
        {
            var subPathArray = GetSubPathArrayFrom(depth);
            var subPath = Path.Combine(subPathArray);
            var fullFilePathString = Path.Combine(
                    basePath.ToString(),
                    subPath
            );
            FileInfo fullFilePath;
            try
            {
                fullFilePath = new FileInfo(fullFilePathString);
                var parentPath = fullFilePath.Directory;
                if (parentPath != null && !parentPath.Exists)
                {
                    parentPath.Create();
                }
                using (fullFilePath.Create())
                {
                    // skip
                }
                var newFullFilePath = new FileInfo(fullFilePath.ToString());
                if (newFullFilePath.Exists)
                {
                    newFullFilePath.Delete();
                }
                if (subPathArray.Length > 1)
                {
                    Directory.Delete(
                            Path.Combine(
                                    basePath.ToString(),
                                    subPathArray[0]
                            ),
                            true
                    );
                }
            }
            catch (DirectoryNotFoundException)
            {
                Logger.GetInstance(typeof(DefaultFileSystemManagerV2)).Error($"Case 1: file path \"{fullFilePathString}\" ({fullFilePathString.Length}) is not found to create test file.");
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.Unsupported
                };
            }
            catch (PathTooLongException)
            {
                Logger.GetInstance(typeof(DefaultFileSystemManagerV2)).Error($"Case 1: file path \"{fullFilePathString}\" ({fullFilePathString.Length}) is too long to create test file.");
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.Unsupported
                };
            }
            catch (UnauthorizedAccessException)
            {
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.InsufficientPermission
                };
            }

            var leafNameLength = 1;
            if (depth > BasicWindowsFileNameLength + 1)
            {
                depth -= BasicWindowsFileNameLength + 1;
                leafNameLength = BasicWindowsFileNameLength;
            }
            subPathArray = GetSubPathArrayFrom(depth);
            subPath = Path.Combine(subPathArray);
            fullFilePathString = Path.Combine(
                    basePath.ToString(),
                    subPath,
                    new string('a', leafNameLength)
            );
            try
            {
                fullFilePath = new FileInfo(fullFilePathString);
                var parentPath = fullFilePath.Directory;
                if (parentPath != null && !parentPath.Exists)
                {
                    parentPath.Create();
                }
                using (fullFilePath.Create())
                {
                    // skip
                }
                var newFullFilePath = new FileInfo(fullFilePath.ToString());
                if (newFullFilePath.Exists)
                {
                    newFullFilePath.Delete();
                }
                if (subPathArray.Length > 0)
                {
                    Directory.Delete(
                            Path.Combine(
                                    basePath.ToString(),
                                    subPathArray[0]
                            ),
                            true
                    );
                }
            }
            catch (DirectoryNotFoundException)
            {
                Logger.GetInstance(typeof(DefaultFileSystemManagerV2)).Error($"Case 2: file path \"{fullFilePathString}\" ({fullFilePathString.Length}) is not found to create test file.");
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.Unsupported
                };
            }
            catch (PathTooLongException)
            {
                Logger.GetInstance(typeof(DefaultFileSystemManagerV2)).Error($"Case 2: file path \"{fullFilePathString}\" ({fullFilePathString.Length}) is too long to create test file.");
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.Unsupported
                };
            }
            catch (UnauthorizedAccessException)
            {
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.InsufficientPermission
                };
            }

            return new VerifyPathDepthResult
            {
                    Status = VerifyPathDepthStatus.Ok
            };
        }
    }
}
