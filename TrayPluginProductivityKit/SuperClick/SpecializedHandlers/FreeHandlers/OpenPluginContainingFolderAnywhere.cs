#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers
{
  public class OpenPluginContainingFolderAnywhere : OpenFileOrFolderAnywhere
  {
    #region Constructors

    public OpenPluginContainingFolderAnywhere()
    {
      this.CustomParameters = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
    }

    #endregion
  }
}