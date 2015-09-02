using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Base;
using SIM.Instances;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class OpenFileOrFolderInsideRoot : InstanceClickHandlerBase
  {
    #region Methods

    protected virtual string GetPathToOpen(string instanceRootPath)
    {
      if (this.CustomParameters != null && this.CustomParameters.Trim().IndexOf(':') > -1)
      {
        return instanceRootPath;
      }
      if (this.CustomParameters.IsNullOrEmpty())
      {
        return instanceRootPath;
      }
      return Path.Combine(instanceRootPath, this.CustomParameters);
    }

    protected virtual bool OpenFileOrFolder(string path)
    {
      if (File.Exists(path))
      {
        OSShellHelper.RunFile(path);
      } else if (Directory.Exists(path))
      {
        OSShellHelper.OpenFolderInExplorer(path);
      }
      else
      {
        OSShellHelper.ShowMessage("File or folder {0} doesn't exist".FormatWith(path), "Doesn't exist error", MessageBoxIcon.Error);
      }
      return true;
    }

    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      var path = this.GetPathToOpen(instance.RootPath);
      return this.OpenFileOrFolder(path);
    }

    #endregion
  }
}