using System;
using System.Collections.Generic;
using System.Text;
using SIM.Instances;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class OpenVisualStudioProjectWithConfirmation : OpenVisualStudioProject
  {
    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      if (OSShellHelper.ConfirmFileRun("Visual Studio project"))
        return base.ProcessInstanceClickInternal(instance);
      return true;
    }
  }
}