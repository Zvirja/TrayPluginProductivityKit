using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SIM.Tool.Plugins.TrayPlugin.Configuration.VisibleAppBehavior;
using TrayPluginProductivityKit.SuperClick;

namespace TrayPluginProductivityKit.BehaviorExtending
{
  public class BehaviorExtender
  {
    #region Public Properties

    public static ClicksChief ClicksChief { get; set; }
    public static AppBehavior ExtendedBehavior { get; set; }

    #endregion

    #region Public Methods and Operators

    public static void Initialize()
    {
      CreateInstances();
      ReplaceDefaultBehavior();
    }

    #endregion

    #region Methods

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