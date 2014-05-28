using System.Configuration;
using Paynova.Api.Client.Net;

namespace Paynova.Api.Client.IntegrationTests
{
    public static class IntegrationTestsRuntime
    {
        public static TestEnvironment Environment { get; private set; }

        static IntegrationTestsRuntime()
        {
            Environment = LoadTestEnvironment();
        }

        private static TestEnvironment LoadTestEnvironment()
        {
            return new TestEnvironment
            {
                ServerUrl = ReadAppSetting("paynova_client_serverurl"),
                Username = ReadAppSetting("paynova_client_username"),
                Password = ReadAppSetting("paynova_client_password")
            };
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