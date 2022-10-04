// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stackstorm.Api.Client;
using Stackstorm.Api.Client.Models;

namespace stackstorm.api.client.Executions
{
    public interface ILinux
    {
        Task<Execution> LinuxFileTouch(Dictionary<string, object> parameters);
        Task<Execution> LinuxRm(Dictionary<string, object> parameters);
    }
    public class Linux : ExecutionsBase, ILinux
    {
        public Linux(ISt2Client host) : base(host)
        {
        }

        /// <summary>
        /// Sends an Linux file touch command
        /// </summary>
        public async Task<Execution> LinuxFileTouch(Dictionary<string, object> parameters)
        {
            return await AddExecution("linux.file_touch", parameters);
        }


        /// <summary>
        /// Sends an Linux rm command
        /// </summary>
        public async Task<Execution> LinuxRm(Dictionary<string, object> parameters)
        {
            return await AddExecution("linux.rm", parameters);
        }
    }
}
