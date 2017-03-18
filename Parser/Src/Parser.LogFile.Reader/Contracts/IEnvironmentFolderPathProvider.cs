namespace Parser.LogFileReader.Contracts
{
    public interface IEnvironmentFolderPathProvider
    {
        string GetEnvironmentFolderPath(string folderName);
    }
}
