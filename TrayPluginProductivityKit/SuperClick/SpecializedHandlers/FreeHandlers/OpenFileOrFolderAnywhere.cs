using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SIM.Base;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenFileOrFolderAnywhere : FreeClickHandlerBase
  {
    #region Methods

    protected virtual bool OpenFileOrFolder(string path)
    {
      if (File.Exists(path))
      {
        OSShellHelper.RunFile(path);
      }
      else
      {
        OSShellHelper.OpenFolderInExplorer(path);
      }
      return true;
    }

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      if (this.CustomParameters.IsNullOrEmpty())
      {
        return false;
      }
      return this.OpenFileOrFolder(this.CustomParameters);
    }

    #endregion
  }
}