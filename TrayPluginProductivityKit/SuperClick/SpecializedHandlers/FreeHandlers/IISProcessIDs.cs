#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM;
using TrayPluginProductivityKit.Helpers;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class IISProcessIDs : FreeClickHandlerBase
  {
    #region Fields

    protected static readonly string DialogLabel = "SIM - Run instance PIDs";

    #endregion

    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      try
      {
        var param = "list wp";
        var psi = new ProcessStartInfo(Environment.ExpandEnvironmentVariables(@"%systemroot%\System32\inetsrv\appcmd.exe"), param);
        psi.CreateNoWindow = true;
        psi.WindowStyle = ProcessWindowStyle.Hidden;
        psi.RedirectStandardOutput = true;
        psi.UseShellExecute = false;
        var proc = Process.Start(psi);
        proc.WaitForExit();
        var output = proc.StandardOutput.ReadToEnd();
        if (output.IsNullOrEmpty())
        {
          OSShellHelper.ShowMessage("No run instances", DialogLabel);
        }
        else
        {
          OSShellHelper.ShowMessage(output.Trim(), DialogLabel);
        }
        return true;
      }
      catch (Exception ex)
      {
        OSShellHelper.ShowMessage("Unable to retrieve list:{0}{1}".FormatWith(Environment.NewLine, ex.ToString()),
          DialogLabel, MessageBoxIcon.Error);
        return true;
      }
    }

    #endregion
  }
}