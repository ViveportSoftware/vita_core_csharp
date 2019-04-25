using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    public static partial class Platform
    {
        public static bool IsDotNetCore { get; } = CheckIsDotNetCore();

        public static bool IsLinux { get; } = CheckIsLinux();

        public static bool IsMacOsX { get; } = CheckIsMacOsX();

        public static bool IsMono { get; } = CheckIsMono();

        public static bool IsWindows { get; } = CheckIsWindows();

        /**
         * https://apisof.net/catalog/System.Runtime.Loader.AssemblyLoadContext
         */
        public static bool CheckIsDotNetCore()
        {
            try
            {
                return System.Type.GetType("System.Runtime.Loader.AssemblyLoadContext") != null;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error("Can not detect if process is running on .NET Core runtime: " + e.Message);
            }
            return false;
        }

        public static bool CheckIsLinux()
        {
            if (!File.Exists(@"/proc/sys/kernel/ostype"))
            {
                return false;
            }
            var osType = File.ReadAllText(@"/proc/sys/kernel/ostype");
            if (!osType.StartsWith("Linux", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public static bool CheckIsMacOsX()
        {
            if (!File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
            {
                return false;
            }
            return true;
        }

        /**
         * https://www.mono-project.com/docs/gui/winforms/porting-winforms-applications/
         */
        public static bool CheckIsMono()
        {
            try
            {
                return System.Type.GetType("Mono.Runtime") != null;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error("Can not detect if process is running on Mono runtime: " + e.Message);
            }
            return false;
        }

        public static bool CheckIsWindows()
        {
            var windir = Environment.GetEnvironmentVariable("windir");
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

        private static ProcessArch ConvertFrom(Interop.Windows.ImageFileMachine imageFileMachine)
        {
            var result = ProcessArch.Unknown;
            if (imageFileMachine == Interop.Windows.ImageFileMachine.I386)
            {
                result = ProcessArch.X86;
            }
            if (imageFileMachine == Interop.Windows.ImageFileMachine.Amd64)
            {
                result = ProcessArch.X64;
            }
            if (imageFileMachine == Interop.Windows.ImageFileMachine.Arm)
            {
                result = ProcessArch.Arm;
            }
            if (imageFileMachine == Interop.Windows.ImageFileMachine.Arm64)
            {
                result = ProcessArch.Arm64;
            }
            if (result == ProcessArch.Unknown)
            {
                Logger.GetInstance(typeof(Platform)).Error("Unknown imageFileMachine: " + imageFileMachine);
            }
            return result;
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

        public static ProcessArch DetectProcessArch()
        {
            if (!IsWindows)
            {
                return ProcessArch.Unknown;
            }

            try
            {
                using (var processHandle = new Interop.Windows.SafeProcessHandle(Process.GetCurrentProcess()))
                {
                    var processMachine = Interop.Windows.ImageFileMachine.Unknown;
                    var nativeMachine = Interop.Windows.ImageFileMachine.Unknown;
                    var success = Interop.Windows.IsWow64Process2(
                            processHandle,
                            ref processMachine,
                            ref nativeMachine
                    );

                    if (success)
                    {
                        Logger.GetInstance(typeof(Platform)).Error("processMachine: " + processMachine);
                        Logger.GetInstance(typeof(Platform)).Error("nativeMachine: " + nativeMachine);
                        if (processMachine != Interop.Windows.ImageFileMachine.Unknown)
                        {
                            return ConvertFrom(processMachine);
                        }

                        return ConvertFrom(nativeMachine);
                    }
                }
            }
            catch (EntryPointNotFoundException)
            {
                Logger.GetInstance(typeof(Platform)).Warn("Can not use IsWow64Process2 to detect process architecture");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error("Can not detect process architecture: " + e.Message);
            }

            return IntPtr.Size == 8 ? ProcessArch.X64 : ProcessArch.X86;
        }

        public static void Exit(ExitType exitType)
        {
            if (IsWindows)
            {
                Windows.ExitInPlatform(exitType);
            }
        }

        public static string GetFrameworkName()
        {
            if (IsDotNetCore)
            {
                return "Unknown .NET Core framework";
            }
            if (IsMono)
            {
                return "Unknown Mono framework";
            }
            if (IsWindows)
            {
                return Windows.GetFrameworkNameInPlatform();
            }
            return "Unknown framework";
        }

        public static string GetMachineId()
        {
            var result = GetMachineGuidFromRegistry();
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

        public static string GetMachineName()
        {
            var result = "UNKNOWN-MACHINE-NAME";
            if (IsWindows)
            {
                result = Environment.MachineName;
            }
            return result;
        }

        public static string GetProductName()
        {
            return Windows.GetProductNameInPlatform();
        }

        public static DateTime GetSystemBootTime()
        {
            return DateTime.Now.Subtract(TimeSpan.FromMilliseconds(Environment.TickCount));
        }

        public static DateTime GetSystemBootTimeUtc()
        {
            return DateTime.UtcNow.Subtract(TimeSpan.FromMilliseconds(Environment.TickCount));
        }

        private static bool Is32BitProcessOn64BitSystem()
        {
            if (!IsWindows)
            {
                return false;
            }
            try
            {
                using (var processHandle = new Interop.Windows.SafeProcessHandle(Process.GetCurrentProcess()))
                {
                    var isWow64 = false;
                    var success = Interop.Windows.IsWow64Process(
                            processHandle,
                            ref isWow64
                    );

                    if (success)
                    {
                        return isWow64;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error("Can not detect if process is 32-bit on 64-bit Windows: " + e.Message);
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
                info.Handle = Interop.Windows.LoadLibraryW(path);
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

        public enum ExitType
        {
            Logoff,
            Shutdown,
            Reboot
        }

        public enum OsArch
        {
            Unknown = 0,
            Bit32 = 1,
            Bit64 = 2
        }

        public enum ProcessArch
        {
            Unknown = 0,
            X86 = 1,
            X64 = 2,
            Arm = 3,
            Arm64 = 4
        }

        public enum Type
        {
            Unknown = 0,
            Windows = 1,
            Linux = 2,
            MacOsX = 3
        }
    }
}