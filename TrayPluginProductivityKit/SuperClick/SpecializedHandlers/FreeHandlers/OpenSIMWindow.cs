using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.WindowZone;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenSIMWindow : FreeClickHandlerBase
  {
    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      WindowWorks.ShowWindow();
      return true;
    }

    #endregion
  }
}