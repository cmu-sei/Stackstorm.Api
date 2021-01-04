// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using Stackstorm.Api.Client;
using Xunit;

namespace Stackstorm.Api.Client.Test
{
    public class StackstormClient : StackstormBase
    {
        [Fact]
        public void ClientGetsToken()
        {
            Assert.True(Client.HasToken());
        }
        
        [Fact]
        public void ClientInstantiatesSt2Vsphere()
        {
            Assert.NotNull(Client.VSphere);
        }
        
        [Fact]
        public void ClientInstantiatesSt2Core()
        {
            Assert.NotNull(Client.Core);
        }
        
        [Fact]
        public void ClientInstantiatesSt2Packs()
        {
            Assert.NotNull(Client.Packs);
        }
        
        [Fact]
        public void ClientInstantiatesSt2Rules()
        {
            Assert.NotNull(Client.Rules);
        }
        
        [Fact]
        public void ClientInstantiatesSt2Actions()
        {
            Assert.NotNull(Client.Actions);
        }
        
        [Fact]
        public void ClientInstantiatesSt2Executions()
        {
            Assert.NotNull(Client.Executions);
        }

        [Fact]
        public void ClientGetsActions()
        {
            var list = Client.Actions.GetActionsAsync().Result;
            foreach (var item in list)
                Assert.True(!string.IsNullOrEmpty(item.name), "Action names can't be null or empty");
            Assert.True(list.Count > 0);
        }
        
        [Fact]
        public void ClientGetsExecutions()
        {
            var list = Client.Executions.GetExecutionsAsync(10).Result;
            foreach (var item in list)
                Assert.True(!string.IsNullOrEmpty(item.id), "Execution id can't be null or empty");
            Assert.True(list.Count > 0);
        }
        
        [Fact]
        public void ClientGetsRules()
        {
            var list = Client.Rules.GetRulesAsync().Result;
            foreach (var item in list)
                Assert.True(!string.IsNullOrEmpty(item.name), "Rule name can't be null or empty");
            Assert.True(list.Count > 0);
        }
        
        [Fact]
        public void ClientGetsPacks()
        {
            var list = Client.Packs.GetPacksAsync().Result;
            foreach (var item in list)
                Assert.True(!string.IsNullOrEmpty(item.id), "Pack id can't be null or empty");
            Assert.True(list.Count > 0);
        }
    }
}
