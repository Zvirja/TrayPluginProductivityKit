#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Instances;
using TrayPluginProductivityKit.Helpers;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class OpenCurrentLog : InstanceClickHandlerBase
  {
    #region Methods

    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      SIMDialogsInteractionHelper.OpenCurrentInstanceLog(instance);
      return true;
    }

    #endregion
  }
}