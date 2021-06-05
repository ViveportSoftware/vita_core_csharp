using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Runtime;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Diagnostics
{
    /// <summary>
    /// Class DefaultWebBrowserManager.
    /// Implements the <see cref="WebBrowserManager" />
    /// </summary>
    /// <seealso cref="WebBrowserManager" />
    public class DefaultWebBrowserManager : WebBrowserManager
    {
        private static readonly Dictionary<string, WebBrowserType> SchemeDisplayNameWithWebBrowserType = new Dictionary<string, WebBrowserType>();
        private static readonly List<WebBrowserType> WebBrowserTypeOrder = new List<WebBrowserType>();
        private static readonly Dictionary<WebBrowserType, string> WebBrowserTypeWithDisplayName = new Dictionary<WebBrowserType, string>();

        private static string _previousMicrosoftInternetExplorerVersion;
        private static bool _previousMicrosoftInternetExplorerVersionChanged;

        static DefaultWebBrowserManager()
        {
            InitKnownWebBrowsers();
        }

        private static FileInfo GetFullFilePath(string fileName)
        {
            if (File.Exists(fileName))
            {
                return new FileInfo(Path.GetFullPath(fileName));
            }

            var paths = Environment.GetEnvironmentVariable("PATH");
            if (string.IsNullOrWhiteSpace(paths))
            {
                Logger.GetInstance(typeof(WebBrowserManager)).Error("Can not find PATH environment variable");
                return null;
            }

            return (
                    from path in paths.Split(';')
                    select Path.Combine(path, fileName) into fullPath
                    where File.Exists(fullPath)
                    select new FileInfo(fullPath)
            ).FirstOrDefault();
        }

        private static List<WebBrowserInfo> GetInstalledWebBrowserListFromRegistryMicrosoftEdge()
        {
            var result = new List<WebBrowserInfo>();
            using (var baseKey = Win32Registry.Key.OpenBaseKey(Win32Registry.Hive.ClassesRoot, Win32Registry.View.Default))
            {
                using (var subKey = baseKey.OpenSubKey("microsoft-edge", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    if (subKey == null)
                    {
                        return result;
                    }

                    var explorerPath = GetFullFilePath("explorer.exe");
                    if (explorerPath == null)
                    {
                        return result;
                    }

                    Scheme[] targetSchemes = { Scheme.Http, Scheme.Https };
                    foreach (var targetScheme in targetSchemes)
                    {
                        const WebBrowserType webBrowserType = WebBrowserType.MicrosoftEdge;
                        result.Add(new WebBrowserInfo
                        {
                                DisplayName = GetKnownWebBrowserNameFrom(webBrowserType),
                                LaunchParams = new[] { "\"microsoft-edge:%1\"" },
                                LaunchPath = explorerPath,
                                SupportedScheme = targetScheme,
                                Type = webBrowserType
                        });
                    }
                }
            }

            return result;
        }

        private static List<WebBrowserInfo> GetInstalledWebBrowserListFromRegistryMicrosoftInternetExplorer()
        {
            var schemeList = new List<Scheme>
            {
                    Scheme.Http,
                    Scheme.Https
            };

            var result = new List<WebBrowserInfo>();
            using (var baseKey = Win32Registry.Key.OpenBaseKey(Win32Registry.Hive.LocalMachine, Win32Registry.View.Default))
            {
                using (var subKey = baseKey.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Capabilities\\UrlAssociations", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    if (subKey != null)
                    {
                        foreach (var scheme in schemeList)
                        {
                            var valueName = scheme.ToString().ToLowerInvariant();
                            var valueData = subKey.GetValue(valueName);
                            if (valueData == null)
                            {
                                continue;
                            }

                            if (subKey.GetValueKind(valueName) != Win32Registry.ValueKind.String)
                            {
                                continue;
                            }

                            var redirectedSchemeName = valueData as string;
                            var schemeDisplayName = GetSchemeDisplayNameFrom(redirectedSchemeName);
                            var launchCommand = GetSchemeLaunchCommandFrom(redirectedSchemeName);
                            var launchCommandTokenList = GetSchemeLaunchCommandTokenListFrom(launchCommand);

                            FileInfo launchCommandPath = null;
                            var launchCommandParam = new List<string>();
                            if (launchCommandTokenList.Count >= 1)
                            {
                                launchCommandPath = GetSchemeLaunchCommandPathFrom(launchCommandTokenList[0]);
                                launchCommandTokenList.RemoveAt(0);
                                launchCommandParam = launchCommandTokenList;
                            }
                            else
                            {
                                Logger.GetInstance(typeof(DefaultWebBrowserManager)).Error($"Can not parse launch command: \"{launchCommand}\"");
                            }

                            if (launchCommandPath == null)
                            {
                                continue;
                            }

                            var version = FilePropertiesInfo.GetPropertiesInfo(launchCommandPath).Version;
                            if (string.IsNullOrWhiteSpace(version))
                            {
                                Logger.GetInstance(typeof(DefaultWebBrowserManager)).Warn($"Can not detect Microsoft Internet Explorer version. Path: {launchCommandPath}");
                                continue;
                            }

                            if (!version.Equals(_previousMicrosoftInternetExplorerVersion))
                            {
                                Logger.GetInstance(typeof(DefaultWebBrowserManager)).Debug($"Microsoft Internet Explorer {version} detected");
                                _previousMicrosoftInternetExplorerVersion = version;
                                _previousMicrosoftInternetExplorerVersionChanged = true;
                            }
                            if (version.StartsWith("8.0"))
                            {
                                // Drop IE8 support
                                if (_previousMicrosoftInternetExplorerVersionChanged)
                                {
                                    Logger.GetInstance(typeof(DefaultWebBrowserManager)).Warn($"Skip adding Microsoft Internet Explorer {version} into available web browser list");
                                    _previousMicrosoftInternetExplorerVersionChanged = false;
                                }
                                continue;
                            }

                            var webBrowserType = GetKnownWebBrowserTypeFrom(schemeDisplayName);
                            result.Add(new WebBrowserInfo
                            {
                                    DisplayName = GetKnownWebBrowserNameFrom(webBrowserType),
                                    LaunchParams = launchCommandParam.ToArray(),
                                    LaunchPath = launchCommandPath,
                                    SchemeDisplayName = schemeDisplayName,
                                    SupportedScheme = scheme,
                                    Type = webBrowserType
                            });
                        }
                    }
                }

            }

            return result;
        }

        private static List<WebBrowserInfo> GetInstalledWebBrowserListFromRegistryStartMenuInternet()
        {
            var registryHiveList = new List<Win32Registry.Hive>
            {
                    Win32Registry.Hive.LocalMachine,
                    Win32Registry.Hive.CurrentUser
            };
            var schemeList = new List<Scheme>
            {
                    Scheme.Http,
                    Scheme.Https
            };

            var result = new List<WebBrowserInfo>();
            foreach (var registryHive in registryHiveList)
            {
                using (var baseKey = Win32Registry.Key.OpenBaseKey(registryHive, Win32Registry.View.Default))
                {
                    using (var subKey = baseKey.OpenSubKey("SOFTWARE\\Clients\\StartMenuInternet", Win32Registry.KeyPermissionCheck.ReadSubTree))
                    {
                        if (subKey != null)
                        {
                            foreach (var subKeyName in subKey.GetSubKeyNames())
                            {
                                if (string.IsNullOrWhiteSpace(subKeyName))
                                {
                                    continue;
                                }

                                using (var subKey2 = subKey.OpenSubKey($"{subKeyName}\\Capabilities\\URLAssociations"))
                                {
                                    if (subKey2 == null)
                                    {
                                        continue;
                                    }

                                    foreach (var scheme in schemeList)
                                    {
                                        var valueName = scheme.ToString().ToLowerInvariant();
                                        var valueData = subKey2.GetValue(valueName);
                                        if (valueData == null)
                                        {
                                            continue;
                                        }

                                        if (subKey2.GetValueKind(valueName) != Win32Registry.ValueKind.String)
                                        {
                                            continue;
                                        }

                                        var redirectedSchemeName = valueData as string;
                                        var schemeDisplayName = GetSchemeDisplayNameFrom(redirectedSchemeName);
                                        var launchCommand = GetSchemeLaunchCommandFrom(redirectedSchemeName);
                                        var launchCommandTokenList = GetSchemeLaunchCommandTokenListFrom(launchCommand);

                                        FileInfo launchCommandPath = null;
                                        var launchCommandParam = new List<string>();
                                        if (launchCommandTokenList.Count >= 1)
                                        {
                                            launchCommandPath = GetSchemeLaunchCommandPathFrom(launchCommandTokenList[0]);
                                            launchCommandTokenList.RemoveAt(0);
                                            launchCommandParam = launchCommandTokenList;
                                        }
                                        else
                                        {
                                            Logger.GetInstance(typeof(DefaultWebBrowserManager)).Error($"Can not parse launch command: \"{launchCommand}\"");
                                        }

                                        if (launchCommandPath == null)
                                        {
                                            continue;
                                        }

                                        var webBrowserType = GetKnownWebBrowserTypeFrom(schemeDisplayName);
                                        result.Add(new WebBrowserInfo
                                        {
                                                DisplayName = GetKnownWebBrowserNameFrom(webBrowserType),
                                                LaunchParams = launchCommandParam.ToArray(),
                                                LaunchPath = launchCommandPath,
                                                SchemeDisplayName = schemeDisplayName,
                                                SupportedScheme = scheme,
                                                Type = webBrowserType
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        private static string GetKnownWebBrowserNameFrom(WebBrowserType webBrowserType)
        {
            if (WebBrowserTypeWithDisplayName.ContainsKey(webBrowserType))
            {
                return WebBrowserTypeWithDisplayName[webBrowserType];
            }

            Logger.GetInstance(typeof(DefaultWebBrowserManager)).Warn($"Can not find known web browser name for {webBrowserType}");
            return "Unknown";
        }

        private static WebBrowserType GetKnownWebBrowserTypeFrom(string schemeDisplayName)
        {
            if (SchemeDisplayNameWithWebBrowserType.ContainsKey(schemeDisplayName))
            {
                return SchemeDisplayNameWithWebBrowserType[schemeDisplayName];
            }

            Logger.GetInstance(typeof(DefaultWebBrowserManager)).Warn($"Can not find known WebBrowserType for \"{schemeDisplayName}\"");
            return WebBrowserType.Unknown;
        }

        private static string GetSchemeDisplayNameFrom(string schemeName)
        {
            var result = string.Empty;
            if (string.IsNullOrWhiteSpace(schemeName))
            {
                return result;
            }

            using (var baseKey = Win32Registry.Key.OpenBaseKey(Win32Registry.Hive.ClassesRoot, Win32Registry.View.Default))
            {
                using (var subKey = baseKey.OpenSubKey(schemeName, Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    if (subKey == null)
                    {
                        return result;
                    }

                    try
                    {
                        if (subKey.GetValueKind(null) == Win32Registry.ValueKind.String)
                        {
                            result = subKey.GetValue(null) as string;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(DefaultWebBrowserManager)).Warn($"Can not find display name from default string for: \"{schemeName}\", {e.Message}");
                    }

                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        return result;
                    }

                    const string valueName = "FriendlyTypeName";
                    try
                    {
                        if (subKey.GetValueKind(valueName) == Win32Registry.ValueKind.String)
                        {
                            result = subKey.GetValue(valueName) as string;
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.GetInstance(typeof(DefaultWebBrowserManager)).Warn($"Can not find display name from FriendlyTypeName string for: \"{schemeName}\", {e.Message}");
                    }
                }
            }

            return result;
        }

        private static string GetSchemeLaunchCommandFrom(string schemeName)
        {
            var result = string.Empty;
            if (string.IsNullOrWhiteSpace(schemeName))
            {
                return result;
            }

            using (var baseKey = Win32Registry.Key.OpenBaseKey(Win32Registry.Hive.ClassesRoot, Win32Registry.View.Default))
            {
                using (var subKey = baseKey.OpenSubKey($"{schemeName}\\shell\\open\\command", Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    if (subKey == null)
                    {
                        return result;
                    }

                    if (subKey.GetValueKind(null) == Win32Registry.ValueKind.String)
                    {
                        result = subKey.GetValue(null) as string;
                    }
                }
            }

            return result;
        }

        private static FileInfo GetSchemeLaunchCommandPathFrom(string stringPath)
        {
            if (string.IsNullOrWhiteSpace(stringPath))
            {
                return null;
            }

            const string quotation = "\"";
            var executablePath = stringPath;
            if (executablePath.StartsWith(quotation))
            {
                executablePath = executablePath.Substring(quotation.Length);
            }
            if (executablePath.EndsWith(quotation))
            {
                executablePath = executablePath.Substring(0, executablePath.Length - quotation.Length);
            }

            try
            {
                var path = new FileInfo(executablePath);
                if (path.Exists)
                {
                    return path;
                }
                Logger.GetInstance(typeof(DefaultWebBrowserManager)).Error($"Can not find existing launch command path: \"{executablePath}\"");
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebBrowserManager)).Error($"Can not parse launch command path: \"{stringPath}\", {e}");
            }

            return null;
        }

        private static List<string> GetSchemeLaunchCommandTokenListFrom(string schemeLaunchCommand)
        {
            var result = new List<string>();
            var builder = new StringBuilder();
            var charArray = schemeLaunchCommand.Trim().ToCharArray();
            var isInQuote = false;
            foreach (var c in charArray)
            {
                if (c == '"')
                {
                    builder.Append(c);
                    if (isInQuote)
                    {
                        result.Add(builder.ToString());
                        builder.Clear();
                    }
                    isInQuote = !isInQuote;
                }
                else if (c == ' ')
                {
                    if (isInQuote)
                    {
                        builder.Append(c);
                    }
                    else
                    {
                        if (builder.Length <= 0)
                        {
                            continue;
                        }
                        result.Add(builder.ToString());
                        builder.Clear();
                    }
                }
                else
                {
                    builder.Append(c);
                }
            }

            if (builder.Length > 0)
            {
                result.Add(builder.ToString());
                builder.Clear();
            }

            return result;
        }

        private static void InitKnownWebBrowsers()
        {
            WebBrowserTypeOrder.Add(WebBrowserType.GoogleChrome);
            WebBrowserTypeOrder.Add(WebBrowserType.MozillaFirefox);
            WebBrowserTypeOrder.Add(WebBrowserType.Opera);
            WebBrowserTypeOrder.Add(WebBrowserType.Qihoo360ExtremeBrowser);
            WebBrowserTypeOrder.Add(WebBrowserType.Qihoo360SafeBrowser);
            WebBrowserTypeOrder.Add(WebBrowserType.MicrosoftEdgeChromium);
            WebBrowserTypeOrder.Add(WebBrowserType.MicrosoftEdge);
            WebBrowserTypeOrder.Add(WebBrowserType.MicrosoftInternetExplorer);

            WebBrowserTypeWithDisplayName.Add(WebBrowserType.GoogleChrome, "Google Chrome");
            WebBrowserTypeWithDisplayName.Add(WebBrowserType.MozillaFirefox, "Mozilla Firefox");
            WebBrowserTypeWithDisplayName.Add(WebBrowserType.MicrosoftEdge, "Microsoft Edge");
            WebBrowserTypeWithDisplayName.Add(WebBrowserType.MicrosoftEdgeChromium, "Microsoft Edge (Chromium-based)");
            WebBrowserTypeWithDisplayName.Add(WebBrowserType.MicrosoftInternetExplorer, "Microsoft Internet Explorer");
            WebBrowserTypeWithDisplayName.Add(WebBrowserType.Opera, "Opera");
            WebBrowserTypeWithDisplayName.Add(WebBrowserType.Qihoo360ExtremeBrowser, "Qihoo 360 Extreme Browser");
            WebBrowserTypeWithDisplayName.Add(WebBrowserType.Qihoo360SafeBrowser, "Qihoo 360 Safe Browser");

            SchemeDisplayNameWithWebBrowserType.Add("360 Chrome HTML Document", WebBrowserType.Qihoo360ExtremeBrowser);
            SchemeDisplayNameWithWebBrowserType.Add("360 se HTML Document", WebBrowserType.Qihoo360SafeBrowser);
            SchemeDisplayNameWithWebBrowserType.Add("Chrome HTML Document", WebBrowserType.GoogleChrome);
            SchemeDisplayNameWithWebBrowserType.Add("Firefox URL", WebBrowserType.MozillaFirefox);
            SchemeDisplayNameWithWebBrowserType.Add("Microsoft Edge HTML Document", WebBrowserType.MicrosoftEdgeChromium);
            SchemeDisplayNameWithWebBrowserType.Add("Opera Web Document", WebBrowserType.Opera);
            SchemeDisplayNameWithWebBrowserType.Add("URL:HyperText Transfer Protocol", WebBrowserType.MicrosoftInternetExplorer);
            SchemeDisplayNameWithWebBrowserType.Add("URL:HyperText Transfer Protocol with Privacy", WebBrowserType.MicrosoftInternetExplorer);
        }

        /// <inheritdoc />
        protected override GetInstalledWebBrowserListResult OnGetInstalledWebBrowserList()
        {
            if (!Platform.IsWindows)
            {
                return new GetInstalledWebBrowserListResult
                {
                        Status = GetInstalledWebBrowserListStatus.UnsupportedPlatform
                };
            }

            var unorderedWebBrowserList = GetInstalledWebBrowserListFromRegistryStartMenuInternet();
            unorderedWebBrowserList.AddRange(GetInstalledWebBrowserListFromRegistryMicrosoftEdge());
            unorderedWebBrowserList.AddRange(GetInstalledWebBrowserListFromRegistryMicrosoftInternetExplorer());

            var countedWebBrowserSet = new HashSet<WebBrowserInfo>();
            var webBrowserList = new List<WebBrowserInfo>();
            foreach (var webBrowserType in WebBrowserTypeOrder)
            {
                foreach (var detectedWebBrowserInfo in unorderedWebBrowserList)
                {
                    if (detectedWebBrowserInfo.Type != webBrowserType)
                    {
                        continue;
                    }

                    countedWebBrowserSet.Add(detectedWebBrowserInfo);
                    webBrowserList.Add(detectedWebBrowserInfo);
                }
            }

            foreach (var detectedWebBrowserInfo in unorderedWebBrowserList)
            {
                if (countedWebBrowserSet.Contains(detectedWebBrowserInfo))
                {
                    continue;
                }

                webBrowserList.Add(detectedWebBrowserInfo);
            }

            return new GetInstalledWebBrowserListResult
            {
                    Status = GetInstalledWebBrowserListStatus.Ok,
                    WebBrowserList = webBrowserList
            };
        }

        /// <inheritdoc />
        protected override bool OnLaunch(
                Uri uri,
                WebBrowserInfo webBrowserInfo)
        {
            var arguments = new StringBuilder();
            foreach (var param in webBrowserInfo.LaunchParams)
            {
                arguments.Append(" ").Append(param.Replace("%1", uri.AbsoluteUri));
            }

            var processStartInfo = new ProcessStartInfo
            {
                    Arguments = arguments.ToString(),
                    CreateNoWindow = true,
                    FileName = webBrowserInfo.LaunchPath.FullName
            };
            try
            {
                using (Process.Start(processStartInfo))
                {
                    // Skip
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.GetInstance(typeof(DefaultWebBrowserManager)).Error($"Can not launch uri: \"{uri} with \"{webBrowserInfo.Type}\", {e.Message}");
            }
            return false;
        }
    }
}
