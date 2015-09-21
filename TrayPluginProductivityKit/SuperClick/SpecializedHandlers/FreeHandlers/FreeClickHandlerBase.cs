#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class FreeClickHandlerBase : MouseClickHandlerBase
  {
    #region Constructors

    protected FreeClickHandlerBase()
    {
      this.ForInstanceClick = false;
    }

    #endregion
  }
}