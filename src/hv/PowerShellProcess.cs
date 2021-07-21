using System;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace hv
{
    public class PowerShellProcess : IDisposable
    {
      private static string PSPath = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";

      private StringBuilder stringBuilder;
      private string guid { set; get; }
      private Process process { set; get; }

      public string ApplicationName { set; get; } = Path.GetFileNameWithoutExtension(Process.GetCurrentProcess().MainModule.FileName);
      public bool OutputLogs { set; get; } = false;
      
      public static PowerShellProcess Default(string value) => new PowerShellProcess(value);
      public static PowerShellProcess Empty() => new PowerShellProcess();

      private PowerShellProcess()
      {
        stringBuilder = new StringBuilder();
      }

      private PowerShellProcess(string value)
      {
        stringBuilder = new StringBuilder();
        stringBuilder.Append(value);
      }

      public void Dispose()
      {
        if (process is not null)
        {
          process.Dispose();
        }
        if (!string.IsNullOrEmpty(guid))
        {
          string appPath = Config.GetAppConfigFolderPath(ApplicationName);
          if (!string.IsNullOrEmpty(appPath) && File.Exists(Path.Combine(appPath, guid)))
          {
            File.Delete(Path.Combine(appPath, guid));
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

      private static string PipeOutFileCommand(string path)
      {
        return $"| Out-File -FilePath {path}";
      }

      private static string PipeConvertToJsonCommand()
      {
        return "| ConvertTo-Json";
      }

      public string Execute()
      {
        guid = Guid.NewGuid().ToString();
        string appPath = Config.CreateAppFolderIfNotExists(ApplicationName);
        string filePath = Path.Combine(appPath, guid);
        process = Process.Start(GetProcessStartInfo(BuildCommand(PipeOutFileCommand(filePath))));
        process.WaitForExit();
        process.Close();

        string[] lines = System.IO.File.ReadAllLines(Path.Combine(appPath, guid));
        return string.Join("\n", lines);
      }

      public string ExecuteToJson()
      {
        guid = Guid.NewGuid().ToString();
        string appPath = Config.CreateAppFolderIfNotExists(ApplicationName);
        string filePath = Path.Combine(appPath, guid);
        process = Process.Start(GetProcessStartInfo(BuildCommand(PipeConvertToJsonCommand(),PipeOutFileCommand(filePath))));
        process.WaitForExit();
        process.Close();

        string[] lines = System.IO.File.ReadAllLines(Path.Combine(appPath, guid));
        return string.Join("\n", lines);
      }

      public string BuildCommand() => stringBuilder.ToString().Trim();
      public string BuildCommand(params string[] pipeCommand) => BuildCommand() + string.Join(" ", pipeCommand);

      public override string ToString() => BuildCommand();
    }
}
