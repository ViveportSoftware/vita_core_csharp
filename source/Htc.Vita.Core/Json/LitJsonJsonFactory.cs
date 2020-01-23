using Htc.Vita.Core.Json.LitJson;

namespace Htc.Vita.Core.Json
{
    /// <summary>
    /// Class LitJsonJsonFactory.
    /// Implements the <see cref="JsonFactory" />
    /// </summary>
    /// <seealso cref="JsonFactory" />
    public partial class LitJsonJsonFactory : JsonFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LitJsonJsonFactory"/> class.
        /// </summary>
        public LitJsonJsonFactory()
        {
            JsonMapper.RegisterImporter<int, long>(l => 0L + l);
        }

        /// <inheritdoc />
        protected override JsonArray OnCreateJsonArray()
        {
            return new LitJsonJsonArray(JsonMapper.ToObject("[]"));
        }

        /// <inheritdoc />
        protected override JsonObject OnCreateJsonObject()
        {
            return new LitJsonJsonObject(JsonMapper.ToObject("{}"));
        }

        /// <inheritdoc />
        protected override T OnDeserializeObject<T>(string content)
        {
            return JsonMapper.ToObject<T>(content);
        }

        /// <inheritdoc />
        protected override JsonArray OnGetJsonArray(string content)
        {
            return new LitJsonJsonArray(JsonMapper.ToObject(content));
        }

        /// <inheritdoc />
        protected override JsonObject OnGetJsonObject(string content)
        {
            return new LitJsonJsonObject(JsonMapper.ToObject(content));
        }

        /// <inheritdoc />
        protected override string OnSerializeObject(object content)
        {
            return JsonMapper.ToJson(content);
        }
    }
}
