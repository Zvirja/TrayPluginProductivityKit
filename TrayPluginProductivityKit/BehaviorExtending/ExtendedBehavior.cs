using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Base;
using SIM.Instances;
using SIM.Tool.Base;
using SIM.Tool.Plugins.TrayPlugin.Configuration;
using SIM.Tool.Plugins.TrayPlugin.Configuration.VisibleAppBehavior;
using SIM.Tool.Plugins.TrayPlugin.Messaging;
using SIM.Tool.Plugins.TrayPlugin.Messaging.ClickMessages;

namespace TrayPluginProductivityKit.BehaviorExtending
{
  public class ExtendedBehavior : DefaultAppBehavior
  {
    protected override void AttachToEvents()
    {
      base.AttachToEvents();
      AttachToExtendedEvents();
    }

    protected override void OnInstanceClick(object sender, TrayPluginMessage message)
    {
      MouseClickDetails args = MouseClickMessageHelper.GetDetailsFromMessage(message);
      if (args.MouseButton == MouseButtons.Left && args.PressedKeyboardKeys.Count == 0)
      {
        bool useBackendBrowser = TrayPluginSettingsManager.OpenPagesInBackendBrowser;
        string suffix = TrayPluginSettingsManager.PageToRunOnClick;
        var instance = args.CustomData as Instance;
        Assert.IsNotNull(instance, "Instance is expected");
        WindowHelper.OpenInBrowser(instance.GetUrl(suffix), !useBackendBrowser);
      }
    }

    protected virtual void AttachToExtendedEvents()
    {
      SubscribeToMessages(StandardMessagesKinds.ContextMenuProviderInstanceClick,
        (sender, message) => BehaviorExtender.ClicksChief.OnInstanceClick(message));
      SubscribeToMessages(StandardMessagesKinds.ContextMenuProviderSIMClick,
        (sender, message) => BehaviorExtender.ClicksChief.OnSimEntryClick(message));
      SubscribeToMessages(StandardMessagesKinds.ContextMenuProviderExitClick,
        (sender, message) => BehaviorExtender.ClicksChief.OnExitEntryClick(message));
      SubscribeToMessages(StandardMessagesKinds.IconProviderTrayIconClick,
        (sender, message) => BehaviorExtender.ClicksChief.OnTrayIconClick(message));
    }
  }
}
