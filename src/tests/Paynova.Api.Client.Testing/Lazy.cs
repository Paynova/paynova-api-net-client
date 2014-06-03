using System;

namespace Paynova.Api.Client.Testing
{
    public class Lazy<T> where T : class
    {
        private T _instance;
        private readonly Func<T> _fn;
        private readonly object _sync;

        public T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                lock (_sync)
                {
                    return _instance ?? (_instance = _fn());
                }
            }
        }

        public Lazy(Func<T> fn)
        {
            _fn = fn;
            _sync = new object();
        }
    }
}