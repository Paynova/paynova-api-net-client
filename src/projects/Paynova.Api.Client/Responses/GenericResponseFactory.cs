using System;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Resources;
using Paynova.Api.Client.Serialization;

namespace Paynova.Api.Client.Responses
{
    public class GenericResponseFactory : IResponseFactory
    {
        protected ISerializer Serializer { get; private set; }

        public GenericResponseFactory(ISerializer serializer)
        {
            Ensure.That(serializer, "serializer").IsNotNull();

            Serializer = serializer;
       }

        public virtual T Create<T>(HttpResponse httpResponse) where T : Response, new()
        {
            EnsureNotFailedHttpResponse(typeof(T), httpResponse);

            return httpResponse.IsEmpty
                ? new T()
                : Serializer.Deserialize<T>(httpResponse.Content);
        }

        protected virtual void EnsureNotFailedHttpResponse(Type responseType, HttpResponse httpResponse)
        {
            var failedResponse = Serializer.Deserialize<FailedResponse>(httpResponse.Content);
            if (failedResponse != null && failedResponse.Status.ErrorNumber != 0)
                throw CreatePaynovaSdkException(responseType, httpResponse.RequestUri, httpResponse.Method, failedResponse.Status);
        }

        protected virtual PaynovaSdkException CreatePaynovaSdkException(Type responseType, Uri requestUri, string requestMethod, InvalidStatus status)
        {
            return new PaynovaSdkException(
                requestUri,
                requestMethod,
                status,
                ExceptionMessages.PaynovaSdkException_WhileCreatingResponse.Inject(responseType.Name, status.StatusMessage));
        }

        protected class FailedResponse
        {
            public InvalidStatus Status { get; set; }
        }
    }
}