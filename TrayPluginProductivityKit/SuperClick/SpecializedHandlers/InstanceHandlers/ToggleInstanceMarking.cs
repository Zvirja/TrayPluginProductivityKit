using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;
using TrayPluginProductivityKit.InstanceMarking;

namespace TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers
{
  public class ToggleInstanceMarking : InstanceClickHandlerBase
  {
    #region Methods

    protected override bool ProcessClickInternal(ClickDetailsWrapper clickDetails)
    {
      var castedArgs = clickDetails.RawArgs as InstanceEntryClickArgs;
      if (castedArgs == null)
      {
        return false;
      }
      var toolStripItem = castedArgs.ToolStripItem;
      if (toolStripItem == null)
      {
        return false;
      }
      var instance = clickDetails.Instance;
      MarkingManager.ToggleInstanceMarking(toolStripItem, instance);
      return true;
    }

    #endregion
  }
}