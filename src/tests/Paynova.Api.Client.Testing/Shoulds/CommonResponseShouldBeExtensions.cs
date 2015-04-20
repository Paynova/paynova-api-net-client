using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public static class CommonResponseShouldBeExtensions
    {
        public static void ShouldBeSuccessful<TResponse>(this TResponse response) where TResponse : Response
        {
            response.Should().NotBeNull();
            response.Status.Should().NotBeNull();
            response.Status.StatusKey.Should().Be("SUCCESS");
            response.Status.StatusMessage.Should().Be("The operation was successful.");
        }
    }
}