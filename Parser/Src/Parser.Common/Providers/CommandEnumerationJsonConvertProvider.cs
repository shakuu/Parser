using System.Collections.Generic;

using Bytes2you.Validation;

using Parser.Common.Contracts;

namespace Parser.Common.Providers
{
    public class CommandEnumerationJsonConvertProvider : ICommandEnumerationJsonConvertProvider
    {
        private readonly ICommandJsonConvertProvider commandJsonConvertProvider;
        private readonly IJsonConvertProvider jsonConvertProvider;

        public CommandEnumerationJsonConvertProvider(ICommandJsonConvertProvider commandJsonConvertProvider, IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(commandJsonConvertProvider, nameof(ICommandJsonConvertProvider)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.commandJsonConvertProvider = commandJsonConvertProvider;
            this.jsonConvertProvider = jsonConvertProvider;
        }

        public ICommand DeserializeCommand(string value)
        {
            return this.commandJsonConvertProvider.DeserializeCommand(value);
        }

        public string SerializeCommand(ICommand command)
        {
            return this.commandJsonConvertProvider.SerializeCommand(command);
        }

        public string SerializeCommandEnumeration(IEnumerable<ICommand> commands)
        {
            if (commands == null)
            {
                return null;
            }

            return this.jsonConvertProvider.SerializeObject(commands);
        }
    }
}
