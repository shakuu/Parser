using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Common.Providers;

namespace Parser.Common.Tests.ProvidersTests.CommandJsonConvertProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateICommandJsonConvertProviderInstance_WhenParametersAreValid()
        {
            // Arrange
            var jsonConvertProvider = new Mock<IJsonConvertProvider>();

            // Act
            var actualInstance = new CommandJsonConvertProvider(jsonConvertProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null.And.InstanceOf<ICommandJsonConvertProvider>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIJsonConvertProviderParameterIsNull()
        {
            // Arrange
            IJsonConvertProvider jsonConvertProvider = null;

            // Act & Assert
            Assert.That(
                () => new CommandJsonConvertProvider(jsonConvertProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IJsonConvertProvider)));
        }
    }
}
