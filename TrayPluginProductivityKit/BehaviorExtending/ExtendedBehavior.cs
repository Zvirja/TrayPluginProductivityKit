﻿#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.Configuration.VisibleAppBehavior;

#endregion

namespace TrayPluginProductivityKit.BehaviorExtending
{
  public class ExtendedBehavior : DefaultAppBehavior
  {
    #region Methods

    protected override void AttachToEvents()
    {
      base.AttachToEvents();
      this.AttachToExtendedEvents();
    }

    protected virtual void AttachToExtendedEvents()
    {
      TrayPluginEvents.ContextMenuInstanceEntryClick += BehaviorExtender.ClicksChief.OnInstanceClick;
      TrayPluginEvents.ContextMenuSIMClick += BehaviorExtender.ClicksChief.OnSimEntryClick;
      TrayPluginEvents.ContextMenuExitClick += BehaviorExtender.ClicksChief.OnExitEntryClick;
      TrayPluginEvents.TrayIconClick += BehaviorExtender.ClicksChief.OnTrayIconClick;
    }

    protected override void DetachFromEvents()
    {
      base.DetachFromEvents();
      this.DetachFromExtendedEvents();
    }

    protected virtual void DetachFromExtendedEvents()
    {
      TrayPluginEvents.ContextMenuInstanceEntryClick -= BehaviorExtender.ClicksChief.OnInstanceClick;
      TrayPluginEvents.ContextMenuSIMClick -= BehaviorExtender.ClicksChief.OnSimEntryClick;
      TrayPluginEvents.ContextMenuExitClick -= BehaviorExtender.ClicksChief.OnExitEntryClick;
      TrayPluginEvents.TrayIconClick -= BehaviorExtender.ClicksChief.OnTrayIconClick;
    }

    #endregion
  }
}