// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;

namespace Stackstorm.Connector.Models.Linux
{
    public class Requests
    {
        public class FileTouch
        {
            public string File { get; set; }
            public string Hosts { get; set; }
            public string Port { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Cwd { get; set; }
            //public bool Sudo { get; set; }
            public string Env { get; set; }
        }
    }

    public class Responses
    {
        public class ResponseBase
        {
            public string Id { get; set; }
            public Exception Exception { get; set; }
        }

        public class FileTouch : ResponseBase
        {
            public bool Success { get; set; }
        }
    }
}
