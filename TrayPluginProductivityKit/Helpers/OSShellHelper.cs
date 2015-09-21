#region Usings

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM;
using TrayPluginProductivityKit.Configuration;

#endregion

namespace TrayPluginProductivityKit.Helpers
{
  public static class OSShellHelper
  {
    #region Methods

    public static bool ConfirmFileRun(string path)
    {
      return MessageBox.Show("Are you sure that you want to open {0} ?".FormatWith(path), "Open confirmation",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question) == DialogResult.Yes;
    }

    public static void OpenFolderInExplorer(string folderPath)
    {
      var args = folderPath;
      Process.Start(TPPKAdvancedSettings.TPPKShell.Value, args);
    }

    public static void RunFile(string filePath)
    {
      Process.Start("explorer.exe", filePath);
    }

    public static void ShowMessage(string text, string header, MessageBoxIcon icon = MessageBoxIcon.Information)
    {
      MessageBox.Show(text, header, MessageBoxButtons.OK, icon);
    }

    #endregion
  }
}