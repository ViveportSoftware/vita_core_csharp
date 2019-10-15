using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Htc.Vita.Core.Crypto;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Net;

namespace Htc.Vita.Core.IO
{
    public static class FileVerifier
    {
        public static async Task<bool> VerifyAsync(string filePath, long size, string hash, HashAlgorithm hashAlgorithm, CancellationToken cancellationToken)
        {
            var fileInfo = new FileInfo(filePath);
            if (fileInfo.Exists)
            {
                if (fileInfo.Length == size)
                {
                    switch (hashAlgorithm)
                    {
                        case HashAlgorithm.Md5:

                            if (hash.Length == 32)
                            {
                                return hash == await Md5.GetInstance()
                                           .GenerateInHexAsync(fileInfo, cancellationToken)
                                           .ConfigureAwait(false);
                            }

                            if (hash.Length == 24)
                            {
                                return hash == await Md5.GetInstance()
                                           .GenerateInBase64Async(fileInfo, cancellationToken)
                                           .ConfigureAwait(false);
                            }

                            break;
                        case HashAlgorithm.Sha1:

                            if (hash.Length == 40)
                            {
                                return hash == await Sha1.GetInstance()
                                           .GenerateInHexAsync(fileInfo, cancellationToken)
                                           .ConfigureAwait(false);
                            }

                            if (hash.Length == 28)
                            {
                                return hash == await Sha1.GetInstance()
                                           .GenerateInBase64Async(fileInfo, cancellationToken)
                                           .ConfigureAwait(false);
                            }

                            break;
                        case HashAlgorithm.Sha256:

                            if (hash.Length == 64)
                            {
                                return hash == await Sha256.GetInstance()
                                           .GenerateInHexAsync(fileInfo, cancellationToken)
                                           .ConfigureAwait(false);
                            }

                            if (hash.Length == 44)
                            {
                                return hash == await Sha256.GetInstance()
                                           .GenerateInBase64Async(fileInfo, cancellationToken)
                                           .ConfigureAwait(false);
                            }

                            break;
                    }
                }
            }

            throw new NotSupportedException($"Not supported! path: {filePath} size: {size} hash: {hash} hashAlgorithm: {hashAlgorithm}");
        }

        public enum HashAlgorithm
        {
            Md5 = 0,
            Sha1 = 1,
            Sha256 = 2,
        }
    }
}
