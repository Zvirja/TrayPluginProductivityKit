using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.Configuration.VisibleAppBehavior;
using SIM.Tool.Plugins.TrayPlugin.Messaging;
using TrayPluginProductivityKit.SuperClick;

namespace TrayPluginProductivityKit.BehaviorExtending
{
  public class BehaviorExtender
  {
    public static ClicksChief ClicksChief { get; set; }
    public static AppBehavior ExtendedBehavior { get; set; }

    public static void Initialize()
    {
      CreateInstances();
      ReplaceDefaultBehavior();
    }

    private static void CreateInstances()
    {
      ClicksChief = new ClicksChief();
      ExtendedBehavior = new ExtendedBehavior();
    }

    private static void ReplaceDefaultBehavior()
    {
      AppBehaviorManager.ChangeBehavior(ExtendedBehavior);
    }
  }
}
