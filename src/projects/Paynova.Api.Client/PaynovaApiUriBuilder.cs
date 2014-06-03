using System;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client
{
    public class PaynovaApiUriBuilder
    {
        protected string ServerUrl { get; set; }
        protected string Username { get; set; }
        protected string Password { get; set; }

        public PaynovaApiUriBuilder(string serverUrl)
        {
            Ensure.That(serverUrl, "serverUrl").IsNotNullOrEmpty();

            ServerUrl = serverUrl;
        }

        public virtual PaynovaApiUriBuilder SetBasicCredentials(string username, string password)
        {
            Username = Uri.EscapeDataString(username);
            Password = Uri.EscapeDataString(password);

            return this;
        }

        public virtual Uri Build()
        {
            var builder = new UriBuilder(ServerUrl)
            {
                UserName = Username,
                Password = Password
            };

            return builder.Uri;
        }
    }
}