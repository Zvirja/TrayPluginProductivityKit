using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Base;
using SIM.Instances;
using SIM.Tool.Base;
using TrayPluginProductivityKit.Helpers;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class OpenFileOrFolderInsideRoot : InstanceClickHandlerBase
  {
    protected override bool ProcessInstanceClickInternal(Instance instance)
    {
      var path = GetPathToOpen(instance.RootPath);
      return OpenFileOrFolder(path);
    }

    protected virtual string GetPathToOpen(string instanceRootPath)
    {
      if (CustomParameters != null && CustomParameters.Trim().IndexOf(':') > -1)
        return instanceRootPath;
      if (CustomParameters.IsNullOrEmpty())
        return instanceRootPath;
      return Path.Combine(instanceRootPath, CustomParameters);
    }

    protected virtual bool OpenFileOrFolder(string path)
    {
      if (File.Exists(path) || Directory.Exists(path))
      {
        OSShellHelper.OpenInExplorer(path);
      }
      else
        OSShellHelper.ShowMessage("File or folder {0} doesn't exist".FormatWith(path),"Doesn't exist error",MessageBoxIcon.Error);
      return true;
    }
  }
}
