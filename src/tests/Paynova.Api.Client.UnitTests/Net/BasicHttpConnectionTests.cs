using System;
using System.Net;
using FluentAssertions;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Resources;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Net
{
    public class BasicHttpConnectionTests : UnitTestsOf<BasicHttpConnection>
    {
        [MyFact]
        public void When_not_passing_server_address_with_basic_credentials_It_will_throw_argument_exception()
        {
            Action a = () => SUT = new BasicHttpConnection(new Uri("https://api.foo.com"));

            a.ShouldThrow<ArgumentException>()
                .Where(ex => ex.ParamName.Equals("serverAddress.UserInfo"))
                .Where(ex => ex.Message.Contains(ExceptionMessages.BasicHttpConnection_MissingBasicCredentials));
        }

        [MyFact]
        public void When_passing_uri_not_ending_with_slash_It_will_append_slash()
        {
            SUT = new BasicHttpConnection(new Uri("https://testUsr:testPwd@foo.com"));

            SUT.ServerAddress.Should().EndWith("/");
        }

        [MyFact]
        public void When_passing_url_not_ending_with_slash_It_will_append_slash()
        {
            SUT = new BasicHttpConnection("https://api.foo.com", "tstUsr", "tstPwd");

            SUT.ServerAddress.Should().EndWith("/");
        }

        [MyFact]
        public void When_passing_url_and_credentials_It_will_strip_out_credentials()
        {
            SUT = new BasicHttpConnection("https://api.foo.com/", "s@", "p@ssword");

            SUT.ServerAddress.Should().Be("https://api.foo.com/");
        }
    }

    public class ExposingBasicHttpConnectionTests : UnitTestsOf<ExposingBasicHttpConnectionTests.ExposingBasicHttpConnection>
    {
        public ExposingBasicHttpConnectionTests()
        {
            SUT = new ExposingBasicHttpConnection(new Uri("https://testUsr:testPwd@api.foo.com"));
        }

        [MyFact]
        public void When_creating_web_request_with_relative_url_not_starting_with_slash_It_generates_correct_url()
        {
            var httpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("orders/1", "GET"));

            httpWebRequest.RequestUri.AbsoluteUri.Should().Be("https://api.foo.com/orders/1");
        }

        [MyFact]
        public void When_creating_web_request_with_relative_url_starting_with_slash_It_generates_correct_url()
        {
            var httpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("/orders/1", "GET"));

            httpWebRequest.RequestUri.AbsoluteUri.Should().Be("https://api.foo.com/orders/1");
        }

        [MyFact]
        public void When_creating_web_request_It_should_append_correct_basic_auth_header()
        {
            var httpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("orders/1", "GET"));

            httpWebRequest.Headers["Authorization"].Should().NotBeNullOrWhiteSpace();
            httpWebRequest.Headers["Authorization"].Should().StartWith("Basic ");
        }

        [MyFact]
        public void When_creating_web_request_It_should_have_accept_application_json_header()
        {
            var httpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("orders/1", "GET"));

            httpWebRequest.Accept.Should().Be("application/json");
        }

        [MyFact]
        public void When_creating_web_request_It_should_have_specific_user_agent_header()
        {
            var httpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("orders/1", "GET"));
#if DEBUG
            httpWebRequest.UserAgent.Should().Be("Paynova Api Client (Debug) v" + typeof(BasicHttpConnection).Assembly.GetName().Version);
#else
            httpWebRequest.UserAgent.Should().Be("Paynova Api Client (Release) v" + typeof(BasicHttpConnection).Assembly.GetName().Version);
#endif
        }

        [MyFact]
        public void When_creating_web_request_It_should_have_correct_method()
        {
            var getHttpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("orders/1", "GET"));
            var putHttpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("orders/1", "PUT"));
            var postHttpWebRequest = SUT.ExposingCreateWebRequest(new HttpRequest("orders/1", "POST"));

            getHttpWebRequest.Method.Should().Be("GET");
            putHttpWebRequest.Method.Should().Be("PUT");
            postHttpWebRequest.Method.Should().Be("POST");
        }

        public class ExposingBasicHttpConnection : BasicHttpConnection
        {
            public ExposingBasicHttpConnection(Uri serverAddress)
                : base(serverAddress)
            {
            }

            public ExposingBasicHttpConnection(string serverUrl, string username, string password)
                : base(serverUrl, username, password)
            {
            }

            public HttpWebRequest ExposingCreateWebRequest(HttpRequest httpRequest)
            {
                return CreateWebRequest(httpRequest);
            }
        }
    }
}