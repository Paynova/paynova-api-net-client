using System;
using System.Linq;
using FluentAssertions;
using Paynova.Api.Client.Extensions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Requests;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.TestData;

namespace Paynova.Api.Client.UnitTests.Requests
{
    public class InitializePaymentRequestTests : UnitTestsOf<InitializePaymentRequest>
    {
        public InitializePaymentRequestTests()
        {
            var interfaceOptions = new InterfaceOptions(
                InterfaceId.Aero,
                "SWE",
                "https://foo.com/payments/success".ToUri(),
                "https://foo.com/payments/cancel".ToUri(),
                "https://foo.com/payments/pending".ToUri());

            SUT = new InitializePaymentRequest(Guid.NewGuid(), 112.75m, PaymentChannelId.Web, interfaceOptions);
        }

        [MyFact]
        public void When_add_of_line_items_It_should_add_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.AddLineItem(LineItemTestData.CreateLineItems(1).Single());

            SUT.LineItems.Length.Should().Be(3);
        }

        [MyFact]
        public void When_setting_line_items_It_should_overwrite_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.WithLineItems(LineItemTestData.CreateLineItems(3));

            SUT.LineItems.Length.Should().Be(3);
        }

        [MyFact]
        public void When_clearing_line_items_It_should_remove_all_lineItems_in_the_request()
        {
            SUT.WithLineItems(LineItemTestData.CreateLineItems(2));

            SUT.ClearLineItems();

            SUT.CustomData.Should().BeEmpty();
        }

        [MyFact]
        public void When_add_of_custom_data_fields_It_should_add_fields_in_the_request()
        {
            SUT.WithCustomData(CreateCustomDataField(1), CreateCustomDataField(2));

            SUT.AddCustomData(CreateCustomDataField(3));

            SUT.CustomData.Length.Should().Be(3);
        }

        [MyFact]
        public void When_setting_custom_data_fields_It_should_overwrite_fields_in_the_request()
        {
            SUT.WithCustomData(CreateCustomDataField(1), CreateCustomDataField(2));

            SUT.WithCustomData(CreateCustomDataField(3), CreateCustomDataField(4), CreateCustomDataField(5));

            SUT.CustomData.Length.Should().Be(3);
        }

        [MyFact]
        public void When_clearing_custom_data_fields_It_should_remove_all_fields_in_the_request()
        {
            SUT.WithCustomData(CreateCustomDataField(1), CreateCustomDataField(2));

            SUT.ClearCustomData();

            SUT.CustomData.Should().BeEmpty();
        }

        [MyFact]
        public void When_add_of_payment_methods_It_should_add_methods_in_the_request()
        {
            SUT.WithPaymentMethods(PaymentMethod.Visa);

            SUT.AddPaymentMethod(PaymentMethod.MasterCard);

            SUT.PaymentMethods.Length.Should().Be(2);
        }

        [MyFact]
        public void When_setting_payment_methods_It_should_overwrite_methods_in_the_request()
        {
            SUT.WithPaymentMethods(PaymentMethod.Visa);

            SUT.WithPaymentMethods(PaymentMethod.MasterCard, PaymentMethod.MaestroInternational);

            SUT.PaymentMethods.Length.Should().Be(2);
        }

        [MyFact]
        public void When_clearing_payment_methods_It_should_remove_all_methods_in_the_request()
        {
            SUT.WithPaymentMethods(PaymentMethod.Visa);

            SUT.ClearPaymentMethods();

            SUT.PaymentMethods.Should().BeEmpty();
        }

        [MyFact]
        public void When_setting_session_timeout_It_should_update_the_request()
        {
            SUT.WithSessionTimeout(100);

            SUT.SessionTimeout.Should().Be(100);
        }

        [MyFact]
        public void When_setting_routing_indicator_profile_It_should_update_the_request()
        {
            SUT.WithRoutingIndicator("test");

            SUT.RoutingIndicator.Should().Be("test");
        }

        [MyFact]
        public void When_setting_fraud_screening_profile_It_should_update_the_request()
        {
            SUT.WithFraudScreeningProfile("test");

            SUT.FraudScreeningProfile.Should().Be("test");
        }

        private CustomDataField CreateCustomDataField(int n)
        {
            return new CustomDataField("Key" + n, "Value" + n);
        }
    }
}