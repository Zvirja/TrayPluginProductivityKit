#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class IISReset : FreeClickHandlerBase
  {
    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      //Kill all w3wp processes before.
      Process[] w3wpProcesses = Process.GetProcessesByName("w3wp");
      foreach (Process w3wpProcess in w3wpProcesses)
      {
        w3wpProcess.Kill();
      }

      var param = "/timeout:0";
      var psi = new ProcessStartInfo("iisreset", param);
      Process.Start(psi);
      return true;
    }

    #endregion
  }
}