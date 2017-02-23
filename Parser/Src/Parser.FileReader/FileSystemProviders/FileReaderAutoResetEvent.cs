﻿using System.Threading;

using Parser.FileReader.Contracts;

namespace Parser.FileReader.FileSystemProviders
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
