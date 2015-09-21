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
  public class OpenVisualStudioProjectWithConfirmation : OpenVisualStudioProject
  {
    #region Methods

    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      if (OSShellHelper.ConfirmFileRun("Visual Studio project"))
      {
        return base.ProcessInstanceClickInternal(instance);
      }
      return true;
    }

    #endregion
  }
}