﻿namespace Parser.FileReader.Contracts
{
    public interface IFileReaderAutoResetEvent
    {
        bool Set();

        bool WaitOne(int milisecondsTimeout);
    }
}
