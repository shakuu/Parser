using System;

using Moq;
using NUnit.Framework;

using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.ViewModels;

namespace Parser.Data.Services.Tests.CreateParserUserServiceTests
{
    [TestFixture]
    public class CreateParserUser_Should
    {
        [Test]
        public void ThrowArgumentNullException_WhenParserUserViewModelParameterIsNull()
        {
            // Arrange
            var parserUserDataProvider = new Mock<IParserUserDataProvider>();
            var entityFrameworkTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();

            var createParserUserService = new CreateParserUserService(parserUserDataProvider.Object, entityFrameworkTransactionFactory.Object);

            ParserUserViewModel model = null;

            // Act & Assert
            Assert.That(
                () => createParserUserService.CreateParserUser(model),
                Throws.InstanceOf<ArgumentNullException>().With.Message.Contains(nameof(ParserUserViewModel)));
        }

        [Test]
        public void InvokeIEntityFrameworkTransactionFactory_CreateEntityFrameworkTransactionMethodOnce()
        {
            // Arrange
            var parserUserDataProvider = new Mock<IParserUserDataProvider>();
            var entityFrameworkTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();

            var transaction = new Mock<IEntityFrameworkTransaction>();
            entityFrameworkTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(transaction.Object);

            var createParserUserService = new CreateParserUserService(parserUserDataProvider.Object, entityFrameworkTransactionFactory.Object);

            var model = new ParserUserViewModel();

            // Act
            createParserUserService.CreateParserUser(model);

            // Assert
            entityFrameworkTransactionFactory.Verify(f => f.CreateEntityFrameworkTransaction(), Times.Once);
        }

        [Test]
        public void InvokeIParserUserDataProvider_CreateParserUserMethodOnceWithCorrectParameter()
        {
            // Arrange
            var parserUserDataProvider = new Mock<IParserUserDataProvider>();
            var entityFrameworkTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();

            var transaction = new Mock<IEntityFrameworkTransaction>();
            entityFrameworkTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(transaction.Object);

            var createParserUserService = new CreateParserUserService(parserUserDataProvider.Object, entityFrameworkTransactionFactory.Object);

            var model = new ParserUserViewModel();

            // Act
            createParserUserService.CreateParserUser(model);

            // Assert
            parserUserDataProvider.Verify(p => p.CreateParserUser(model), Times.Once);
        }

        [Test]
        public void InvokeIEntityFrameworkTransaction_SaveChangesMethodOnce()
        {
            // Arrange
            var parserUserDataProvider = new Mock<IParserUserDataProvider>();
            var entityFrameworkTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();

            var transaction = new Mock<IEntityFrameworkTransaction>();
            entityFrameworkTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(transaction.Object);

            var createParserUserService = new CreateParserUserService(parserUserDataProvider.Object, entityFrameworkTransactionFactory.Object);

            var model = new ParserUserViewModel();

            // Act
            createParserUserService.CreateParserUser(model);

            // Assert
            transaction.Verify(t => t.SaveChanges(), Times.Once);
        }

        [Test]
        public void ReturnCorrectParserUserViewModelInstance()
        {
            // Arrange
            var parserUserDataProvider = new Mock<IParserUserDataProvider>();
            var entityFrameworkTransactionFactory = new Mock<IEntityFrameworkTransactionFactory>();

            var transaction = new Mock<IEntityFrameworkTransaction>();
            entityFrameworkTransactionFactory.Setup(f => f.CreateEntityFrameworkTransaction()).Returns(transaction.Object);

            var createParserUserService = new CreateParserUserService(parserUserDataProvider.Object, entityFrameworkTransactionFactory.Object);

            var model = new ParserUserViewModel();

            // Act
            var actualReturnedParserUserViewModelInstance = createParserUserService.CreateParserUser(model);

            // Assert
            Assert.That(actualReturnedParserUserViewModelInstance, Is.SameAs(model));
        }
    }
}
