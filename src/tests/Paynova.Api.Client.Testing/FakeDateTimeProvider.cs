using System;

namespace Paynova.Api.Client.Testing
{
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        private static readonly DateTime FakeNow = new DateTime(2014,4,1, 12,0,0);

        public DateTime Now { get { return FakeNow; } }
    }
}