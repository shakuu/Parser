using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Data.Contracts;
using Parser.Data.Models;
using Parser.Data.ViewModels;

namespace Parser.Data.DataProviders
{
    public class ParserUserDataProvider : IParserUserDataProvider
    {
        private readonly IEntityFrameworkRepository<ParserUser> parserUserEntityFrameworkRepository;
        private readonly IObjectMapperProvider objectMapperProvider;

        public ParserUserDataProvider(IEntityFrameworkRepository<ParserUser> parserUserEntityFrameworkRepository, IObjectMapperProvider objectMapperProvider)
        {
            Guard.WhenArgument(parserUserEntityFrameworkRepository, nameof(IEntityFrameworkRepository<ParserUser>)).IsNull().Throw();
            Guard.WhenArgument(objectMapperProvider, nameof(IObjectMapperProvider)).IsNull().Throw();

            this.parserUserEntityFrameworkRepository = parserUserEntityFrameworkRepository;
            this.objectMapperProvider = objectMapperProvider;
        }

        public ParserUserViewModel CreateParserUser(ParserUserViewModel model)
        {
            Guard.WhenArgument(model, nameof(ParserUserViewModel)).IsNull().Throw();

            var parserUser = this.objectMapperProvider.Map<ParserUser>(model);

            var dbParserUser = this.parserUserEntityFrameworkRepository.Create(parserUser);

            var viewModel = this.objectMapperProvider.Map<ParserUserViewModel>(dbParserUser);

            return viewModel;
        }
    }
}
