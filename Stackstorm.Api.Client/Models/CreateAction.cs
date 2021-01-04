// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
using System.Collections.Generic;

namespace Stackstorm.Api.Client.Models
{
 public class CreateAction
 {
  public string description { get; set; }
  public string runner_type { get; set; }
  public bool enabled { get; set; }
  public string pack { get; set; }
  public string entry_point { get; set; }
  public Dictionary<string, ActionParameter> parameters { get; set; }
  public string name { get; set; }
 }
}

