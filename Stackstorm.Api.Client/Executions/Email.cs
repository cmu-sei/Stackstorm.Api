// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stackstorm.Api.Client;
using Stackstorm.Api.Client.Models;

namespace stackstorm.api.client.Executions
{
    public interface IEmail
    {
        Task<Execution> SendEmail(Dictionary<string, object> parameters);
    }
    public class Email : ExecutionsBase, IEmail
    {
        public Email(ISt2Client host) : base(host)
        {
        }

        /// <summary>
        /// Sends an email
        /// </summary>
        public async Task<Execution> SendEmail(Dictionary<string, object> parameters)
        {
            return await AddExecution("email.send_email", parameters);
        }
    }
}
