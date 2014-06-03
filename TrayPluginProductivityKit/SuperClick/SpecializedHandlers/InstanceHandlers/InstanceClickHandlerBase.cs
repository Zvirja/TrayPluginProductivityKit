using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Instances;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class InstanceClickHandlerBase : MouseClickHandlerBase
  {
    #region Constructors and Destructors

    protected InstanceClickHandlerBase()
    {
      ForInstanceClick = true;
    }

    #endregion

    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      Instance instance = clickDetails.Instance;
      return this.ProcessInstanceClickInternal(instance);
    }

    protected virtual bool ProcessInstanceClickInternal(Instance instance)
    {
      return false;
    }

    #endregion
  }
}