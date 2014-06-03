using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class FreeClickHandlerBase : MouseClickHandlerBase
  {
    #region Constructors and Destructors

    protected FreeClickHandlerBase()
    {
      ForInstanceClick = false;
    }

    #endregion
  }
}