using System;
using System.Collections.Generic;
using System.Text;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class InstallInstance : FreeClickHandlerBase
  {
    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      SIMDialogsInteractionHelper.StartInstanceInstallation();
      return true;
    }
  }
}