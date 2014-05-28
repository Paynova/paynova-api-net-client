using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using Microsoft.Owin.Hosting;

namespace Paynova.TestServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new StartOptions();
            foreach (var url in AppSettings.HostUrls)
                RegisterHostUrl(url, AppSettings.HostDefaultPort, options.Urls);

            if(AppSettings.HostAutoRegisterIp)
                RegisterLocalIpAsHostUrl(AppSettings.HostDefaultPort, options.Urls);

            using (WebApp.Start<Startup>(options))
            {
                Console.WriteLine("Running on {0}", string.Join(" || ", options.Urls));
                Console.WriteLine("Press enter to exit");
                Console.ReadLine();
            }
        }

        private static void RegisterLocalIpAsHostUrl(int port, ICollection<string> urls)
        {
            var ip = GetLocalIpAddress();
            if(ip != null)
                RegisterHostUrl("http://" + ip, port, urls);
        }

        private static void RegisterHostUrl(string address, int defaultPort, ICollection<string> urls)
        {
            var uri = new Uri(address);
            var uriHasPort = !string.IsNullOrEmpty(uri.GetComponents(UriComponents.Port, UriFormat.UriEscaped));

            if (!uriHasPort)
            {
                var builder = new UriBuilder(address)
                {
                    Port = defaultPort
                };
                uri = builder.Uri;
            }

            if (!urls.Contains(uri.AbsoluteUri))
                urls.Add(uri.AbsoluteUri);
        }

        private static string GetLocalIpAddress()
        {
            var addresses = Dns.GetHostEntry(Dns.GetHostName()).AddressList;

            var address = addresses != null && addresses.Any()
                ? addresses.FirstOrDefault(a => a.AddressFamily == AddressFamily.InterNetwork)
                : null;

            return address != null ? address.ToString() : null;
        }
    }
}
