using System.Threading;

using Parser.LogFileReader.Contracts;

namespace Parser.LogFileReader.FileSystemProviders
{
    public class FileReaderAutoResetEvent : IFileReaderAutoResetEvent
    {
        private readonly AutoResetEvent autoResetEvent;

        public FileReaderAutoResetEvent(bool initialState)
        {
            this.autoResetEvent = new AutoResetEvent(initialState);
        }

        public bool Set()
        {
            return this.autoResetEvent.Set();
        }

        public bool WaitOne(int milisecondsTimeout)
        {
            return this.autoResetEvent.WaitOne(milisecondsTimeout);
        }
    }
}
