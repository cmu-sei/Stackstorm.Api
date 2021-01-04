// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
namespace Stackstorm.Api.Client.Models
{
    public class Execution
    {
        public string status { get; set; }
        public string start_timestamp { get; set; }
        public TriggerType trigger_type { get; set; }
        public Runner runner { get; set; }
        public Rule rule { get; set; }
        public Trigger trigger { get; set; }
        public ExecutionContext context { get; set; }
        public Action action { get; set; }
        public string id { get; set; }
        public string end_timestamp { get; set; }
        
        public string elapsed_seconds{ get; set; }
        
        public object result { get; set; }
        
        public Execution()
        {
            this.result = new object();
        }

        public bool IsComplete()
        {
            return this.status.ToUpper() != "REQUESTED" && this.status.ToUpper() != "SCHEDULED" && this.status.ToUpper() != "RUNNING";
        }
        
    }


}
