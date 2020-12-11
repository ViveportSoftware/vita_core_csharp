using System.Collections.Generic;
using Htc.Vita.Core.Interop;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class BitsFileTransfer.
    /// Implements the <see cref="FileTransfer" />
    /// </summary>
    /// <seealso cref="FileTransfer" />
    public class BitsFileTransfer : FileTransfer
    {
        private readonly Windows.BitsManager _bitsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitsFileTransfer"/> class.
        /// </summary>
        public BitsFileTransfer()
        {
            _bitsManager = Windows.BitsManager.GetInstance();
        }

        /// <inheritdoc />
        protected override List<string> OnGetJobIdList()
        {
            return _bitsManager?.GetJobIdList();
        }
    }
}
