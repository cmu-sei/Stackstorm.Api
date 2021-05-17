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
    public class VSphere
    {
        private readonly St2Client _client;

        public VSphere(St2Client client)
        {
            _client = client;
        }

        public async Task<Responses.VmGuid> GetGuid()
        {
            var returnObject = new Responses.VmGuid();
            var executionResult = await _client.VSphere.Hello(null);
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var j = JObject.Parse(executionResult.result.ToString());
                var guid = j["result"].ToString();
                returnObject.Guid = new Guid(guid);
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

        /// <summary>
        /// clusters get auto-formatted into st2-friendly array: "[\"domain-c9\"]"
        /// </summary>
        /// <param name="cluster"></param>
        public async Task<Responses.VmList> GetVms(IEnumerable<string> clusters)
        {
            var returnObject = new Responses.VmList();
            var executionResult = await _client.VSphere.GetVms(new Dictionary<string, object> { { "clusters", clusters.ToSt2Array() } });
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var j = executionResult.result.ToString().ToJObject();

                //get vms and spit out
                foreach (var node in j["result"])
                {
                    returnObject.Vms.Add(new Responses.Vm(node["moid"], node["name"], node["runtime.powerState"], null));
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

        /// <summary>
        /// clusters get auto-formatted into st2-friendly array: "[\"domain-c9\"]"
        /// </summary>
        /// <param name="cluster"></param>
        public async Task<Responses.VmList> GetVmsWithUuid(IEnumerable<string> clusters)
        {
            var returnObject = new Responses.VmList();
            var executionResult = await _client.VSphere.GetVmsWithUuid(new Dictionary<string, object> { { "clusters", clusters.ToSt2Array() } });
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var j = executionResult.result.ToString().ToJObject();

                //get vms and spit out
                foreach (var node in j["result"])
                {
                    returnObject.Vms.Add(new Responses.Vm(node["moid"], node["name"], node["runtime.powerState"], node["config.uuid"]));
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

        /// <summary>
        /// moids or vm_ids "[\"vm-776\"]"
        /// </summary>
        /// <param name="moids"></param>
        public async Task<Responses.VmDetailList> GetVmDetails(IEnumerable<string> moids)
        {
            var returnObject = new Responses.VmDetailList();
            var executionResult = await _client.VSphere.VmGetDetail(new Dictionary<string, object> { { "vm_ids", moids.ToSt2Array() } });
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var tokens = executionResult.result.ToString().ToJTokens();

                foreach (var token in tokens)
                {
                    var vm = new Responses.VmDetail();

                    vm.Moid = token.Path.Replace("result['", "").Replace("']", "");
                    vm.Uuid = token.First["config"]["uuid"].ToString();
                    vm.InstanceId = token.First["config"]["instanceUuid"].ToString();
                    vm.Name = token.First["config"]["name"].ToString();
                    vm.VmPathName = token.First["config"]["vmPathName"].ToString();
                    vm.GuestId = token.First["config"]["guestId"].ToString();
                    vm.GuestFullName = token.First["config"]["guestFullName"].ToString();
                    Int32 intValue;
                    if (!Int32.TryParse(token.First["config"]["numCpu"].ToString(), out intValue)) { intValue = 0; }
                    vm.Cpus = intValue;
                    if (!Int32.TryParse(token.First["config"]["memorySizeMB"].ToString(), out intValue)) { intValue = 0; }
                    vm.MemorySizeInMb = intValue;
                    if (!Int32.TryParse(token.First["config"]["numVirtualDisks"].ToString(), out intValue)) { intValue = 0; }
                    vm.VirtualDisks = intValue;

                    returnObject.Vms.Add(vm);
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

        /// <summary>
        /// "[\"Win10.10\"]";
        /// </summary>
        public async Task<Responses.VmList> GetMoids(Requests.Moids request)
        {
            var returnObject = new Responses.VmList();
            var executionResult = await _client.VSphere.GetMoid(new Dictionary<string, object>
                {{"object_names", request.MachineNames.ToSt2Array()}, {"object_type", "VirtualMachine"}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var j = ((JToken)executionResult.result)["result"];

                foreach (var prop in j.OfType<JProperty>())
                {
                    returnObject.Vms.Add(new Responses.Vm(prop.Value.ToString(), prop.Name, null, null));
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

        public async Task<Responses.VmStringValue> GuestFileRead(Requests.FileRead request)
        {
            var returnObject = new Responses.VmStringValue();
            var executionResult = await _client.VSphere.GuestFileRead(new Dictionary<string, object>
                {{"vm_id", request.Moid}, {"username", request.Username}, {"password", request.Password}, {"guest_file", request.GuestFilePath}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var fileTextObject = ((JObject)executionResult.result)["result"];
                returnObject.Value = fileTextObject.ToString();
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

        public async Task<Responses.VmStringValue> GuestFileWrite(Requests.FileWrite request)
        {
            var returnObject = new Responses.VmStringValue();
            var executionResult = await _client.VSphere.GuestFileUploadContent(new Dictionary<string, object>
                {{"vm_id", request.Moid}, {"username", request.Username}, {"password", request.Password},
                    {"guest_file_path", request.GuestFilePath}, {"file_content", request.GuestFileContent}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var commandTextObject = (JObject)executionResult.result;
                var result = commandTextObject.GetValue("result");
                returnObject.Value = (string)result;
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

        public async Task<Responses.VmStringValue> GuestCommand(Requests.Command request)
        {
            var returnObject = new Responses.VmStringValue();
            var executionResult = await _client.VSphere.GuestProcessRun(new Dictionary<string, object>
                {{"vm_id", request.Moid}, {"username", request.Username}, {"password", request.Password},
                    {"command", request.CommandText}, {"arguments", request.CommandArgs}, {"workdir", request.CommandWorkDirectory}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var commandTextObject = ((JObject)executionResult.result).First.First["stdout"];
                returnObject.Value = commandTextObject.ToString();
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

        public async Task<Responses.VmStringValue> GuestCommandFast(Requests.Command request)
        {
            var returnObject = new Responses.VmStringValue();
            var executionResult = await _client.VSphere.GuestProcessRunFast(new Dictionary<string, object>
                {{"vm_id", request.Moid}, {"username", request.Username}, {"password", request.Password},
                    {"command", request.CommandText}, {"arguments", request.CommandArgs}, {"workdir", request.CommandWorkDirectory}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var commandTextObject = ((JObject)executionResult.result)["result"];
                returnObject.Value = commandTextObject.ToString();
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

        public async Task<Responses.Power> GuestPowerOn(string moid)
        {
            var response = new Responses.Power();
            var executionResult = await _client.VSphere.VmPowerOn(new Dictionary<string, object>
                {{"vm_id", moid}});

            response.Id = executionResult.id;
            response.State = Responses.PowerStates.On;
            return response;
        }

        public async Task<Responses.Power> GuestPowerOff(string moid)
        {
            var response = new Responses.Power();
            var executionResult = await _client.VSphere.VmPowerOff(new Dictionary<string, object>
                {{"vm_id", moid}});

            response.Id = executionResult.id;
            response.State = Responses.PowerStates.Off;
            return response;
        }

        public async Task<Responses.VmStringValue> CreateVmFromTemplate(Requests.CreateVmFromTemplate request)
        {
            var returnObject = new Responses.VmStringValue();
            var executionResult = await _client.VSphere.CreateVmFromTemplate(new Dictionary<string, object>
                {{"template_id", request.TemplateMoid}, {"name", request.Name}, {"datacenter_id", request.DataCenter}, {"datastore_id", request.DataStore}, {"resourcepool_id", request.ResourcePool}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var fileTextObject = ((JObject)executionResult.result)["result"];
                returnObject.Value = fileTextObject.ToString();
            }
            catch (Exception e)
            {
                Log.Error($"Object was not in expected format: {e}");
                Console.WriteLine(e);
                returnObject.Exception = e;
            }

            return returnObject;
        }

        public async Task<Responses.VmStringValue> RemoveVm(string moid)
        {
            var returnObject = new Responses.VmStringValue();
            var executionResult = await _client.VSphere.VmRemove(new Dictionary<string, object>
                {{"vm_id", moid}, {"delete_permanently", "true"}});
            Log.Trace($"ExecutionResult: {executionResult}");

            try
            {
                returnObject.Id = executionResult.id;
                var fileTextObject = ((JObject)executionResult.result)["result"];
                returnObject.Value = fileTextObject.ToString();
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
