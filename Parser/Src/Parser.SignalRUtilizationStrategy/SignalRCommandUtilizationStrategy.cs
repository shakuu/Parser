﻿using System;
using System.Threading.Tasks;

using Microsoft.AspNet.SignalR.Client;

using Bytes2you.Validation;

using Parser.LogFileReader.Contracts;
using Parser.SignalRUtilizationStrategy.Contracts;

namespace Parser.SignalRUtilizationStrategy
{
    public class SignalRCommandUtilizationStrategy : ICommandUtilizationStrategy
    {
        private readonly IHubConnectionProviderFactory hubConnectionProviderFactory;
        private readonly IJsonConvertProvider jsonConvertProvider;

        private readonly IHubProxy logFileParserHubProxy;
        private string assignedId;

        public SignalRCommandUtilizationStrategy(IHubConnectionProviderFactory hubConnectionProviderFactory, IJsonConvertProvider jsonConvertProvider)
        {
            Guard.WhenArgument(hubConnectionProviderFactory, nameof(IHubConnectionProviderFactory)).IsNull().Throw();
            Guard.WhenArgument(jsonConvertProvider, nameof(IJsonConvertProvider)).IsNull().Throw();

            this.hubConnectionProviderFactory = hubConnectionProviderFactory;
            this.jsonConvertProvider = jsonConvertProvider;

            this.logFileParserHubProxy = this.CreateProxy("http://localhost:52589").Result;
        }

        public void UtilizeCommand(ICommand command)
        {
            var serializedCommand = this.jsonConvertProvider.SerializeObject(command);

            this.logFileParserHubProxy.Invoke("SendCommand", this.assignedId, serializedCommand);
        }

        private async Task<IHubProxy> CreateProxy(string url)
        {
            var connection = this.hubConnectionProviderFactory.CreateHubConnectionProvider(url);

            var logFileParserHubProxy = connection.CreateHubProxy("LogFileParserHub");

            logFileParserHubProxy.On<string>("ReceiveStatus", (status) =>
            {
                Console.WriteLine(status);
            });

            logFileParserHubProxy.On<string>("UpdateUserId", this.OnUpdateUserId);

            await connection.Start();
            await logFileParserHubProxy.Invoke("GetUserId");

            return logFileParserHubProxy;
        }

        private void OnUpdateUserId(string userId)
        {
            this.assignedId = userId;
        }
    }
}
