using System;
using System.Configuration;

namespace Paynova.TestServer
{
    public static class AppSettings
    {
        private static readonly Lazy<string[]> LazyHostUrls = new Lazy<string[]>(() => ReadArray("host_urls"));
        private static readonly Lazy<bool> LazyHostAutoRegisterIp = new Lazy<bool>(() => ReadBool("host_auto_register_ip"));
        private static readonly Lazy<int> LazyHostDefaultPort = new Lazy<int>(() => ReadInt("host_default_port"));
        private static readonly Lazy<string> LazyHostPassword = new Lazy<string>(() => Read("host_password"));

        public static string[] HostUrls { get { return LazyHostUrls.Value; } }
        public static bool HostAutoRegisterIp { get { return LazyHostAutoRegisterIp.Value; } }
        public static int HostDefaultPort { get { return LazyHostDefaultPort.Value; } }
        public static string HostPassword { get { return LazyHostPassword.Value; } }

        private static string Read(string key)
        {
            var setting = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(setting))
                throw new ConfigurationErrorsException(string.Format("AppSetting: '{0}' is missing value", key));

            return setting;
        }

        private static string[] ReadArray(string key)
        {
            var setting = Read(key);

            return setting == null ? new string[0] : setting.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static int ReadInt(string key)
        {
            return int.Parse(Read(key));
        }

        private static bool ReadBool(string key)
        {
            return bool.Parse(Read(key));
        }
    }
}