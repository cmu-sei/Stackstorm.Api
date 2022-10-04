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
    public class Linux
    {
        private readonly St2Client _client;

        public Linux(St2Client client)
        {
            _client = client;
        }
        public async Task<Models.Linux.Responses.LinuxFileTouch> LinuxFileTouch(Models.Linux.Requests.LinuxFileTouch request)
        {
            var returnObject = new Models.Linux.Responses.LinuxFileTouch();
            var executionResult = await _client.Linux.LinuxFileTouch(new Dictionary<string, object>
                {{"username", request.Username}, {"password", request.Password}, {"hosts", request.Hosts}, {"port", request.Port},
                 {"file", request.File}, /*{"sudo", request.Sudo}, */ {"cwd", request.Cwd}, {"env", request.Env}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
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

       public async Task<Models.Linux.Responses.LinuxRm> LinuxRm(Models.Linux.Requests.LinuxRm request)
        {
            var returnObject = new Models.Linux.Responses.LinuxRm();
            var executionResult = await _client.Linux.LinuxRm(new Dictionary<string, object>
                {{"username", request.Username}, {"password", request.Password}, {"hosts", request.Hosts}, {"port", request.Port},
                 {"target", request.Target}, {"sudo", request.Sudo}, {"cwd", request.Cwd}, {"env", request.Env}, {"args", request.Args},
                 {"force", request.Force}, {"recursive", request.Recursive}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
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
