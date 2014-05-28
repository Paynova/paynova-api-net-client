namespace Paynova.Api.Client.EnsureThat
{
    public static class Ensure
    {
        public static Param<T> That<T>(T value)
        {
            return new Param<T>(Param.DefaultName, value);
        }

        public static Param<T> That<T>(T value, string name)
        {
            return new Param<T>(name, value);
        }

        public static TypeParam ThatTypeFor<T>(T value)
        {
            return new TypeParam(Param.DefaultName, value.GetType());
        }

        public static TypeParam ThatTypeFor<T>(T value, string name)
        {
            return new TypeParam(name, value.GetType());
        }
    }
}