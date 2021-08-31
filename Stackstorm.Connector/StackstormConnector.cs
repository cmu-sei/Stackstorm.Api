// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using Stackstorm.Api.Client;

namespace Stackstorm.Connector
{
    public class StackstormConnector : StackstormBase
    {
        public VSphere VSphere { get; private set; }
        public Email Email { get; private set; }
        public AzureGov AzureGov { get; private set; }

        public StackstormConnector(string url, string username, string password) : base(url, username, password)
        {
            this.AzureGov = new AzureGov(this.Client);
            this.Email = new Email(this.Client);
            this.VSphere = new VSphere(this.Client);
        }
    }
}
