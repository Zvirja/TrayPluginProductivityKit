using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Pipelines.Install;

namespace TrayPluginProductivityKit.InstallCleanup
{
  public class InstallFilesCleanup : InstallProcessor
  {
    #region Methods

    protected override void Process(InstallArgs args)
    {
      InstallCleanupHelper.DoInstanceCleanup(args.RootFolderPath);
    }

    #endregion
  }
}