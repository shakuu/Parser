using System;

using Bytes2you.Validation;

using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.FileSystemProviders
{
    public class EnvironmentFolderPathProvider : IEnvironmentFolderPathProvider
    {
        public string GetEnvironmentFolderPath(string folderName)
        {
            Guard.WhenArgument(folderName, nameof(folderName)).IsNullOrEmpty().Throw();

            Environment.SpecialFolder environmentSpecialFolderValue;
            var isSuccessfullyParsed = Enum.TryParse<Environment.SpecialFolder>(folderName, out environmentSpecialFolderValue);
            if (isSuccessfullyParsed)
            {
                return Environment.GetFolderPath(environmentSpecialFolderValue);
            }
            else
            {
                throw new ArgumentException(nameof(folderName));
            }
        }
    }
}
