// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
namespace Stackstorm.Connector
{
    public class StackstormConnector
    {
        public VSphere VSphere { get; private set; }
        
        public StackstormConnector()
        {
            this.VSphere = new VSphere();
        }
    }
}
