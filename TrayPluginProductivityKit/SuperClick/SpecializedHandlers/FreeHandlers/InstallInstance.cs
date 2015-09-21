#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrayPluginProductivityKit.Helpers;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class InstallInstance : FreeClickHandlerBase
  {
    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      SIMDialogsInteractionHelper.StartInstanceInstallation();
      return true;
    }

    #endregion
  }
}