using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class IISReset:FreeClickHandlerBase
  {
    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      var param = "/timeout:0";
      var psi = new ProcessStartInfo("iisreset", param);
      Process.Start(psi);
      return true;
    }
  }
}
