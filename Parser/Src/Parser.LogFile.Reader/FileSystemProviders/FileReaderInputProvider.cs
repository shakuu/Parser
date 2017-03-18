using System;
using System.IO;

using Bytes2you.Validation;
using Parser.LogFile.Reader.Contracts;

namespace Parser.LogFile.Reader.FileSystemProviders
{
    public class FileReaderInputProvider : IFileReaderInputProvider, IDisposable
    {
        private readonly FileStream fileStream;
        private readonly StreamReader streamReader;

        public FileReaderInputProvider(string filePath)
        {
            this.fileStream = this.CreateFileStream(filePath);
            this.streamReader = this.CreateStreamReader(this.fileStream);
        }

        public string ReadLine()
        {
            return this.streamReader.ReadLine();
        }

        public void Dispose()
        {
            this.fileStream.Close();
        }

        private FileStream CreateFileStream(string filePath)
        {
            Guard.WhenArgument(filePath, nameof(filePath)).IsNullOrEmpty().Throw();

            if (!File.Exists(filePath))
            {
                throw new ArgumentException("File does not exists.");
            }

            return new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        private StreamReader CreateStreamReader(FileStream fileStream)
        {
            Guard.WhenArgument(fileStream, nameof(FileStream)).IsNull().Throw();

            return new StreamReader(fileStream);
        }
    }
}
