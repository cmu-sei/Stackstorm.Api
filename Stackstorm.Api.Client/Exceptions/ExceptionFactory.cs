// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Net.Http;

namespace Stackstorm.Api.Client.Exceptions
{
    public static class ExceptionFactory
    {
        public static FailedRequestException FailedPostRequest(HttpResponseMessage response)
        {
            return new FailedRequestException("POST Request failed", response.RequestMessage.RequestUri, response.StatusCode,
                (response.Content != null ? response.Content.ToString() : ""));
        }

        public static FailedRequestException FailedGetRequest(HttpResponseMessage response)
        {
            return new FailedRequestException("GET Request failed", response.RequestMessage.RequestUri, response.StatusCode,
                (response.Content != null ? response.Content.ToString() : ""));
        }

        public static FailedRequestException FailedTokenException(HttpResponseMessage response)
        {
            return new FailedRequestException("Token Auth Request failed", response.RequestMessage.RequestUri, response.StatusCode,
                (response.Content != null ? response.Content.ToString() : ""));
        }

        public static FailedRequestException FailedDeleteRequest(HttpResponseMessage response)
        {
            return new FailedRequestException("DELETE Request failed", response.RequestMessage.RequestUri, response.StatusCode,
                (response.Content != null ? response.Content.ToString() : ""));
        }
    }
}
