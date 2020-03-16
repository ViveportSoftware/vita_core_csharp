using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal partial class Windows
    {
        internal class DxgiOutput : IDisposable
        {
            private readonly IDxgiOutput _dxgiOutput;

            internal DxgiOutput(IDxgiOutput dxgiOutput)
            {
                _dxgiOutput = dxgiOutput;
            }

            public void Dispose()
            {
                if (_dxgiOutput == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_dxgiOutput))
                {
                    Marshal.ReleaseComObject(_dxgiOutput);
                }
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
                    Logger.GetInstance(typeof(DxgiOutput)).Error("Can not get DXGI output description. error: " + e.Message);
                }

                return new DxgiOutputDescription();
            }
        }
    }
}
