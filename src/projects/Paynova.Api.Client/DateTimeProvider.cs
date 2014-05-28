using System;

namespace Paynova.Api.Client
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime Now { get { return DateTime.Now; } }
    }
}