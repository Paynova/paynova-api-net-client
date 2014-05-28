using System;

namespace Paynova.Api.Client.Testing
{
    public class FakeRuntime : IRuntime
    {
        public IDateTimeProvider DateTimeProvider { get; private set; }
        public IFormatProvider NumberFormatProvider { get; private set; }

        public FakeRuntime()
        {
            DateTimeProvider = new FakeDateTimeProvider();
            NumberFormatProvider = Runtime.Instance.NumberFormatProvider;
        }
    }
}