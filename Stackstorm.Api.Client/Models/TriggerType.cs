// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;

namespace Stackstorm.Api.Client.Models
{
    public class TriggerType
    {
        public string description { get; set; }
        public dynamic parameters_schema { get; set; }
        public List<object> tags { get; set; }
        public string name { get; set; }
        public PayloadSchema payload_schema { get; set; }
        public string pack { get; set; }
        public string id { get; set; }
        public string uid { get; set; }
    }
}
