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

namespace Stackstorm.Connector.Models.Core
{
    public class Requests
    {
        public class SendLinuxRemoteCommand
        {
            public string Moid { get; set; } 
            public string Username { get; set; } 
            public string Password { get; set; } 
            public string Cmd { get; set; }
        }

        public class Http
        {
            public string Url { get; set; }
        }

    }

    public class Responses
    {
        public class ResponseBase
        {
            public string Id { get; set; }
            public Exception Exception { get; set; }
        }

        public class StringValue : ResponseBase
        {
            public string Value { get; set; }
        }

        public class BondAgentList : ResponseBase
        {
            public IList<BondAgent> BondAgents { get; set; }

            public BondAgentList()
            {
                this.BondAgents = new List<BondAgent>();
            }
        }

        public class BondAgent
        {
            public string Id { get; set; }
            public string MachineName { get; set; }        
            public string FQDN { get; set; }        
            public string GuestIp { get; set; }        
            public Guid VmWareUuid { get; set; }
            public string AgentName { get; set; }        
            public string AgentVersion { get; set; }        
            public string AgentInstalledPath { get; set; }
            public List<LocalUser> LocalUsers { get; set; }
            public OS OperatingSystem { get; set; }
            public List<SshPort> SshPorts { get; set; }
            public DateTime? CheckinTime { get; set; }
            public IEnumerable<MonitoredTool> MonitoredTools { get; set; }

            public BondAgent()
            {
                this.SshPorts = new List<SshPort>();
                this.LocalUsers = new List<LocalUser>();
                this.MonitoredTools = new List<MonitoredTool>();
                this.OperatingSystem = new OS();
            }
        }
        
        public class MonitoredTool
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsRunning { get; set; }
            public string Version { get; set; }
            public string Location { get; set; }
        }

        public class LocalUser
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Domain { get; set; }
            public bool IsCurrent { get; set; }
        }

        public class OS
        {
            public int Id { get; set; }
            public string Platform { get; set; }
            public string ServicePack { get; set; }
            public string Version { get; set; }
            public string VersionString { get; set; }

            public OS() { }

            public OS(OperatingSystem os)
            {
                this.Version = os.Version.ToString();
                this.Platform = os.Platform.ToString();
                this.ServicePack = os.ServicePack;
                this.VersionString = os.VersionString;
            }
        }

        public class SshPort
        {
            public string Name { get; set; }
            public string Server { get; set; }
            public uint ServerPort { get; set; }
            public string Client { get; set; }
            public uint ClientPort { get; set; }
            
            public SshPort() { }

            public SshPort(string server, uint serverPort, string client, uint clientPort)
            {
                this.Server = server;
                this.ServerPort = serverPort;
                this.Client = client;
                this.ClientPort = clientPort;
            }
        }

    }
}
