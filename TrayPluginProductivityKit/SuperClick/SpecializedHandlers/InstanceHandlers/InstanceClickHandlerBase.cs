using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using SIM.Instances;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class InstanceClickHandlerBase: MouseClickHandlerBase
  {
    protected InstanceClickHandlerBase()
    {
      ForInstanceClick = true;
    }

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      var instance = clickDetails.ClickDetails.CustomData as Instance;
      if (instance == null)
        return false;
      return ProcessInstanceClickInternal(instance);
    }

    protected virtual bool ProcessInstanceClickInternal(Instance instance)
    {
      return false;
    }
  }
}
