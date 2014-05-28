using System;
using System.Globalization;
using Paynova.Api.Client.EnsureThat;

namespace Paynova.Api.Client
{
    public class Runtime : IRuntime
    {
        public static IRuntime Instance { get; private set; }
        public IDateTimeProvider DateTimeProvider { get; private set; }
        public IFormatProvider NumberFormatProvider { get; private set; }

        static Runtime()
        {
            Instance = new Runtime();
        }

        private Runtime()
        {
            DateTimeProvider = new DateTimeProvider();
            NumberFormatProvider = CultureInfo.InvariantCulture;
        }

        public static IRuntime ReplaceWith(IRuntime runtime)
        {
            Ensure.That(runtime, "runtime").IsNotNull();

            Instance = runtime;

            return Instance;
        }
    }
}