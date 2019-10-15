using System;

namespace Htc.Vita.Core.Net
{
    partial class FileDownloader
    {
        public class SynchronousProgress<T> : IProgress<T>
        {
            private readonly Action<T> _callback;
            public SynchronousProgress(Action<T> callback) { _callback = callback; }
            void IProgress<T>.Report(T data) => _callback(data);
        }
    }
}
