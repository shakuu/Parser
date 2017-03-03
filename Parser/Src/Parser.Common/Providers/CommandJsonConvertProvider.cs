using Bytes2you.Validation;

using Parser.Common.Contracts;
using Parser.Common.Models;

namespace Parser.Common.Providers
{
    public class CommandJsonConvertProvider : ICommandJsonConvertProvider
    {
        private readonly IJsonConvertProvider jsonConvertProvider;

        public CommandJsonConvertProvider(IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.jsonConvertProvider = jsonConvertProvider;
        }
        
        public string SerializeCommand(ICommand command)
        {
            if (command == null)
            {
                return null;
            }

            return this.jsonConvertProvider.SerializeObject(command);
        }

        public ICommand DeserializeCommand(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }

            return this.jsonConvertProvider.DeserializeObject<Command>(value);
        }
    }
}
