#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Instances;
using SIM.Pipelines.InstallModules;
using SIM.Products;

#endregion

namespace TrayPluginProductivityKit.InstallCleanup
{
  public class InstallCleanupAction : IPackageInstallActions
  {
    #region Interface Impl

    public void Execute(Instance instance, Product module)
    {
      InstallCleanupHelper.DoInstanceCleanup(instance.RootPath);
    }

    #endregion
  }
}