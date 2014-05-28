using System;

namespace Paynova.Api.Client.EnsureThat
{
    public static class CustomMessageExtensions
    {
        public static Param<T> WithExtraMessageOf<T>(this Param<T> param, Func<string> messageFn)
        {
            param.ExtraMessageFn = messageFn;
            return param;
        }
    }
}