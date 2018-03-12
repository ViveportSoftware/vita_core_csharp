using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Net
{
    public abstract class WebRequestFactory
    {
        private static Dictionary<string, WebRequestFactory> Instances { get; } = new Dictionary<string, WebRequestFactory>();
        private static Type _defaultType = typeof(DefaultWebRequestFactory);

        private readonly Logger _logger;

        protected WebRequestFactory()
        {
            _logger = Logger.GetInstance();
        }

        public static void Register<T>() where T : WebRequestFactory
        {
            _defaultType = typeof(T);
            Logger.GetInstance().Info("Registered default web request factory type to " + _defaultType);
        }

        public static WebRequestFactory GetInstance()
        {
            WebRequestFactory instance;
            try
            {
                instance = DoGetInstance(_defaultType);
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error: " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultWebRequestFactory).FullName + "...");
                instance = new DefaultWebRequestFactory();
            }
            return instance;
        }

        public static WebRequestFactory GetInstance<T>() where T : WebRequestFactory
        {
            WebRequestFactory instance;
            try
            {
                instance = DoGetInstance(typeof(T));
            }
            catch (Exception e)
            {
                Logger.GetInstance().Fatal("Instance initialization error: " + e);
                Logger.GetInstance().Info("Initializing " + typeof(DefaultWebRequestFactory).FullName + "...");
                instance = new DefaultWebRequestFactory();
            }
            return instance;
        }

        private static WebRequestFactory DoGetInstance(Type type)
        {
            if (type == null)
            {
                throw new ArgumentException("Invalid arguments to get web request factory instance");
            }

            var key = type.FullName + "_";
            WebRequestFactory instance = null;
            if (Instances.ContainsKey(key))
            {
                instance = Instances[key];
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + key + "...");
                var constructor = type.GetConstructor(new Type[] { });
                if (constructor != null)
                {
                    instance = (WebRequestFactory)constructor.Invoke(new object[] { });
                }
            }
            if (instance == null)
            {
                Logger.GetInstance().Info("Initializing " + typeof(DefaultWebRequestFactory).FullName + "...");
                instance = new DefaultWebRequestFactory();
            }
            if (!Instances.ContainsKey(key))
            {
                Instances.Add(key, instance);
            }
            return instance;
        }

        public HttpWebRequest GetHttpWebRequest(Uri uri)
        {
            if (uri == null)
            {
                return null;
            }
            HttpWebRequest result = null;
            try
            {
                result = OnGetHttpWebRequest(uri);
            }
            catch (Exception e)
            {
                _logger.Error(e.ToString());
            }
            return result;
        }

        protected abstract HttpWebRequest OnGetHttpWebRequest(Uri uri);

        public class UserAgent
        {
            private const string Chrome43 = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/43.0.2357.130 Safari/537.36";
            private const string DefaultName = "Unknown";
            private const string Firefox35 = "Mozilla/5.0 (Windows NT 6.1; rv:35.0) Gecko/20100101 Firefox/35.0";
            private const string Msie11 = "Mozilla/5.0 (Windows NT 6.1; Trident/7.0; rv:11.0) like Gecko";

            private readonly StringBuilder _mandatorySection = new StringBuilder();
            private readonly StringBuilder _optionalSection = new StringBuilder();

            private static readonly Assembly ModuleAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();
            private static string _name = DefaultName;

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

            public UserAgent AsChrome()
            {
                _mandatorySection.Clear().Append(Chrome43);
                return this;
            }

            public UserAgent AsFirefox()
            {
                _mandatorySection.Clear().Append(Firefox35);
                return this;
            }

            public UserAgent AsIe()
            {
                _mandatorySection.Clear().Append(Msie11);
                return this;
            }

            public UserAgent Reset()
            {
                _mandatorySection.Clear();
                _optionalSection.Clear();
                return this;
            }

            public UserAgent Append(string part)
            {
                if (string.IsNullOrWhiteSpace(part))
                {
                    return this;
                }
                if (_optionalSection.Length > 0)
                {
                    _optionalSection.Append(" " + part + ";");
                }
                return this;
            }

            public override string ToString()
            {
                if (_mandatorySection.Length > 0)
                {
                    _mandatorySection.Append(" ");
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
                return _mandatorySection + " " + _optionalSection;
            }

            public static string GetModuleName()
            {
                return Name;
            }

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
}
