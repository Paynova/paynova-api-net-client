using System;

namespace Paynova.Api.Client.Testing
{
    public abstract class TestsOf<TSut> : Tests, IDisposable
    {
        protected bool IsDisposed { get; private set; }
        protected TSut SUT { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            IsDisposed = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed || !disposing)
                return;

            TryAndDispose(SUT);
        }

        protected virtual void TryAndDispose<T>(T i)
        {
            var obj = i as IDisposable;

            if (obj == null)
                return;

            obj.Dispose();
        }
    }
}