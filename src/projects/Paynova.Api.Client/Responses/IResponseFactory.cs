using Paynova.Api.Client.Net;

namespace Paynova.Api.Client.Responses
{
    public interface IResponseFactory
    {
        T Create<T>(HttpResponse httpResponse) where T : Response, new();
    }

    public interface IResponseFactory<T> where T : Response
    {
        T Create(HttpResponse httpResponse);
    }
}