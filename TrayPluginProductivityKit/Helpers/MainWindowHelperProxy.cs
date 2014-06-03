using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TrayPluginProductivityKit.Helpers
{
  public class MainWindowHelperProxy
  {
    #region Static Fields

    public static Type MainWindowHelperType = Assembly.GetEntryAssembly().GetType("SIM.Tool.Windows.MainWindowHelper", false);

    #endregion
  }
}