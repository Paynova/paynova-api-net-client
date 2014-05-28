using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Nancy;
using Newtonsoft.Json;

namespace Paynova.TestServer.Modules
{
    public abstract class DiagModule : NancyModule
    {
        protected JsonSerializer Serializer { get; private set; }

        protected DiagModule(string modulePath, JsonSerializer serializer)
            : base(modulePath)
        {
            Serializer = serializer;
        }

        protected virtual dynamic GetJson(dynamic p)
        {
            var data = new
            {
                RequestUrl = Request.Url.ToString(),
                At = DateTime.Now,
                Accept = Request.Headers.Accept,
                ContentType = Request.Headers.ContentType,
                Query = GetAsDictionary(Request.Query),
                Form = GetAsDictionary(Request.Form),
                Content = GetBodyAsString(),
            };

            var sb = new StringBuilder();

            using (var writer = new JsonTextWriter(new StringWriter(sb)))
                Serializer.Serialize(writer, data);

            return sb.ToString();
        }

        protected virtual Dictionary<string, dynamic> GetAsDictionary(dynamic d)
        {
            var r = new Dictionary<string, dynamic>();

            DynamicDictionary t = d;
            foreach (var k in t.Keys)
            {
                if (t[k] is DynamicDictionaryValue)
                {
                    DynamicDictionaryValue v = t[k];
                    r.Add(k, v.HasValue ? v.Value : null);
                }
                else
                    r.Add(k, t[k]);
            }
            return r;
        } 

        protected virtual string GetBodyAsString()
        {
            if (Request.Body.Length < 1)
                return null;

            using (var reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}