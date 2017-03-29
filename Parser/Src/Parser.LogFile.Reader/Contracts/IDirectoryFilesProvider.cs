using System.Collections.Generic;

namespace Parser.LogFile.Reader.Contracts
{
    public interface IDirectoryFilesProvider
    {
        IEnumerable<string> GetDirectoryFiles(string directoryPath);
    }
}
