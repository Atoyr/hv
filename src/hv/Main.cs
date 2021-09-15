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


          // vm list
          var vm_ListCommand = new Command("list");
          vm_ListCommand.Action = (ctx) => {
            PowerShellResult result;
            using(var p = PowerShellProcess.Default("Get-VM"))
            {
              ApplyLogger(p, ctx.Bool("logger"));
              p.RemoteServer = ctx.String("server");
              result = Execute(p, Output.GetOutputs(ctx.String("output")));
            }
            WriteResult(result);
          };

          // vm start
          var vm_StartCommand = new Command("start");
          var nameOption = new StringFlag("name");
          nameOption.Alias = new string[]{"n"};
          nameOption.Usage = "The name of the Virtual Machine.";
          vm_StartCommand.Flags.Add(nameOption);
          vm_StartCommand.Action = (ctx) => {
            PowerShellResult result;
            string name = ctx.String("name");
            if ( string.IsNullOrEmpty(name))
            {
              Console.WriteLine("name not found");
              return;
            }

            using(var p = PowerShellProcess.Default("Start-VM"))
            {
              p.AddFlag("-Name", name);
              p.RemoteServer = ctx.String("server");
              ApplyLogger(p, ctx.Bool("logger"));
              result = Execute(p,Output.GetOutputs(ctx.String("output")));
            }
            WriteResult(result);
          };

          // vm stop
          var vm_StopCommand = new Command("stop");
          vm_StopCommand.Flags.Add(nameOption);
          vm_StopCommand.Action = (ctx) => {
            PowerShellResult result;
            string name = ctx.String("name");
            if ( string.IsNullOrEmpty(name))
            {
              Console.WriteLine("name not found");
              return;
            }

            using(var p = PowerShellProcess.Default("Stop-VM"))
            {
              p.AddFlag("-Name", name);
              p.RemoteServer = ctx.String("server");
              ApplyLogger(p, ctx.Bool("logger"));
              result = Execute(p,Output.GetOutputs(ctx.String("output")));
            }
            WriteResult(result);
          };

          // vm list-ip-addresses
          var vm_ListIpAddressesCommand = new Command("list-ip-addresses");
          vm_ListIpAddressesCommand.Action = (ctx) => {
            PowerShellResult result;
            using(var p = PowerShellProcess.Default("Get-VM"))
            {
              p.AddPipe().AddCommand("select").AddFlag("-ExpandProperty", "NetworkAdapters").AddPipe().AddCommand("select").AddParameters("VMName", ",IPAddresses");
              p.RemoteServer = ctx.String("server");
              ApplyLogger(p, ctx.Bool("logger"));
              result = Execute(p,Output.GetOutputs(ctx.String("output")));
            }
            WriteResult(result);
          };

          vmCommand.SubCommands.Add(vm_ListCommand);
          vmCommand.SubCommands.Add(vm_StartCommand);
          vmCommand.SubCommands.Add(vm_StopCommand);
          vmCommand.SubCommands.Add(vm_ListIpAddressesCommand);

          
          app.Commands.Add(vmCommand);
#endregion



          app.Run(args);
        }

        private static void ApplyLogger(PowerShellProcess powerShellProcess, bool useLogger)
        {
          var logger = Logger.Default();
          logger.WriteDebugMessage = true;
          ApplyLogger(powerShellProcess, useLogger, logger);
        }
        private static void ApplyLogger(PowerShellProcess powerShellProcess, bool useLogger, ILogger logger)
        {
              if (useLogger)
              {
                powerShellProcess.SetLogger(logger);
              }
        }

        private static PowerShellResult Execute(PowerShellProcess powerShellProcess,Outputs output)
        {
              PowerShellResult result;
              switch(output)
              {
                case Outputs.json:
                    result = powerShellProcess.ExecuteToJson();
                    break;
                case Outputs.table:
                    result = powerShellProcess.Execute();
                    break;
                default:
                    result = new PowerShellResult();
                    break;
              }
              return result;
        }

        private static void WriteResult(PowerShellResult result)
        {
          if (!string.IsNullOrEmpty(result.Error()))
          {
            var defaultForegroundColor = Console.ForegroundColor;
            var defaultBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error());
            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;
          }
          if (!string.IsNullOrEmpty(result.Warning()))
          {
            var defaultForegroundColor = Console.ForegroundColor;
            var defaultBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(result.Warning());
            Console.ForegroundColor = defaultForegroundColor;
            Console.BackgroundColor = defaultBackgroundColor;
          }
          if (!string.IsNullOrEmpty(result.Result()))
          {
            Console.WriteLine(result.Result());
          }
        }
    }
}
