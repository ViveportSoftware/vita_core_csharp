using System.Collections.Generic;
using System.Linq;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        public partial class DefaultPreferences : Preferences
        {
            private Dictionary<string, string> _properties;
            private readonly Storage _storage;

            public DefaultPreferences(string category, string label) : base(label)
            {
                _storage = new Storage(category, Label);
            }

            protected override ICollection<string> OnAllKeys()
            {
                return _properties.Keys.ToList();
            }

            protected override Preferences OnClear()
            {
                _properties.Clear();
                return this;
            }

            protected override bool OnHasKey(string key)
            {
                return _properties.ContainsKey(key);
            }

            protected override Preferences OnInitialize()
            {
                _properties = _storage.LoadFromFile();
                return this;
            }

            protected override bool OnParseBool(string key, bool defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToBool(_properties[key], defaultValue);
            }

            protected override double OnParseDouble(string key, double defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToDouble(_properties[key], defaultValue);
            }

            protected override float OnParseFloat(string key, float defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return (float)Util.Convert.ToDouble(_properties[key], defaultValue);
            }

            protected override int OnParseInt(string key, int defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToInt32(_properties[key], defaultValue);
            }

            protected override long OnParseLong(string key, long defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return Util.Convert.ToInt64(_properties[key], defaultValue);
            }

            protected override string OnParseString(string key, string defaultValue)
            {
                if (!_properties.ContainsKey(key))
                {
                    return defaultValue;
                }
                return _properties[key] ?? defaultValue;
            }

            protected override Preferences OnPutBool(string key, bool value)
            {
                _properties[key] = "" + value;
                return this;
            }

            protected override Preferences OnPutDouble(string key, double value)
            {
                _properties[key] = "" + value;
                return this;
            }

            protected override Preferences OnPutFloat(string key, float value)
            {
                _properties[key] = "" + value;
                return this;
            }

            protected override Preferences OnPutInt(string key, int value)
            {
                _properties[key] = "" + value;
                return this;
            }

            protected override Preferences OnPutLong(string key, long value)
            {
                _properties[key] = "" + value;
                return this;
            }

            protected override Preferences OnPutString(string key, string value)
            {
                _properties[key] = value;
                return this;
            }

            protected override bool OnSave()
            {
                return _storage.SaveToFile(_properties);
            }

            public override string ToString()
            {
                return "DefaultPreferences";
            }
        }
    }
}
