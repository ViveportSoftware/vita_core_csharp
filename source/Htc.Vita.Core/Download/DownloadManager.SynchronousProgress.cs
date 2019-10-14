using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Htc.Vita.Core.Download
{
    partial class DownloadManager
    {
        public class SynchronousProgress<T> : IProgress<T>
        {
            private readonly Action<T> _callback;
            public SynchronousProgress(Action<T> callback) { _callback = callback; }
            void IProgress<T>.Report(T data) => _callback(data);
        }
    }
}
