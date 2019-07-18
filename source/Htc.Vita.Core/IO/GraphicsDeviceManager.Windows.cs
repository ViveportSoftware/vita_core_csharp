using System.Collections.Generic;

namespace Htc.Vita.Core.IO
{
    public static partial class GraphicsDeviceManager
    {
        internal static class Windows
        {
            internal static List<GraphicsAdapterInfo> GetGraphicsAdapterListInPlatform()
            {
                var result = new List<GraphicsAdapterInfo>();

                using (var dxgiFactory = Interop.Windows.DxgiFactory.GetInstance())
                {
                    if (dxgiFactory == null)
                    {
                        return result;
                    }

                    foreach (var dxgiAdapter in dxgiFactory.EnumerateAdapters())
                    {
                        if (dxgiAdapter == null)
                        {
                            continue;
                        }

                        using (dxgiAdapter)
                        {
                            var dxgiAdapterDescription = dxgiAdapter.GetDescription();

                            result.Add(new GraphicsAdapterInfo
                            {
                                    Description = dxgiAdapterDescription.description,
                                    DeviceId = ParseDeviceId(dxgiAdapterDescription.deviceId),
                                    RevisionId = ParseRevisionId(dxgiAdapterDescription.revision),
                                    SubsystemDeviceId = ParseSubsystemDeviceId(dxgiAdapterDescription.subSysId),
                                    SubsystemVendorId = ParseSubsystemVendorId(dxgiAdapterDescription.subSysId),
                                    VendorId = ParseVendorId(dxgiAdapterDescription.vendorId)
                            });
                        }
                    }
                }

                return result;
            }

            private static string ParseDeviceId(uint deviceId)
            {
                return deviceId.ToString("X4");
            }

            private static string ParseRevisionId(uint revisionId)
            {
                return revisionId.ToString("X2");
            }

            private static string ParseSubsystemDeviceId(uint subsystemId)
            {
                return subsystemId.ToString("X8").Substring(4, 4);
            }

            private static string ParseSubsystemVendorId(uint subsystemId)
            {
                return subsystemId.ToString("X8").Substring(0, 4);
            }

            private static string ParseVendorId(uint vendorId)
            {
                return vendorId.ToString("X4");
            }
        }
    }
}
