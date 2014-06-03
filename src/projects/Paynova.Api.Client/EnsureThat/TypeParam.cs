using System;

namespace Paynova.Api.Client.EnsureThat
{
    public class TypeParam : Param
    {
        public readonly Type Type;

        internal TypeParam(string name, Type type) : this(name, type, null)
        {
        }

        internal TypeParam(string name, Type type, Func<string> extraMessageFn)
            : base(name, extraMessageFn)
        {
            Type = type;
        }
    }
}