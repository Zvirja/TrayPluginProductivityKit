using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Windows.MainWindowComponents;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class RunShowConfigBuilder : FreeClickHandlerBase
  {
    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      new ConfigBuilderButton().OnClick(null, null);
      return true;
    }

    #endregion
  }
}