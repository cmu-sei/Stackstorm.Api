// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NLog;
using Stackstorm.Api.Client.Apis;
using Stackstorm.Api.Client.Models;
using Stackstorm.Api.Client.Exceptions;
using RestSharp;
using RestSharp.Authenticators;
using stackstorm.api.client.Executions;
using Stackstorm.Api.Client.Extensions;

namespace Stackstorm.Api.Client
{
    /// <summary> StackStorm API Client. </summary>
    public class St2Client : ISt2Client, IDisposable
    {
        private static readonly Logger _log = LogManager.GetCurrentClassLogger();

        /// <summary> The username. </summary>
        private string _username;

        /// <summary> The password. </summary>
        private string _password;

        /// <summary> URL of the authentication endpoint. </summary>
        private Uri _authUrl;

        /// <summary> URL of the API. </summary>
        private Uri _apiUrl;

        /// <summary> The token. </summary>
        private TokenResponse _token;

        /// <summary> Initializes a new instance of the Stackstorm.Api.Client.St2Client class. </summary>
        /// <exception cref="ArgumentException">Thrown when one or more arguments have unsupported or
        ///          illegal values. </exception>
        /// <param name="authUrl">        URL of the authentication endpoint. </param>
        /// <param name="apiUrl">        URL of the API. </param>
        /// <param name="username">        The username. </param>
        /// <param name="password">        The password. </param>
        /// <param name="ignoreCertificateValidation"> true to ignore certificate validation. </param>
        public St2Client(string authUrl, string apiUrl, string username, string password, bool ignoreCertificateValidation = false)
        {
            if (String.IsNullOrWhiteSpace(authUrl))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'authUrl'.",
                    "authUrl");

            if (String.IsNullOrWhiteSpace(apiUrl))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'apiUrl'.",
                    "apiUrl");

            if (String.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'password'.",
                    "password");

            if (String.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Argument cannot be null, empty, or composed entirely of whitespace: 'username'.",
                    "username");

            _apiUrl = new Uri(apiUrl);
            _authUrl = new Uri(authUrl);
            _password = password;
            _username = username;

            if (ignoreCertificateValidation)
            {
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
            }

            Actions = new ActionsApi(this);
            Packs = new PacksApi(this);
            Executions = new ExecutionsApi(this);
            Rules = new RulesApi(this);
            VSphere = new VSphere(this);
            Core = new Core(this);
            Email = new Email(this);
            Linux = new Linux(this);
            AzureGov = new AzureGov(this);
        }

        /// <summary> Refresh the auth token. </summary>
        /// <returns> A Task. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.ISt2Client.RefreshTokenAsync()"/>
        public Task RefreshTokenAsync()
        {
            var client = new RestClient($"{_authUrl}/tokens");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            var encoded = Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes($"{_username}:{_password}"));
            request.AddHeader("Authorization", $"Basic {encoded}");
            request.AddHeader("Content-Type", "application/json");

            try
            {
                var response = client.Execute(request);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    _token = JsonConvert.DeserializeObject<TokenResponse>(response.Content);
                    _log.Trace($"Login successful: {_token.token}");

                }
                else
                {
                    _log.Trace($"Login failed: {response.StatusCode} {response.Content}");
                }
            }
            catch (Exception e)
            {
                _log.Error(e);
            }

            return Task.CompletedTask;
        }

        /// <summary> Make an asynchronous GET request to the API. </summary>
        /// <typeparam name="TResponseType"> Expected Type of the response. </typeparam>
        /// <param name="url"> URL of the GET request. </param>
        /// <returns> The Typed response. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.ISt2Client.GetApiRequestAsync{TResponseType}(string)"/>
        public async Task<TResponseType> GetApiRequestAsync<TResponseType>(string url)
        {
            var client = new RestClient($"{_apiUrl}/{url}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-auth-token", _token.token); //HACK
            request.AddHeader("Content-Type", "application/json");

            var s = string.Empty;

            try
            {
                var response = client.Execute(request);
                s = response.Content;
                return JsonConvert.DeserializeObject<TResponseType>(response.Content);

//       if (response.StatusCode == HttpStatusCode.Created)
//           return JsonConvert.DeserializeObject<TResponseType>(response.Content);
//       else
//           throw new Exception();
//           //throw ExceptionFactory.FailedGetRequest(response);
            }
            catch (HttpRequestException hre)
            {
                _log.Error($"bad req! {s}");
                throw new FailedRequestException(hre.Message);
            }
        }

        /// <summary> Make an asynchronous GET request to an API endpoint returning a string. </summary>
        /// <typeparam name="TResponseType"> Expected Type of the response. </typeparam>
        /// <param name="url"> URL of the GET request. </param>
        /// <returns> The Typed response. </returns>
        public async Task<string> GetApiRequestStringAsync(string url)
        {
            var client = new RestClient($"{_apiUrl}/{url}");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("x-auth-token", _token.token); //HACK
            request.AddHeader("Content-Type", "application/json");

            try
            {
                var response = client.Execute(request);
                return (string) response.Content;
            }
            catch (HttpRequestException hre)
            {
                _log.Error($"bad req!");
                throw new FailedRequestException(hre.Message);
            }
        }

        /// <summary> Make an asynchronous POST request to the API. </summary>
        /// <typeparam name="TResponseType"> Expected Type of the response. </typeparam>
        /// <typeparam name="TRequestType">  Expected Type of of the request message. </typeparam>
        /// <param name="url">    URL of the POST request. </param>
        /// <param name="request"> The request. </param>
        /// <returns> The Typed response. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.ISt2Client.PostApiRequestAsync{TResponseType,TRequestType}(string,TRequestType)"/>
        public async Task<TResponseType> PostApiRequestAsync<TResponseType, TRequestType>(string url, TRequestType requestType)
        {
            var client = new RestClient($"{_apiUrl}/{url}");
            var request = new RestRequest(Method.POST);
            if (_token != null)
            {
                request.AddHeader("x-auth-token", _token.token);
            }
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(requestType);

            _log.Trace($"{_apiUrl}/{url} POSTED {JsonConvert.SerializeObject(requestType)}");

            try
            {
                var response = client.Execute(request);
                _log.Trace($"Response: {response.StatusCode} {response.StatusDescription}");
                return JsonConvert.DeserializeObject<TResponseType>(response.Content);
            }
            catch (Exception e)
            {
                _log.Error($"PostApiRequestAsync exception: {e}");
            }

            return JsonConvert.DeserializeObject<TResponseType>("");

//                    if (response.IsSuccessStatusCode)
//                        return await response.Content.ReadAsJsonAsync<TResponseType>();
//
//                    throw ExceptionFactory.FailedPostRequest(response);
//                }
//                catch (HttpRequestException hre)
//                {
//                    throw new FailedRequestException(hre.Message);
//                }
        }

        /// <summary> Make a DELETE request to the API. </summary>
        /// <param name="url"> URL of the request. </param>
        /// <returns> A Task. </returns>
        /// <seealso cref="M:Stackstorm.Api.Client.ISt2Client.DeleteApiRequestAsync(string)"/>
        public async Task DeleteApiRequestAsync(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _apiUrl;
                client.AddXAuthToken(_token);

                try
                {
                    var response = await client.DeleteAsync(url);
                    if (!response.IsSuccessStatusCode)
                        throw ExceptionFactory.FailedDeleteRequest(response);
                }
                catch (HttpRequestException hre)
                {
                    throw new FailedRequestException(hre.Message);
                }
            }
        }

        public IVSphere VSphere { get; private set; }
        public IEmail Email { get; private set; }
        public ILinux Linux { get; private set; }
        public IAzureGov AzureGov { get; private set; }
        public ICore Core { get; private set; }

        /// <summary> Accessor for the Actions related methods. </summary>
        /// <value> The actions accessor. </value>
        /// <seealso cref="P:Stackstorm.Api.Client.ISt2Client.Actions"/>
        public IActionsApi Actions { get; private set; }

        /// <summary> Accessor for the Packs related methods. </summary>
        /// <value> The Packs accessor. </value>
        /// <seealso cref="P:Stackstorm.Api.Client.ISt2Client.Packs"/>
        public IPacksApi Packs { get; private set; }

        /// <summary> Accessor for the Executions related methods. </summary>
        /// <value> The Executions accessor. </value>
        /// <seealso cref="P:Stackstorm.Api.Client.ISt2Client.Executions"/>
        public IExecutionsApi Executions { get; private set; }

        /// <summary> Accessor for the Rules related methods. </summary>
        /// <value> The Rules accessor. </value>
        public IRulesApi Rules { get; private set; }

        public void Dispose()
        {
            // cleanup
        }

        public bool HasToken()
        {
            if (_token == null)
            {
                return false;
            }
            else
            {
                var expiryDate = DateTime.ParseExact(_token.expiry.Substring(0, 19), "yyyy-MM-ddTHH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                var nowTime = DateTime.UtcNow;
                var timeDiff = DateTime.Compare(expiryDate.AddHours(-1), nowTime);
                return timeDiff >= 0;
            }
        }
    }
}
