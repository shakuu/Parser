namespace Parser.Common.Contracts
{
    public interface IObjectMapperProvider
    {
        TDestination Map<TDestination>(object source);
    }
}
