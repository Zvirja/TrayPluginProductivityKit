﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TrayPluginProductivityKit.Helpers;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenFileOrFolderAnywhereWithConfirmation : OpenFileOrFolderAnywhere
  {
    #region Methods

    protected override bool OpenFileOrFolder(string path)
    {
      if (OSShellHelper.ConfirmFileRun(path))
      {
        return base.OpenFileOrFolder(path);
      }
      return true;
    }

    #endregion
  }
}