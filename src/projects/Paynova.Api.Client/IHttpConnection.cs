using Paynova.Api.Client.Net;

namespace Paynova.Api.Client
{
    /// <summary>
    /// Used to issue HTTP-requests against remote end-point.
    /// </summary>
    public interface IHttpConnection
    {
        string ServerAddress { get; }

        HttpResponse Send(HttpRequest httpRequest);
    }
}