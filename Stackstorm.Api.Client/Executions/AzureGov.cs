// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stackstorm.Api.Client;
using Stackstorm.Api.Client.Models;

namespace stackstorm.api.client.Executions
{
    public interface IAzureGov
    {
        Task<Execution> GetVms(Dictionary<string, object> parameters);
        Task<Execution> VmShellScript(Dictionary<string, object> parameters);
    }
    public class AzureGov : ExecutionsBase, IAzureGov
    {
        public AzureGov(ISt2Client host) : base(host)
        {
        }

        /// <summary>
        /// Retrieves the virtual machines from an Azure Gov Resource Group
        /// </summary>
        public async Task<Execution> GetVms(Dictionary<string, object> parameters)
        {
            return await AddExecution("sei_azure_vm.get_vms", parameters);
        }

        /// <summary>
        /// Run a process inside the guest
        /// </summary>
        public async Task<Execution> VmShellScript(Dictionary<string, object> parameters)
        {
            return await AddExecution("sei_azure_vm.vm_shell_script", parameters);
        }

    }
}
