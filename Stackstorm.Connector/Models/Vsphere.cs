// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;

namespace Stackstorm.Connector.Models.Vsphere
{
    public class Requests
    {
        public class FileRead
        {
            public string Moid { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string GuestFilePath { get; set; }
        }

        public class FileWrite
        {
            public string Moid { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string GuestFilePath { get; set; }
            public string GuestFileContent { get; set; }
            public string FilePath { get; set; }
        }

        public class Command
        {
            public string Moid { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string CommandText { get; set; }
            public string CommandArgs { get; set; }
            public string CommandWorkDirectory { get; set; }
        }

        public class CreateVmFromTemplate
        {
            public string TemplateMoid { get; set; }
            public string Name { get; set; }
            public string DataCenter { get; set; }
            public string DataStore { get; set; }
            public string ResourcePool { get; set; }
        }

        public class Moids
        {
            public IEnumerable<string> MachineNames { get; set; }
        }
    }

    public class Responses
    {
        public class ResponseBase
        {
            public string Id { get; set; }
            public Exception Exception { get; set; }
        }

        public class Power : ResponseBase
        {
            public PowerStates State { get; set; }
        }

        public class VmDetailList : ResponseBase
        {
            public IList<VmDetail> Vms { get; set; }

            public VmDetailList()
            {
                this.Vms = new List<VmDetail>();
            }
        }

        public class VmDetail
        {
            public string Moid { get; set; }
            public string Uuid { get; set; }
            public string Name { get; set; }
            public string InstanceId { get; set; }
            public string VmPathName { get; set; }
            public int VirtualDisks { get; set; }
            public int Cpus { get; set; }
            public int MemorySizeInMb { get; set; }
            public PowerStates PowerState { get; set; }
            public string ConnectionState { get; set; }
            public string OverallStatus { get; set; }
            public string HostName { get; set; }
            public string ToolsRunningStatus { get; set; }
            public string ToolsStatus { get; set; }
            public string GuestId { get; set; }
            public string GuestFullName { get; set; }
        }

        public class VmList : ResponseBase
        {
            public IList<Vm> Vms { get; set; }

            public VmList()
            {
                this.Vms = new List<Vm>();
            }
        }

        public class Vm
        {
            public PowerStates PowerState { get; set; }
            public string Moid { get; set; }
            public string Name { get; set; }
            public string Uuid { get; set; }

            public Vm()
            {
            }

            public Vm(object moid, object name, object powerState, object uuid)
            {
                this.Moid = moid.ToString();
                this.Name = name.ToString();
                if (powerState != null)
                {
                    this.PowerState = powerState.ToString().ToUpper().IndexOf("ON", StringComparison.CurrentCultureIgnoreCase) >= 0
                        ? PowerStates.On
                        : PowerStates.Off;
                }
                this.Uuid = uuid.ToString();
            }
        }

        public enum PowerStates
        {
            Off = 0,
            On = 1
        }

        public class VmGuid : ResponseBase
        {
            public Guid Guid { get; set; }
        }

        public class VmStringValue : ResponseBase
        {
            public string Value { get; set; }
        }
    }
}
