using System;
using System.Collections.Generic;
using System.Text;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class OpenFileOrFolderInsideRootWithConfirmation : OpenFileOrFolderInsideRoot
  {
    protected override bool OpenFileOrFolder(string path)
    {
      if(OSShellHelper.ConfirmFileRun(path))
        return base.OpenFileOrFolder(path);
      return true;
    }
  }
}