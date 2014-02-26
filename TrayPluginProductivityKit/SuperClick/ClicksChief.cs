using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Tool.Plugins.TrayPlugin.Common;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;
using TrayPluginProductivityKit.SuperClick.ClicksMapping;

namespace TrayPluginProductivityKit.SuperClick
{
  public class ClicksChief
  {
    #region Properties

    protected ClickHandlersMappings ActualMappings
    {
      get { return ClickHandlersMappings.ActualMappings; }
    }

    #endregion

    #region Public Methods and Operators

    public virtual void OnExitEntryClick(object sender, ExtendedMouseClickArgs args)
    {
      MouseClickInformation clickDetails = args.ClickInformation;
      this.ActualMappings.HandleEvent(ClickHandlersMappings.ExitEntryClickKey, new ClickDetailsWrapper(clickDetails, null, args));
    }

    public virtual void OnInstanceClick(object sender, InstanceEntryClickArgs args)
    {
      MouseClickInformation clickDetails = args.ClickInformation;
      this.ActualMappings.HandleEvent(ClickHandlersMappings.InstanceClickKey, new ClickDetailsWrapper(clickDetails, args.Instance, args));
    }

    public virtual void OnSimEntryClick(object sender, ExtendedMouseClickArgs args)
    {
      MouseClickInformation clickDetails = args.ClickInformation;
      this.ActualMappings.HandleEvent(ClickHandlersMappings.SimEntryClickKey, new ClickDetailsWrapper(clickDetails, null, args));
    }

    public virtual void OnTrayIconClick(object sender, ExtendedMouseClickArgs args)
    {
      MouseClickInformation clickDetails = args.ClickInformation;
      if (clickDetails.MouseButton == MouseButtons.Right)
        return;
      this.ActualMappings.HandleEvent(ClickHandlersMappings.TrayIconClickKey, new ClickDetailsWrapper(clickDetails, null, args));
    }

    #endregion
  }
}