// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;

namespace Stackstorm.Api.Client.Exceptions
{
    /// <summary> Exception for signalling invalid token errors. </summary>
    /// <seealso cref="T:System.Exception"/>
    public class InvalidTokenException
        : Exception
    {
        /// <summary>
        ///  Initializes a new instance of the Stackstorm.Api.Client.Exceptions.InvalidTokenException
        ///  class.
        /// </summary>
        /// <param name="message"> The message. </param>
        public InvalidTokenException(string message) :
            base(message)
        {
        }
    }
}
