using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.Resourcing;

namespace TrayPluginProductivityKit.Resources
{
  public static class ResourcesInjector
  {
    #region Public Methods and Operators

    public static void InjectResources()
    {
      MultisourceResourcesManager.ActualProvider.Sources.Add(ProductivityKitResources.ResourceManager);
    }

    #endregion
  }
}