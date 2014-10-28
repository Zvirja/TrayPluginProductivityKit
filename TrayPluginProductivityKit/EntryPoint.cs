using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SIM.Tool.Base.Plugins;
using SIM.Tool.Windows;
using TrayPluginProductivityKit.BehaviorExtending;
using TrayPluginProductivityKit.Configuration.Mappings;
using TrayPluginProductivityKit.Configuration.Mappings.Metadata;
using TrayPluginProductivityKit.Helpers;
using TrayPluginProductivityKit.InstanceIcons;
using TrayPluginProductivityKit.InstanceMarking;
using TrayPluginProductivityKit.Resources;
using TrayPluginProductivityKit.SilentStartup;
using TrayPluginProductivityKit.TrayNotifications;

namespace TrayPluginProductivityKit
{
  public class EntryPoint : IInitProcessor, IMainWindowLoadedProcessor
  {
    #region Public Methods and Operators

    public void Process()
    {
      if (!this.IsTrayPluginAvailable())
      {
        return;
      }
      TrayPluginAssemblyResolver.Initialize(); //We still need this resolver, because TrayPlugin assembly reference will not be resolved
      //InstanceMenuCollector.Initialize();
      ResourcesInjector.InjectResources();
      MetadataManager.Initialize();
      MappingsManager.Initialize();
      MarkingManager.Initialize();
      IconsPatcher.Initialize();
      BehaviorExtender.Initialize();
      //ProcessIDsCustodian.Actual.Initialize();

      TrayNotificationManager.Initialize();
      UISyncContextHolder.CatchSyncContext();

      MinimizedStartupManager.Init(MainWindow.Instance);
    }

    public void Process(Window mainWindow)
    {
      SIMDialogsInteractionHelper.SaveMainWindowReference(mainWindow);
    }

    #endregion

    #region Methods

    protected virtual bool IsTrayPluginAvailable()
    {
      return AppDomain.CurrentDomain.GetAssemblies().Any(x => x.FullName.Contains("SIM.Tool.Plugins.TrayPlugin"));
    }

    #endregion
  }
}