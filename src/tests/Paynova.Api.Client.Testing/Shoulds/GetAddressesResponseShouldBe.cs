using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class GetAddressesResponseShouldBe : ShouldBe<GetAddressesResponse>
    {
        [DebuggerStepThrough]
        public GetAddressesResponseShouldBe(GetAddressesResponse response) : base(response) { }

        public virtual void Ok()
        {
            Item.Should().NotBeNull();
            Item.Status.Should().NotBeNull();
            Item.Status.StatusKey.Should().Be("SUCCESS");
            Item.Status.StatusMessage.Should().Be("The operation was successful.");
            Item.GovernmentId.Should().NotBeNullOrWhiteSpace();
            Item.CountryCode.Should().NotBeNullOrWhiteSpace();

            foreach (var item in Item.Addresses)
            {
                item.Name.FirstName.Should().NotBeNullOrWhiteSpace();
                item.Name.LastName.Should().NotBeNullOrWhiteSpace();

                item.Address.Type.Should().NotBeNullOrWhiteSpace();
                item.Address.Street1.Should().NotBeNullOrWhiteSpace();
            }
        }
    }
}