// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stackstorm.Api.Client.Models;

namespace Stackstorm.Api.Client.Apis
{
    /// <summary> The rules api. </summary>
    /// <seealso cref="T:Stackstorm.Api.Client.Apis.IRulesApi"/>
    public class RulesApi :
        IRulesApi
    {
        /// <summary> The host process. </summary>
        private ISt2Client _host;

        /// <summary>
        ///  Initializes a new instance of the Stackstorm.Api.Client.Apis.RulesApi class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null. </exception>
        /// <param name="host"> The host. </param>
        public RulesApi(ISt2Client host)
        {
            if (host == null)
                throw new ArgumentNullException("host");
            _host = host;
        }

        /// <summary> Gets rules . </summary>
        /// <returns> The rules . </returns>
        public async Task<IList<Rule>> GetRulesAsync()
        {
            return await _host.GetApiRequestAsync<IList<Rule>>("/v1/rules/");
        }

        /// <summary> Gets rules for pack . </summary>
        /// <param name="packName"> Name of the pack. </param>
        /// <returns> The rules for pack . </returns>
        public async Task<IList<Rule>> GetRulesForPackAsync(string packName)
        {
            return await _host.GetApiRequestAsync<IList<Rule>>("/v1/rules?pack=" + packName);
        }

        /// <summary> Gets rules by name . </summary>
        /// <param name="name"> The rule name. </param>
        /// <returns> The rules by name . </returns>
        public async Task<IList<Rule>> GetRulesByNameAsync(string name)
        {
            return await _host.GetApiRequestAsync<IList<Rule>>("/v1/rules?name=" + name);
        }

        /// <summary> Deletes the rule described by ruleId. </summary>
        /// <param name="ruleId"> Identifier for the rule. </param>
        /// <returns> A Task. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.Apis.IRulesApi.DeleteRule(string)"/>
        public async Task DeleteRuleAsync(string ruleId)
        {
            await _host.DeleteApiRequestAsync("/v1/rules/" + ruleId);
        }
    }
}
