#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.Configuration.VisibleAppBehavior;
using TrayPluginProductivityKit.SuperClick;

#endregion

namespace TrayPluginProductivityKit.BehaviorExtending
{
  public class BehaviorExtender
  {
    #region Properties

    public static ClicksChief ClicksChief { get; set; }
    public static AppBehavior ExtendedBehavior { get; set; }

    #endregion

    #region Methods

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

    #endregion
  }
}