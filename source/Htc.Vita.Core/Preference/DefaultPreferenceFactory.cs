using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    public class DefaultPreferenceFactory : PreferenceFactory
    {
        protected override Preferences OnLoadPreferences(string label)
        {
            return new DefaultPreferences("", label);
        }

        public class DefaultPreferences : Preferences
        {
            private readonly Dictionary<string, string> _properties;
            private readonly Storage _storage;

            public DefaultPreferences(string category, string label) : base(label)
            {
                _storage = new Storage(category, Label);
                _properties = _storage.LoadFromFile();
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

            public class Storage
            {
                private readonly Logger _logger;
                private readonly string _path;

                public Storage(string category, string label)
                {
                    _logger = Logger.GetInstance();
                    category = !string.IsNullOrWhiteSpace(category) ? category : "Vita";
                    label = !string.IsNullOrWhiteSpace(label) ? label : "default";
                    _path = GetWindowsFilePath(category, label);
                    if (string.IsNullOrWhiteSpace(_path)){
                        _path = GetUnixFilePath(category, label);
                    }
                }

                public Dictionary<string, string> LoadFromFile()
                {
                    var result = new Dictionary<string, string>();
                    if (string.IsNullOrWhiteSpace(_path))
                    {
                        return result;
                    }

                    var data = "";
                    try
                    {
                        var file = new FileInfo(_path);
                        if (!file.Exists)
                        {
                            return result;
                        }
                        data = File.ReadAllText(file.FullName);
                        if (string.IsNullOrWhiteSpace(data))
                        {
                            return result;
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString());
                    }

                    var jsonObject = JsonFactory.GetInstance().GetJsonObject(data);
                    if (jsonObject == null)
                    {
                        return result;
                    }
                    foreach (var k in jsonObject.AllKeys())
                    {
                        result[k] = jsonObject.ParseString(k);
                    }
                    return result;
                }

                public bool SaveToFile(Dictionary<string, string> properties)
                {
                    if (properties == null)
                    {
                        return false;
                    }
                    if (string.IsNullOrWhiteSpace(_path))
                    {
                        return false;
                    }
                    var jsonFactory = JsonFactory.GetInstance();
                    var jsonObject = jsonFactory.CreateJsonObject();
                    foreach (var k in properties.Keys)
                    {
                        jsonObject.Put(k, properties[k]);
                    }
                    try
                    {
                        var file = new FileInfo(_path);
                        if (!file.Exists)
                        {
                            var directory = file.Directory;
                            if (directory == null)
                            {
                                return false;
                            }
                            if (!directory.Exists)
                            {
                                directory.Create();
                            }
                        }
                        File.WriteAllText(file.FullName, jsonObject.ToPrettyString());
                        return true;
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.ToString());
                    }
                    return false;
                }

                public static string GetWindowsFilePath(string category, string label)
                {
                    if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(label))
                    {
                        return "";
                    }
                    var path = Environment.GetEnvironmentVariable("LocalAppData");
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        return "";
                    }
                    return Path.Combine(path, "HTC", category, label + ".pref");
                }

                public static string GetUnixFilePath(string category, string label)
                {
                    if (string.IsNullOrWhiteSpace(category) || string.IsNullOrWhiteSpace(label))
                    {
                        return "";
                    }
                    var path = Environment.GetEnvironmentVariable("HOME");
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        return "";
                    }
                    return Path.Combine(path, ".htc", category, label + ".pref");
                }
            }
        }
    }
}
