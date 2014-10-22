using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Base.Pipelines;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class PostInstallAction
  {
    #region Public Methods and Operators

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