#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

namespace TrayPluginProductivityKit.Helpers
{
  public class MainWindowHelperProxy
  {
    #region Fields

    public static Type MainWindowHelperType = Assembly.GetEntryAssembly().GetType("SIM.Tool.Windows.MainWindowHelper", false);

    #endregion
  }
}