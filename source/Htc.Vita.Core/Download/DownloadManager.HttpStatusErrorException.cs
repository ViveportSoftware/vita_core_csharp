using System;
using System.Net;

namespace Htc.Vita.Core.Download
{
    partial class DownloadManager
    {
        public class HttpStatusErrorException : Exception
        {
            public HttpStatusCode HttpStatusCode { get; set; }

            public HttpStatusErrorException() : base() { }

            public HttpStatusErrorException(HttpStatusCode httpStatusCode) : base()
            {
                HttpStatusCode = httpStatusCode;
            }

            public HttpStatusErrorException(HttpStatusCode httpStatusCode, string message) : base(message)
            {
                HttpStatusCode = httpStatusCode;
            }
        }
    }
}
