using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenPluginContainingFolderAnywhere : OpenFileOrFolderAnywhere
  {
    #region Constructors and Destructors

    public OpenPluginContainingFolderAnywhere()
    {
      this.CustomParameters = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
    }

    #endregion
  }
}