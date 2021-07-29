using System;
using System.Linq;
using System.Collections.Generic;
using hv.Models;

namespace hv
{
  public static class FormatExtend
  {
    public static string[] Result(this VirtualMachine[] self, Outputs output, params string[] options)
    {
      var line = new List<string>();

      switch(output)
      {
        case Outputs.json:
          var resultline = self.ToJson().Split("\n");
          foreach(var l in resultline)
          {
            line.Add(l);
          }
          break;
        case Outputs.table:
          var list = new List<Dictionary<string, string>>();

          break;
      }

      return line.ToArray();
    }
  }
}
