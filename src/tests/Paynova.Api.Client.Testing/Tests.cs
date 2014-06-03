using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.Testing
{
    public abstract class Tests
    {
        protected IRuntime Runtime { get; set; }
        protected ISerializer Serializer { get; set; }

        protected Tests()
        {
            Runtime = Api.Client.Runtime.ReplaceWith(new FakeRuntime());
            Serializer = new DefaultJsonSerializer();
        }
    }
}