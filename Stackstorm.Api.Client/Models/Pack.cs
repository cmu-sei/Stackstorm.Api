// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;

namespace Stackstorm.Api.Client.Models
{
 public class Pack
 {
  public List<string> files { get; set; }
  public string description { get; set; }
  public string name { get; set; }
  public string author { get; set; }
  public string id { get; set; }
  public string version { get; set; }
  public List<string> keywords { get; set; }
  public string @ref { get; set; }
  public string email { get; set; }
  public string uid { get; set; }
 }
}

