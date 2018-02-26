using System;
using System.Collections.Generic;
using System.IO;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        internal class Storage
        {
            private readonly Logger _logger;
            private readonly string _path;

            internal Storage(string category, string label)
            {
                _logger = Logger.GetInstance();
                var targetCategory = !string.IsNullOrWhiteSpace(category) ? category : "Vita";
                var targetLabel = !string.IsNullOrWhiteSpace(label) ? label : "default";
                _path = GetFilePath(targetCategory, targetLabel);
            }

            private static string GetAppDataPath()
            {
                var path = Windows.GetAppDataPath();
                if (string.IsNullOrWhiteSpace(path))
                {
                    path = Unix.GetAppDataPath();
                }
                return path;
            }

            internal static string GetFilePath(string category, string label)
            {
                var path = GetAppDataPath();
                if (string.IsNullOrWhiteSpace(path))
                {
                    return "";
                }
                return Path.Combine(path, category, label + ".pref");
            }

            internal Dictionary<string, string> LoadFromFile()
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

            internal bool SaveToFile(Dictionary<string, string> properties)
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
                catch (IOException e)
                {
                    _logger.Error(e.Message);
                }
                catch (Exception e)
                {
                    _logger.Error(e.ToString());
                }

                return false;
            }
        }
    }
}
