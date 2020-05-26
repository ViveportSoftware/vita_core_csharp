using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public static class NetworkInterface
    {
        public static IPAddress GetIPv4BroadcastAddress(
                IPAddress address,
                IPAddress subnetMask)
        {
            if (address == null || subnetMask == null)
            {
                return null;
            }

            var addressInBytes = address.GetAddressBytes();
            var subnetMaskInBytes = subnetMask.GetAddressBytes();
            if (addressInBytes.Length != subnetMaskInBytes.Length)
            {
                return null;
            }

            var broadcastAddressInBytes = new byte[addressInBytes.Length];
            for (var i = 0; i < broadcastAddressInBytes.Length; i++)
            {
                broadcastAddressInBytes[i] = (byte)(addressInBytes[i] | (subnetMaskInBytes[i] ^ 255));
            }
            return new IPAddress(broadcastAddressInBytes);
        }

        public static Dictionary<IPAddress, IPAddress> GetLocalIPv4AddressesWithBroadcastAddress()
        {
            var result = new Dictionary<IPAddress, IPAddress>();
            var addressesWithSubnetMask = GetLocalIPv4AddressesWithSubnetMask();
            foreach (var address in addressesWithSubnetMask.Keys)
            {
                if (address == null)
                {
                    continue;
                }

                var subnetMask = addressesWithSubnetMask[address];
                if (subnetMask == null)
                {
                    continue;
                }

                var broadcastAddress = GetIPv4BroadcastAddress(address, subnetMask);
                if (broadcastAddress == null)
                {
                    continue;
                }

                if (result.ContainsKey(address))
                {
                    Logger.GetInstance(typeof(NetworkInterface)).Warn($"Duplicate IP Address: {address}");
                    continue;
                }

                result.Add(address, broadcastAddress);
            }

            return result;
        }

        public static Dictionary<IPAddress, IPAddress> GetLocalIPv4AddressesWithSubnetMask()
        {
            var result = new Dictionary<IPAddress, IPAddress>();
            foreach (var networkInterface in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                var unicastAddresses = networkInterface?.GetIPProperties().UnicastAddresses;
                if (unicastAddresses == null)
                {
                    continue;
                }

                foreach (var unicastAddress in unicastAddresses)
                {
                    if (unicastAddress == null)
                    {
                        continue;
                    }

                    if (unicastAddress.Address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        continue;
                    }

                    if (result.ContainsKey(unicastAddress.Address))
                    {
                        Logger.GetInstance(typeof(NetworkInterface)).Warn($"Duplicate IP Address: {unicastAddress.Address}");
                        continue;
                    }

                    result.Add(
                            unicastAddress.Address,
                            unicastAddress.IPv4Mask
                    );
                }
            }

            return result;
        }

        public static bool IsInSameIPv4Subnet(
                IPAddress firstAddress,
                IPAddress secondAddress,
                IPAddress subnetMask)
        {
            var firstAddressInBytes = firstAddress.GetAddressBytes();
            var secondAddressInBytes = secondAddress.GetAddressBytes();
            var subnetMaskInBytes = subnetMask.GetAddressBytes();
            if (firstAddressInBytes.Length != secondAddressInBytes.Length || firstAddressInBytes.Length != subnetMaskInBytes.Length)
            {
                return false;
            }

            var firstSubnetInBytes = new byte[firstAddressInBytes.Length];
            var secondSubnetInBytes = new byte[secondAddressInBytes.Length];

            var result = true;
            for (var i = 0; i < subnetMaskInBytes.Length; i++)
            {
                firstSubnetInBytes[i] = (byte)(firstAddressInBytes[i] & subnetMaskInBytes[i]);
                secondSubnetInBytes[i] = (byte)(secondAddressInBytes[i] & subnetMaskInBytes[i]);

                if (firstSubnetInBytes[i] == secondSubnetInBytes[i])
                {
                    continue;
                }

                result = false;
            }

            var firstSubnet = new IPAddress(firstSubnetInBytes);
            if (result)
            {
                Logger.GetInstance(typeof(NetworkInterface)).Debug($"{firstAddress} and {secondAddress} are in the same subnet: {firstSubnet}");
            }

            return result;
        }

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
