using Paynova.Api.Client.Testing;

namespace Paynova.Api.Client.IntegrationTests
{
    public abstract class IntegrationTestsOf<TSut> : TestsOf<TSut>
    {
        protected TestEnvironment Environment { get; private set; }
        protected IPaynovaClient Client { get; private set; }

        protected IntegrationTestsOf()
        {
            Environment = IntegrationTestsRuntime.Environment;
            Client = IntegrationTestsRuntime.CreateClient();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if(IsDisposed || !disposing)
                return;

            TryAndDispose(Client);
        }
    }
}