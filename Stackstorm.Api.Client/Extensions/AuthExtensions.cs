// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Net.Http;
using Stackstorm.Api.Client.Exceptions;
using Stackstorm.Api.Client.Models;

namespace Stackstorm.Api.Client.Extensions
{
    /// <summary> An authentication extensions. </summary>
    public static class AuthExtensions
    {
        /// <summary>
        ///  A HttpClient extension method that adds an x-auth-token to the client headers
        /// </summary>
        /// <param name="client"> The client to act on. </param>
        /// <param name="token">  The token. </param>
        public static void AddXAuthToken(this HttpClient client, TokenResponse token)
        {
            if (token == null)
                throw new InvalidTokenException("Please login first, or could not find a login token.");

            client.DefaultRequestHeaders.Add("x-auth-token", token.token);
        }
    }
}
