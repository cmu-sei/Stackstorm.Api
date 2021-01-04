// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Stackstorm.Api.Client.Extensions
{
    public static class StringExtensions
    {
        public class BetterToken
        {
            public string TokenId { get; set; }
            public JToken JToken { get; set; }
        }
        
        public static IList<JToken> ToJTokens(this string o)
        {
            var tokens = new List<JToken>();
            var j = JObject.Parse(o);
            foreach (var node in j["result"].Children())
                tokens.Add(node);
            return tokens;
        }

        public static JObject ToJObject(this string o)
        {
            return JObject.Parse(o);
        }

        public static string ToSt2Array(this IEnumerable<string> strings)
        {
            return $"[\"{string.Join("\",\"", strings).Trim()}\"]";
        }
    }
}
