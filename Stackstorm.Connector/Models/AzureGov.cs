// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;

namespace Stackstorm.Connector.Models.AzureGov
{
    public class Requests
    {
        public class VmShellScript
        {
            public string VmName { get; set; }
            public string Script { get; set; }
            public string Params { get; set; }
            public string Shell { get; set; }
        }
    }

    public class Responses
    {
        public class ResponseBase
        {
            public string Id { get; set; }
            public Exception Exception { get; set; }
        }

        public class VmShellScript : ResponseBase
        {
            public string Value { get; set; }
        }
    }
}
