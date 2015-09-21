#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Base.Pipelines;

#endregion

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class PostInstallAction
  {
    #region Methods

    public static void MarkInstance(InstallWizardArgs args)
    {
      MarkingManager.MarkInstance(null, new InstanceData(args.InstanceName, args.InstanceRootPath));
    }

    public static void MarkInstanceExclusive(InstallWizardArgs args)
    {
      MarkingManager.MarkSingleInstanceOnly(null, new InstanceData(args.InstanceName, args.InstanceRootPath));
    }

    #endregion
  }
}