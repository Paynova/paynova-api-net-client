namespace Paynova.Api.Client.Serialization
{
    public interface ISerializer
    {
        string Serialize<T>(T item);
        T Deserialize<T>(string data) where T : class;
    }
}