using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;
using SIM.Tool.Plugins.TrayPlugin.Messaging;
using SIM.Tool.Plugins.TrayPlugin.Messaging.ClickMessages;
using TrayPluginProductivityKit.SuperClick.ClicksMapping;

namespace TrayPluginProductivityKit.SuperClick
{
  public class ClicksChief
  {
    protected ClickHandlersMappings ActualMappings
    {
      get { return ClickHandlersMappings.ActualMappings; }
    }

    public virtual void OnInstanceClick(TrayPluginMessage message)
    {
      MouseClickDetails clickDetails = MouseClickMessageHelper.GetDetailsFromMessage(message);
      if (clickDetails == null)
        return;
      ActualMappings.HandleEvent(ClickHandlersMappings.InstanceClickKey, new ClickDetailsWrapper(clickDetails, true), message);
    }

    public virtual void OnTrayIconClick(TrayPluginMessage message)
    {
      MouseClickDetails clickDetails = MouseClickMessageHelper.GetDetailsFromMessage(message);
      if (clickDetails == null)
        return;
      if (clickDetails.MouseButton == MouseButtons.Right)
        return;
      ActualMappings.HandleEvent(ClickHandlersMappings.TrayIconClickKey, new ClickDetailsWrapper(clickDetails, false), message);
    }

    public virtual void OnSimEntryClick(TrayPluginMessage message)
    {
      MouseClickDetails clickDetails = MouseClickMessageHelper.GetDetailsFromMessage(message);
      if (clickDetails == null)
        return;
      ActualMappings.HandleEvent(ClickHandlersMappings.SimEntryClickKey, new ClickDetailsWrapper(clickDetails, false), message);
    }
    public virtual void OnExitEntryClick(TrayPluginMessage message)
    {
      MouseClickDetails clickDetails = MouseClickMessageHelper.GetDetailsFromMessage(message);
      if (clickDetails == null)
        return;
      ActualMappings.HandleEvent(ClickHandlersMappings.ExitEntryClickKey, new ClickDetailsWrapper(clickDetails, false), message);
    }
  }
}
