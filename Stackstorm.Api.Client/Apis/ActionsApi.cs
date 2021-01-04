// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stackstorm.Api.Client.Models;

namespace Stackstorm.Api.Client.Apis
{
    using Action = Models.Action;

    /// <summary>The actions api. </summary>
    public class ActionsApi : IActionsApi
    {
        private ISt2Client _host;

        /// <summary>
        /// Initializes a new instance of the Stackstorm.Api.Client.Apis.ActionsApi class.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null. </exception>
        /// <param name="host">The host. </param>
        public ActionsApi(ISt2Client host)
        {
            if (host == null)
                throw new ArgumentNullException("host");
            _host = host;
        }

        /// <summary>Get all available Actions. </summary>
        /// <returns>A List of Actions<see cref="Action"/>. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.Apis.IActionsApi.GetActionsAsync()"/>
        public async Task<IList<Action>> GetActionsAsync()
        {
            return await _host.GetApiRequestAsync<List<Action>>("/v1/actions");
        }

        /// <summary>Gets actions for pack. </summary>
        /// <param name="pack">The pack name. </param>
        /// <returns>A List of Actions<see cref="Action"/>. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.Apis.IActionsApi.GetActionsForPackAsync(string)"/>
        public async Task<IList<Action>> GetActionsForPackAsync(string pack)
        {
            return await _host.GetApiRequestAsync<List<Action>>("/v1/actions?pack=" + pack);
        }

        public async Task<IList<Action>> GetActionsForPackByNameAsync(string pack, string name)
        {
            return await _host.GetApiRequestAsync<List<Action>>("/v1/actions?pack=" + pack + "&name=" + name);
        }

        /// <summary>Gets actions by name. </summary>
        /// <param name="name">The action name. </param>
        /// <returns>A List of Actions<see cref="Action"/>. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.Apis.IActionsApi.GetActionsByNameAsync(string)"/>
        public async Task<IList<Action>> GetActionsByNameAsync(string name)
        {
            return await _host.GetApiRequestAsync<List<Action>>("/v1/actions?name=" + name);
        }

        /// <summary>Deletes the action described by actionId. </summary>
        /// <param name="actionId">can be either the ID (e.g. 1 or the ref e.g. mypack.myaction). </param>
        /// <seealso cref="M:Stackstorm.Api.Client.Apis.IActionsApi.DeleteActionAsync(string)"/>
        public async Task DeleteActionAsync(string actionId)
        {
            await _host.DeleteApiRequestAsync("/v1/actions/" + actionId);
        }

        /// <summary>Creates a new action. </summary>
        /// <param name="action">The <see cref="Action"/> to create. </param>
        /// <returns>The new action asynchronous. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.Apis.IActionsApi.CreateActionAsync(Action)"/>
        public async Task<Action> CreateActionAsync(CreateAction action)
        {
            return await _host.PostApiRequestAsync<Action, CreateAction>("/v1/actions/", action);
        }
    }
}
