using Paynova.Api.Client.Net;
using Paynova.Api.Client.Requests;

namespace Paynova.Api.Client.HttpRequestFactories
{
    public interface IHttpRequestFactory<T> where T : Request
    {
        HttpRequest Create(T request);
    }
}