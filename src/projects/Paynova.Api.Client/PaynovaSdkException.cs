using System;
using Paynova.Api.Client.Model;

namespace Paynova.Api.Client
{
    public class PaynovaSdkException : Exception
    {
        public Uri RequestUri { get; private set; }
        public string RequestMethod { get; set; }
        public int ErrorNumber { get; private set; }
        public string StatusKey { get; private set; }
        public string StatusMessage { get; private set; }
        public Error[] Errors { get; private set; }

        public PaynovaSdkException(Uri requestUri, string requestMethod, InvalidStatus status, string message)
            : base(message)
        {
            RequestUri = requestUri;
            RequestMethod = requestMethod;
            ErrorNumber = status.ErrorNumber;
            StatusKey = status.StatusKey;
            StatusMessage = status.StatusMessage;
            Errors = status.Errors ?? new Error[0];
        }
    }
}