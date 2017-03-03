namespace Parser.Common.Contracts
{
    public interface IJsonConvertProvider
    {
        string SerializeObject(object value);

        T DeserializeObject<T>(string value);
    }
}
