namespace Parser.LogFile.Reader.Contracts
{
    public interface IEnvironmentFolderPathProvider
    {
        string GetEnvironmentFolderPath(string folderName);
    }
}
