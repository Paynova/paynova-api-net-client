using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.UnitTests.Model
{
    public class CurrencyCodeTests : UnitTestsOf<CurrencyCode>
    {
        [MyFact]
        public void When_United_states_dollar_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.UnitedStatesDollar;

            SUT.Alpha3.Should().Be("USD");
            SUT.Iso.Should().Be("840");
        }

        [MyFact]
        public void When_European_euro_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.EuropeanEuro;

            SUT.Alpha3.Should().Be("EUR");
            SUT.Iso.Should().Be("978");
        }

        [MyFact]
        public void When_British_pound_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.BritishPound;

            SUT.Alpha3.Should().Be("GBP");
            SUT.Iso.Should().Be("826");
        }

        [MyFact]
        public void When_Swedish_krona_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.SwedishKrona;

            SUT.Alpha3.Should().Be("SEK");
            SUT.Iso.Should().Be("752");
        }

        [MyFact]
        public void When_Norwegian_krone_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.NorwegianKrone;

            SUT.Alpha3.Should().Be("NOK");
            SUT.Iso.Should().Be("578");
        }

        [MyFact]
        public void When_Danish_krone_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.DanishKrone;

            SUT.Alpha3.Should().Be("DKK");
            SUT.Iso.Should().Be("208");
        }

        [MyFact]
        public void When_Swiss_franc_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.SwissFranc;

            SUT.Alpha3.Should().Be("CHF");
            SUT.Iso.Should().Be("756");
        }

        [MyFact]
        public void When_Australian_dollar_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.AustralianDollar;

            SUT.Alpha3.Should().Be("AUD");
            SUT.Iso.Should().Be("036");
        }

        [MyFact]
        public void When_New_Zealand_dollar_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.NewZealandDollar;

            SUT.Alpha3.Should().Be("NZD");
            SUT.Iso.Should().Be("554");
        }

        [MyFact]
        public void When_Hong_Kong_dollar_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.HongKongDollar;

            SUT.Alpha3.Should().Be("HKD");
            SUT.Iso.Should().Be("344");
        }

        [MyFact]
        public void When_Singapore_dollar_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.SingaporeDollar;

            SUT.Alpha3.Should().Be("SGD");
            SUT.Iso.Should().Be("702");
        }

        [MyFact]
        public void When_Canadian_dollar_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.CanadianDollar;

            SUT.Alpha3.Should().Be("CAD");
            SUT.Iso.Should().Be("124");
        }

        [MyFact]
        public void When_Polish_Złoty_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.PolishZłoty;

            SUT.Alpha3.Should().Be("PLN");
            SUT.Iso.Should().Be("985");
        }

        [MyFact]
        public void When_Turkish_Lira_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.TurkishLira;

            SUT.Alpha3.Should().Be("TRY");
            SUT.Iso.Should().Be("949");
        }

        [MyFact]
        public void When_Chinese_Yuan_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.ChineseYuan;

            SUT.Alpha3.Should().Be("CNY");
            SUT.Iso.Should().Be("156");
        }

        [MyFact]
        public void When_Japanese_Yen_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.JapaneseYen;

            SUT.Alpha3.Should().Be("JPY");
            SUT.Iso.Should().Be("392");
        }

        [MyFact]
        public void When_Israeli_New_Sheqel_It_is_correct_ISO_4217_values()
        {
            SUT = CurrencyCode.IsraeliNewSheqel;

            SUT.Alpha3.Should().Be("ILS");
            SUT.Iso.Should().Be("376");
        }
    }
}