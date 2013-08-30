using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SIM.Tool.Base;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenFileOrFolderAnywhereWithConfirmation : OpenFileOrFolderAnywhere
  {
    protected override bool OpenFileOrFolder(string path)
    {
      if (OSShellHelper.ConfirmFileRun(path))
        return base.OpenFileOrFolder(path);
      return true;
    }
  }
}