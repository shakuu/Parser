using System;

using Moq;
using NUnit.Framework;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.DataProviders;
using Parser.Data.Models;
using Parser.Data.ViewModels;

namespace Parser.Data.Tests.DataProvidersTests.ParserUserDataProviderTests
{
    [TestFixture]
    public class CreateParserUser_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenParserUserViewModelParameter()
        {
            // Arrange
            var parserUserEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<ParserUser>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var parserUserDataProvider = new ParserUserDataProvider(parserUserEntityFrameworkRepository.Object, objectMapperProvider.Object);

            ParserUserViewModel model = null;

            // Act & Assert
            Assert.That(
                () => parserUserDataProvider.CreateParserUser(model),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ParserUserViewModel)));
        }

        [Test]
        public void InvokeIObjectMapperProvider_MapParserUserMethodOnceWithCorrectParameters()
        {
            // Arrange
            var parserUserEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<ParserUser>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var parserUserDataProvider = new ParserUserDataProvider(parserUserEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new ParserUserViewModel();

            // Act
            parserUserDataProvider.CreateParserUser(model);

            // Assert
            objectMapperProvider.Verify(p => p.Map<ParserUser>(model), Times.Once);
        }

        [Test]
        public void InvokeIEntityFrameworkRepository_CreateMethodOnceWithCorrectParameters()
        {
            // Arrange
            var parserUserEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<ParserUser>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var parserUserDataProvider = new ParserUserDataProvider(parserUserEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new ParserUserViewModel();

            var parserUser = new ParserUser();
            objectMapperProvider.Setup(p => p.Map<ParserUser>(It.IsAny<ParserUserViewModel>())).Returns(parserUser);

            // Act
            parserUserDataProvider.CreateParserUser(model);

            // Assert
            parserUserEntityFrameworkRepository.Verify(r => r.Create(parserUser), Times.Once);
        }

        [Test]
        public void InvokeIObjectMapperProvider_MapParserUserViewModelOnceWithCorrectParameters()
        {
            // Arrange
            var parserUserEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<ParserUser>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var parserUserDataProvider = new ParserUserDataProvider(parserUserEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new ParserUserViewModel();

            var dbParserUser = new ParserUser();
            parserUserEntityFrameworkRepository.Setup(r => r.Create(It.IsAny<ParserUser>())).Returns(dbParserUser);

            // Act
            parserUserDataProvider.CreateParserUser(model);

            // Assert
            objectMapperProvider.Verify(p => p.Map<ParserUserViewModel>(dbParserUser), Times.Once);
        }

        [Test]
        public void ReturnCorrectParserUserViewModelInstance()
        {
            // Arrange
            var parserUserEntityFrameworkRepository = new Mock<IEntityFrameworkRepository<ParserUser>>();
            var objectMapperProvider = new Mock<IObjectMapperProvider>();

            var parserUserDataProvider = new ParserUserDataProvider(parserUserEntityFrameworkRepository.Object, objectMapperProvider.Object);

            var model = new ParserUserViewModel();

            var expectedReturnedParserUserViewModelInstance = new ParserUserViewModel();
            objectMapperProvider.Setup(p => p.Map<ParserUserViewModel>(It.IsAny<ParserUser>())).Returns(expectedReturnedParserUserViewModelInstance);

            // Act
            var actualReturnedParserUserViewModelInstance = parserUserDataProvider.CreateParserUser(model);

            // Assert
            Assert.That(actualReturnedParserUserViewModelInstance, Is.SameAs(expectedReturnedParserUserViewModelInstance));
        }
    }
}
