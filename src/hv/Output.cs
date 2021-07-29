using System;
using System.Threading;
using System.Security.Principal;
using System.Security.Permissions;
using System.Management.Automation;
using Medoz.CommandLine;
using hv.Models;

namespace hv
{
  public enum Outputs
  {
    json,
    jsonc,
    yaml,
    table,
    tsv,
    none,
  }

  public static class Output
  {
    public static Outputs GetOutputs(string value)
    {
      Console.WriteLine(value);
      switch(value.ToLower())
      {
        case "json":
          return Outputs.json;
        case "jsonc":
          return Outputs.jsonc;
        case "yaml":
          return Outputs.yaml;
        case "table":
          return Outputs.table;
        case "tsv":
          return Outputs.tsv;
        default:
          return Outputs.none;
      }
    }
  }
}

