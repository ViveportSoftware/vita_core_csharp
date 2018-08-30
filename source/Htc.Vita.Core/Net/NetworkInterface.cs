using System;
using System.Reflection;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public static class NetworkInterface
    {
        public static bool IsNetworkAvailable()
        {
            return System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable();
        }

        public static bool IsInternetAvailable()
        {
            if (!IsNetworkAvailable())
            {
                return false;
            }

            var guid = new Guid("DCB00C01-570F-4A9B-8D69-199FDBA5723B");
            var type = Type.GetTypeFromCLSID(guid);
            if (type == null)
            {
                Logger.GetInstance(typeof(NetworkInterface)).Error("Can not find type class from system with CLSID: " + guid);
                return false;
            }

            object networkListManager = null;
            try
            {
                networkListManager = Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkInterface)).Error("Can not create networkListManager class from system with CLSID: " + guid + ", " + e.Message);
            }
            if (networkListManager == null)
            {
                return false;
            }

            object isConnectedToInternet = null;
            var success = false;
            try
            {
                isConnectedToInternet = type.InvokeMember(
                        "IsConnectedToInternet",
                        BindingFlags.GetProperty,
                        null,
                        networkListManager,
                        null
                );
                success = true;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkInterface)).Error("Can not get IsConnectedToInternet property from networkListManager: " + e.Message);
            }
            finally
            {
                Marshal.FinalReleaseComObject(networkListManager);
            }
            if (!success)
            {
                return false;
            }

            var result = false;
            try
            {
                result = (bool) isConnectedToInternet;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(NetworkInterface)).Error("Can not convert IsConnectedToInternet property: " + e.Message);
            }
            return result;
        }
    }
}
