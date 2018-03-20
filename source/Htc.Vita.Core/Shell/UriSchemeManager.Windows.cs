using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace Htc.Vita.Core.Shell
{
    public partial class UriSchemeManager
    {
        public class Windows
        {
            internal static UriSchemeInfo GetSystemUriSchemeInPlatform(string schemeName)
            {
                var uriSchemeInfo = new UriSchemeInfo
                {
                        Name = schemeName
                };

                var realSchemeName = GetRealSchemeNameFromHkcu(schemeName);
                if (string.IsNullOrWhiteSpace(realSchemeName))
                {
                    realSchemeName = schemeName;
                }

                try
                {
                    using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.ClassesRoot, RegistryView.Default))
                    {
                        uriSchemeInfo.DefaultIcon = GetDefaultIconPath(baseKey, realSchemeName);
                        var commandPair = GetCommandPair(baseKey, realSchemeName);
                        if (!string.IsNullOrWhiteSpace(commandPair.Key))
                        {
                            uriSchemeInfo.CommandPath = commandPair.Key;
                            uriSchemeInfo.CommandParameter = commandPair.Value;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Can not get system URI scheme: " + e.Message);
                }
                return uriSchemeInfo;
            }

            private static string GetRealSchemeNameFromHkcu(string schemeName)
            {
                try
                {
                    using (var baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default))
                    {
                        var relativeKeyPath = "Software\\Microsoft\\Windows\\Shell\\Associations\\UrlAssociations\\" + schemeName + "\\UserChoice";
                        using (var subKey = baseKey.OpenSubKey(relativeKeyPath, RegistryKeyPermissionCheck.ReadSubTree))
                        {
                            const string valueName = "ProgId";
                            var value = subKey?.GetValue(valueName);
                            if (value == null)
                            {
                                return null;
                            }

                            if (subKey.GetValueKind(valueName) == RegistryValueKind.String)
                            {
                                return (string)value;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Can not get real scheme from HKCU: " + e.Message);
                }

                return null;
            }

            private static string GetDefaultIconPath(RegistryKey baseKey, string schemeName)
            {
                if (baseKey == null || string.IsNullOrWhiteSpace(schemeName))
                {
                    return null;
                }

                var relativeKeyPath = schemeName + "\\DefaultIcon";
                try
                {
                    using (var subKey = baseKey.OpenSubKey(relativeKeyPath, RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        var value = subKey?.GetValue(null);
                        if (value == null)
                        {
                            return null;
                        }

                        if (subKey.GetValueKind(null) == RegistryValueKind.String)
                        {
                            return (string)value;
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Can not get default icon path: " + e.Message);
                }

                return null;
            }

            private static KeyValuePair<string, string> GetCommandPair(RegistryKey baseKey, string schemeName)
            {
                var empty = new KeyValuePair<string, string>();
                if (baseKey == null || string.IsNullOrWhiteSpace(schemeName))
                {
                    return empty;
                }

                try
                {
                    var relativeKeyPath = schemeName + "\\Shell\\open\\command";
                    using (var subKey = baseKey.OpenSubKey(relativeKeyPath, RegistryKeyPermissionCheck.ReadSubTree))
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
                        if (subKey.GetValueKind(null) == RegistryValueKind.String)
                        {
                            data = (string)value;
                        }
                        var result = GetCommandPair(data);
                        if (!File.Exists(result.Key))
                        {
                            return empty;
                        }
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Log.Error("Can not get command pair: " + e.Message);
                }

                return empty;
            }

            private static KeyValuePair<string, string> GetCommandPair(string launcherCommand)
            {
                if (string.IsNullOrWhiteSpace(launcherCommand))
                {
                    return new KeyValuePair<string, string>();
                }

                if (launcherCommand.StartsWith("\"")) // separate by double quote
                {
                    var secondDoubleQuoteIndex = launcherCommand.IndexOf("\"", 1, StringComparison.Ordinal);
                    if (secondDoubleQuoteIndex < 0)
                    {
                        return new KeyValuePair<string, string>(launcherCommand.Substring(1), "");
                    }
                    return new KeyValuePair<string, string>(
                            launcherCommand.Substring(1, secondDoubleQuoteIndex - 1),
                            launcherCommand.Substring(secondDoubleQuoteIndex + 1)
                    );
                }

                var spaceIndex = launcherCommand.IndexOf(" ", StringComparison.Ordinal); // separate by space
                if (spaceIndex < 0)
                {
                    return new KeyValuePair<string, string>(launcherCommand, "");
                }
                return new KeyValuePair<string, string>(
                        launcherCommand.Substring(0, spaceIndex - 0),
                        launcherCommand.Substring(spaceIndex + 1)
                );
            }
        }
    }
}
