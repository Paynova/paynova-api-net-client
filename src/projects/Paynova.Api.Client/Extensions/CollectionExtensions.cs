using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace Paynova.Api.Client.Extensions
{
    public static class CollectionExtensions
    {
        public static Dictionary<string, string> ToDictionary(this NameValueCollection data)
        {
            return data.AllKeys
                .Select(key => new KeyValuePair<string, string>(key, data.Get(key)))
                .ToDictionary(kv => kv.Key, kv => kv.Value);
        }
    }
}