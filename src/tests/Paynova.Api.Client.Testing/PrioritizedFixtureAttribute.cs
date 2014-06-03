using Xunit;

namespace Paynova.Api.Client.Testing
{
    /// <summary>
    /// See <![CDATA[https://github.com/xunit/xunit/tree/v1/samples/PrioritizedFixtureExample]]>
    /// </summary>
    public class PrioritizedFixtureAttribute : RunWithAttribute
    {
        public PrioritizedFixtureAttribute() : base(typeof(PrioritizedFixtureClassCommand)) { }
    }
}