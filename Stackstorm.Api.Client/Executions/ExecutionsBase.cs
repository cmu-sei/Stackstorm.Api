// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NLog;
using Stackstorm.Api.Client;
using Stackstorm.Api.Client.Models;

namespace stackstorm.api.client.Executions
{
    public class ExecutionsBase
    {
        private ISt2Client _host;
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        protected ExecutionsBase(ISt2Client host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));
        }

        internal async Task<Execution> AddExecution(string action, Dictionary<string, object> parameters)
        {
            if (!_host.HasToken())
            {
                await _host.RefreshTokenAsync();
            }
            try
            {
                // var tempParameters = new Dictionary<string, object>();
                // //delete any empty parameters
                // if (parameters != null)
                // {
                //     foreach (var p in parameters)
                //         if (!string.IsNullOrEmpty(p.Value))
                //             tempParameters.Add(p.Key, p.Value);
                // }

                var executionRequest = new ExecutionRequest(action, parameters);

                //var requestString = Newtonsoft.Json.JsonConvert.SerializeObject(executionRequest);
                //Console.WriteLine(requestString);

                var r = await _host.PostApiRequestAsync<Execution, ExecutionRequest>("v1/executions", executionRequest);
                return await Resolve(r);
            }
            catch (Exception e)
            {
                _log.Error(e);
                return null;
            }
        }

        private async Task<Execution> Resolve(Execution executionResult)
        {
            if (executionResult.id == null)
                throw new Exception();

            while (executionResult.IsComplete() == false)
            {
                executionResult = await _host.Executions.GetExecutionAsync(executionResult.id);
                _log.Trace($"Execution status is {executionResult.status}");
                Thread.Sleep(2000);
            }

            return executionResult;
        }
    }
}
