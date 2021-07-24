using System;
using System.IO;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.IO
{
    /// <summary>
    /// Class FileSystemManagerV2.
    /// </summary>
    public abstract partial class FileSystemManagerV2
    {
        static FileSystemManagerV2()
        {
            TypeRegistry.RegisterDefault<FileSystemManagerV2, DefaultFileSystemManagerV2>();
        }

        /// <summary>
        /// Registers this instance type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Register<T>()
                where T : FileSystemManagerV2, new()
        {
            TypeRegistry.Register<FileSystemManagerV2, T>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns>FileSystemManagerV2.</returns>
        public static FileSystemManagerV2 GetInstance()
        {
            return TypeRegistry.GetInstance<FileSystemManagerV2>();
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>FileSystemManagerV2.</returns>
        public static FileSystemManagerV2 GetInstance<T>()
                where T : FileSystemManagerV2, new()
        {
            return TypeRegistry.GetInstance<FileSystemManagerV2, T>();
        }

        /// <summary>
        /// Gets the disk space.
        /// </summary>
        /// <param name="directoryInfo">The directory information.</param>
        /// <returns>GetDiskSpaceResult.</returns>
        public GetDiskSpaceResult GetDiskSpace(DirectoryInfo directoryInfo)
        {
            if (directoryInfo == null)
            {
                return new GetDiskSpaceResult
                {
                        Status = GetDiskSpaceStatus.InvalidData
                };
            }

            var newDirectoryInfo = new DirectoryInfo(directoryInfo.ToString());
            if (!newDirectoryInfo.Exists)
            {
                return new GetDiskSpaceResult
                {
                        Status = GetDiskSpaceStatus.InvalidData
                };
            }

            GetDiskSpaceResult result = null;
            try
            {
                result = OnGetDiskSpace(newDirectoryInfo);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileSystemManagerV2)).Error(e.ToString());
            }
            return result ?? new GetDiskSpaceResult();
        }

        /// <summary>
        /// Verifies the path depth.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="depth">The depth.</param>
        /// <returns>VerifyPathDepthResult.</returns>
        public VerifyPathDepthResult VerifyPathDepth(
                DirectoryInfo basePath,
                int depth)
        {
            if (basePath == null || depth <= 0)
            {
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.InvalidData
                };
            }

            var newBasePath = new DirectoryInfo(basePath.ToString());
            if (!newBasePath.Exists)
            {
                return new VerifyPathDepthResult
                {
                        Status = VerifyPathDepthStatus.InvalidData
                };
            }

            VerifyPathDepthResult result = null;
            try
            {
                result = OnVerifyPathDepth(
                        basePath,
                        depth
                );
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(FileSystemManagerV2)).Error(e.ToString());
            }
            return result ?? new VerifyPathDepthResult();
        }

        /// <summary>
        /// Called when getting disk space.
        /// </summary>
        /// <param name="directoryInfo">The directory information.</param>
        /// <returns>GetDiskSpaceResult.</returns>
        protected abstract GetDiskSpaceResult OnGetDiskSpace(DirectoryInfo directoryInfo);
        /// <summary>
        /// Called when verifying path depth.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="depth">The depth.</param>
        /// <returns>VerifyPathDepthResult.</returns>
        protected abstract VerifyPathDepthResult OnVerifyPathDepth(
                DirectoryInfo basePath,
                int depth
        );
    }
}
