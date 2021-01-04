// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;

namespace Stackstorm.Api.Client.Models
{
    public class ExecutionRequest
    {
        public Dictionary<string, string> parameters { get; private set; }
 
        public string action { get; private set; }

        public ExecutionRequest()
        {
            this.parameters = new Dictionary<string, string>();
        }

        public ExecutionRequest(string action, Dictionary<string, string> parameterDictionary)
        {
            this.parameters = parameterDictionary ?? new Dictionary<string, string>();
            this.action = action;
        }

        public void AddParameter(string key, string value)
        {
            if (this.parameters.ContainsKey(key))
            {
                this.parameters[key] = $"{this.parameters[key]}, {value}";
            }
            else
            {
                this.parameters.Add(key, value);
            }
        }

        public void AddParameter(string key, IEnumerable<string> values)
        {
            if (values == null) return;
            
            foreach (var val in values)
            {
                AddParameter(key, val);
            }
        }
    }
}
