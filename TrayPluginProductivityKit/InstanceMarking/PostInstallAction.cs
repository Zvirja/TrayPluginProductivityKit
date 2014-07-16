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
      MarkingManager.MarkInstance(null, args.Instance);
    }

    #endregion
  }
}