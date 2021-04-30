using System.Collections.Generic;
using Htc.Vita.Core.Crypto;

namespace Htc.Vita.Core.Runtime
{
    public partial class DefaultSingleInstance
    {
        /// <summary>
        /// Class DefaultNamedPipeIpcChannel.
        /// </summary>
        public class DefaultNamedPipeIpcChannel
        {
            private const string SingleInstancePrefix = "SI";

            /// <summary>
            /// Class Client.
            /// Implements the <see cref="NamedPipeIpcChannel.Client" />
            /// </summary>
            /// <seealso cref="NamedPipeIpcChannel.Client" />
            public class Client : NamedPipeIpcChannel.Client
            {
                private readonly Dictionary<string, string> _translatedNameMap = new Dictionary<string, string>();

                /// <inheritdoc />
                protected override string OnOverrideTranslateName(string name)
                {
                    string translatedName = null;
                    if (_translatedNameMap.ContainsKey(name))
                    {
                        translatedName = _translatedNameMap[name];
                    }

                    if (!string.IsNullOrWhiteSpace(translatedName))
                    {
                        return translatedName;
                    }

                    var prefix = SingleInstancePrefix;
                    prefix = Sha1.GetInstance().GenerateInHex(prefix)?.Substring(0, 8) ?? prefix;
                    translatedName = $"{prefix}_{base.OnOverrideTranslateName(name)}";
                    _translatedNameMap[name] = translatedName;

                    return translatedName;
                }
            }

            /// <summary>
            /// Class Provider.
            /// Implements the <see cref="NamedPipeIpcChannel.Provider" />
            /// </summary>
            /// <seealso cref="NamedPipeIpcChannel.Provider" />
            public class Provider : NamedPipeIpcChannel.Provider
            {
                private readonly Dictionary<string, string> _translatedNameMap = new Dictionary<string, string>();

                /// <inheritdoc />
                protected override string OnOverrideTranslateName(string name)
                {
                    string translatedName = null;
                    if (_translatedNameMap.ContainsKey(name))
                    {
                        translatedName = _translatedNameMap[name];
                    }

                    if (!string.IsNullOrWhiteSpace(translatedName))
                    {
                        return translatedName;
                    }

                    var prefix = SingleInstancePrefix;
                    prefix = Sha1.GetInstance().GenerateInHex(prefix)?.Substring(0, 8) ?? prefix;
                    translatedName = $"{prefix}_{base.OnOverrideTranslateName(name)}";
                    _translatedNameMap[name] = translatedName;

                    return translatedName;
                }
            }
        }
    }
}
