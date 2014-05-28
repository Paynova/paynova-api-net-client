using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using Paynova.Api.Client.EnsureThat;
using Paynova.Api.Client.Extensions;
using Paynova.Api.Client.Resources;

namespace Paynova.Api.Client.Net
{
    public class BasicHttpConnection : IHttpConnection
    {
        public string ServerAddress { get; private set; }
        protected BasicAuthenticationString BasicAuthorizationCredentials { get; private set; }
        protected string UserAgentString { get; private set; }

        public BasicHttpConnection(string serverUrl, string username, string password)
        {
            Ensure.That(serverUrl, "serverUrl").IsNotNullOrEmpty();
            Ensure.That(username, "username").IsNotNullOrEmpty();
            Ensure.That(password, "password").IsNotNullOrEmpty();

            Initialize(new Uri(serverUrl), username, password);
        }

        public BasicHttpConnection(Uri serverAddress)
        {
            Ensure.That(serverAddress, "serverAddress").IsNotNull();
            Ensure.That(serverAddress.UserInfo, "serverAddress.UserInfo")
                .WithExtraMessageOf(() => ExceptionMessages.BasicHttpConnection_MissingBasicCredentials)
                .IsNotNullOrEmpty();

            var credentialsParts = serverAddress.GetUserInfoParts();

            Ensure.That(credentialsParts.Length == 2, "serverAddress.UserInfo")
                .WithExtraMessageOf(() => ExceptionMessages.BasicHttpConnection_InvalidFormatOfBasicCredentials)
                .IsTrue();

            Initialize(serverAddress, credentialsParts[0], credentialsParts[1]);
        }

        private void Initialize(Uri serverAddress, string username, string password)
        {
            ServerAddress = serverAddress.GetAbsoluteUriExceptUserInfo();
            BasicAuthorizationCredentials = new BasicAuthenticationString(username, password);
            UserAgentString = GenerateUserAgentString();
        }

        protected virtual string GenerateUserAgentString()
        {
            var assembly = typeof(BasicHttpConnection).Assembly;
            var nameInfo = assembly.GetName();
            var assemblyProduct = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false).FirstOrDefault() as AssemblyProductAttribute;

            return string.Format("{0} v{1}",
                assemblyProduct != null ? assemblyProduct.Product : nameInfo.Name,
                nameInfo.Version);
        }

        public virtual HttpResponse Send(HttpRequest httpRequest)
        {
            Ensure.That(httpRequest, "httpRequest").IsNotNull();

            var webRequest = CreateWebRequest(httpRequest);

            return CreateHttpResponse(webRequest);
        }

        protected virtual HttpWebRequest CreateWebRequest(HttpRequest httpRequest)
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(GenerateCompleteRequestUrl(httpRequest));
            webRequest.Headers.Add(HttpRequestHeader.Authorization, "Basic " + BasicAuthorizationCredentials);
            webRequest.Accept = httpRequest.Accept;
            webRequest.Method = httpRequest.Method;
            webRequest.ContentType = httpRequest.ContentType;
            webRequest.UserAgent = UserAgentString;

            if (httpRequest.HasContent())
            {
                using (var stream = webRequest.GetRequestStream())
                    WriteRequestToStream(httpRequest, stream);
            }

            return webRequest;
        }

        protected virtual string GenerateCompleteRequestUrl(HttpRequest httpRequest)
        {
            return ServerAddress + httpRequest.RelativeUrl.TrimStart(new[] { '/' });
        }

        protected virtual void WriteRequestToStream(HttpRequest httpRequest, Stream stream)
        {
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(httpRequest.Content);
                writer.Flush();
                writer.Close();
            }
        }

        protected virtual HttpResponse CreateHttpResponse(HttpWebRequest webRequest)
        {
            HttpWebResponse webResponse = null;

            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();

                var httpResponse = new HttpResponse(webRequest.RequestUri, webResponse.Method, webResponse.StatusCode, webResponse.StatusDescription);

                //Can not use webResponse.ContentLength as it is -1 even
                //though response stream has data.
                using (var stream = webResponse.GetResponseStream())
                    PopulateResponseFromStream(httpResponse, stream);

                return httpResponse;
            }
            finally
            {
                if (webResponse != null)
                    webResponse.Close();
            }
        }

        protected virtual void PopulateResponseFromStream(HttpResponse response, Stream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8))
                response.Content = reader.ReadToEnd();
        }
    }
}