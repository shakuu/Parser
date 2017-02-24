﻿using System.Threading;

using Moq;
using NUnit.Framework;

using Parser.FileReader.Contracts;
using Parser.FileReader.Engines;
using Parser.FileReader.Factories;

namespace Parser.FileReader.Tests.EnginesTests.FileReaderEngineTests
{
    [TestFixture]
    public class Start_Should
    {
        [Test]
        public void InvokeIFileReaderAutoResetEventFactory_CreateFileReaderAutoResetEventMethod_OnceWithCorrectArgument()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var fileReaderInputProviderMock = new Mock<IFileReaderInputProvider>();
            fileReaderInputProviderMock.Setup(i => i.ReadLine()).Returns<string>(null);
            fileReaderInputProviderFactory.Setup(f => f.CreateFileReaderInputProvider(It.IsAny<string>())).Returns(fileReaderInputProviderMock.Object);

            var fileReaderAutoResetEventMock = new Mock<IFileReaderAutoResetEvent>();
            fileReaderAutoResetEventMock.Setup(e => e.WaitOne(It.IsAny<int>())).Returns(true);
            fileReaderAutoResetEventFactory.Setup(f => f.CreateFileReaderAutoResetEvent(It.IsAny<bool>())).Returns(fileReaderAutoResetEventMock.Object);

            var logFilePath = "Fake Log Path";
            logFilePathDiscoveryStrategy.Setup(s => s.DiscoverLogFile()).Returns(logFilePath);

            var fileReaderEngine = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            // Act
            fileReaderEngine.StartAsync();
            Thread.Sleep(50);
            fileReaderEngine.Stop();

            // Assert
            fileReaderAutoResetEventFactory.Verify(f => f.CreateFileReaderAutoResetEvent(false), Times.Once());
        }

        [Test]
        public void InvokeIFileReaderFileSystemWatcherFactory_CreateFileReaderFileSystemWatcherMethod_OnceWithCorrectArguments()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var fileReaderInputProviderMock = new Mock<IFileReaderInputProvider>();
            fileReaderInputProviderMock.Setup(i => i.ReadLine()).Returns<string>(null);
            fileReaderInputProviderFactory.Setup(f => f.CreateFileReaderInputProvider(It.IsAny<string>())).Returns(fileReaderInputProviderMock.Object);

            var fileReaderAutoResetEventMock = new Mock<IFileReaderAutoResetEvent>();
            fileReaderAutoResetEventMock.Setup(e => e.WaitOne(It.IsAny<int>())).Returns(true);
            fileReaderAutoResetEventFactory.Setup(f => f.CreateFileReaderAutoResetEvent(It.IsAny<bool>())).Returns(fileReaderAutoResetEventMock.Object);

            var logFilePath = "Fake Log Path";
            logFilePathDiscoveryStrategy.Setup(s => s.DiscoverLogFile()).Returns(logFilePath);

            var fileReaderEngine = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            // Act
            fileReaderEngine.StartAsync();
            Thread.Sleep(50);
            fileReaderEngine.Stop();

            // Assert
            fileReaderFileSystemWatcherFactory.Verify(f => f.CreateFileReaderFileSystemWatcher(logFilePath, true, fileReaderAutoResetEventMock.Object), Times.Once());
        }

        [Test]
        public void InvokeIFileReaderInputProviderFactory_CreateFileReaderInputProviderMethod_OnceWithCorrectArgument()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var fileReaderInputProviderMock = new Mock<IFileReaderInputProvider>();
            fileReaderInputProviderMock.Setup(i => i.ReadLine()).Returns<string>(null);
            fileReaderInputProviderFactory.Setup(f => f.CreateFileReaderInputProvider(It.IsAny<string>())).Returns(fileReaderInputProviderMock.Object);

            var fileReaderAutoResetEventMock = new Mock<IFileReaderAutoResetEvent>();
            fileReaderAutoResetEventMock.Setup(e => e.WaitOne(It.IsAny<int>())).Returns(true);
            fileReaderAutoResetEventFactory.Setup(f => f.CreateFileReaderAutoResetEvent(It.IsAny<bool>())).Returns(fileReaderAutoResetEventMock.Object);

            var logFilePath = "Fake Log Path";
            logFilePathDiscoveryStrategy.Setup(s => s.DiscoverLogFile()).Returns(logFilePath);

            var fileReaderEngine = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            // Act
            fileReaderEngine.StartAsync();
            Thread.Sleep(50);
            fileReaderEngine.Stop();

            // Assert
            fileReaderInputProviderFactory.Verify(f => f.CreateFileReaderInputProvider(logFilePath), Times.Once());
        }

        [Test]
        public void InvokeIFileReaderInputProvider_DisposeMethod()
        {
            // Arrange
            var commandParsingStrategy = new Mock<ICommandParsingStrategy>();
            var commandUtilizationStrategy = new Mock<ICommandUtilizationStrategy>();
            var logFilePathDiscoveryStrategy = new Mock<ILogFilePathDiscoveryStrategy>();
            var fileReaderAutoResetEventFactory = new Mock<IFileReaderAutoResetEventFactory>();
            var fileReaderFileSystemWatcherFactory = new Mock<IFileReaderFileSystemWatcherFactory>();
            var fileReaderInputProviderFactory = new Mock<IFileReaderInputProviderFactory>();

            var fileReaderInputProviderMock = new Mock<IFileReaderInputProvider>();
            fileReaderInputProviderMock.Setup(i => i.ReadLine()).Returns<string>(null);
            fileReaderInputProviderFactory.Setup(f => f.CreateFileReaderInputProvider(It.IsAny<string>())).Returns(fileReaderInputProviderMock.Object);

            var fileReaderAutoResetEventMock = new Mock<IFileReaderAutoResetEvent>();
            fileReaderAutoResetEventMock.Setup(e => e.WaitOne(It.IsAny<int>())).Returns(true);
            fileReaderAutoResetEventFactory.Setup(f => f.CreateFileReaderAutoResetEvent(It.IsAny<bool>())).Returns(fileReaderAutoResetEventMock.Object);

            var logFilePath = "Fake Log Path";
            logFilePathDiscoveryStrategy.Setup(s => s.DiscoverLogFile()).Returns(logFilePath);

            var fileReaderEngine = new FileReaderEngine(commandParsingStrategy.Object, commandUtilizationStrategy.Object, logFilePathDiscoveryStrategy.Object, fileReaderAutoResetEventFactory.Object, fileReaderFileSystemWatcherFactory.Object, fileReaderInputProviderFactory.Object);

            // Act
            fileReaderEngine.StartAsync();
            Thread.Sleep(50);
            fileReaderEngine.Stop();

            // Assert
            fileReaderInputProviderMock.Verify(i => i.Dispose(), Times.Once());
        }
    }
}