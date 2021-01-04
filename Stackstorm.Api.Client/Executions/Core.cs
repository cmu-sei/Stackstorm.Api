// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using Stackstorm.Api.Client;
using Stackstorm.Api.Client.Models;

namespace stackstorm.api.client.Executions
{
    public interface ICore
    {
        Task<Execution> Announcement(Dictionary<string, string> parameters);
        Task<Execution> Ask(Dictionary<string, string> parameters);
        Task<Execution> Echo(Dictionary<string, string> parameters);
        Task<Execution> Http(Dictionary<string, string> parameters);
        Task<Execution> InjectTrigger(Dictionary<string, string> parameters);
        Task<Execution> SendLocalCommand(Dictionary<string, string> parameters);
        Task<Execution> SendLocalSudo(Dictionary<string, string> parameters);
        Task<Execution> Noop(Dictionary<string, string> parameters);
        Task<Execution> Pause(Dictionary<string, string> parameters);
        Task<Execution> SendLinuxRemoteCommand(Dictionary<string, string> parameters);
        Task<Execution> SendLinuxRemoteSudo(Dictionary<string, string> parameters);
        Task<Execution> SendEmail(Dictionary<string, string> parameters);
        Task<Execution> Uuid(Dictionary<string, string> parameters);
        Task<Execution> SendWindowsCommand(Dictionary<string, string> parameters);
        Task<Execution> SendWinRmCommand(Dictionary<string, string> parameters);
        Task<Execution> SendWinRmPowershell(Dictionary<string, string> parameters);
    }

    public class Core : ExecutionsBase, ICore
    {
        public Core(ISt2Client host) : base(host)
        {
        }

        /// <summary>
        /// Action that broadcasts the announcement to all stream consumers
        /// </summary>
        public async Task<Execution> Announcement(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.announcement", parameters);
        }

        /// <summary>
        /// Action for initiating an Inquiry (usually in a workflow)
        /// </summary>
        public async Task<Execution> Ask(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.ask", parameters);
        }

        /// <summary>
        /// Action that executes the Linux echo command on the localhost
        /// </summary>
        public async Task<Execution> Echo(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.echo", parameters);
        }

        /// <summary>
        /// Action that performs an http request    
        /// </summary>
        public async Task<Execution> Http(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.http", parameters);
        }

        /// <summary>
        /// Action which injects a new trigger in the system    
        /// </summary>
        public async Task<Execution> InjectTrigger(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.inject_trigger", parameters);
        }

        /// <summary>
        /// Action that executes an arbitrary Linux command on the localhost
        /// </summary>
        public async Task<Execution> SendLocalCommand(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.local", parameters);
        }

        /// <summary>
        /// Action that executes an arbitrary Linux command on the localhost    
        /// </summary>
        public async Task<Execution> SendLocalSudo(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.local_sudo", parameters);
        }

        /// <summary>
        /// Action that does nothing    
        /// </summary>
        public async Task<Execution> Noop(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.noop", parameters);
        }

        /// <summary>
        /// Action to pause current thread of workflow/sub-workflow
        /// </summary>
        public async Task<Execution> Pause(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.pause", parameters);
        }

        /// <summary>
        /// Action to execute arbitrary linux command remotely
        /// </summary>
        public async Task<Execution> SendLinuxRemoteCommand(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.remote", parameters);
        }

        /// <summary>
        /// Action to execute arbitrary linux command remotely
        /// </summary>
        public async Task<Execution> SendLinuxRemoteSudo(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.remote_sudo", parameters);
        }

        /// <summary>
        /// This sends an email    
        /// </summary>
        public async Task<Execution> SendEmail(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.sendmail", parameters);
        }

        /// <summary>
        /// Generate a new UUID (default uuid4)
        /// </summary>
        public async Task<Execution> Uuid(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.uuid", parameters);
        }

        /// <summary>
        /// Action to execute arbitrary Windows command remotely
        /// </summary>
        public async Task<Execution> SendWindowsCommand(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.windows_cmd", parameters);
        }

        /// <summary>
        /// Action to execute arbitrary Windows Command Prompt command remotely via WinRM
        /// </summary>
        public async Task<Execution> SendWinRmCommand(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.winrm_cmd", parameters);
        }

        /// <summary>
        /// Action to execute arbitrary Windows PowerShell command remotely via WinRM.
        /// </summary>
        public async Task<Execution> SendWinRmPowershell(Dictionary<string, string> parameters)
        {
            return await AddExecution("core.winrm_ps_cmd", parameters);
        }
    }
}
