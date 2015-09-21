#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;
using TrayPluginProductivityKit.InstanceMarking;

#endregion

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class MarkSingleInstanceOnly : InstanceClickHandlerBase
  {
    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      var castedArgs = clickDetails.RawArgs as InstanceEntryClickArgs;
      if (castedArgs == null)
      {
        return false;
      }
      ToolStripItem toolStripItem = castedArgs.ToolStripItem;
      if (toolStripItem == null)
      {
        return false;
      }
      var instance = clickDetails.Instance;
      MarkingManager.MarkSingleInstanceOnly(toolStripItem, InstanceData.FromInstance(instance));
      return true;
    }

    #endregion
  }
}