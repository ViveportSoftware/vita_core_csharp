using System.IO;
using System.Reflection;
using System.Text;

namespace Htc.Vita.Core.Net
{
    /// <summary>
    /// Class WebUserAgent.
    /// </summary>
    public class WebUserAgent
    {
        private const string Chrome43 = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.130 Safari/537.36";
        private const string DefaultName = "Unknown";
        private const string Firefox35 = "Mozilla/5.0 (Windows NT 6.1; rv:35.0) Gecko/20100101 Firefox/35.0";
        private const string Msie11 = "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko";

        private readonly StringBuilder _mandatorySection = new StringBuilder();
        private readonly StringBuilder _optionalSection = new StringBuilder();

        private static readonly Assembly ModuleAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

        private static string _name = DefaultName;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public static string Name
        {
            get { return _name; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _name = value;
                }
            }
        }

        /// <summary>
        /// Act as Chrome.
        /// </summary>
        /// <returns>WebUserAgent.</returns>
        public WebUserAgent AsChrome()
        {
            _mandatorySection.Clear().Append(Chrome43);
            return this;
        }

        /// <summary>
        /// Act as Firefox.
        /// </summary>
        /// <returns>WebUserAgent.</returns>
        public WebUserAgent AsFirefox()
        {
            _mandatorySection.Clear().Append(Firefox35);
            return this;
        }

        /// <summary>
        /// Act as IE.
        /// </summary>
        /// <returns>WebUserAgent.</returns>
        public WebUserAgent AsIe()
        {
            _mandatorySection.Clear().Append(Msie11);
            return this;
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <returns>WebUserAgent.</returns>
        public WebUserAgent Reset()
        {
            _mandatorySection.Clear();
            _optionalSection.Clear();
            return this;
        }

        /// <summary>
        /// Appends the specified part.
        /// </summary>
        /// <param name="part">The part.</param>
        /// <returns>WebUserAgent.</returns>
        public WebUserAgent Append(string part)
        {
            if (string.IsNullOrWhiteSpace(part))
            {
                return this;
            }
            if (_optionalSection.Length > 0)
            {
                _optionalSection.Append($" {part};");
            }
            return this;
        }

        /// <summary>
        /// Returns a <see cref="string" /> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string" /> that represents this instance.</returns>
        public override string ToString()
        {
            if (_mandatorySection.Length > 0)
            {
                _mandatorySection.Append(' ');
            }
            var moduleName = GetModuleName();
            if (!string.IsNullOrWhiteSpace(moduleName))
            {
                _mandatorySection.Append(string.Format(
                        System.Globalization.CultureInfo.InvariantCulture,
                        "{0}/{1} ({2})",
                        moduleName,
                        GetModuleVersion(),
                        GetModuleInstanceName()
                ));
            }
            return $"{_mandatorySection} {_optionalSection}";
        }

        /// <summary>
        /// Gets the module name.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetModuleName()
        {
            return Name;
        }

        /// <summary>
        /// Gets the module version.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetModuleVersion()
        {
            var result = "0.0.0.0";
            if (ModuleAssembly == null)
            {
                return result;
            }
            var version = ModuleAssembly.GetName().Version;
            if (version == null)
            {
                return result;
            }
            result = string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    "{0}.{1}.{2}.{3}",
                    version.Major,
                    version.Minor,
                    version.Build,
                    version.Revision
            );
            return result;
        }

        /// <summary>
        /// Gets the module instance name.
        /// </summary>
        /// <returns>System.String.</returns>
        public static string GetModuleInstanceName()
        {
            var result = "UnknownInstance";
            if (ModuleAssembly == null)
            {
                return result;
            }
            var location = ModuleAssembly.Location;
            if (string.IsNullOrWhiteSpace(location))
            {
                return result;
            }
            var fileName = Path.GetFileName(location);
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return result;
            }
            result = fileName;
            return result;
        }
    }
}
