using System.Diagnostics;
using System.IO;
using System.Reflection;
using Htc.Vita.Core.Runtime;

namespace Htc.Vita.Core.Net
{
    public partial class DefaultWebUserAgentV2Factory
    {
        /// <summary>
        /// Class DefaultWebUserAgentV2.
        /// Implements the <see cref="WebUserAgentV2" />
        /// </summary>
        /// <seealso cref="WebUserAgentV2" />
        public class DefaultWebUserAgentV2 : WebUserAgentV2
        {
            private static readonly Assembly ModuleAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

            private static string _moduleInstanceName;
            private static string _moduleVersion;

            private static string GetModuleInstanceNameFromAssembly()
            {
                if (ModuleAssembly == null)
                {
                    return null;
                }
                var location = ModuleAssembly.Location;
                if (string.IsNullOrWhiteSpace(location))
                {
                    return null;
                }
                var fileName = Path.GetFileName(location);
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    return null;
                }
                if (Platform.IsUnity && "Assembly-CSharp.dll".Equals(fileName))
                {
                    return null;
                }
                return fileName;
            }

            private static string GetModuleInstanceNameFromProcess()
            {
                using (var process = Process.GetCurrentProcess())
                {
                    return process.MainModule?.ModuleName;
                }
            }

            private static string GetModuleVersionFromAssembly()
            {
                if (ModuleAssembly == null)
                {
                    return null;
                }
                var version = ModuleAssembly.GetName().Version;
                if (version == null || "0.0.0.0".Equals(version.ToString()))
                {
                    return null;
                }
                return string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        "{0}.{1}.{2}.{3}",
                        version.Major,
                        version.Minor,
                        version.Build,
                        version.Revision
                );
            }

            private static string GetModuleVersionFromProcess()
            {
                using (var process = Process.GetCurrentProcess())
                {
                    return process.MainModule?.FileVersionInfo.FileVersion ?? FileVersionInfo.GetVersionInfo(process.ProcessName).ToString();
                }
            }

            /// <inheritdoc />
            protected override string OnGetModuleInstanceName()
            {
                return _moduleInstanceName ?? (_moduleInstanceName = GetModuleInstanceNameFromAssembly() ?? GetModuleInstanceNameFromProcess());
            }

            /// <inheritdoc />
            protected override string OnGetModuleName()
            {
                return Name;
            }

            /// <inheritdoc />
            protected override string OnGetModuleVersion()
            {
                return _moduleVersion ?? (_moduleVersion = GetModuleVersionFromAssembly() ?? GetModuleVersionFromProcess());
            }

            /// <summary>
            /// Called when overriding to string.
            /// </summary>
            /// <returns>System.String.</returns>
            protected virtual string OnOverrideToString()
            {
                return string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        "{0}/{1} ({2})",
                        GetModuleName(),
                        GetModuleVersion(),
                        GetModuleInstanceName()
                );
            }

            /// <inheritdoc />
            protected override string OnToString()
            {
                return OnOverrideToString();
            }
        }
    }
}
