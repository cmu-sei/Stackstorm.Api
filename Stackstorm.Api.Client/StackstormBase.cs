// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using Microsoft.Extensions.Configuration;
using NLog;

namespace Stackstorm.Api.Client
{
    public class StackstormBase
    {
        private string _username;
        private string _password;
        private string _baseUrl;
        public St2Client Client { get; private set; }
        public static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public StackstormBase()
        {
            var config = new ConfigurationBuilder().AddJsonFile("settings.json").Build();
            _baseUrl = config["Url"].Trim('/');
            _username = config["Username"];
            _password = config["Password"];
            Init();
        }

        public StackstormBase(string url, string username, string password)
        {
            _baseUrl = url;
            _username = username;
            _password = password;
            Init();
        }

        private async void Init()
        {
            var authUrl = $"{_baseUrl}/auth/v1";
            var apiUrl = $"{_baseUrl}/api";

            Client = new St2Client(authUrl, apiUrl, _username, _password, true);
            await Client.RefreshTokenAsync();
        }
    }
}
