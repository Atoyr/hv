using System;
using System.Text;

namespace hv
{
  public interface ILogger 
  {
    public void Ok(string message);
    public void Info(string message);
    public void Warning(string message);
    public void Error(string message);
    public void Debug(string message);
  }

  public class Logger : ILogger
  {
    public static Logger Default() => new Logger();
    public bool WriteDebugMessage { set; get; }= false;

    public void Ok(string message)
    {
      var defaultForegroundColor = Console.ForegroundColor;
      var defaultBackgroundColor = Console.BackgroundColor;
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("[ O  K ]");
      var messageLine = message.Split('\n');
      var isFirstLine = true;
      Console.ForegroundColor = defaultForegroundColor;
      Console.BackgroundColor = defaultBackgroundColor;
      foreach(var m in messageLine)
      {
        if (isFirstLine)
        {
          Console.WriteLine(m);
          isFirstLine = false;
        }
        else
        {
          Console.WriteLine("         " + m);
        }
      }
    }

    public void Info(string message)
    {
      var defaultForegroundColor = Console.ForegroundColor;
      var defaultBackgroundColor = Console.BackgroundColor;
      Console.ForegroundColor = ConsoleColor.Blue;
      Console.Write("[ INFO ]");
      var messageLine = message.Split('\n');
      var isFirstLine = true;
      Console.ForegroundColor = defaultForegroundColor;
      Console.BackgroundColor = defaultBackgroundColor;
      foreach(var m in messageLine)
      {
        if (isFirstLine)
        {
          Console.WriteLine(m);
          isFirstLine = false;
        }
        else
        {
          Console.WriteLine("         " + m);
        }
      }
    }

    public void Warning(string message)
    {
      var defaultForegroundColor = Console.ForegroundColor;
      var defaultBackgroundColor = Console.BackgroundColor;
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.Write("[ WARN ]");
      var messageLine = message.Split('\n');
      var isFirstLine = true;
      Console.ForegroundColor = defaultForegroundColor;
      Console.BackgroundColor = defaultBackgroundColor;
      foreach(var m in messageLine)
      {
        if (isFirstLine)
        {
          Console.WriteLine(m);
          isFirstLine = false;
        }
        else
        {
          Console.WriteLine("         " + m);
        }
      }
    }

    public void Error(string message)
    {
      var defaultForegroundColor = Console.ForegroundColor;
      var defaultBackgroundColor = Console.BackgroundColor;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("[ ERRO ]");
      var messageLine = message.Split('\n');
      var isFirstLine = true;
      Console.ForegroundColor = defaultForegroundColor;
      Console.BackgroundColor = defaultBackgroundColor;
      foreach(var m in messageLine)
      {
        if (isFirstLine)
        {
          Console.WriteLine(m);
          isFirstLine = false;
        }
        else
        {
          Console.WriteLine("         " + m);
        }
      }
    }

    public void Debug(string message)
    {
      if(WriteDebugMessage)
      {
        var defaultForegroundColor = Console.ForegroundColor;
        var defaultBackgroundColor = Console.BackgroundColor;
        var messageLine = message.Split('\n');
        foreach(var m in messageLine)
        {
          Console.ForegroundColor = ConsoleColor.Red;
          Console.Write("[ DEBG ]");
          Console.ForegroundColor = ConsoleColor.Green;
          Console.WriteLine(m);
        }
        Console.ForegroundColor = defaultForegroundColor;
        Console.BackgroundColor = defaultBackgroundColor;
      }
    }

  }
}

