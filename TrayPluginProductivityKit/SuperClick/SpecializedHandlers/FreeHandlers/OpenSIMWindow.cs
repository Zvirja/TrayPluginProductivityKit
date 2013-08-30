using System;
using System.Collections.Generic;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.WindowZone;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenSIMWindow : FreeClickHandlerBase
  {
    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      WindowWorks.ShowWindow();
      return true;
    }
  }
}