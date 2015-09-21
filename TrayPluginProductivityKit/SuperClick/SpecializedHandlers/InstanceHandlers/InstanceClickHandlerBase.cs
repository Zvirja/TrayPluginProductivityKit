#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Instances;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class InstanceClickHandlerBase : MouseClickHandlerBase
  {
    #region Constructors

    protected InstanceClickHandlerBase()
    {
      this.ForInstanceClick = true;
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