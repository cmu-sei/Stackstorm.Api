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
    public class Email
    {
        private readonly St2Client _client;

        public Email(St2Client client)
        {
            _client = client;
        }
        public async Task<Models.Email.Responses.EmailSent> SendEmail(Models.Email.Requests.EmailSend request)
        {
            var returnObject = new Models.Email.Responses.EmailSent();
            var executionResult = await _client.Email.SendEmail(new Dictionary<string, object>
                {{"account", request.Account}, {"email_from", request.EmailFrom}, {"email_to", request.EmailTo}, {"message", request.Message},
                 {"subject", request.Subject}, {"mime", request.Mime}, {"email_cc", request.EmailCC}});
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
