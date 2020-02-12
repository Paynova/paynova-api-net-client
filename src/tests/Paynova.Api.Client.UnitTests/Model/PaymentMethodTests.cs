using FluentAssertions;
using Paynova.Api.Client.Model;
using Xunit;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class PaymentMethodTests : UnitTestsOf<PaymentMethod>
    {
        [Fact]
        public void When_All_It_should_have_0()
        {
            PaymentMethod.All.Id.Should().Be(0);
        }

        [Fact]
        public void When_AllCardTypes_It_should_have_99()
        {
            PaymentMethod.AllCardTypes.Id.Should().Be(99);
        }

        [Fact]
        public void When_Aktia_It_should_have_114()
        {
            PaymentMethod.Aktia.Id.Should().Be(114);
        }

        [Fact]
        public void When_AmericanExpress_It_should_have_3()
        {
            PaymentMethod.AmericanExpress.Id.Should().Be(3);
        }

        [Fact]
        public void When_ChinaPay_It_should_have_116()
        {
            PaymentMethod.ChinaPay.Id.Should().Be(116);
        }

        [Fact]
        public void When_ChinaPayDomestic_It_should_have_119()
        {
            PaymentMethod.ChinaPayDomestic.Id.Should().Be(119);
        }

        [Fact]
        public void When_DanskeBankDenmark_It_should_have_121()
        {
            PaymentMethod.DanskeBankDenmark.Id.Should().Be(121);
        }

        [Fact]
        public void When_DanskeBankFinland_It_should_have_115()
        {
            PaymentMethod.DanskeBankFinland.Id.Should().Be(115);
        }

        [Fact]
        public void When_DinersClub_It_should_have_4()
        {
            PaymentMethod.DinersClub.Id.Should().Be(4);
        }

        [Fact]
        public void When_ELV_It_should_have_111()
        {
            PaymentMethod.ELV.Id.Should().Be(111);
        }

        [Fact]
        public void When_Giropay_It_should_have_105()
        {
            PaymentMethod.Giropay.Id.Should().Be(105);
        }

        [Fact]
        public void When_Handelsbanken_It_should_have_103()
        {
            PaymentMethod.Handelsbanken.Id.Should().Be(103);
        }

        [Fact]
        public void When_MaestroInternational_It_should_have_12()
        {
            PaymentMethod.MaestroInternational.Id.Should().Be(12);
        }

        [Fact]
        public void When_MasterCard_It_should_have_2()
        {
            PaymentMethod.MasterCard.Id.Should().Be(2);
        }

        [Fact]
        public void When_NordeaFinland_It_should_have_113()
        {
            PaymentMethod.NordeaFinland.Id.Should().Be(113);
        }

        [Fact]
        public void When_NordeaSweden_It_should_have_101()
        {
            PaymentMethod.NordeaSweden.Id.Should().Be(101);
        }

        [Fact]
        public void When_PayPal_It_should_have_304()
        {
            PaymentMethod.PayPal.Id.Should().Be(304);
        }

        [Fact]
        public void When_Pohjola_It_should_have_117()
        {
            PaymentMethod.Pohjola.Id.Should().Be(117);
        }

        [Fact]
        public void When_SEB_It_should_have_104()
        {
            PaymentMethod.SEB.Id.Should().Be(104);
        }

        [Fact]
        public void When_Skrill_It_should_have_302()
        {
            PaymentMethod.Skrill.Id.Should().Be(302);
        }

        [Fact]
        public void When_Sofortbanking_It_should_have_123()
        {
            PaymentMethod.Sofortbanking.Id.Should().Be(123);
        }

        [Fact]
        public void When_Swedbank_It_should_have_102()
        {
            PaymentMethod.Swedbank.Id.Should().Be(102);
        }

        [Fact]
        public void When_Visa_It_should_have_1()
        {
            PaymentMethod.Visa.Id.Should().Be(1);
        }

        [Fact]
        public void When_iDEAL_It_should_have_110()
        {
            PaymentMethod.iDEAL.Id.Should().Be(110);
        }

        [Fact]
        public void When_Überweisung_It_should_have_118()
        {
            PaymentMethod.Überweisung.Id.Should().Be(118);
        }

        [Fact]
        public void When_PaynovaInvoice_It_should_have_311()
        {
            PaymentMethod.PaynovaInvoice.Id.Should().Be(311);
        }
    }
}