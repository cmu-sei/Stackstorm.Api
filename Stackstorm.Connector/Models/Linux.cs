// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;

namespace Stackstorm.Connector.Models.Linux
{
    public class Requests
    {
        public class LinuxFileTouch
        {
            public string File { get; set; }
            public string Hosts { get; set; }
            public string Port { get; set; }
            public string Username { get; set; }
            public string PrivateKey { get; set; }
            public string Cwd { get; set; }
            public string Env { get; set; }
            public bool Sudo { get; set; }
        }

        public class LinuxRm
        {
            public string Target { get; set; }
            public string Hosts { get; set; }
            public string Port { get; set; }
            public string Username { get; set; }
            public string PrivateKey { get; set; }
            public string Cwd { get; set; }
            public string Args { get; set; }
            public bool Sudo { get; set; }
            public string Env { get; set; }
            public bool Recursive { get; set; }
            public bool Force { get; set; }
        }
    }

    public class Responses
    {
        public class ResponseBase
        {
            public string Id { get; set; }
            public Exception Exception { get; set; }
            public string Value { get; set; }
            public bool Success { get; set; }


        }

        public class LinuxFileTouch : ResponseBase
        {
        }

        public class LinuxRm : ResponseBase
        {
        }

    }
}
