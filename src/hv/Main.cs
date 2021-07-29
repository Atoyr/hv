using System;
using System.Threading;
using System.Security.Principal;
using System.Security.Permissions;
using System.Management.Automation;
using Medoz.CommandLine;
using hv.Models;

namespace hv
{
    class Program
    {
        static void Main(string[] args)
        {
          var app = Cli.NewApp();
          app.Authors.Add("r.uchiyama");
          app.Usage = "Hyper-V Command Tools";

          // Global Flags
          var outputOption = new StringFlag("output");
          outputOption.Alias = new string[]{"out", "o"};
          outputOption.Usage = "Choose Output format.";
          outputOption.SetDefaultValue("json");

          var serverOption = new StringFlag("server");
          serverOption.Alias = new string[]{"svr", "s"};
          serverOption.Usage = "Choose Execute Server";
          serverOption.SetDefaultValue("");

          var loggerOption = new BoolFlag("logger");
          loggerOption.Usage = "Use Logger";
          loggerOption.SetDefaultValue(false);
            
          app.Flags.Add(outputOption);
          app.Flags.Add(serverOption);
          app.Flags.Add(loggerOption);
#region vm command
          // vm command
          var vmCommand = new Command("vm");
          vmCommand.Usage = "Virtual Machine Action";

          var vm_ListCommand = new Command("list");
          vm_ListCommand.Action = (ctx) => {

            using(var p = PowerShellProcess.Default("Get-VM"))
            {
              var useLogger = ctx.Bool("logger");
              if (useLogger)
              {
                p.SetLogger(Logger.Default());
              }

              string result = string.Empty;
              switch(Output.GetOutputs(ctx.String("output")))
              {
                case Outputs.json:
                    result = p.ExecuteToJson();
                    break;
                case Outputs.table:
                    result = p.Execute();
                    break;
                default:
                    break;
              }

              Console.WriteLine(result);
            }
          };

          var vm_StartCommand = new Command("start");
          var nameOption = new StringFlag("name");
          nameOption.Alias = new string[]{"n"};
          nameOption.Usage = "The name of the Virtual Machine.";
          vm_StartCommand.Flags.Add(nameOption);
          vm_StartCommand.Action = (ctx) => {
            using(var p = PowerShellProcess.Default("Get-VM"))
            {
              Console.WriteLine(p.Execute());
            }
          };

          vmCommand.SubCommands.Add(vm_ListCommand);
          vmCommand.SubCommands.Add(vm_StartCommand);

          
          app.Commands.Add(vmCommand);
#endregion



          app.Run(args);

          //出力された結果を表示
          // var p = PowerShellProcess.Default("Get-VM");
          // Console.WriteLine(p.Execute());
          // Console.WriteLine(p.ExecuteToJson());
          // Console.WriteLine(p.Execute());
          /// var ps = PowerShell.Create();
          /// ps.AddScript("Get-VM");
          /// var results = ps.Invoke();
          /// foreach (var result in results)
          ///   Console.WriteLine(result);
        }

    }
}
