using Paynova.Api.Client.Extensions;

namespace Paynova.Api.Client.Serialization
{
    internal class CustomSerializerStrategy : PocoJsonSerializerStrategy
    {
        protected override string MapClrMemberNameToJsonFieldName(string clrPropertyName)
        {
            return clrPropertyName.ToCamelCase();
        }
    }
}