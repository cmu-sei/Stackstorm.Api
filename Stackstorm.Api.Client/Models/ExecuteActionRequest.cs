// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;

namespace Stackstorm.Api.Client.Models
{
    public class ExecuteActionRequest
    {
        public string action;
        public Dictionary<string, object> parameters;
    }

    public class ExecuteComplexActionRequest
    {
        public string action;
        public Dictionary<string, object> parameters;
    }
}
