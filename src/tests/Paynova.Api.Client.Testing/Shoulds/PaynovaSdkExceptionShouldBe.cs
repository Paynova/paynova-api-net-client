using System.Diagnostics;
using FluentAssertions;

namespace Paynova.Api.Client.Testing.Shoulds
{
    public class PaynovaSdkExceptionShouldBe : ShouldBe<PaynovaSdkException>
    {
        [DebuggerStepThrough]
        public PaynovaSdkExceptionShouldBe(PaynovaSdkException item) : base(item) { }

        public virtual void AnyFailure()
        {
            Item.ErrorNumber.Should().NotBe(0);
            Item.Errors.Should().NotBeEmpty();
            Item.StatusKey.Should().NotBeNullOrWhiteSpace();
            Item.StatusMessage.Should().NotBeNullOrEmpty();
        }

        public virtual void DueToAnyValidationFailure()
        {
            AnyFailure();
            Item.StatusKey.Should().Be("VALIDATION_ERROR");
        }

        public virtual void DueToValidationFailure(string exceptionMessageFormat, params object[] formattingArgs)
        {
            DueToAnyValidationFailure();
            Item.Message.Should().Be(string.Format(exceptionMessageFormat, formattingArgs));
        }
    }
}