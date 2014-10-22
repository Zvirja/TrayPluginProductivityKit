using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkedInstance
  {
    #region Constructors and Destructors

    public MarkedInstance(InstanceData instanceData, ToolStripItem lastKnownToolstrip)
    {
      this.InstanceData = instanceData;
      this.LastKnownToolstrip = lastKnownToolstrip;
    }

    #endregion

    #region Public Properties

    public InstanceData InstanceData { get; set; }
    public ToolStripItem LastKnownToolstrip { get; set; }

    #endregion
  }
}