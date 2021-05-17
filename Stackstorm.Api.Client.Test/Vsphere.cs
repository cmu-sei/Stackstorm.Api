// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Stackstorm.Api.Client.Extensions;
using Xunit;

namespace Stackstorm.Api.Client.Test
{
    public class Vsphere : StackstormBase
    {
        [Fact]
        public void HelloReturnsGuid()
        {
            var executionResult = this.Client.VSphere.Hello(null);

            var j = JObject.Parse(executionResult.Result.result.ToString());
            var guid = j["result"].ToString();
            var isValid = Guid.TryParse(guid, out _);
            Assert.True(isValid);
        }

        [Fact]
        public async void GetVmsReturnsArrayOfVms()
        {
            var cluster = "[\"domain-c9\"]";
            var executionResult = await this.Client.VSphere.GetVms(new Dictionary<string, object> { { "clusters", cluster } });
            var j = JObject.Parse(executionResult.result.ToString());

            var hasVms = false;
            //get vms and spit out
            foreach (var node in j["result"])
            {
                Assert.NotNull(node["name"]);
                Assert.NotNull(node["moid"]);

                Assert.True(!string.IsNullOrEmpty(node["name"].ToString()), "vm names can't be null or empty");
                Assert.True(!string.IsNullOrEmpty(node["moid"].ToString()), "moid names can't be null or empty");
                hasVms = true;
            }

            Assert.True(hasVms, $"Cluster {cluster} did not return any VMs");
        }

        [Fact]
        public async void GetVmDetailReturnsUuid()
        {
            var vmId = "[\"vm-303\"]"; //this is a moid

            var executionResult = await this.Client.VSphere.VmGetDetail(new Dictionary<string, object> { { "vm_ids", vmId } });
            var tokens = executionResult.result.ToString().ToJTokens();
            foreach (var token in tokens)
            {
                var uuid = token.First["config"]["uuid"];
                var isValid = Guid.TryParse(uuid.ToString(), out _);
                Assert.True(isValid, $"{vmId} uuid is not a guid");
            }
        }

        [Fact]
        public async void GetGuestInfoReturns()
        {
            var vmId = "[\"vm-303\"]"; //this is a moid

            var executionResult = await this.Client.VSphere.VmGetGuestInfo(new Dictionary<string, object> { { "vm_ids", vmId } });
            var tokens = executionResult.result.ToString().ToJTokens();

            foreach (var token in tokens)
            {
                Assert.NotNull(token);
                Assert.NotNull(token.Path);
                //TODO
            }
        }

        [Fact]
        public async void GetMoidReturnsMoid()
        {
            var vmName = "[\"Win10.10\"]";
            var executionResult = await this.Client.VSphere.GetMoid(new Dictionary<string, object> { { "object_names", vmName }, { "object_type", "VirtualMachine" } });
            var j = ((JToken)executionResult.result)["result"];

            foreach (var prop in j.OfType<JProperty>())
            {
                Assert.True(!string.IsNullOrEmpty(prop.Value.ToString()));
                Assert.True(!string.IsNullOrEmpty(prop.Name));
            }
        }

        [Fact]
        public async void VmGetMoidReturnsMoid()
        {
            var vmName = "[\"Win10.10\"]";
            var executionResult = await this.Client.VSphere.VmGetMoid(new Dictionary<string, object> { { "vm_names", vmName } });
            var j = ((JToken)executionResult.result)["result"];

            foreach (var prop in j.OfType<JProperty>())
            {
                Assert.True(!string.IsNullOrEmpty(prop.Value.ToString()));
                Assert.True(!string.IsNullOrEmpty(prop.Name));
            }
        }

        [Fact]
        public async void GuestFileRead()
        {
            var executionResult = await this.Client.VSphere.GuestFileRead(new Dictionary<string, object> { { "vm_id", "vm-302" }, { "username", "Developer" }, { "password", "develop@1" }, { "guest_file", @"C:\Users\Developer\testGet.txt" } });
            var fileTextObject = ((JObject)executionResult.result)["result"];

            Assert.NotNull(fileTextObject);
            Assert.True(!string.IsNullOrEmpty(fileTextObject.ToString()));
        }

        [Fact]
        public async void VmTurnOn()
        {
            var executionResult = await this.Client.VSphere.VmPowerOn(new Dictionary<string, object> { { "vm_id", "vm-303" } });
            Assert.NotNull(executionResult);
        }

        [Fact]
        public async void VmTurnOff()
        {
            var executionResult = await this.Client.VSphere.VmPowerOff(new Dictionary<string, object> { { "vm_id", "vm-303" } });
            Assert.NotNull(executionResult);
        }

        [Fact]
        public async void WaitNotImplemented()
        {
            await Assert.ThrowsAsync<NotImplementedException>(() => this.Client.VSphere.Wait(null));
        }
    }
}
