using System.Collections.Generic;

namespace Parser.Common.Contracts
{
    public interface ICommandEnumerationJsonConvertProvider : ICommandJsonConvertProvider
    {
        string SerializeCommandEnumeration(IEnumerable<ICommand> commands);
    }
}
