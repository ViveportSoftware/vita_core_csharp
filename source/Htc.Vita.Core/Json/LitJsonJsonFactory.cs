using Htc.Vita.Core.Json.LitJson;

namespace Htc.Vita.Core.Json
{
    public partial class LitJsonJsonFactory : JsonFactory
    {
        public LitJsonJsonFactory()
        {
            JsonMapper.RegisterImporter<int, long>(l => 0L + l);
        }

        protected override JsonArray OnCreateJsonArray()
        {
            return new LitJsonJsonArray(JsonMapper.ToObject("[]"));
        }

        protected override JsonObject OnCreateJsonObject()
        {
            return new LitJsonJsonObject(JsonMapper.ToObject("{}"));
        }

        protected override T OnDeserializeObject<T>(string content)
        {
            return JsonMapper.ToObject<T>(content);
        }

        protected override JsonArray OnGetJsonArray(string content)
        {
            return new LitJsonJsonArray(JsonMapper.ToObject(content));
        }

        protected override JsonObject OnGetJsonObject(string content)
        {
            return new LitJsonJsonObject(JsonMapper.ToObject(content));
        }

        protected override string OnSerializeObject(object content)
        {
            return JsonMapper.ToJson(content);
        }
    }
}
