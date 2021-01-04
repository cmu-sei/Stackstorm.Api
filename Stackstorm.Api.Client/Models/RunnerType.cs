// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.ComponentModel;

namespace Stackstorm.Api.Client.Models
{
    public enum RunnerType
    {
        [Description("local-shell-cmd")] LocalShellCmd,

        [Description("local-shell-script")] LocalShellScript,

        [Description("remote-shell-cmd")] RemoteShellCmd,

        [Description("python-script")] PythonScript,

        [Description("http-request")] HttpRequest,

        [Description("action-chain")] ActionChain,

        [Description("mistral-v2")] MistralV2,

        [Description("cloud-slang")] CloudSlang
    }
}
