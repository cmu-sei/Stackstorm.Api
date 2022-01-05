// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Stackstorm.Api.Client;
using NLog.Fluent;

namespace Stackstorm.Connector
{
    public class AzureGov
    {
        private readonly St2Client _client;

        public AzureGov(St2Client client)
        {
            _client = client;
        }
        public async Task<Models.AzureGov.Responses.ResponseBase> GetVms(Models.AzureGov.Requests.BaseRequest request)
        {
            var returnObject = new Models.AzureGov.Responses.ResponseBase();
            var parameters = new Dictionary<string, object>
                {{"tenant", request.Tenant}, {"resource_group", request.ResourceGroup}, {"subscription_id", request.SubscriptionId}, {"client_id", request.ClientId}, {"client_secret", request.ClientSecret}, {"token_url", request.TokenUrl}, {"resource_url", request.ResourceUrl}};
            var executionResult = await _client.AzureGov.GetVms(parameters);
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var resultObject = (JObject)executionResult.result;
                var stdout = ((JValue)resultObject.GetValue("stdout")).ToString();
                var stderr = ((JValue)resultObject.GetValue("stderr")).ToString();
                if (stderr == "")
                {
                    returnObject.Value = resultObject.GetValue("result").ToString();
                }
                else
                {
                    returnObject.Value = "***ERROR***: " + stderr;

                }
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
                returnObject.Value = e.InnerException != null ? e.InnerException.Message : e.Message;
            }

            return returnObject;
        }

        public async Task<Models.AzureGov.Responses.ResponseBase> VmPowerOff(Models.AzureGov.Requests.VmOnOff request)
        {
            var returnObject = new Models.AzureGov.Responses.ResponseBase();
            var parameters = new Dictionary<string, object>
                {{"vm_name", request.VmName}};
            var executionResult = await _client.AzureGov.PowerOff(parameters);
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var resultObject = (JObject)executionResult.result;
                var stderr = ((JValue)resultObject.GetValue("stderr")).ToString();
                if (stderr == "")
                {
                    returnObject.Value = ((JValue)resultObject.GetValue("result")).ToString();
                }
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
                returnObject.Value = e.InnerException != null ? e.InnerException.Message : e.Message;
            }

            return returnObject;
        }

        public async Task<Models.AzureGov.Responses.ResponseBase> VmPowerOn(Models.AzureGov.Requests.VmOnOff request)
        {
            var returnObject = new Models.AzureGov.Responses.ResponseBase();
            var parameters = new Dictionary<string, object>
                {{"vm_name", request.VmName}};
            var executionResult = await _client.AzureGov.PowerOn(parameters);
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var resultObject = (JObject)executionResult.result;
                var stderr = ((JValue)resultObject.GetValue("stderr")).ToString();
                if (stderr == "")
                {
                    returnObject.Value = ((JValue)resultObject.GetValue("result")).ToString();
                }
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
                returnObject.Value = e.InnerException != null ? e.InnerException.Message : e.Message;
            }

            return returnObject;
        }

        public async Task<Models.AzureGov.Responses.ResponseBase> ShellScript(Models.AzureGov.Requests.VmShellScript request)
        {
            var returnObject = new Models.AzureGov.Responses.ResponseBase();
            var scriptLines = request.Script.Split('\n');
            var script = "[\"";
            foreach (var line in scriptLines)
            {
                script = script + line.Replace("\"", "\\\"") + "\",\"";
            }
            script = script.Substring(0, script.Length - 2) + "]";
            var paramLines = request.Params.Split('\n');
            var scriptParams = "[";
            if (paramLines[0].Contains("="))
            {
                foreach (var line in paramLines)
                {
                    var parts = line.Split('=');
                    scriptParams = scriptParams + "{\"name\": \"" + parts[0] + "\",\"value\":\"" + parts[1].Replace("\"", "\\\"") + "\"},";
                }
                scriptParams = scriptParams.Substring(0, scriptParams.Length - 1) + "]";
            }
            else
            {
                scriptParams = "[]";
            }
            // var scriptParams = JArray.Parse(request.Params);
            var parameters = new Dictionary<string, object>
                {{"script", script}, {"params", scriptParams}, {"target_shell", request.Shell}, {"vm_name", request.VmName}};
            var executionResult = await _client.AzureGov.VmShellScript(parameters);
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var resultObject = (JObject)executionResult.result;
                var stdout = ((JValue)resultObject.GetValue("stdout")).ToString();
                var stderr = ((JValue)resultObject.GetValue("stderr")).ToString();
                if (stderr == "")
                {
                    var resultResultObject = (JObject)resultObject.GetValue("result");
                    var valueArray = (JArray)resultResultObject.GetValue("value");
                    var message = valueArray[0]["message"].ToString();
                    // bash message contains [stdout], while PowerShell just contains the message
                    var indexOfStdout = message.IndexOf("[stdout]");
                    if (indexOfStdout < 0)
                    {
                        returnObject.Value = message;
                    }
                    else
                    {
                        var start = indexOfStdout + 9;
                        var length = message.LastIndexOf("[stderr]") - start - 2;
                        returnObject.Value = message.Substring(start, length);
                    }
                }
                else
                {
                    returnObject.Value = "***ERROR***: " + stderr;

                }
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
                returnObject.Value = e.InnerException != null ? e.InnerException.Message : e.Message;
            }

            return returnObject;
        }
    }
}
