// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;
using System.Threading.Tasks;
using Stackstorm.Api.Client.Models;

namespace Stackstorm.Api.Client.Apis
{
    using Action = Action;

    /// <summary>Interface for actions API. </summary>
    public interface IActionsApi
    {
        /// <summary>Get all available Actions. </summary>
        /// <returns>A List of <see cref="Action"/>. </returns>
        Task<IList<Action>> GetActionsAsync();

        /// <summary>Gets actions for pack. </summary>
        /// <param name="pack">The pack name. </param>
        /// <returns>A List of <see cref="Action"/>. </returns>
        Task<IList<Action>> GetActionsForPackAsync(string pack);

        Task<IList<Action>> GetActionsForPackByNameAsync(string pack, string name);

        /// <summary>Gets actions by name. </summary>
        /// <param name="name">The action name. </param>
        /// <returns>A List of <see cref="Action"/>. </returns>
        Task<IList<Action>> GetActionsByNameAsync(string name);

        /// <summary>Deletes the action described by actionId. </summary>
        /// <param name="actionId">can be either the ID (e.g. 1 or the ref e.g. mypack.myaction). </param>
        Task DeleteActionAsync(string actionId);

        /// <summary>Creates a new action. </summary>
        /// <param name="action">The <see cref="Action"/> to create. </param>
        Task<Action> CreateActionAsync(CreateAction action);
    }
}
