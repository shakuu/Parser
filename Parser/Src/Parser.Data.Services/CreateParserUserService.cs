using Bytes2you.Validation;

using Parser.Data.Contracts;
using Parser.Data.Factories;
using Parser.Data.Services.Contracts;
using Parser.Data.ViewModels;

namespace Parser.Data.Services
{
    public class CreateParserUserService : ICreateParserUserService
    {
        private readonly IParserUserDataProvider parserUserDataProvider;
        private readonly IEntityFrameworkTransactionFactory entityFrameworkTransactionFactory;

        public CreateParserUserService(IParserUserDataProvider parserUserDataProvider, IEntityFrameworkTransactionFactory entityFrameworkTransactionFactory)
        {
            Guard.WhenArgument(parserUserDataProvider, nameof(IParserUserDataProvider)).IsNull().Throw();
            Guard.WhenArgument(entityFrameworkTransactionFactory, nameof(IEntityFrameworkTransactionFactory)).IsNull().Throw();

            this.parserUserDataProvider = parserUserDataProvider;
            this.entityFrameworkTransactionFactory = entityFrameworkTransactionFactory;
        }

        public ParserUserViewModel CreateParserUser(ParserUserViewModel model)
        {
            Guard.WhenArgument(model, nameof(ParserUserViewModel)).IsNull().Throw();

            using (var transaction = this.entityFrameworkTransactionFactory.CreateEntityFrameworkTransaction())
            {
                this.parserUserDataProvider.CreateParserUser(model);

                transaction.SaveChanges();
            }

            return model;
        }
    }
}
