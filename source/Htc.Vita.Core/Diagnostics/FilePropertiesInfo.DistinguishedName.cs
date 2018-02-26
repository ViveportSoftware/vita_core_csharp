using System;
using System.Collections.Generic;

namespace Htc.Vita.Core.Diagnostics
{
    public partial class FilePropertiesInfo
    {
        internal class DistinguishedName
        {
            private readonly List<KeyValuePair<string, string>> _pairs = new List<KeyValuePair<string, string>>();

            public string O
            {
                get
                {
                    foreach (var pair in _pairs)
                    {
                        if ("O".Equals(pair.Key))
                        {
                            return pair.Value;
                        }
                    }
                    return "";
                }
            }

            public DistinguishedName(string data)
            {
                var content = data;
                content = !string.IsNullOrWhiteSpace(content) ? content.Trim() : string.Empty;

                while (true)
                {
                    var equalIndex = content.IndexOf("=", StringComparison.Ordinal);
                    if (equalIndex <= 0)
                    {
                        break;
                    }

                    var escapedIndex = content.IndexOf("\\", StringComparison.Ordinal);
                    if (escapedIndex >= 0 && escapedIndex < equalIndex)
                    {
                        break;
                    }

                    var commaIndex = content.IndexOf(",", StringComparison.Ordinal);
                    if (commaIndex >= 0 && commaIndex < equalIndex)
                    {
                        break;
                    }

                    var key = content.Substring(0, equalIndex - 0).Trim();
                    string value;
                    if (commaIndex == escapedIndex + 1)
                    {
                        commaIndex = content.IndexOf(",", commaIndex + 1, StringComparison.Ordinal);
                    }
                    if (commaIndex < 0)
                    {
                        value = content.Substring(
                                Math.Min(
                                        equalIndex + 1,
                                        content.Length
                                )
                        ).Replace("\\", "");
                        _pairs.Add(new KeyValuePair<string, string>(key, value));
                        break;
                    }

                    value = content.Substring(
                            Math.Min(
                                    equalIndex + 1,
                                    content.Length
                            ),
                            commaIndex - equalIndex - 1
                    ).Replace("\\", "");
                    content = content.Substring(commaIndex + 1);
                    _pairs.Add(new KeyValuePair<string, string>(key, value));
                }
            }

            public static DistinguishedName Parse(string distinguishedName)
            {
                if (string.IsNullOrWhiteSpace(distinguishedName))
                {
                    return null;
                }
                return new DistinguishedName(distinguishedName);
            }
        }
    }
}
