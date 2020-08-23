using System.Collections.Generic;
using System.Linq;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        /// <summary>
        /// Class DefaultPreferences.
        /// Implements the <see cref="Preferences" />
        /// </summary>
        /// <seealso cref="Preferences" />
        public partial class DefaultPreferences : Preferences
        {
            private Dictionary<string, string> _properties;
            private readonly Storage _storage;

            /// <summary>
            /// Initializes a new instance of the <see cref="DefaultPreferences"/> class.
            /// </summary>
            /// <param name="category">The category.</param>
            /// <param name="label">The label.</param>
            public DefaultPreferences(
                    string category,
                    string label) : base(label)
            {
                _storage = new Storage(category, Label);
            }

            /// <inheritdoc />
            protected override ICollection<string> OnAllKeys()
            {
                return _properties.Keys.ToList();
            }

            /// <inheritdoc />
            protected override Preferences OnClear()
            {
                _properties.Clear();
                return this;
            }

            /// <inheritdoc />
            protected override bool OnHasKey(string key)
            {
                return _properties.ContainsKey(key);
            }

            /// <inheritdoc />
            protected override Preferences OnInitialize()
            {
                _properties = _storage.LoadFromFile();
                return this;
            }

            /// <inheritdoc />
            protected override bool OnParseBool(
                    string key,
                    bool defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToBool(
                        _properties[key],
                        defaultValue
                );
            }

            /// <inheritdoc />
            protected override double OnParseDouble(
                    string key,
                    double defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToDouble(
                        _properties[key],
                        defaultValue
                );
            }

            /// <inheritdoc />
            protected override float OnParseFloat(
                    string key,
                    float defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return (float)Util.Convert.ToDouble(
                        _properties[key],
                        defaultValue
                );
            }

            /// <inheritdoc />
            protected override int OnParseInt(
                    string key,
                    int defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToInt32(
                        _properties[key],
                        defaultValue
                );
            }

            /// <inheritdoc />
            protected override long OnParseLong(
                    string key,
                    long defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToInt64(
                        _properties[key],
                        defaultValue
                );
            }

            /// <inheritdoc />
            protected override string OnParseString(
                    string key,
                    string defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return _properties[key] ?? defaultValue;
            }

            /// <inheritdoc />
            protected override Preferences OnPutBool(
                    string key,
                    bool value)
            {
                _properties[key] = $"{value}";
                return this;
            }

            /// <inheritdoc />
            protected override Preferences OnPutDouble(
                    string key,
                    double value)
            {
                _properties[key] = $"{value}";
                return this;
            }

            /// <inheritdoc />
            protected override Preferences OnPutFloat(
                    string key,
                    float value)
            {
                _properties[key] = $"{value}";
                return this;
            }

            /// <inheritdoc />
            protected override Preferences OnPutInt(
                    string key,
                    int value)
            {
                _properties[key] = $"{value}";
                return this;
            }

            /// <inheritdoc />
            protected override Preferences OnPutLong(
                    string key,
                    long value)
            {
                _properties[key] = $"{value}";
                return this;
            }

            /// <inheritdoc />
            protected override Preferences OnPutString(
                    string key,
                    string value)
            {
                _properties[key] = value;
                return this;
            }

            /// <inheritdoc />
            protected override bool OnSave()
            {
                return _storage.SaveToFile(_properties);
            }

            /// <summary>
            /// Returns a <see cref="string" /> that represents this instance.
            /// </summary>
            /// <returns>A <see cref="string" /> that represents this instance.</returns>
            public override string ToString()
            {
                return "DefaultPreferences";
            }
        }
    }
}
