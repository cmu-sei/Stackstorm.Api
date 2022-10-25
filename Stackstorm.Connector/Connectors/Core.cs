// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Stackstorm.Connector.Models.Vsphere;
using Stackstorm.Api.Client;
using Stackstorm.Api.Client.Extensions;
using NLog.Fluent;

namespace Stackstorm.Connector
{
    public class Core
    {
        private readonly St2Client _client;

        public Core(St2Client client)
        {
            _client = client;
        }
        public async Task<Models.Core.Responses.SendLinuxRemoteCommand> SendLinuxRemoteCommand(Models.Core.Requests.SendLinuxRemoteCommand request)
        {
            var returnObject = new Models.Core.Responses.SendLinuxRemoteCommand();
            var executionResult = await _client.Core.SendLinuxRemoteCommand(new Dictionary<string, object>
                {{"username", request.Username}, {"private_key", request.PrivateKey}, {"hosts", request.Hosts}, {"port", request.Port},
                 {"cmd", request.Cmd}, {"cwd", request.Cwd}, {"env", request.Env}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                returnObject.Value = executionResult.output;
                returnObject.Success = executionResult.status.ToLower() == "succeeded";
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
                returnObject.Success = false;
            }

            return returnObject;
        }

    }
}
