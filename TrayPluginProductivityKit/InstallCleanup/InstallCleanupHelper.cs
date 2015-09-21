#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIM;
using TrayPluginProductivityKit.Configuration;

#endregion

namespace TrayPluginProductivityKit.InstallCleanup
{
  public static class InstallCleanupHelper
  {
    #region Methods

    public static void DoInstanceCleanup(string instaceRootPath)
    {
      AdvancedProperty<string> filesToRemoveSetting = TPPKAdvancedSettings.TPPKRemAfterInstall;
      if (filesToRemoveSetting.Value.IsNullOrEmpty())
      {
        return;
      }

      List<string> filesToRemove = filesToRemoveSetting.Value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();

      foreach (string fileToRem in filesToRemove)
      {
        string pathToRem = fileToRem;
        if (pathToRem.StartsWith(".\\"))
        {
          pathToRem = Path.Combine(instaceRootPath, pathToRem.Substring(2));
        }

        if (Directory.Exists(pathToRem))
        {
          Directory.Delete(pathToRem, true);
        }
        else if (File.Exists(pathToRem))
        {
          File.Delete(pathToRem);
        }
      }
    }

    #endregion
  }
}