#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;

#endregion

namespace TrayPluginProductivityKit.Resources
{
  public static class ResourcesInjector
  {
    #region Methods

    public static void InjectResources()
    {
      MultisourceResourcesManager.ActualProvider.Sources.Add(ProductivityKitResources.ResourceManager);
    }

    #endregion
  }
}