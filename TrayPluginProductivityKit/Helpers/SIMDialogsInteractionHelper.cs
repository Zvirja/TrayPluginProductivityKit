using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIM.Instances;
using SIM.Tool;
using SIM.Tool.Base;
using SIM.Tool.MainWindowComponents;
using SIM.Tool.Plugins.TrayPlugin.Lifecycle;
using SIM.Tool.Plugins.TrayPlugin.WindowZone;

namespace TrayPluginProductivityKit.Helpers
{
  public static class SIMDialogsInteractionHelper
  {
    public static void StartInstanceInstallation()
    {
      new InstallInstanceButton().OnClick(null, null);
    }

    public static void UninstallInstance(Instance instance)
    {
      new DeleteInstanceButton().OnClick(null, instance);
    }

    public static void ShowRefreshDialog()
    {
      new RefreshButton().OnClick(null, null);
    }

    public static void OpenCurrentInstanceLog(Instance instance)
    {
      try
      {
        new OpenCurrentLogButton().OnClick(null, instance);
      }
      catch
      {
        
      }
    }
  }
}
