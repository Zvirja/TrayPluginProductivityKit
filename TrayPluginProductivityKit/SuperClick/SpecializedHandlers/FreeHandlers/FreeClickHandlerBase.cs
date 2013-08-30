using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class FreeClickHandlerBase : MouseClickHandlerBase
  {
    protected FreeClickHandlerBase()
    {
      ForInstanceClick = false;
    }
  }
}
