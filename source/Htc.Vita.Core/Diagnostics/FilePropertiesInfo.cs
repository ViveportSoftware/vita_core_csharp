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
    public partial class FilePropertiesInfo
    {
        private static readonly HashSet<string> CachedErrorPaths = new HashSet<string>();

        private const int ErrorPathCacheTimeInMilli = 1000 * 60 * 60;

        public string IssuerDistinguishedName { get; }
        public string IssuerName { get; }
        public string ProductVersion { get; }
        public string PublicKey { get; }
        public string SubjectDistinguishedName { get; }
        public string SubjectName { get; }
        public List<DateTime> TimestampList { get; } = new List<DateTime>();
        public bool Verified { get; }
        public string Version { get; }

        private FilePropertiesInfo(FileInfo fileInfo)
        {
            if (fileInfo == null)
            {
                return;
            }
            if (!fileInfo.Exists)
            {
                Logger.GetInstance(typeof(FilePropertiesInfo)).Warn("Can not find " + fileInfo.FullName + " to get properties");
                return;
            }

            X509Certificate certificate = null;
            try
            {
                certificate = X509Certificate.CreateFromSignedFile(fileInfo.FullName);
            }
            catch (Exception)
            {
                var key = Sha1.GetInstance().GenerateInHex(
                        fileInfo.FullName + "_" + Util.Convert.ToTimestampInMilli(DateTime.UtcNow) / ErrorPathCacheTimeInMilli
                );
                if (string.IsNullOrEmpty(key))
                {
                    Logger.GetInstance(typeof(FilePropertiesInfo)).Warn("Can not find certificate from file " + fileInfo.FullName);
                }
                else if (!CachedErrorPaths.Contains(key))
                {
                    Logger.GetInstance(typeof(FilePropertiesInfo)).Warn("Can not find certificate from file " + fileInfo.FullName);
                    CachedErrorPaths.Add(key);
                }
            }
            if (certificate != null)
            {
                IssuerDistinguishedName = certificate.Issuer;
                IssuerName = DistinguishedName.Parse(IssuerDistinguishedName).O;
                SubjectDistinguishedName = certificate.Subject;
                SubjectName = DistinguishedName.Parse(SubjectDistinguishedName).O;
                PublicKey = certificate.GetPublicKeyString();
                Verified = Authenticode.IsVerified(fileInfo);
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
                Logger.GetInstance(typeof(FilePropertiesInfo)).Warn("Can not find version from file " + fileInfo.FullName);
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
                Logger.GetInstance(typeof(FilePropertiesInfo)).Warn("Can not find product version from file " + fileInfo.FullName);
                ProductVersion = "0.0.0.0";
            }

            if (Verified)
            {
                TimestampList.AddRange(Authenticode.GetTimestampList(fileInfo));
            }
        }

        public static FilePropertiesInfo GetPropertiesInfo(FileInfo fileInfo)
        {
            return new FilePropertiesInfo(fileInfo);
        }
    }
}
