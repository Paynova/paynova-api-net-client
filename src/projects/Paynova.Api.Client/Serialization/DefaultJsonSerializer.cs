namespace Paynova.Api.Client.Serialization
{
    public class DefaultJsonSerializer : ISerializer
    {
        private readonly IJsonSerializerStrategy _serializerStrategy;

        public DefaultJsonSerializer()
        {
            _serializerStrategy = new CustomSerializerStrategy();
        }

        public virtual string Serialize<T>(T item)
        {
            return SimpleJson.SerializeObject(item, _serializerStrategy);
        }

        public virtual T Deserialize<T>(string data) where T : class
        {
            return string.IsNullOrEmpty(data)
                ? null
                : SimpleJson.DeserializeObject<T>(data, _serializerStrategy);
        }
    }
}