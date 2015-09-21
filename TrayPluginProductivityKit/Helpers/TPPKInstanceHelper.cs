#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIM.Instances;
using TrayPluginProductivityKit.Configuration;

#endregion

namespace TrayPluginProductivityKit.Helpers
{
  internal static class TPPKInstanceHelper
  {
    #region Methods

    public static string GetInstanceRoot(Instance instance)
    {
      if (TPPKAdvancedSettings.TPPKFastRootEvaluation.Value)
      {
        return Path.GetDirectoryName(instance.WebRootPath);
      }

      return instance.RootPath;
    }

    #endregion
  }
}