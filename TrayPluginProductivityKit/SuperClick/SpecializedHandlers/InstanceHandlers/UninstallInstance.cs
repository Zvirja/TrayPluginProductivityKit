using System;
using System.Collections.Generic;
using System.Text;
using SIM.Instances;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class UninstallInstance : InstanceClickHandlerBase
  {
    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      SIMDialogsInteractionHelper.UninstallInstance(instance);
      return true;
    }
  }
}