using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Net
{
    public class HttpRequest
    {
        public string RelativeUrl { get; private set; }
        public string Method { get; private set; }
        public string Accept { get; set; }
        public string Content { get; set; }
        public string ContentType { get; private set; }

        public HttpRequest(string relativeUrl, string method)
        {
            Ensure.That(relativeUrl, "relativeUrl").IsNotNull();
            Ensure.That(method, "method").IsNotNullOrEmpty();

            RelativeUrl = relativeUrl;
            Method = method;
            Accept = ContentTypes.Json;
        }

        public virtual HttpRequest SetJson(string content)
        {
            ContentType = ContentTypes.Json;
            Content = content;

            return this;
        }

        public virtual bool HasContent()
        {
            return !string.IsNullOrEmpty(Content);
        }
    }
}