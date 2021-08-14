using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class LocationReport : IDisposable
        {
            private readonly PropertyKey _sensorDataTypeCountryRegion = new PropertyKey(SensorDataTypeLocationGuid, 28);

            private bool _disposed;
            private ILocationReport _locationReport;

            internal LocationReport(ILocationReport locationReport)
            {
                _locationReport = locationReport;
            }

            ~LocationReport()
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

                if (_locationReport == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_locationReport))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_locationReport);
#pragma warning restore CA1416
                }
                _locationReport = null;

                _disposed = true;
            }

            internal string GetCountryRegion()
            {
                return GetStringProperty(
                        _locationReport,
                        _sensorDataTypeCountryRegion
                );
            }

            private static string GetStringProperty(
                    ILocationReport locationReport,
                    PropertyKey propertyKey)
            {
                if (locationReport == null)
                {
                    return null;
                }

                using (var propVariant = new PropVariant())
                {
                    var hResult = locationReport.GetValue(
                            ref propertyKey,
                            propVariant
                    );
                    if (hResult == HResult.SOk)
                    {
                        return propVariant.GetValue() as string;
                    }

                    Logger.GetInstance(typeof(LocationReport)).Error($"Can not get string property for key: {propertyKey}. HResult: {hResult}");
                    return null;
                }
            }
        }
    }
}
