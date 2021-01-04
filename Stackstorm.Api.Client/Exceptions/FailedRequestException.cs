// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Net;

namespace Stackstorm.Api.Client.Exceptions
{
    /// <summary> Exception for signalling failed request errors. </summary>
    /// <seealso cref="T:System.Exception"/>
    public class FailedRequestException
        : Exception
    {
        public string FailureMessage;

        public Uri RequestUri;

        public HttpStatusCode StatusCode;

        public string ResponseMessage;

        /// <summary>
        ///  Initializes a new instance of the
        ///  Stackstorm.Api.Client.Exceptions.FailedRequestException class.
        /// </summary>
        /// <param name="message"> The message. </param>
        public FailedRequestException(string message) :
            base(message)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the
        ///  FailedRequestException class.
        /// </summary>
        /// <param name="failureMessage">  Message describing the failure. </param>
        /// <param name="requestUri">    URI of the request. </param>
        /// <param name="statusCode">    The status code. </param>
        /// <param name="responseMessage"> Message describing the response. </param>
        public FailedRequestException(string failureMessage, Uri requestUri, HttpStatusCode statusCode, string responseMessage)
            : base(String.Format("{3}, Request to {0} failed with status '{1}', response was {2}", requestUri, statusCode,
                responseMessage, failureMessage))
        {
            FailureMessage = failureMessage;
            RequestUri = requestUri;
            StatusCode = statusCode;
            ResponseMessage = responseMessage;
        }
    }
}
