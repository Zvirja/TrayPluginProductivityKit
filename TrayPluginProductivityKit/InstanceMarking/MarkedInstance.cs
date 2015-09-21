#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#endregion

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkedInstance
  {
    #region Constructors

    public MarkedInstance(InstanceData instanceData, ToolStripItem lastKnownToolstrip)
    {
      this.InstanceData = instanceData;
      this.LastKnownToolstrip = lastKnownToolstrip;
    }

    #endregion

    #region Properties

    public InstanceData InstanceData { get; set; }
    public ToolStripItem LastKnownToolstrip { get; set; }

    #endregion
  }
}