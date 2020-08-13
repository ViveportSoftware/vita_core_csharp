using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class DxgiAdapter : IDisposable
        {
            private readonly IDxgiAdapter _dxgiAdapter;

            internal DxgiAdapter(IDxgiAdapter dxgiAdapter)
            {
                _dxgiAdapter = dxgiAdapter;
            }

            public void Dispose()
            {
                if (_dxgiAdapter == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_dxgiAdapter))
                {
                    Marshal.ReleaseComObject(_dxgiAdapter);
                }
            }

            internal List<DxgiOutput> EnumerateOutputs()
            {
                var result = new List<DxgiOutput>();
                if (_dxgiAdapter == null)
                {
                    return result;
                }

                try
                {
                    var index = 0U;
                    while (true)
                    {
                        IDxgiOutput iDxgiOutput;
                        var dxgiError = _dxgiAdapter.EnumOutputs(
                                index,
                                out iDxgiOutput
                        );
                        if (dxgiError != DxgiError.SOk)
                        {
                            break;
                        }

                        result.Add(new DxgiOutput(iDxgiOutput));
                        index++;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiAdapter)).Error($"Can not enumerate DXGI outputs. error: {e.Message}");
                }

                return result;
            }

            internal DxgiAdapterDescription GetDescription()
            {
                if (_dxgiAdapter == null)
                {
                    return new DxgiAdapterDescription();
                }

                try
                {
                    DxgiAdapterDescription result;
                    var dxgiError = _dxgiAdapter.GetDesc(out result);
                    if (dxgiError == DxgiError.SOk)
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiAdapter)).Error($"Can not get DXGI adapter description. error: {e.Message}");
                }

                return new DxgiAdapterDescription();
            }
        }
    }
}
