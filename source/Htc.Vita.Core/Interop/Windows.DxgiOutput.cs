using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal partial class Windows
    {
        internal class DxgiOutput : IDisposable
        {
            private IDxgiOutput _dxgiOutput;
            private bool _disposed;

            internal DxgiOutput(IDxgiOutput dxgiOutput)
            {
                _dxgiOutput = dxgiOutput;
            }

            ~DxgiOutput()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (_disposed)
                {
                    return;
                }

                if (disposing)
                {
                    // Disposing managed resource
                }

                if (_dxgiOutput == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_dxgiOutput))
                {
                    Marshal.ReleaseComObject(_dxgiOutput);
                }
                _dxgiOutput = null;

                _disposed = true;
            }

            internal DxgiOutputDescription GetDescription()
            {
                if (_dxgiOutput == null)
                {
                    return new DxgiOutputDescription();
                }

                try
                {
                    DxgiOutputDescription result;
                    var dxgiError = _dxgiOutput.GetDesc(out result);
                    if (dxgiError == DxgiError.SOk)
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiOutput)).Error($"Can not get DXGI output description. error: {e.Message}");
                }

                return new DxgiOutputDescription();
            }
        }
    }
}
