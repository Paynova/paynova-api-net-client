using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Paynova.TestServer.Serialization
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatting = Formatting.Indented;
        }
    }
}