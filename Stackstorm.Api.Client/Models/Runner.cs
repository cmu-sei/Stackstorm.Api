// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;

namespace Stackstorm.Api.Client.Models
{
    public class Runner
    {
        public string runner_module { get; set; }
        public string description { get; set; }
        public bool enabled { get; set; }
        public Dictionary<string, RunnerParameter> runner_parameters { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
}
