using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Base;

namespace TrayPluginProductivityKit.Configuration
{
  public static class TPPKAdvancedSettings
  {
    #region Static Fields

    public static readonly AdvancedProperty<string> TPPKRemAfterInstall = AdvancedSettings.Create("App/Plugins/TrayPluginProductivityKit/Install/RemOnInstall", string.Empty);

    #endregion

    #region Constructors and Destructors

    static TPPKAdvancedSettings()
    {
    }

    #endregion

    #region Public Methods and Operators

    public static void Init()
    {
    }

    #endregion
  }
}