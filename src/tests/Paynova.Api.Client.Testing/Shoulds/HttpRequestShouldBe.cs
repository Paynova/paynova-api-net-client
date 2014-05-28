using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Net;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class HttpRequestShouldBe : ShouldBe<HttpRequest>
    {
        [DebuggerStepThrough]
        public HttpRequestShouldBe(HttpRequest request) : base(request) { }

        public virtual void GetAgainst(string relativeUrlFormat, params object[] formattingArgs)
        {
            Against(HttpMethods.Get, relativeUrlFormat, formattingArgs);
        }

        public virtual void PostAgainst(string relativeUrlFormat, params object[] formattingArgs)
        {
            Against(HttpMethods.Post, relativeUrlFormat, formattingArgs);
        }

        public virtual void DeleteAgainst(string relativeUrlFormat, params object[] formattingArgs)
        {
            Against(HttpMethods.Delete, relativeUrlFormat, formattingArgs);
        }

        private void Against(string method, string relativeUrlFormat, params object[] formattingArgs)
        {
            Item.Should().NotBeNull();
            Item.RelativeUrl.Should().Be(string.Format(relativeUrlFormat, formattingArgs));
            Item.Method.Should().Be(method);
        }

        public virtual void GetWithNoJson()
        {
            Item.Should().NotBeNull();
            Item.Method.Should().Be(HttpMethods.Get);
            Item.Accept.Should().Be(ContentTypes.Json);
            Item.ContentType.Should().BeNull();
            Item.Content.Should().BeNull();
        }

        public virtual void PostWithJson(string json)
        {
            Item.Should().NotBeNull();
            Item.Method.Should().Be(HttpMethods.Post);
            Item.Accept.Should().Be(ContentTypes.Json);
            Item.ContentType.Should().Be(ContentTypes.Json);
            Item.Content.Should().Be(json);
        }

        public virtual void DeleteWithNoJson()
        {
            Item.Should().NotBeNull();
            Item.Method.Should().Be(HttpMethods.Delete);
            Item.Accept.Should().Be(ContentTypes.Json);
            Item.ContentType.Should().BeNull();
            Item.Content.Should().BeNull();
        }
    }
}