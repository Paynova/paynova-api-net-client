using System.Configuration;
using Microsoft.Extensions.Configuration;
using Paynova.Api.Client.Net;

namespace Paynova.Api.Client.IntegrationTests
{
    public static class IntegrationTestsRuntime
    {
        public static TestEnvironment Environment { get; private set; }

        static IntegrationTestsRuntime()
        {
            var config = LoadConfiguration();
            Environment = LoadTestEnvironment(config);
        }

        private static TestEnvironment LoadTestEnvironment(IConfiguration config)
        {
            return new TestEnvironment
            {
                ServerUrl = config["paynova_client_serverurl"],
                Username = config["paynova_client_username"],
                Password = config["paynova_client_password"],
                CustomerGovernmentId = config["customer_governmentId"],
                CustomerEmail = config["customer_email"],
            };
        }

        private static IConfiguration LoadConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config;
        }

        private static string ReadAppSetting(string key)
        {
            var machineSpecKey = string.Format("{0}@{1}", key, System.Environment.MachineName);

            return ConfigurationManager.AppSettings.Get(machineSpecKey) ?? ConfigurationManager.AppSettings.Get(key);
        }

        public static IPaynovaClient CreateClient()
        {
            return CreateClient(Environment);
        }

        private static IPaynovaClient CreateClient(TestEnvironment environment)
        {
            var uri = new PaynovaApiUriBuilder(environment.ServerUrl)
                .SetBasicCredentials(environment.Username, environment.Password)
                .Build();

            return new PaynovaClient(new BasicHttpConnection(uri));
        }
    }
}