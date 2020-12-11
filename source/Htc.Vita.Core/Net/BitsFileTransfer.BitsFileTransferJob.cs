using Htc.Vita.Core.Interop;

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

            /// <summary>
            /// Initializes a new instance of the <see cref="BitsFileTransferJob"/> class.
            /// </summary>
            /// <param name="bitsJob">The bits job.</param>
            public BitsFileTransferJob(object bitsJob)
            {
                _bitsJob = bitsJob as Windows.BitsJob;
            }

            /// <inheritdoc />
            protected override bool OnAddItem(FileTransferItem item)
            {
                return _bitsJob?.AddFile(ConvertFrom(item)) ?? false;
            }

            /// <inheritdoc />
            protected override bool OnCancel()
            {
                return _bitsJob?.Cancel() ?? false;
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
            protected override string OnGetId()
            {
                return _bitsJob?.GetId();
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
        }
    }
}
