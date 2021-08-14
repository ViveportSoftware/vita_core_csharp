using System;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Interop
{
    internal static partial class Windows
    {
        internal class Location : IDisposable
        {
            private static readonly Guid ReportTypeICivicAddressReport = new Guid(ComInterfaceICivicAddressReport);
            private static readonly Guid ReportTypeILatLongReport = new Guid(ComInterfaceILatLongReport);

            private ILocation _location;
            private bool _disposed;

            internal Location(ILocation location)
            {
                _location = location;
            }

            ~Location()
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

                if (_location == null)
                {
                    return;
                }
                if (Marshal.IsComObject(_location))
                {
#pragma warning disable CA1416
                    Marshal.ReleaseComObject(_location);
#pragma warning restore CA1416
                }
                _location = null;

                _disposed = true;
            }

            internal static Location GetInstance()
            {
                var iLocation = Activator.CreateInstance(typeof(ClsidLocation)) as ILocation;
                if (iLocation != null)
                {
                    return new Location(iLocation);
                }

                Logger.GetInstance(typeof(Location)).Error($"Cannot create new {nameof(ILocation)}");
                return null;
            }

            internal LocationReport GetReport(LocationReportType reportType)
            {
                if (reportType == LocationReportType.Unknown)
                {
                    return null;
                }

                var reportTypeGuid = ToReportTypeGuid(reportType);
                ILocationReport report;
                var hResult = _location.GetReport(
                        ref reportTypeGuid,
                        out report
                );
                if (hResult == HResult.SOk)
                {
                    return new LocationReport(report);
                }

                Logger.GetInstance(typeof(Location)).Error($"Can not get report for report type: {reportType}. HResult: {hResult}");
                return null;
            }

            internal LocationReportStatus GetReportStatus(LocationReportType reportType)
            {
                if (reportType == LocationReportType.Unknown)
                {
                    return LocationReportStatus.NotSupported;
                }

                var reportTypeGuid = ToReportTypeGuid(reportType);
                LocationReportStatus status;
                var hResult = _location.GetReportStatus(
                        ref reportTypeGuid,
                        out status
                );
                if (hResult == HResult.SOk)
                {
                    return status;
                }
                if (hResult == HResult.EWin32NotSupported)
                {
                    return LocationReportStatus.NotSupported;
                }

                Logger.GetInstance(typeof(Location)).Error($"Can not get report status for report type: {reportType}. HResult: {hResult}");
                return LocationReportStatus.Error;
            }

            internal bool RequestPermission(LocationReportType reportType)
            {
                if (reportType == LocationReportType.Unknown)
                {
                    return false;
                }

                var reportTypeGuid = ToReportTypeGuid(reportType);
                var hResult = _location.RequestPermissions(
                        IntPtr.Zero,
                        ref reportTypeGuid,
                        1,
                        true
                );
                if (hResult == HResult.SOk)
                {
                    return true;
                }

                Logger.GetInstance(typeof(Location)).Error($"Can not request permission for report type: {reportType}. HResult: {hResult}");
                return false;
            }

            private static Guid ToReportTypeGuid(LocationReportType reportType)
            {
                if (reportType == LocationReportType.ICivicAddressReport)
                {
                    return ReportTypeICivicAddressReport;
                }
                if (reportType == LocationReportType.ILatLongReport)
                {
                    return ReportTypeILatLongReport;
                }

                return ReportTypeICivicAddressReport;
            }
        }
    }
}
