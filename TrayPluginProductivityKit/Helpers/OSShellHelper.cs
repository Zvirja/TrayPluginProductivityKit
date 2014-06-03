using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Base;

namespace TrayPluginProductivityKit.Helpers
{
  public static class OSShellHelper
  {
    #region Public Methods and Operators

    public static bool ConfirmFileRun(string path)
    {
      return MessageBox.Show("Are you sure that you want to open {0} ?".FormatWith(path), "Open confirmation",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question) == DialogResult.Yes;
    }

    public static void OpenInExplorer(string folderPath)
    {
      var args = folderPath;
      Process.Start("explorer.exe", args);
    }

    public static void ShowMessage(string text, string header, MessageBoxIcon icon = MessageBoxIcon.Information)
    {
      MessageBox.Show(text, header, MessageBoxButtons.OK, icon);
    }

    #endregion
  }
}