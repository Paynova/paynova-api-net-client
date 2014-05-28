using System.Diagnostics;

namespace Paynova.Api.Client.Testing.Shoulds
{
    [DebuggerStepThrough]
    public abstract class ShouldBe<T>
    {
        protected T Item { get; private set; }

        protected ShouldBe(T item)
        {
            Item = item;
        }
    }
}