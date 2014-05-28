using System;

namespace Paynova.Api.Client
{
    public interface IRuntime
    {
        IDateTimeProvider DateTimeProvider { get; }
        IFormatProvider NumberFormatProvider { get; }
    }
}