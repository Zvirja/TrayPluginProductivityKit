#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM;

#endregion

namespace TrayPluginProductivityKit.Configuration
{
  public static class TPPKAdvancedSettings
  {
    #region Fields

    public static readonly AdvancedProperty<string> TPPKRemAfterInstall = AdvancedSettings.Create("App/Plugins/TrayPluginProductivityKit/Install/RemOnInstall", string.Empty);

    public static readonly AdvancedProperty<string> TPPKShell = AdvancedSettings.Create("App/Plugins/TrayPluginProductivityKit/OSShell", "explorer.exe");

    #endregion

    #region Constructors

    static TPPKAdvancedSettings()
    {
    }

    #endregion

    #region Methods

    public static void Init()
    {
    }

    #endregion
  }
}