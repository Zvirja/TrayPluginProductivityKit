using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Instances;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkedInstance
  {
    #region Constructors and Destructors

    public MarkedInstance(Instance instance, ToolStripItem lastKnownToolstrip)
    {
      this.Instance = instance;
      this.LastKnownToolstrip = lastKnownToolstrip;
    }

    #endregion

    #region Public Properties

    public Instance Instance { get; set; }
    public ToolStripItem LastKnownToolstrip { get; set; }

    #endregion
  }
}