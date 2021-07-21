using System;
using System.IO;

namespace hv
{
  public static class Config
  {
    public static string GetAppConfigFolderPath(string appName)
    {
      var userProfile = Environment.GetEnvironmentVariable("USERPROFILE");
      if (userProfile is null)
      {
        return string.Empty;
      }
      string path = (string)userProfile;
      return Path.Combine(path,"." + appName);
    }

    public static string CreateAppFolderIfNotExists(string appName)
    {
      var path = GetAppConfigFolderPath(appName);
      if (string.IsNullOrEmpty(path)) return string.Empty;

      if (!Directory.Exists(path))
      {
        Directory.CreateDirectory(path);
      }
      return path;
    }
  }
}
