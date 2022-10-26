// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;

namespace Stackstorm.Connector.Models.Email
{
    public class Requests
    {
        public class EmailSend
        {
            public string Account { get; set; }
            public string EmailFrom { get; set; }
            public string[] EmailTo { get; set; }
            public string Message { get; set; }
            public string Subject { get; set; }
            public string[] AttachmentPaths { get; set; }
            public string[] EmailCC { get; set; }
            public string Mime { get; set; }
        }
    }

    public class Responses
    {
        public class ResponseBase
        {
            public string Id { get; set; }
            public Exception Exception { get; set; }
            public string Value { get; set; }
        }

        public class EmailSent : ResponseBase
        {
            public bool Success { get; set; }
        }
    }
}
