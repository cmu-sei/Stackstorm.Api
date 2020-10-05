/*
Crucible
Copyright 2020 Carnegie Mellon University.
NO WARRANTY. THIS CARNEGIE MELLON UNIVERSITY AND SOFTWARE ENGINEERING INSTITUTE MATERIAL IS FURNISHED ON AN "AS-IS" BASIS. CARNEGIE MELLON UNIVERSITY MAKES NO WARRANTIES OF ANY KIND, EITHER EXPRESSED OR IMPLIED, AS TO ANY MATTER INCLUDING, BUT NOT LIMITED TO, WARRANTY OF FITNESS FOR PURPOSE OR MERCHANTABILITY, EXCLUSIVITY, OR RESULTS OBTAINED FROM USE OF THE MATERIAL. CARNEGIE MELLON UNIVERSITY DOES NOT MAKE ANY WARRANTY OF ANY KIND WITH RESPECT TO FREEDOM FROM PATENT, TRADEMARK, OR COPYRIGHT INFRINGEMENT.
Released under a MIT (SEI)-style license, please see license.txt or contact permission@sei.cmu.edu for full terms.
[DISTRIBUTION STATEMENT A] This material has been approved for public release and unlimited distribution.  Please see Copyright notice for non-US Government use and distribution.
Carnegie Mellon(R) and CERT(R) are registered in the U.S. Patent and Trademark Office by Carnegie Mellon University.
DM20-0181
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Stackstorm.Api.Client;
using Stackstorm.Connector.Models.Core;

namespace Stackstorm.Connector
{
    public class Core : StackstormBase
    {
        public Core()
        {

        }

        public Core(string url, string username, string password) : base(url, username, password)
        {

        }

        public async Task<Responses.StringValue> SendLinuxRemoteCommand(Requests.SendLinuxRemoteCommand request)
        {
            var returnObject = new Responses.StringValue();
            var executionResult = await this.Client.Core.SendLinuxRemoteCommand(new Dictionary<string, string>
                {{"hosts", request.Moid}, {"username", request.Username}, {"password", request.Password}, {"cmd", request.Cmd}});
            Log.Trace($"ExecutionResult: {executionResult}");
            returnObject.Id = executionResult.id;

            try
            {
                var jtoken = (JToken) executionResult.result;

                foreach (var hostResult in jtoken.OfType<JProperty>())
                {
                    var propList = hostResult.Value.OfType<JProperty>().ToList<JProperty>();
                    var stderr = propList.Find(p => p.Name == "stderr").Value.ToString();
                    var stdout = propList.Find(p => p.Name == "stdout").Value.ToString();
                    returnObject.Value = stderr + stdout;
                }
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

        public async Task<Responses.BondAgentList> Http(Requests.Http request)
        {
            var returnObject = new Responses.BondAgentList();
            var executionResult = await this.Client.Core.Http(new Dictionary<string, string>
                {{"url", request.Url}});
            Log.Trace($"ExecutionResult: {executionResult}");
            returnObject.Id = executionResult.id;

            try
            {
                var jobject = (JObject)executionResult.result;
                var resultString = jobject["body"].ToString();
                // TODO: remove this hack that adds in a uuid, when bond agents start returning a uuid
                resultString = resultString.Replace("VmWareUuid\":\"\"", "VmWareUuid\":\"423cf4ed-4d7e-9fd2-42aa-86bdb2ba98b3\"");
                var jarray = JArray.Parse(resultString);
                foreach (JObject sshVm in jarray)
                {
                    returnObject.BondAgents.Add(sshVm.ToObject<Responses.BondAgent>());
                }
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

    }
}
