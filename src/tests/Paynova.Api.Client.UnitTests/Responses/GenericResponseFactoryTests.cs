using System;
using System.Net;
using System.Text;
using FluentAssertions;
using Paynova.Api.Client.Model;
using Paynova.Api.Client.Net;
using Paynova.Api.Client.Resources;
using Paynova.Api.Client.Responses;
using Paynova.Api.Client.Testing;
using Paynova.Api.Client.Testing.Shoulds;

namespace Paynova.Api.Client.UnitTests.Responses
{
    public class GenericResponseFactoryTests : UnitTestsOf<GenericResponseFactory>
    {
        public GenericResponseFactoryTests()
        {
            SUT = new GenericResponseFactory(Serializer);
        }

        [MyFact]
        public void When_failed_http_response_content_It_will_throw_an_exception()
        {
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Post, HttpStatusCode.OK, "Ok.")
            {
                Content = "{\"status\":{\"isSuccess\":false,\"errorNumber\":-2,\"statusKey\":\"VALIDATION_ERROR\","
                          + "\"statusMessage\":\"The request contained one or more validation errors.  See the errors collection for further details.\","
                          + "\"errors\":["
                          + "{\"errorCode\":\"Predicate\",\"fieldName\":\"Fake\",\"message\":\"Some fake message.\"}]}}"
            };

            VerifyException<CreateOrderResponse>(httpResponse);
            VerifyException<AuthorizeInvoiceResponse>(httpResponse);
            VerifyException<InitializePaymentResponse>(httpResponse);
            VerifyException<RefundPaymentResponse>(httpResponse);
            VerifyException<FinalizeAuthorizationResponse>(httpResponse);
            VerifyException<AnnulAuthorizationResponse>(httpResponse);
            VerifyException<GetAddressesResponse>(httpResponse);
            VerifyException<GetCustomerProfileResponse>(httpResponse);
            VerifyException<GetPaymentOptionsResponse>(httpResponse);
            VerifyException<RemoveCustomerProfileCardResponse>(httpResponse);
            VerifyException<RemoveCustomerProfileResponse>(httpResponse);
        }

        private void VerifyException<TResponse>(HttpResponse httpResponse) where TResponse : Response, new()
        {
            Action a = () => SUT.Create<TResponse>(httpResponse);

            a.ShouldThrow<PaynovaSdkException>().And.ShouldBe().DueToValidationFailure(
                ExceptionMessages.PaynovaSdkException_WhileCreatingResponse,
                typeof(TResponse).Name,
                "The request contained one or more validation errors.  See the errors collection for further details.");
        }

        [MyFact]
        public void When_successful_create_order_response_content_It_creates_the_response()
        {
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Post, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse("\"orderId\":\"c61d1cd5-3fcd-4484-9eeb-a31c00c26807\"")
            };

            var response = SUT.Create<CreateOrderResponse>(httpResponse);

            response.ShouldBe().Ok();
        }

        [MyFact]
        public void When_successful_authorize_invoice_response_content_It_creates_the_response()
        {
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Post, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse(
                        "\"orderId\": \"6834c731-908f-4013-8335-a47d006c7c58\"," +
                        "\"transactionId\": \"201504170836062270\"," +
                        "\"acquirerId\": 1045," +
                        "\"acquirerReferenceId\": \"10197\"")
            };

            var response = SUT.Create<AuthorizeInvoiceResponse>(httpResponse);

            response.ShouldBe().Ok();
        }

        [MyFact]
        public void When_successful_initialize_payment_response_content_It_creates_the_response()
        {
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Post, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse(
                        "\"sessionId\":\"b97d3e73-2e83-4fa4-96f7-a31f007a33ea\"," +
                        "\"url\":\"https://08r2paygate.paynova.com/Aero/Payment/2/b97d3e73-2e83-4fa4-96f7-a31f007a33ea\"")
            };

            var response = SUT.Create<InitializePaymentResponse>(httpResponse);

            response.ShouldBe().Ok();
        }

        [MyFact]
        public void When_successful_refund_payment_response_content_It_creates_the_response()
        {
            const decimal totalAmount = 100m;
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Post, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse(
                        "\"totalAmountRefunded\": \"100\"," +
                        "\"canRefundAgain\": false," +
                        "\"transactionId\": \"201405051544508102\"," +
                        "\"batchId\": \"125030203046\"," +
                        "\"acquirerId\": \"1010\"")
            };

            var response = SUT.Create<RefundPaymentResponse>(httpResponse);

            response.ShouldBe().RefundedInFull(totalAmount);
        }

        [MyFact]
        public void When_successful_fully_finalized_auth_response_content_It_creates_the_response()
        {
            const decimal totalAmount = 100m;
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Post, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse(
                        "\"totalAmountFinalized\": \"100\"," +
                        "\"canFinalizeAgain\": false," +
                        "\"transactionId\": \"201405051544508102\"," +
                        "\"batchId\": \"125030203046\"," +
                        "\"acquirerId\": \"1010\"")
            };

            var response = SUT.Create<FinalizeAuthorizationResponse>(httpResponse);

            response.ShouldBe().AuthorizedInFull(totalAmount);
        }

        [MyFact]
        public void When_successful_annul_auth_response_content_It_creates_the_response()
        {
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Post, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse()
            };

            var response = SUT.Create<AnnulAuthorizationResponse>(httpResponse);

            response.ShouldBe().Ok();
        }

        [MyFact]
        public void When_successful_get_addresses_response_content_It_creates_the_response()
        {
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Get, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse(
                        "\"governmentId\": \"198005039212\"," +
                        "\"countryCode\": \"SE\"," +
                        "\"addresses\": [{" +
                        "    \"name\":{" +
                        "        \"firstName\": \"Tester\"," +
                        "        \"lastName\": \"Joe\"," +
                        "    }," +
                        "    \"address\":{" +
                        "        \"type\": \"Legal\"," +
                        "        \"street1\": \"Some street 1\"," +
                        "    }" +
                        "}]")
            };

            var response = SUT.Create<GetAddressesResponse>(httpResponse);

            response.ShouldBe().Ok();
        }

        [MyFact]
        public void When_successful_get_payment_options_response_content_It_creates_the_response()
        {
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Get, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse(
                        "\"availablePaymentMethods\": [" +
                        "{" +
                        "    \"paymentMethodId\": 311," +
                        "    \"paymentMethodProductId\": \"InstallmentsThreeMonths\"," +
                        "    \"displayName\": \"Delbetalning 3 månader\"," +
                        "    \"group\": {" +
                        "        \"key\": \"installment\"," +
                        "        \"displayName\": \"Part-Payment\"" +
                        "    }," +
                        "    \"interestRate\": {" +
                        "        \"label\": \"Annuity\"," +
                        "        \"symbol\": \"%\"," +
                        "        \"value\": \"10.00\"" +
                        "    }," +
                        "    \"notificationFee\": {" +
                        "        \"label\": \"Aviavgift\"," +
                        "        \"symbol\": \"kr\"," +
                        "        \"value\": \"29.00\"" +
                        "    }," +
                        "    \"setupFee\": {" +
                        "        \"label\": \"Uppläggningsavgift\"," +
                        "        \"symbol\": \"kr\"," +
                        "        \"value\": \"95.75\"" +
                        "    }," +
                        "    \"numberOfInstallments\": 3," +
                        "    \"installmentPeriod\": 1," +
                        "    \"installmentUnit\": \"month\"," +
                        "    \"legalDocuments\": [" +
                        "        {" +
                        "            \"label\": \"Avtalsvilkor 3 månader delbetalning\"," +
                        "            \"uri\": \"http://www.paynova.com\"" +
                        "        }" +
                        "    ]," +
                        "    \"addressTypeRestrictions\": [" +
                        "        \"legal\"" +
                        "    ]" +
                        "}]")
            };

            var response = SUT.Create<GetPaymentOptionsResponse>(httpResponse);

            response.ShouldBe().Containing(new PaymentMethodDetail
            {
                PaymentMethodId = 311,
                PaymentMethodProductId = "InstallmentsThreeMonths",
                DisplayName = "Delbetalning 3 månader",
                Group = new KeyedDisplayName
                {
                    Key = "installment",
                    DisplayName = "Part-Payment"
                },
                InterestRate = new LabelSymbolValue<decimal>
                {
                    Label = "Annuity",
                    Symbol = "%",
                    Value = 10.00m
                },
                NotificationFee = new LabelSymbolValue<decimal>
                {
                    Label = "Aviavgift",
                    Symbol = "kr",
                    Value = 29.00m
                },
                SetupFee = new LabelSymbolValue<decimal>
                {
                    Label = "Uppläggningsavgift",
                    Symbol = "kr",
                    Value = 95.75m
                },
                NumberOfInstallments = 3,
                InstallmentPeriod = 1,
                InstallmentUnit = "month",
                LegalDocuments = new[] { new Link
                {
                    Label = "Avtalsvilkor 3 månader delbetalning",
                    Uri = "http://www.paynova.com"
                } },
                AddressTypeRestrictions = new []{"legal"}
            });
        }

        [MyFact]
        public void When_successful_get_customer_profile_response_content_It_creates_the_response()
        {
            const string profileId = "netsdk";
            var httpResponse = new HttpResponse(new Uri("http://foo.com"), HttpMethods.Get, HttpStatusCode.OK, "Ok.")
            {
                Content = CreateSuccessfulJsonResponse(
                        "\"profileId\": \"netsdk\"," +
                        "\"profileCards\": [" +
                        "{" +
                        "    \"cardId\": \"0129d8d4-48f3-4211-9354-a326008e881d\"," +
                        "    \"expirationYear\": 15," +
                        "    \"expirationMonth\": 5," +
                        "    \"firstSix\": \"411111\"," +
                        "    \"lastFour\": \"1111\"" +
                        "}]")
            };

            var response = SUT.Create<GetCustomerProfileResponse>(httpResponse);

            response.ShouldBe().ContainingProfileAndCards(
                profileId,
                new ProfileCardDetails
                {
                    CardId = new Guid("0129d8d4-48f3-4211-9354-a326008e881d"),
                    ExpirationYear = 15,
                    ExpirationMonth = 5,
                    FirstSix = "411111",
                    LastFour = "1111"
                });
        }

        private static string CreateSuccessfulJsonResponse(string json = null)
        {
            var result = new StringBuilder("{\"status\": {\"isSuccess\": true, \"errorNumber\": 0, \"statusKey\": \"SUCCESS\", \"statusMessage\": \"The operation was successful.\"}");

            if (!string.IsNullOrEmpty(json))
            {
                result.Append(",");
                result.Append(json);
            }

            return result.Append("}").ToString();
        }
    }
}