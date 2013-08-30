using System;
using System.Collections.Generic;
using System.Text;
using SIM.Tool.Plugins.ShowConfigBuilder;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class RunShowConfigBuilder : FreeClickHandlerBase
  {
    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      new ConfigBuilderButton().OnClick(null, null);
      return true;
    }
  }
}