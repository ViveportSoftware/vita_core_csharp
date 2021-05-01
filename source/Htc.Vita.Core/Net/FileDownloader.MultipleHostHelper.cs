using System;
using System.Collections.Generic;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Net
{
    partial class FileDownloader
    {
        internal class MultipleHostHelper
        {
            private List<string> _hostList;
            private int _index = -1;
            private int _retryCount;
            private int _maxRetryCount;

            public MultipleHostHelper(List<string> hostList, int maxRetryCount)
            {
                _hostList = hostList;
                _maxRetryCount = maxRetryCount;
            }

            public string GetNextUrl(string originalUrl)
            {
                _index++;

                if (_retryCount >= _maxRetryCount) return "";
                if (_index >= _hostList.Count)
                {
                    _index = -1;
                    _retryCount++;
                    return GetNextUrl(originalUrl);
                }
                try
                {
                    var uriStringFromNetMethodTargetField = _hostList[_index];
                    var uriDownload = new UriBuilder(uriStringFromNetMethodTargetField);
                    UriBuilder uri;

                    if (!originalUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) &&
                        !originalUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                    {
                        uri = new UriBuilder($"http://a.b.c/{originalUrl.TrimStart('/')}");
                    }
                    else
                    {
                        uri = new UriBuilder(originalUrl);
                    }

                    uriDownload.Path = uriDownload.Path.Equals("/")
                        ? uri.Path
                        : $"{uriDownload.Path.TrimEnd('/')}{uri.Path}";

                    var query = HttpUtilityLite.ParseQueryString(uri.Query);
                    uriDownload.Query = query.ToString();

                    return uriDownload.ToString();
                }
                catch (Exception exc)
                {
                    Logger.GetInstance(typeof(MultipleHostHelper)).Error($"Exception: {exc}");
                    return GetNextUrl(originalUrl);
                }
            }
        }
    }
}
