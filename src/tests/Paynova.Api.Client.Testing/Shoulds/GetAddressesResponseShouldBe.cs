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
            Item.ShouldBeSuccessful();

            Item.GovernmentId.Should().NotBeNullOrWhiteSpace();
            Item.CountryCode.Should().NotBeNullOrWhiteSpace();

            Item.Addresses.Should().NotBeNull();
            Item.Addresses.Should().NotBeEmpty();
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