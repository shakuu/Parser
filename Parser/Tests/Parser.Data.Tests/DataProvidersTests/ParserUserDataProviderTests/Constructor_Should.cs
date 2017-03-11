using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.DataProviders;
using Parser.Data.Models;

namespace Parser.Data.Tests.DataProvidersTests.ParserUserDataProviderTests
{
    [TestFixture]
    public class Constructor_Should
    {
        [Test]
        public void CreateCorrectIParserUserDataProviderInstance_WhenParametersAreCorrect()
        {
            // Arrange
            var parserUserEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<ParserUser>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act
            var actualInstance = new ParserUserDataProvider(parserUserEntityFrameworkRepository.Object, objectMapperProvider.Object);

            // Assert
            Assert.That(actualInstance, Is.Not.Null);
            Assert.That(actualInstance, Is.InstanceOf<IParserUserDataProvider>());
        }

        [Test]
        public void ThrowArgumentNullException_WhenIEntityFrameworkRepositoryParametersIsNull()
        {
            // Arrange
            IEntityFrameworkRepository<ParserUser> parserUserEntityFrameworkRepository = null;
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            // Act & Assert
            Assert.That(
                () => new ParserUserDataProvider(parserUserEntityFrameworkRepository, objectMapperProvider.Object),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IEntityFrameworkRepository<ParserUser>)));
        }

        [Test]
        public void ThrowArgumentNullException_WhenIObjectMapperProviderParametersIsNull()
        {
            // Arrange
            var parserUserEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<ParserUser>>();
            IObjectMapperProvider objectMapperProvider = null;

            // Act & Assert
            Assert.That(
                () => new ParserUserDataProvider(parserUserEntityFrameworkRepository.Object, objectMapperProvider),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(IObjectMapperProvider)));
        }
    }
}
