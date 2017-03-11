using System;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.Services.Contracts;

namespace Parser.Data.Services.Tests.CreateParserUserServiceTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectICreateParserUserServiceInstance_WhenParametersAreValid()
        {
            // Arrange
            var parserUserDataProvider = new Mock<IParserUserDataProvider>();
            var entityFrameworkTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();

            // Act
            var actualInstance = new CreateParserUserService(parserUserDataProvider.Object, entityFrameworkTransactionFactory.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<ICreateParserUserService>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIParserUserDataProviderParameterIsNull()
        {
            // Arrange
            IParserUserDataProvider parserUserDataProvider = null;
            var entityFrameworkTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();

            // Act & Assert
            Assert.That(
                () => new CreateParserUserService(parserUserDataProvider, entityFrameworkTransactionFactory.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IParserUserDataProvider)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIEntityFrameworkTransactionFactoryParameterIsNull()
        {
            // Arrange
            var parserUserDataProvider = new Mock<IParserUserDataProvider>();
            IEntityFrameworkTransactionFactory entityFrameworkTransactionFactory = null;

            // Act & Assert
            Assert.That(
                () => new CreateParserUserService(parserUserDataProvider.Object, entityFrameworkTransactionFactory),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IEntityFrameworkTransactionFactory)));
        }
    }
}
