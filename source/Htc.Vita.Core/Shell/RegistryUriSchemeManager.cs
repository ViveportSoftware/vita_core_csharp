using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Htc.Vita.Core.Log;
using Htc.Vita.Core.Util;

namespace Htc.Vita.Core.Shell
{
    /// <summary>
    /// Class RegistryUriSchemeManager.
    /// Implements the <see cref="UriSchemeManager" />
    /// </summary>
    /// <seealso cref="UriSchemeManager" />
    [Obsolete("This class is obsoleted.")]
    public class RegistryUriSchemeManager : UriSchemeManager
    {
        private static readonly HashSet<string> ProtocolCommandPathWhitelist = new HashSet<string>();

        static RegistryUriSchemeManager()
        {
            InitWhitelist();
        }

        private static void InitWhitelist()
        {
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("http", "chrome.exe"));
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("http", "firefox.exe"));
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("http", "iexplore.exe"));
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("http", "launchwinapp.exe"));
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("https", "chrome.exe"));
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("https", "firefox.exe"));
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("https", "iexplore.exe"));
            ProtocolCommandPathWhitelist.Add(GetProtocolCommandPathKey("https", "launchwinapp.exe"));
        }

        private static string GetProtocolCommandPathKey(string protocol, string fileName)
        {
            if (string.IsNullOrWhiteSpace(protocol) || string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            return $"{protocol}_{fileName}";
        }

        /// <inheritdoc />
        protected override UriSchemeInfo OnGetSystemUriScheme(
                string schemeName,
                Dictionary<string, string> options)
        {
            var shouldAcceptWhitelistOnly = false;
            if (options.ContainsKey(OptionAcceptWhitelistOnly))
            {
                shouldAcceptWhitelistOnly = Util.Convert.ToBool(options[OptionAcceptWhitelistOnly]);
            }
            var uriSchemeInfo = new UriSchemeInfo
            {
                    Name = schemeName
            };

            var realSchemeName = GetRealSchemeNameFromHkcu(schemeName);
            if (string.IsNullOrWhiteSpace(realSchemeName))
            {
                realSchemeName = schemeName;
            }

            using (var baseKey = Win32Registry.Key.OpenBaseKey(
                    Win32Registry.Hive.ClassesRoot,
                    Win32Registry.View.Default))
            {
                var commandPair = GetCommandPair(
                        baseKey,
                        schemeName,
                        realSchemeName,
                        shouldAcceptWhitelistOnly
                );
                if (string.IsNullOrWhiteSpace(commandPair.Key))
                {
                    return uriSchemeInfo;
                }
                uriSchemeInfo.CommandPath = commandPair.Key;
                uriSchemeInfo.CommandParameter = commandPair.Value;
                uriSchemeInfo.DefaultIcon = GetDefaultIconPath(baseKey, realSchemeName);
            }
            return uriSchemeInfo;
        }

        private static string GetRealSchemeNameFromHkcu(string schemeName)
        {
            using (var baseKey = Win32Registry.Key.OpenBaseKey(
                    Win32Registry.Hive.CurrentUser,
                    Win32Registry.View.Default))
            {
                var relativeKeyPath = $"Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\{schemeName}\\UserChoice";
                using (var subKey = baseKey.OpenSubKey(
                        relativeKeyPath,
                        Win32Registry.KeyPermissionCheck.ReadSubTree))
                {
                    const string valueName = "ProgId";
                    var value = subKey?.GetValue(valueName);
                    if (value == null)
                    {
                        return null;
                    }

                    if (subKey.GetValueKind(valueName) == Win32Registry.ValueKind.String)
                    {
                        return (string)value;
                    }
                }
            }

            return null;
        }

        private static string GetDefaultIconPath(
                Win32Registry.Key baseKey,
                string schemeName)
        {
            if (baseKey == null || string.IsNullOrWhiteSpace(schemeName))
            {
                return null;
            }

            var relativeKeyPath = $"{schemeName}\\DefaultIcon";
            using (var subKey = baseKey.OpenSubKey(
                    relativeKeyPath,
                    Win32Registry.KeyPermissionCheck.ReadSubTree))
            {
                var value = subKey?.GetValue(null);
                if (value == null)
                {
                    return null;
                }

                if (subKey.GetValueKind(null) == Win32Registry.ValueKind.String)
                {
                    return (string)value;
                }
            }

            return null;
        }

        private static KeyValuePair<string, string> GetCommandPair(
                Win32Registry.Key baseKey,
                string schemeName,
                string realSchemeName,
                bool whitelistOnly)
        {
            var empty = new KeyValuePair<string, string>();
            if (baseKey == null || string.IsNullOrWhiteSpace(realSchemeName))
            {
                return empty;
            }

            var relativeKeyPath = $"{realSchemeName}\\Shell\\open\\command";
            using (var subKey = baseKey.OpenSubKey(
                    relativeKeyPath,
                    Win32Registry.KeyPermissionCheck.ReadSubTree))
            {
                if (subKey == null)
                {
                    return empty;
                }
                var value = subKey.GetValue(null);
                if (value == null)
                {
                    return empty;
                }
                var data = string.Empty;
                if (subKey.GetValueKind(null) == Win32Registry.ValueKind.String)
                {
                    data = (string)value;
                }
                var result = GetCommandPair(data);
                var commandPath = result.Key;
                if (!File.Exists(commandPath))
                {
                    return empty;
                }

                var key = GetProtocolCommandPathKey(
                        schemeName,
                        new FileInfo(commandPath).Name.ToLower(CultureInfo.InvariantCulture)
                );
                if (whitelistOnly && !ProtocolCommandPathWhitelist.Contains(key))
                {
                    Logger.GetInstance(typeof(RegistryUriSchemeManager)).Warn($"The command \"{commandPath}\" is not in {schemeName} whitelist");
                    return empty;
                }

                return result;
            }
        }

        private static KeyValuePair<string, string> GetCommandPair(string launcherCommand)
        {
            if (string.IsNullOrWhiteSpace(launcherCommand))
            {
                return new KeyValuePair<string, string>();
            }

            if (launcherCommand.StartsWith("\"")) // separate by double quote
            {
                var secondDoubleQuoteIndex = launcherCommand.IndexOf(
                        "\"",
                        1,
                        StringComparison.Ordinal
                );
                if (secondDoubleQuoteIndex < 0)
                {
                    return new KeyValuePair<string, string>(
                            launcherCommand.Substring(1),
                            ""
                    );
                }
                return new KeyValuePair<string, string>(
                        launcherCommand.Substring(1, secondDoubleQuoteIndex - 1),
                        launcherCommand.Substring(secondDoubleQuoteIndex + 1)
                );
            }

            var spaceIndex = launcherCommand.IndexOf(
                    " ",
                    StringComparison.Ordinal
            ); // separate by space
            if (spaceIndex < 0)
            {
                return new KeyValuePair<string, string>(
                        launcherCommand,
                        ""
                );
            }
            return new KeyValuePair<string, string>(
                    launcherCommand.Substring(0, spaceIndex - 0),
                    launcherCommand.Substring(spaceIndex + 1)
            );
        }
    }
}
