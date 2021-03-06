﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SIM.Tool.Base.Plugins;
using SIM.Tool.Windows;
using TrayPluginProductivityKit.BehaviorExtending;
using TrayPluginProductivityKit.Configuration;
using TrayPluginProductivityKit.Configuration.Mappings;
using TrayPluginProductivityKit.Configuration.Mappings.Metadata;
using TrayPluginProductivityKit.Helpers;
using TrayPluginProductivityKit.InstanceIcons;
using TrayPluginProductivityKit.InstanceMarking;
using TrayPluginProductivityKit.Resources;
using TrayPluginProductivityKit.SilentStartup;
using TrayPluginProductivityKit.TrayNotifications;

#endregion

namespace TrayPluginProductivityKit
{
  public class EntryPoint : IInitProcessor, IMainWindowLoadedProcessor
  {
    #region Interface Impl

    public void Process()
    {
      if (!this.IsTrayPluginAvailable())
      {
        return;
      }

      TPPKAdvancedSettings.Init(); //Need this to promote custom settings to config manager
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

      //  InstallPipelineInjector.InjectCustomPipeline();
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