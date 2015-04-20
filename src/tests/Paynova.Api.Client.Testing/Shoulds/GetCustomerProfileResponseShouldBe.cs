using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class GetCustomerProfileResponseShouldBe : ShouldBe<GetCustomerProfileResponse>
    {
        [DebuggerStepThrough]
        public GetCustomerProfileResponseShouldBe(GetCustomerProfileResponse response) : base(response) { }

        public virtual void ContainingProfileAndCards(string profileId, params ProfileCardDetails[] cardDetails)
        {
            Item.ShouldBeSuccessful();
            Item.ProfileId.Should().Be(profileId);

            for (var index = 0; index < Item.ProfileCards.Length; index++)
            {
                var card = Item.ProfileCards[index];
                var expectedCard = cardDetails[index];

                card.ShouldBeEquivalentTo(expectedCard);
            }
        }
    }
}