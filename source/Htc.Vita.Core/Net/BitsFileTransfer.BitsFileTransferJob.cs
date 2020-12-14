using System;
using System.Collections.Generic;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public partial class BitsFileTransfer
    {
        /// <summary>
        /// Class BitsFileTransferJob.
        /// Implements the <see cref="FileTransfer.FileTransferJob" />
        /// </summary>
        /// <seealso cref="FileTransfer.FileTransferJob" />
        public class BitsFileTransferJob : FileTransferJob
        {
            private readonly Windows.BitsJob _bitsJob;
            private Uri _lastRemotePath;

            /// <summary>
            /// Initializes a new instance of the <see cref="BitsFileTransferJob"/> class.
            /// </summary>
            /// <param name="bitsJob">The bits job.</param>
            public BitsFileTransferJob(object bitsJob)
            {
                _bitsJob = bitsJob as Windows.BitsJob;
            }

            /// <summary>
            /// Gets the inner instance.
            /// </summary>
            /// <returns>Windows.BitsJob.</returns>
            internal Windows.BitsJob GetInnerInstance()
            {
                return _bitsJob;
            }

            private static Uri GetProxyUri(Uri remoteUri)
            {
                var webProxyFactory = WebProxyFactory.GetInstance();
                var webProxy = webProxyFactory.GetWebProxy();
                var webProxyStatus = webProxyFactory.GetWebProxyStatus(webProxy);
                if (webProxyStatus != WebProxyFactory.WebProxyStatus.Working)
                {
                    return null;
                }

                if (remoteUri == null)
                {
                    return null;
                }

                return webProxy.GetProxy(remoteUri);
            }

            /// <inheritdoc />
            protected override bool OnAddItem(FileTransferItem item)
            {
                if (item == null)
                {
                    return false;
                }

                var remotePath = item.RemotePath;
                if (remotePath != null)
                {
                    _lastRemotePath = remotePath;
                }

                return _bitsJob?.AddFile(ConvertFrom(item)) ?? false;
            }

            /// <inheritdoc />
            protected override bool OnCancel()
            {
                return _bitsJob?.Cancel() ?? false;
            }

            /// <inheritdoc />
            protected override bool OnComplete()
            {
                return _bitsJob?.Complete() ?? false;
            }

            /// <inheritdoc />
            protected override void OnDispose()
            {
                _bitsJob?.Dispose();
            }

            /// <inheritdoc />
            protected override string OnGetDisplayName()
            {
                return _bitsJob?.GetDisplayName();
            }

            /// <inheritdoc />
            protected override FileTransferError OnGetError()
            {
                if (_bitsJob == null)
                {
                    return null;
                }

                using (var bitsError = _bitsJob.GetError())
                {
                    if (bitsError == null)
                    {
                        return null;
                    }

                    return new FileTransferError
                    {
                            ErrorDescription = bitsError.GetErrorDescription()
                    };
                }
            }

            /// <inheritdoc />
            protected override string OnGetId()
            {
                return _bitsJob?.GetId();
            }

            /// <inheritdoc />
            protected override List<FileTransferItem> OnGetItemList()
            {
                if (_bitsJob == null)
                {
                    return null;
                }

                using (var files = _bitsJob.GetFiles())
                {
                    if (files == null)
                    {
                        return null;
                    }

                    var result = new List<FileTransferItem>();
                    var fileCount = files.GetCount();
                    for (var i = 0u; i < fileCount; i++)
                    {
                        using (var file = files.GetFile(i))
                        {
                            if (file == null)
                            {
                                continue;
                            }

                            var item = ConvertFrom(file);
                            if (item == null)
                            {
                                continue;
                            }

                            result.Add(item);
                        }
                    }
                    return result;
                }
            }

            /// <inheritdoc />
            protected override FileTransferPriority OnGetPriority()
            {
                if (_bitsJob == null)
                {
                    return FileTransferPriority.Unknown;
                }

                return ConvertFrom(_bitsJob.GetPriority());
            }

            /// <inheritdoc />
            protected override FileTransferProgress OnGetProgress()
            {
                if (_bitsJob == null)
                {
                    return null;
                }

                return ConvertFrom(_bitsJob.GetProgress());
            }

            /// <inheritdoc />
            protected override FileTransferState OnGetState()
            {
                if (_bitsJob == null)
                {
                    return FileTransferState.Unknown;
                }

                return ConvertFrom(_bitsJob.GetState());
            }

            /// <inheritdoc />
            protected override FileTransferType OnGetTransferType()
            {
                if (_bitsJob == null)
                {
                    return FileTransferType.Unknown;
                }

                return ConvertFrom(_bitsJob.GetType());
            }

            /// <inheritdoc />
            protected override bool OnResume()
            {
                if (_bitsJob == null)
                {
                    return false;
                }

                var proxyUri = GetProxyUri(_lastRemotePath);
                if (proxyUri != null)
                {
                    Logger.GetInstance(typeof(BitsFileTransferJob)).Debug($"Try to use proxy: {proxyUri}");
                    var proxySettings = new Windows.BitsJobProxySettings
                    {
                            Usage = Windows.BitsJobProxyUsage.Override,
                            ProxyList = proxyUri.ToString().TrimEnd('/')
                    };
                    _bitsJob.SetProxySettings(proxySettings);
                }

                return _bitsJob.Resume();
            }

            /// <inheritdoc />
            protected override bool OnSetPriority(FileTransferPriority priority)
            {
                return _bitsJob?.SetPriority(ConvertFrom(priority)) ?? false;
            }

            /// <inheritdoc />
            protected override bool OnSuspend()
            {
                return _bitsJob?.Suspend() ?? false;
            }
        }
    }
}
