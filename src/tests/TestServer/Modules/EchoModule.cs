using System;
using Nancy;
using Newtonsoft.Json;

namespace Paynova.TestServer.Modules
{
    public class EchoModule : DiagModule
    {
        public EchoModule(JsonSerializer serializer) : base("echo", serializer)
        {
            Get[@"/{message}"] = p => Response.AsJson(new { At = DateTime.Now, Msg = p.message });
            Post["/"] = Echo;
            Put["/"] = Echo;
        }

        private dynamic Echo(dynamic p)
        {
            var json = GetJson(p);

            return FormatterExtensions.AsText(Response, json, "application/json");
        }
    }
}