using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class DxgiFactory : IDisposable
        {
            private readonly IDxgiFactory _dxgiFactory;

            internal DxgiFactory(IDxgiFactory dxgiFactory)
            {
                _dxgiFactory = dxgiFactory;
            }

            public void Dispose()
            {
                if (_dxgiFactory == null)
                {
                    return;
                }

                if (Marshal.IsComObject(_dxgiFactory))
                {
                    Marshal.ReleaseComObject(_dxgiFactory);
                }
            }

            internal List<DxgiAdapter> EnumerateAdapters()
            {
                var result = new List<DxgiAdapter>();
                if (_dxgiFactory == null)
                {
                    return result;
                }

                try
                {
                    var index = 0U;
                    while (true)
                    {
                        IDxgiAdapter iDxgiAdapter;
                        var dxgiError = _dxgiFactory.EnumAdapters(
                                index,
                                out iDxgiAdapter
                        );
                        if (dxgiError != DxgiError.SOk)
                        {
                            break;
                        }

                        result.Add(new DxgiAdapter(iDxgiAdapter));
                        index++;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(DxgiFactory)).Error($"Can not enumerate DXGI adapters. error: {e.Message}");
                }

                return result;
            }

            internal static DxgiFactory GetInstance()
            {
                IDxgiFactory iDxgiFactory;

                var guid = typeof(IDxgiFactory).GUID;
                var dxgiError = CreateDXGIFactory(
                        ref guid,
                        out iDxgiFactory
                );

                if (dxgiError != DxgiError.SOk)
                {
                    Logger.GetInstance(typeof(DxgiFactory)).Error($"Can not create DXGI factory. error: {dxgiError}");
                    return null;
                }

                return new DxgiFactory(iDxgiFactory);
            }
        }
    }
}
