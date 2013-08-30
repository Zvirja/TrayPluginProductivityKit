using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using SIM.Tool.Base.Plugins;
using TrayPluginProductivityKit.BehaviorExtending;
using TrayPluginProductivityKit.Configuration.Mappings;
using TrayPluginProductivityKit.Configuration.Mappings.Metadata;
using TrayPluginProductivityKit.Helpers;
using TrayPluginProductivityKit.InstanceIcons;
using TrayPluginProductivityKit.InstanceMarking;
using TrayPluginProductivityKit.Resources;

namespace TrayPluginProductivityKit
{
  public class EntryPoint : IInitProcessor
  {

    public void Process()
    {
      if (!IsTrayPluginAvailable())
        return;
      TrayPluginAssemblyResolver.Initialize();
      MetadataManager.Initialize();
      MappingsManager.Initialize();
      ResourcesInjector.InjectResources();
      MarkingManager.Initialize();
      IconsPatcher.Initialize();
      BehaviorExtender.Initialize();
    }

    protected virtual bool IsTrayPluginAvailable()
    {
      return AppDomain.CurrentDomain.GetAssemblies().Any(x => x.FullName.Contains("SIM.Tool.Plugins.TrayPlugin"));
    }
  }
}
