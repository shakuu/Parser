using System.Collections.Generic;

namespace Parser.LogFileReader.Contracts
{
    public interface IDirectoryFilesProvider
    {
        IEnumerable<string> GetDirectoryFiles(string directoryPath);
    }
}
