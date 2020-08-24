using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Runtime
{
    /// <summary>
    /// Class Platform.
    /// </summary>
    public static partial class Platform
    {
        /// <summary>
        /// Gets a value indicating whether this instance is running on Android.
        /// </summary>
        /// <value><c>true</c> if this instance is running on Android; otherwise, <c>false</c>.</value>
        public static bool IsAndroid { get; } = CheckIsAndroid();

        /// <summary>
        /// Gets a value indicating whether this instance is running on .Net Core.
        /// </summary>
        /// <value><c>true</c> if this instance is is running on .Net Core; otherwise, <c>false</c>.</value>
        public static bool IsDotNetCore { get; } = CheckIsDotNetCore();

        /// <summary>
        /// Gets a value indicating whether this instance is running on Linux.
        /// </summary>
        /// <value><c>true</c> if this instance is running on Linux; otherwise, <c>false</c>.</value>
        public static bool IsLinux { get; } = CheckIsLinux();

        /// <summary>
        /// Gets a value indicating whether this instance is running on Mac OSX.
        /// </summary>
        /// <value><c>true</c> if this instance is running on Mac OSX; otherwise, <c>false</c>.</value>
        public static bool IsMacOsX { get; } = CheckIsMacOsX();

        /// <summary>
        /// Gets a value indicating whether this instance is running on Mono.
        /// </summary>
        /// <value><c>true</c> if this instance is running on Mono; otherwise, <c>false</c>.</value>
        public static bool IsMono { get; } = CheckIsMono();

        /// <summary>
        /// Gets a value indicating whether this instance is running on Unity.
        /// </summary>
        /// <value><c>true</c> if this instance is running on Unity; otherwise, <c>false</c>.</value>
        public static bool IsUnity { get; } = CheckIsUnity();

        /// <summary>
        /// Gets a value indicating whether this instance is running on Windows.
        /// </summary>
        /// <value><c>true</c> if this instance is running on Windows; otherwise, <c>false</c>.</value>
        public static bool IsWindows { get; } = CheckIsWindows();

        /// <summary>
        /// Gets a value indicating whether this instance is running on Android.
        /// </summary>
        /// <value><c>true</c> if this instance is running on Android; otherwise, <c>false</c>.</value>
        public static bool CheckIsAndroid()
        {
            // TODO should add more checking rule
#if UNITY_ANDROID
            return true;
#else
            return false;
#endif
        }

        /// <summary>
        /// Checks if the platform is .Net Core. See <see href="https://apisof.net/catalog/System.Runtime.Loader.AssemblyLoadContext"/>
        /// </summary>
        /// <returns><c>true</c> if the platform is .Net Core, <c>false</c> otherwise.</returns>
        public static bool CheckIsDotNetCore()
        {
            try
            {
                return System.Type.GetType("System.Runtime.Loader.AssemblyLoadContext") != null;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error($"Can not detect if process is running on .NET Core runtime: {e.Message}");
            }
            return false;
        }

        /// <summary>
        /// Checks if the platform is Linux.
        /// </summary>
        /// <returns><c>true</c> if the platform is Linux, <c>false</c> otherwise.</returns>
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

        /// <summary>
        /// Checks if the platform is Mac OSX
        /// </summary>
        /// <returns><c>true</c> if the platform is Mac OSX, <c>false</c> otherwise.</returns>
        public static bool CheckIsMacOsX()
        {
            if (!File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if the platform is Mono. See <see href="https://www.mono-project.com/docs/gui/winforms/porting-winforms-applications/"/>
        /// </summary>
        /// <returns><c>true</c> if the platform is Mono, <c>false</c> otherwise.</returns>
        public static bool CheckIsMono()
        {
            try
            {
                return System.Type.GetType("Mono.Runtime") != null;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error($"Can not detect if process is running on Mono runtime: {e.Message}");
            }
            return false;
        }

        /// <summary>
        /// Checks if the platform is Unity.
        /// </summary>
        /// <returns><c>true</c> if the platform is Unity, <c>false</c> otherwise.</returns>
        public static bool CheckIsUnity()
        {
            try
            {
                return System.Type.GetType("UnityEngine.Debug,UnityEngine") != null;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(Platform)).Error($"Can not detect if process is running on Unity: {e.Message}");
            }
            return false;
        }

        /// <summary>
        /// Checks if the platform is Windows.
        /// </summary>
        /// <returns><c>true</c> if the platform is Windows, <c>false</c> otherwise.</returns>
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
                Logger.GetInstance(typeof(Platform)).Error($"Unknown imageFileMachine: {imageFileMachine}");
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
            Debug.WriteLine($"[{qualifiedName}] {message}");
        }

        /// <summary>
        /// Detects the running operating system.
        /// </summary>
        /// <returns>Type.</returns>
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

        /// <summary>
        /// Detects the running operating system architecture.
        /// </summary>
        /// <returns>OsArch.</returns>
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

        /// <summary>
        /// Detects the running process architecture.
        /// </summary>
        /// <returns>ProcessArch.</returns>
        public static ProcessArch DetectProcessArch()
        {
            if (!IsWindows)
            {
                return ProcessArch.Unknown;
            }

            try
            {
                using (var processHandle = new Interop.Windows.SafeProcessHandle(
                        Process.GetCurrentProcess(),
                        false))
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
                        Logger.GetInstance(typeof(Platform)).Info($"processMachine: {processMachine}");
                        Logger.GetInstance(typeof(Platform)).Info($"nativeMachine: {nativeMachine}");
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
                Logger.GetInstance(typeof(Platform)).Error($"Can not detect process architecture: {e.Message}");
            }

            return IntPtr.Size == 8 ? ProcessArch.X64 : ProcessArch.X86;
        }

        /// <summary>
        /// Exits the platform
        /// </summary>
        /// <param name="exitType">Type of the exit.</param>
        public static void Exit(ExitType exitType)
        {
            if (IsWindows)
            {
                Windows.ExitInPlatform(exitType);
            }
        }

        /// <summary>
        /// Gets the epoch time.
        /// </summary>
        /// <returns>DateTime.</returns>
        public static DateTime GetEpochTime()
        {
            return new DateTime(
                    1970,
                    1,
                    1,
                    0,
                    0,
                    0,
                    DateTimeKind.Utc
            );
        }

        /// <summary>
        /// Gets the framework name.
        /// </summary>
        /// <returns>System.String.</returns>
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

        /// <summary>
        /// Gets the machine identifier.
        /// </summary>
        /// <returns>System.String.</returns>
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
            return Registry.GetStringValue(
                    Registry.Hive.LocalMachine,
                    "SOFTWARE\\Microsoft\\Cryptography",
                    "MachineGuid",
                    ""
            );
        }

        /// <summary>
        /// Gets the machine name.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetMachineName()
        {
            var result = "UNKNOWN-MACHINE-NAME";
            if (IsWindows)
            {
                result = Environment.MachineName;
            }
            return result;
        }

        /// <summary>
        /// Gets the maximum time in UTC.
        /// </summary>
        /// <returns>DateTime.</returns>
        public static DateTime GetMaxTimeUtc()
        {
            return new DateTime(
                    9999,
                    12,
                    31,
                    23,
                    59,
                    59,
                    999,
                    DateTimeKind.Utc
            ).Subtract(TimeSpan.FromHours(13)); //for GMT +13:00
        }

        /// <summary>
        /// Gets the minimum time in UTC.
        /// </summary>
        /// <returns>DateTime.</returns>
        public static DateTime GetMinTimeUtc()
        {
            return new DateTime(
                    0L,
                    DateTimeKind.Utc
            ).Add(TimeSpan.FromHours(12)); //for GMT -12:00
        }

        /// <summary>
        /// Gets the product name.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetProductName()
        {
            return Windows.GetProductNameInPlatform();
        }

        /// <summary>
        /// Gets the system boot time.
        /// </summary>
        /// <returns>DateTime.</returns>
        public static DateTime GetSystemBootTime()
        {
            return DateTime.Now.Subtract(TimeSpan.FromMilliseconds(Environment.TickCount));
        }

        /// <summary>
        /// Gets the system boot time in UTC.
        /// </summary>
        /// <returns>DateTime.</returns>
        public static DateTime GetSystemBootTimeUtc()
        {
            return DateTime.UtcNow.Subtract(TimeSpan.FromMilliseconds(Environment.TickCount));
        }

        /// <summary>
        /// Check if the current process is 32-bit and running on 64-bit operating system.
        /// </summary>
        /// <returns><c>true</c> if the current process is 32-bit and running on 64-bit operating system, <c>false</c> otherwise.</returns>
        private static bool Is32BitProcessOn64BitSystem()
        {
            if (!IsWindows)
            {
                return false;
            }
            try
            {
                using (var processHandle = new Interop.Windows.SafeProcessHandle(
                        Process.GetCurrentProcess(),
                        false))
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
                Logger.GetInstance(typeof(Platform)).Error($"Can not detect if process is 32-bit on 64-bit Windows: {e.Message}");
            }
            return false;
        }

        /// <summary>
        /// Loads the native library.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>NativeLibInfo.</returns>
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
            Trace.WriteLine($"[{qualifiedName}] {message}");
        }

        /// <summary>
        /// Enum ExitType
        /// </summary>
        public enum ExitType
        {
            /// <summary>
            /// Logs off this platform.
            /// </summary>
            Logoff,
            /// <summary>
            /// Shuts down this platform.
            /// </summary>
            Shutdown,
            /// <summary>
            /// Reboots this platform.
            /// </summary>
            Reboot
        }

        /// <summary>
        /// Enum OsArch
        /// </summary>
        public enum OsArch
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// 32-bit
            /// </summary>
            Bit32 = 1,
            /// <summary>
            /// 64-bit
            /// </summary>
            Bit64 = 2
        }

        /// <summary>
        /// Enum ProcessArch
        /// </summary>
        public enum ProcessArch
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// X86
            /// </summary>
            X86 = 1,
            /// <summary>
            /// X64
            /// </summary>
            X64 = 2,
            /// <summary>
            /// ARM32
            /// </summary>
            Arm = 3,
            /// <summary>
            /// ARM64
            /// </summary>
            Arm64 = 4
        }

        /// <summary>
        /// Enum Type
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// Unknown
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Windows
            /// </summary>
            Windows = 1,
            /// <summary>
            /// Linux
            /// </summary>
            Linux = 2,
            /// <summary>
            /// MacOSX
            /// </summary>
            MacOsX = 3
        }
    }
}