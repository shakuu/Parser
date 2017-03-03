namespace Parser.Common.Contracts
{
    public interface ICommandJsonConvertProvider
    {
        string SerializeCommand(ICommand command);

        ICommand DeserializeCommand(string value);
    }
}
