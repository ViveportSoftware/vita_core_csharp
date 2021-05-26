using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class FilePropertiesInfo.
    /// </summary>
    public partial class FilePropertiesInfo
    {
        private static readonly HashSet<string> CachedErrorPaths = new HashSet<string>();

        private const int ErrorPathCacheTimeInMilli = 1000 * 60 * 60;

        /// <summary>
        /// Gets the inner instance.
        /// </summary>
        /// <value>The instance.</value>
        public FileInfo Instance { get; }
        /// <summary>
        /// Gets the distinguished name of the issuer.
        /// </summary>
        /// <value>The name of the issuer distinguished.</value>
        public string IssuerDistinguishedName { get; }
        /// <summary>
        /// Gets the name of the issuer.
        /// </summary>
        /// <value>The name of the issuer.</value>
        public string IssuerName { get; }
        /// <summary>
        /// Gets the product version.
        /// </summary>
        /// <value>The product version.</value>
        public string ProductVersion { get; }
        /// <summary>
        /// Gets the public key.
        /// </summary>
        /// <value>The public key.</value>
        public string PublicKey { get; }
        /// <summary>
        /// Gets the distinguished name of the subject.
        /// </summary>
        /// <value>The name of the subject distinguished.</value>
        public string SubjectDistinguishedName { get; }
        /// <summary>
        /// Gets the name of the subject.
        /// </summary>
        /// <value>The name of the subject.</value>
        public string SubjectName { get; }
        /// <summary>
        /// Gets the timestamp list.
        /// </summary>
        /// <value>The timestamp list.</value>
        public List<DateTime> TimestampList { get; } = new List<DateTime>();
        /// <summary>
        /// Gets a value indicating whether the file is verified.
        /// </summary>
        /// <value><c>true</c> if verified; otherwise, <c>false</c>.</value>
        public bool Verified { get; }
        /// <summary>
        /// Gets the version of the file.
        /// </summary>
        /// <value>The version.</value>
        public string Version { get; }

        private FilePropertiesInfo(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                return;
            }
            if (!fileInfo.Exists)
            {
                Logger.GetInstance(typeof(FilePropertiesInfo)).Warn($"Can not find {fileInfo.FullName} to get properties");
                return;
            }

            Instance = fileInfo;

            try
            {
                var certificate = X509Certificate.CreateFromSignedFile(fileInfo.FullName);
                IssuerDistinguishedName = certificate.Issuer;
                IssuerName = DistinguishedName.Parse(IssuerDistinguishedName).O;
                SubjectDistinguishedName = certificate.Subject;
                SubjectName = DistinguishedName.Parse(SubjectDistinguishedName).O;
                PublicKey = certificate.GetPublicKeyString();
                Verified = Authenticode.IsVerified(fileInfo);
            }
            catch (FileLoadException e)
            {
                Logger.GetInstance(typeof(FilePropertiesInfo)).Error($"Can not load module to find certificate from file {fileInfo.FullName}. exception: {e}");
            }
            catch (Exception)
            {
                var key = Sha1.GetInstance().GenerateInHex($"{fileInfo.FullName}_{Util.Convert.ToTimestampInMilli(DateTime.UtcNow) / ErrorPathCacheTimeInMilli}");
                if (string.IsNullOrEmpty(key))
                {
                    Logger.GetInstance(typeof(FilePropertiesInfo)).Warn($"Can not find certificate from file {fileInfo.FullName}");
                }
                else if (!CachedErrorPaths.Contains(key))
                {
                    Logger.GetInstance(typeof(FilePropertiesInfo)).Warn($"Can not find certificate from file {fileInfo.FullName}");
                    CachedErrorPaths.Add(key);
                }
            }

            var versionInfo = FileVersionInfo.GetVersionInfo(fileInfo.FullName);
            try
            {
                Version = string.Format(
                        CultureInfo.InvariantCulture,
                        @"{0}.{1}.{2}.{3}",
                        versionInfo.FileMajorPart,
                        versionInfo.FileMinorPart,
                        versionInfo.FileBuildPart,
                        versionInfo.FilePrivatePart
                );
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(FilePropertiesInfo)).Warn($"Can not find version from file {fileInfo.FullName}");
                Version = "0.0.0.0";
            }
            try
            {
                ProductVersion = string.Format(
                        CultureInfo.InvariantCulture,
                        @"{0}.{1}.{2}.{3}",
                        versionInfo.ProductMajorPart,
                        versionInfo.ProductMinorPart,
                        versionInfo.ProductBuildPart,
                        versionInfo.ProductPrivatePart
                );
            }
            catch (Exception)
            {
                Logger.GetInstance(typeof(FilePropertiesInfo)).Warn($"Can not find product version from file {fileInfo.FullName}");
                ProductVersion = "0.0.0.0";
            }

            if (Verified)
            {
                TimestampList.AddRange(Authenticode.GetTimestampList(fileInfo));
            }
        }

        /// <summary>
        /// Gets the properties information.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <returns>FilePropertiesInfo.</returns>
        public static FilePropertiesInfo GetPropertiesInfo(FileInfo fileInfo)
        {
            return new FilePropertiesInfo(fileInfo);
        }
    }
}
