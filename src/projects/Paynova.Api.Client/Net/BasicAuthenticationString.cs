using System;
using System.Text;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client.Net
{
    public class BasicAuthenticationString
    {
        public string Value { get; private set; }

        public BasicAuthenticationString(string username, string password)
        {
            Ensure.That(username, "username").IsNotNullOrEmpty();
            Ensure.That(password, "password").IsNotNullOrEmpty();

            Value = GenerateBasicAuthorizationCredentials(username, password);
        }

        private string GenerateBasicAuthorizationCredentials(string username, string password)
        {
            var credentialsBytes = Encoding.UTF8.GetBytes($"{username}:{password}");

            return Convert.ToBase64String(credentialsBytes);
        }

        public static implicit operator string(BasicAuthenticationString item)
        {
            return item.Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}