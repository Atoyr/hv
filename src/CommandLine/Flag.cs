using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Medoz.CommandLine
{
  public abstract class Flag
  {
    public string Name { set; get; }
    public string[] Alias { set; get; }
    public string Usage { set; get; }
    public object Value { protected set; get; }

    public Flag(string name)
    {
      Name = name;
      Alias = new string[0];
      Usage = string.Empty;
      Value = null;
    }

    public string[] Names()
    {
      var names = new string[Alias.Length + 1];
      names[0] = Name.Length == 1 ? "-" + Name : "--" + Name;
      for(int i = 0; i < Alias.Length; i++)
      {
        names[i+1] = Alias[i].Length == 1 ? "-" + Alias[i] : "--" + Alias[i];
      }
      return names;
    }
  }

  public class Flag<T> : Flag
  {
    public Flag(string name) : base(name) {}
    public void SetDefaultValue(T value) => Value = value;
    public T GetDefaultValue() => (T)Value;
  }

  public class IntFlag : Flag
  {
    public IntFlag(string name) : base(name) {}
    public void SetDefaultValue(int value) => Value = value;
    public int GetDefaultValue() => (int)Value;
  }

  public class StringFlag : Flag
  {
    public StringFlag(string name) : base(name) {}
    public void SetDefaultValue(string value) => Value = value;
    public string GetDefaultValue() => (string)Value;
  }

  public class BoolFlag : Flag
  {
    public BoolFlag(string name) : base(name) {}
    public void SetDefaultValue(bool value) => Value = value;
    public bool GetDefaultValue() => (bool)Value;
  }
}
