# Paynova .NET API Client #

## Documentation & Samples ##
The documentation for the client is provided here in [the Wiki](https://github.com/Paynova/paynova-api-net-client/wiki).

The documentation for the API is provided here: [http://docs.paynova.com](http://docs.paynova.com)

There is also [a repository](https://github.com/Paynova/paynova-api-net-client-samples) where we collect samples of how it can be used.

## Quick start ##
The Paynova .NET API Client is [distributed via NuGet](http://www.nuget.org/packages/paynova.api.client/).

```
install-package Paynova.Api.Client
```

You consume the API using the `PaynovaClient`. To create it you need a `serverUrl`, `username` & `password`.

```csharp
IPaynovaClient client = new PaynovaClient(serverUrl, username, password);
```

The operations available are exposed on the client:

- CreateOrder
- InitializePayment
- FinalizeAuthorization
- AnnulAuthorization
- RefundPayment
- GetCustomerProfile
- RemoveCustomerProfile
- RemoveCustomerProfileCard

### Requests & Responses ###
The Client works in a request & response fashion, hence you provide a request object and get a response back.

In some cases there exists "simplifying functions, accepting simple arguments instead of a request".

All required arguments must be passed to the constructors:

```csharp
var request = new CreateOrderRequest(orderNumber, currencyCode, amount);
var response = client.CreateOrder(request);
```

### Exceptions ###
If the call reaches a valid enpoint at Paynova and there's an error e.g. caused by validation, a `PaynovaClientException` will be thrown.

If you fail to provide required arguments to any of the requests, an `ArgumentException` will be thrown.

## Get up and running with the code ##
The solution is built using Visual Studio 2012. The projects target .Net framework 3.5.

All tests are written using [xUnit](https://github.com/xunit/xunit) and uses [FluentAssertions](https://github.com/dennisdoomen/fluentassertions) for asserts.

**Note!** `TestServer`is not built by default. It's a help project (NancyFx web server) that you can spin up as a web server and use as an endpoint for callbacks form Paynova's API's. Look in the `App.Config` to see what it listens on. An example enpoint is: `http:\\localhost:8091\echo` accepting `POSTs`. The [Nancy Diagnostics](https://github.com/NancyFx/Nancy/wiki/Diagnostics) is also enabled, so you can visit: `http:\\localhost:8091\_nancy` and sign in with `test` (controlled via `App.Config`).

## License ##
For more information about the license, look in the [LICENSE.md](https://github.com/Paynova/paynova-api-net-client/blob/master/LICENSE.md) file
