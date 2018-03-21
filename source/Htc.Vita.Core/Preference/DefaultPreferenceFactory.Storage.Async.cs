using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Htc.Vita.Core.Json;
using Htc.Vita.Core.Log;

namespace Htc.Vita.Core.Preference
{
    public partial class DefaultPreferenceFactory
    {
        internal partial class Storage
        {
            internal async Task<Dictionary<string, string>> LoadFromFileAsync()
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
                    using (var streamReader = File.OpenText(file.FullName))
                    {
                        data = await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    }
                    if (string.IsNullOrWhiteSpace(data))
                    {
                        return result;
                    }
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Storage)).Error(e.ToString());
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

            internal async Task<bool> SaveToFileAsync(Dictionary<string, string> properties)
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

                    using (var fileStream = File.OpenWrite(file.FullName))
                    {
                        var bytes = Encoding.UTF8.GetBytes(jsonObject.ToPrettyString());
                        fileStream.SetLength(0);
                        await fileStream.WriteAsync(bytes, 0, bytes.Length).ConfigureAwait(false);
                    }
                    return true;
                }
                catch (IOException e)
                {
                    Logger.GetInstance(typeof(Storage)).Error(e.Message);
                }
                catch (Exception e)
                {
                    Logger.GetInstance(typeof(Storage)).Error(e.ToString());
                }

                return false;
            }
        }
    }
}
