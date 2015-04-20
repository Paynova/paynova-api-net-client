using System.Diagnostics;
using FluentAssertions;
using Paynova.Api.Client.Responses;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class AnnulAuthorizationResponseShouldBe : ShouldBe<AnnulAuthorizationResponse>
    {
        [DebuggerStepThrough]
        public AnnulAuthorizationResponseShouldBe(AnnulAuthorizationResponse response) : base(response) { }

        public virtual void Ok()
        {
            Item.ShouldBeSuccessful();
        }
    }
}