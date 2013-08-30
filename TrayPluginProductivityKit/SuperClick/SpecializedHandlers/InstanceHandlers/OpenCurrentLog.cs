using System;
using System.Collections.Generic;
using System.Text;
using SIM.Instances;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class OpenCurrentLog : InstanceClickHandlerBase
  {
    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      SIMDialogsInteractionHelper.OpenCurrentInstanceLog(instance);
      return true;
    }
  }
}