using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenPluginContainingFolderAnywhere : OpenFileOrFolderAnywhere
  {
    public OpenPluginContainingFolderAnywhere()
    {
      CustomParameters = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
    }
  }
}