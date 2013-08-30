using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using SIM.Instances;
using TrayPluginProductivityKit.InstanceMarking;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class ToggleInstanceMarking : InstanceClickHandlerBase
  {
    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      var toolStripItem = clickDetails.ClickDetails.ClickSource as ToolStripItem;
      if (toolStripItem == null)
        return false;
      var instance = clickDetails.ClickDetails.CustomData as Instance;
      if (instance == null)
        return false;
      MarkingManager.ToggleInstanceMarking(toolStripItem, instance);
      return true;
    }
  }
}