using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser.SignalR.Contracts
{
    public interface ILogFileParserHubService
    {
        string SendCommand(string engineId, string serializedCommand);

        string GetParsingSessionId();
    }
}
