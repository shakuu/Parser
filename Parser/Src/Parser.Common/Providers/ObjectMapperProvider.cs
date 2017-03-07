using AutoMapper;

using Bytes2you.Validation;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class ObjectMapperProvider : IObjectMapperProvider
    {
        private readonly IMapper mapper;

        public ObjectMapperProvider(IMapper mapper)
        {
            Guard.WhenArgument(mapper, nameof(IMapper)).IsNull().Throw();

            this.mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
        {
            Guard.WhenArgument(source, nameof(source)).IsNull().Throw();

            return this.mapper.Map<TDestination>(source);
        }
    }
}
