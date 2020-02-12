using System;
using System.Net;

namespace Paynova.Api.Client.Net
{
    public class HttpResponse
    {
        public Uri RequestUri { get; private set; }
        public string Method { get; private set; }
        public int StatusCode { get; private set; }
        public string StatusDescription { get; private set; }
        public string Content { get; set; }
        public bool IsEmpty => string.IsNullOrEmpty(Content);

        public HttpResponse(Uri requestUri, string method, HttpStatusCode statusCode, string statusDescription)
        {
            RequestUri = requestUri;
            Method = method;
            StatusCode = (int)statusCode;
            StatusDescription = statusDescription;
        }
    }
}