using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Interop;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    public static class Platform
    {
        public static bool IsLinux { get; } = CheckIsLinux();

        public static bool IsMacOsX { get; } = CheckIsMacOsX();

        public static bool IsWindows { get; } = CheckIsWindows();

        public class NativeLibInfo
        {
            public Exception InnerException { get; internal set; }
            public IntPtr Handle { get; internal set; }

            public bool IsNoError()
            {
                return (InnerException == null);
            }
        }

        public enum Type
        {
            Unknown = 0,
            Windows = 1,
            Linux = 2,
            MacOsX = 3
        }

        public enum OsArch
        {
            Unknown = 0,
            Bit32 = 1,
            Bit64 = 2
        }

        public static Type Detect()
        {
            if (CheckIsWindows())
            {
                DebugInternal("Platform is detected as Windows.");
                return Type.Windows;
            }
            if (CheckIsLinux())
            {
                DebugInternal("Platform is detected as Linux.");
                return Type.Linux;
            }
            if (CheckIsMacOsX())
            {
                DebugInternal("Platform is detected as MacOSX.");
                return Type.MacOsX;
            }
            DebugInternal("Platform is detected as unknown OS.");
            return Type.Unknown;
        }

        public static OsArch DetectOsArch()
        {
            if (IntPtr.Size == 8)
            {
                return OsArch.Bit64;
            }
            if (IntPtr.Size == 4 && IsWindows)
            {
                return Is32BitProcessOn64BitSystem() ? OsArch.Bit64 : OsArch.Bit32;
            }
            return OsArch.Unknown;
        }

        public static bool CheckIsMacOsX()
        {
            if (!File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
            {
                return false;
            }
            return true;
        }

        public static bool CheckIsLinux()
        {
            if (!File.Exists(@"/proc/sys/kernel/ostype"))
            {
                return false;
            }
            string osType = File.ReadAllText(@"/proc/sys/kernel/ostype");
            if (!osType.StartsWith("Linux", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static bool CheckIsWindows()
        {
            string windir = Environment.GetEnvironmentVariable("windir");
            if (string.IsNullOrEmpty(windir))
            {
                return false;
            }
            if (!windir.Contains(@"\"))
            {
                return false;
            }
            if (!Directory.Exists(windir))
            {
                return false;
            }
            return true;
        }

        private static bool Is32BitProcessOn64BitSystem()
        {
            if (!IsWindows)
            {
                return false;
            }
            try
            {
                var isWow64 = false;
                var sucess = Windows.IsWow64Process(
                        Process.GetCurrentProcess().Handle,
                        ref isWow64
                );

                if (sucess)
                {
                    return isWow64;
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error("Can not detect if process is 32-bit onder 64-bit Windows: " + e.Message);
            }
            return false;
        }

        public static NativeLibInfo LoadNativeLib(string path)
        {
            var info = new NativeLibInfo();
            if (string.IsNullOrEmpty(path))
            {
                TraceInternal("Native library path is empty.");
                info.InnerException = new ArgumentNullException(nameof(path));
                return info;
            }
            if (IsWindows)
            {
                info.Handle = Windows.LoadLibraryW(path);
                if (info.Handle == IntPtr.Zero)
                {
                    TraceInternal("Load Windows native library error.");
                    info.InnerException = new Win32Exception(Marshal.GetLastWin32Error());
                }
                return info;
            }
            TraceInternal("Do not support loading native library for this platform.");
            info.InnerException = new PlatformNotSupportedException();
            return info;
        }

        public static string GetMachineId()
        {
            string result = GetMachineGuidFromRegistry();
            if (!string.IsNullOrWhiteSpace(result))
            {
                return result;
            }
            return string.Empty;
        }

        private static string GetMachineGuidFromRegistry()
        {
            return Registry.GetStringValue(Registry.Hive.LocalMachine, "SOFTWARE\\Microsoft\\Cryptography", "MachineGuid", "");
        }

        private static void DebugInternal(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            var qualifiedName = typeof(Platform).AssemblyQualifiedName;
            if (string.IsNullOrWhiteSpace(qualifiedName))
            {
                return;
            }
            Debug.WriteLine("[" + qualifiedName + "] " + message);
        }

        private static void TraceInternal(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            var qualifiedName = typeof(Platform).AssemblyQualifiedName;
            if (string.IsNullOrWhiteSpace(qualifiedName))
            {
                return;
            }
            Trace.WriteLine("[" + qualifiedName + "] " + message);
        }
    }
}