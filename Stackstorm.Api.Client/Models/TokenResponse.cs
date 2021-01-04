// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
namespace Stackstorm.Api.Client.Models
{
    public class TokenResponse
    {
        public string user { get; set; }
        public string token { get; set; }
        public string expiry { get; set; }
        public string id { get; set; }
        public object metadata { get; set; }
    }
}
