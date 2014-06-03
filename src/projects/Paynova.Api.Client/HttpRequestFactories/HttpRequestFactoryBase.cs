using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public abstract class HttpRequestFactoryBase<TRequest> : IHttpRequestFactory<TRequest> where TRequest : Request
    {
        protected IRuntime Runtime { get; private set; }
        protected ISerializer Serializer { get; private set; }

        protected HttpRequestFactoryBase(IRuntime runtime, ISerializer serializer)
        {
            Ensure.That(runtime, "runtime").IsNotNull();
            Ensure.That(serializer, "serializer").IsNotNull();

            Runtime = runtime;
            Serializer = serializer;
        }

        public virtual HttpRequest Create(TRequest request)
        {
            var httpRequest = new HttpRequest(GenerateRelativeUrl(request), GenerateMethod(request));
            var json = GenerateJson(request);

            return json != null ? httpRequest.SetJson(json) : httpRequest;
        }

        protected abstract string GenerateMethod(TRequest request);

        protected abstract string GenerateRelativeUrl(TRequest request);

        protected virtual string GenerateJson(TRequest request)
        {
            return Serializer.Serialize(request);
        }
    }
}