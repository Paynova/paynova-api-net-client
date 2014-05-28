using System;
using Nancy;
using Newtonsoft.Json;

namespace Paynova.TestServer.Modules
{
    public class LogModule : DiagModule
    {
        public LogModule(JsonSerializer serializer) : base("log", serializer)
        {
            Get[@"/{message}"] = p => Response.AsJson(new { At = DateTime.Now, Msg = p.message });
            Post["/"] = Log;
            Put["/"] = Log;
        }

        private dynamic Log(dynamic p)
        {
            var json = GetJson(p);

            Context.Trace.TraceLog.WriteLog(s => s.AppendLine(json));
            Console.WriteLine(json);

            return HttpStatusCode.OK;
        }
    }
}