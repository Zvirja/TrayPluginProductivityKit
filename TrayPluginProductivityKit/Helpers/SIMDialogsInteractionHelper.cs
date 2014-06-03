using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SIM.Instances;
using SIM.Tool.Windows.MainWindowComponents;

namespace TrayPluginProductivityKit.Helpers
{
  public static class SIMDialogsInteractionHelper
  {
    #region Public Properties

    public static Window MainWindow { get; set; }

    #endregion

    #region Public Methods and Operators

    public static void OpenCurrentInstanceLog(Instance instance)
    {
      try
      {
        new OpenCurrentLogButton().OnClick(MainWindow, instance);
      }
      catch
      {
      }
    }

    public static void RunVisualStudioProject(Instance instance)
    {
      new OpenVisualStudioButton().OnClick(MainWindow, instance);
    }


    public static void SaveMainWindowReference(Window mainWindow)
    {
      MainWindow = mainWindow;
    }

    public static void ShowRefreshDialog()
    {
      new RefreshButton().OnClick(MainWindow, null);
    }

    public static void StartInstanceInstallation()
    {
      new InstallInstanceButton().OnClick(MainWindow, null);
    }

    public static void UninstallInstance(Instance instance)
    {
      new DeleteInstanceButton().OnClick(MainWindow, instance);
    }

    #endregion
  }
}