using System;

namespace Paynova.Api.Client
{
    public interface IDateTimeProvider
    {
        DateTime Now { get; }
    }
}