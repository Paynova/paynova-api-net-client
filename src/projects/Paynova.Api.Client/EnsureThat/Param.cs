using System;

namespace Paynova.Api.Client.EnsureThat
{
    public abstract class Param
    {
        public const string DefaultName = "";
        public Func<string> ExtraMessageFn;

        public readonly string Name;

        protected Param(string name) : this(name, null) { }

        protected Param(string name, Func<string> extraMessageFn)
        {
            Name = name;
            ExtraMessageFn = extraMessageFn;
        }
    }

    public class Param<T> : Param
    {
        public readonly T Value;

        internal Param(string name, T value) : this(name, value, null)
        {
        }

        internal Param(string name, T value, Func<string> extraMessageFn)
            : base(name, extraMessageFn)
        {
            Value = value;
        }
    }
}