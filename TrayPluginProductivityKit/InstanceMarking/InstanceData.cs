using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Instances;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class InstanceData
  {
    #region Constructors and Destructors

    public InstanceData(string instanceName, string instaceRootPath)
    {
      this.InstanceName = instanceName;
      this.InstaceRootPath = instaceRootPath;
    }

    #endregion

    #region Public Properties

    public string InstaceRootPath { get; private set; }
    public string InstanceName { get; private set; }

    #endregion

    #region Public Methods and Operators

    public static InstanceData FromInstance(Instance instance)
    {
      return new InstanceData(instance.Name, instance.RootPath);
    }

    #endregion
  }
}