// Copyright 2021 Carnegie Mellon University. All Rights Reserved.
// Released under a MIT (SEI)-style license. See LICENSE.md in the project root for license information.
namespace Stackstorm.Api.Client.Models
{
 public class ActionParameter
 {
  public string type;
  public string description;
  public object @default;
  public bool required;
  public int order;
  public bool immutable=false;
 }
}

