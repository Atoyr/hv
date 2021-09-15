using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace hv
{
    public class PowerShellProcess : IDisposable
    {
      private static string PSPath = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";
      private const string u0027 = "'";

      private StringBuilder stringBuilder;
      private string resultGuid { set; get; }
      private string errorGuid { set; get; }
      private string warningGuid { set; get; }
      private Process process { set; get; }
      private ILogger logger { set; get; }

      public string ApplicationName { set; get; } = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);
      public string RemoteServer { set; get; }
      
      public static PowerShellProcess Default(string value) => new PowerShellProcess(value);
      public static PowerShellProcess Empty() => new PowerShellProcess();

      private PowerShellProcess()
      {
        stringBuilder = new StringBuilder();
      }

      private PowerShellProcess(string value)
      {
        stringBuilder = new StringBuilder();
        stringBuilder.Append(value).Append(" ");
      }

      public void Dispose()
      {
        if (process is not null)
        {
          process.Dispose();
        }
        if (!string.IsNullOrEmpty(resultGuid))
        {
          string appPath = Config.GetAppConfigFolderPath(ApplicationName);
          if (!string.IsNullOrEmpty(appPath) && File.Exists(Path.Combine(appPath, resultGuid)))
          {
            File.Delete(Path.Combine(appPath, resultGuid));
            logger?.Info($"File deleted at {Path.Combine(appPath, resultGuid)}");
          }
        }
        if (!string.IsNullOrEmpty(errorGuid))
        {
          string appPath = Config.GetAppConfigFolderPath(ApplicationName);
          if (!string.IsNullOrEmpty(appPath) && File.Exists(Path.Combine(appPath, errorGuid)))
          {
            File.Delete(Path.Combine(appPath, errorGuid));
            logger?.Info($"File deleted at {Path.Combine(appPath, errorGuid)}");
          }
        }
        if (!string.IsNullOrEmpty(warningGuid))
        {
          string appPath = Config.GetAppConfigFolderPath(ApplicationName);
          if (!string.IsNullOrEmpty(appPath) && File.Exists(Path.Combine(appPath, warningGuid)))
          {
            File.Delete(Path.Combine(appPath, warningGuid));
            logger?.Info($"File deleted at {Path.Combine(appPath, warningGuid)}");
          }
        }
      }

      private ProcessStartInfo GetProcessStartInfo() => GetProcessStartInfo(BuildCommand());

      private static ProcessStartInfo GetProcessStartInfo(string args)
      {
            //Processオブジェクトを作成
            var psi = new System.Diagnostics.ProcessStartInfo();

            psi.FileName = PSPath;
            psi.Verb = "RunAs"; // Define Run as administrator
            psi.UseShellExecute = true;
            psi.RedirectStandardOutput = false; // This will enable to read Powershell run output
            psi.RedirectStandardInput = false;
            psi.RedirectStandardError = false;
            psi.CreateNoWindow = false;
            psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            psi.Arguments = args;
            return psi;
      }

      public PowerShellProcess AddCommand(string command)
      {
        stringBuilder.Append(command).Append(" ");
        return this;
      }

      public PowerShellProcess AddParameters(params string[] parameters)
      {
        stringBuilder.Append(FormatParameters(parameters)).Append(" ");
        return this;
      }

      public PowerShellProcess AddFlag(string flag)
      {
        stringBuilder.Append(flag).Append(" ");
        return this;
      }

      public PowerShellProcess AddFlag(string flag, string param)
      {
        stringBuilder.Append(flag).Append(" ").Append(FormatParameters(param)).Append(" ");
        return this;
      }

      public PowerShellProcess AddPipe()
      {
        stringBuilder.Append("| ");
        return this;
      }


      private string FormatParameters(params string[] parameters)
      {
        StringBuilder sb = new StringBuilder();
        foreach(var param in parameters)
        {
          var s = param;
          s = s.Replace(@"\",@"\\");
          s = s.Replace("\"","`\"");
          if (s.IndexOf(" ") >= 0 || s.IndexOf("　") >= 0)
          {
            s = "\"" + s + "\"";
          }
          sb.Append(s).Append(" ");
        }
        return sb.ToString();
      }

      private static string PipeOutResultFileCommand(string path)
      {
        return $" 1> {path}";
      }

      private static string PipeOutErrorFileCommand(string path)
      {
        return $" 2> {path}";
      }
      private static string PipeOutWarninngFileCommand(string path)
      {
        return $" 3> {path}";
      }

      private static string PipeConvertToJsonCommand()
      {
        return "| ConvertTo-Json";
      }

      public PowerShellResult Execute()
      {
        logger?.Info("Call Execute");
        return execute();
      }

      public PowerShellResult ExecuteToJson()
      {
        logger?.Info("Call Execute to Json");
        return execute(PipeConvertToJsonCommand());
      }

      private PowerShellResult execute(params string[] options)
      {
        resultGuid = Guid.NewGuid().ToString();
        errorGuid = Guid.NewGuid().ToString();
        warningGuid = Guid.NewGuid().ToString();

        logger?.Info($"Generate ResultGuid [{resultGuid}]");
        logger?.Info($"Generate ErrorGuid [{errorGuid}]");
        logger?.Info($"Generate WarningGuid [{warningGuid}]");
        string appPath = Config.CreateAppFolderIfNotExists(ApplicationName);
        string resultFilePath = Path.Combine(appPath, resultGuid);
        string errorFilePath = Path.Combine(appPath, errorGuid);
        string warningFilePath = Path.Combine(appPath, warningGuid);
        List<string> optionList = new List<string>();
        foreach(var option in options)
        {
          optionList.Add(option);
        }
          
        string command = BuildCommand(optionList.ToArray());

        command += PipeOutResultFileCommand(resultFilePath);
        command += PipeOutErrorFileCommand(errorFilePath);
        command += PipeOutWarninngFileCommand(warningFilePath);

        logger?.Debug("PowerShell Command Call");
        logger?.Debug(command);
        process = Process.Start(GetProcessStartInfo(command));
        process.WaitForExit();
        process.Close();

        string[] resultLines = new string[0];
        string[] errorLines = new string[0];
        string[] warningLines = new string[0];

        if (File.Exists(resultFilePath))
        {
          resultLines = System.IO.File.ReadAllLines(resultFilePath);
          logger?.Debug(string.Join("\n", resultLines));
        }

        if (File.Exists(errorFilePath))
        {
          errorLines = System.IO.File.ReadAllLines(errorFilePath);
          logger?.Debug(string.Join("\n", errorLines));
        }

        if (File.Exists(warningFilePath))
        {
          warningLines = System.IO.File.ReadAllLines(warningFilePath);
          logger?.Debug(string.Join("\n", warningLines));
        }
        return new PowerShellResult(resultLines, errorLines, warningLines);
      }

      public string BuildCommand() 
      {
        string command = stringBuilder.ToString().Trim();
        if (!string.IsNullOrEmpty(RemoteServer))
        {
          command = "Invoke-Command -ComputerName " + RemoteServer + " -Credential $cred -ScriptBlock {" + command + "}";
        }
        return command;
      }

      public string BuildCommand(params string[] pipeCommand)
      {
        string command = stringBuilder.ToString().Trim() + " " + string.Join(" ", pipeCommand);
        if (!string.IsNullOrEmpty(RemoteServer))
        {
          command = "Invoke-Command -ComputerName " + RemoteServer + " -Credential $cred -ScriptBlock {" + command + "}";
        }
        return command;
      }

      public override string ToString() => BuildCommand();

      public void SetLogger(ILogger logger)
      {
        this.logger = logger;
      }
    }

    public class PowerShellResult
    {
        public string[] ResultLines;
        public string[] ErrorLines;
        public string[] WarningLines;

        public PowerShellResult() {}

        public PowerShellResult(string[] resultLines, string[] errorLines, string[] warningLines)
        {
          ResultLines = resultLines;
          ErrorLines = errorLines;
          WarningLines = warningLines;
        }

        public string Result()
        {
          if (ResultLines != null)
          {
            return string.Join("\n", ResultLines);
          }
          else
          {
            return string.Empty;
          }
        }

        public string Error()
        {
          if (ErrorLines != null)
          {
            return string.Join("\n", ErrorLines);
          }
          else
          {
            return string.Empty;
          }
        }

        public string Warning()
        {
          if (WarningLines != null)
          {
            return string.Join("\n", WarningLines);
          }
          else
          {
            return string.Empty;
          }
        }
    }
}
