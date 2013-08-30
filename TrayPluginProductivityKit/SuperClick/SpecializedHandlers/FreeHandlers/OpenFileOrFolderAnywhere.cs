using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Base;
using SIM.Tool.Base;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenFileOrFolderAnywhere : FreeClickHandlerBase
  {
    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      if (CustomParameters.IsNullOrEmpty())
        return false;
      return OpenFileOrFolder(CustomParameters);
    }

    protected virtual bool OpenFileOrFolder(string path)
    {
      OSShellHelper.OpenInExplorer(path);
      return true;
    }
  }
}
