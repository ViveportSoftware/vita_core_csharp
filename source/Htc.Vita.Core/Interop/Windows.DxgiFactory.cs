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
            private IDxgiFactory _dxgiFactory;
            private bool _disposed;

            internal DxgiFactory(IDxgiFactory dxgiFactory)
            {
                _dxgiFactory = dxgiFactory;
            }

            ~DxgiFactory()
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

                if (_dxgiFactory == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_dxgiFactory))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_dxgiFactory);
#pragma warning restore CA1416
                }
                _dxgiFactory = null;

                _disposed = true;
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
