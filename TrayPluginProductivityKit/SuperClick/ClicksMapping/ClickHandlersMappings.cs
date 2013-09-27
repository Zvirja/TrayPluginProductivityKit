using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using SIM.Base;
using SIM.Tool.Plugins.TrayPlugin.Messaging;
using SIM.Tool.Plugins.TrayPlugin.Messaging.ClickMessages;
using TrayPluginProductivityKit.Configuration.Mappings.Metadata;
using TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers;
using TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers;

namespace TrayPluginProductivityKit.SuperClick.ClicksMapping
{
  public class ClickHandlersMappings
  {
    public static ClickHandlersMappings ActualMappings { get; set; }
    public static readonly string InstanceClickKey = "InstanceClick";
    public static readonly string TrayIconClickKey = "TrayIconClick";
    public static readonly string ExitEntryClickKey = "ExitEntryClick";
    public static readonly string SimEntryClickKey = "SimEntryClick";


    static ClickHandlersMappings()
    {
      ActualMappings = new ClickHandlersMappings();
    }

    protected Dictionary<string, List<MouseClickHandlerBase>> HandlerMappings { get; set; }

    public ClickHandlersMappings()
    {
      HandlerMappings = new Dictionary<string, List<MouseClickHandlerBase>>();
      InitializeMappings();
    }


    public virtual void HandleEvent(string groupKey, ClickDetailsWrapper clickDetails, TrayPluginMessage message)
    {
      if (groupKey.IsNullOrEmpty())
        return;
      if (!HandlerMappings.ContainsKey(groupKey))
        return;
      List<MouseClickHandlerBase> handlers = HandlerMappings[groupKey];
      foreach (MouseClickHandlerBase mouseClickHandlerBase in handlers)
      {
        if (mouseClickHandlerBase.ProcessClick(clickDetails, message))
          return;
      }
    }

    protected void InitializeMappings()
    {


      AddMapping(TrayIconClickKey, new IISReset() { MouseButtonTrigger = MouseButtons.Middle });
      AddMapping(TrayIconClickKey, new IISProcessIDs() { MouseButtonTrigger = MouseButtons.Middle, KeyTriggers = new List<Key> { Key.LeftCtrl } });

      AddMapping(ExitEntryClickKey, new OpenPluginContainingFolderAnywhere() { MouseButtonTrigger = MouseButtons.Middle });

      AddMapping(SimEntryClickKey, new InstallInstance() { MouseButtonTrigger = MouseButtons.Right });
      AddMapping(SimEntryClickKey, new CallRefreshDialog() { MouseButtonTrigger = MouseButtons.Middle });
      AddMapping(SimEntryClickKey, new RunShowConfigBuilder() { MouseButtonTrigger = MouseButtons.XButton1 });



      AddMapping(InstanceClickKey, new RunPage() { MouseButtonTrigger = MouseButtons.Right, CustomParameters = "f|" });
      AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot() { MouseButtonTrigger = MouseButtons.Middle });
      AddMapping(InstanceClickKey, new ToggleInstanceMarking() { MouseButtonTrigger = MouseButtons.XButton1 });
      AddMapping(InstanceClickKey, new UninstallInstance() { MouseButtonTrigger = MouseButtons.XButton2 });

      //LEFT CTRL
      AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRootWithConfirmation()
      {
        MouseButtonTrigger = MouseButtons.Left,
        KeyTriggers = new List<Key>() { Key.LeftCtrl },
        CustomParameters = "Solution.sln"
      });

      AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>() { Key.LeftCtrl },
        CustomParameters = @"Data\logs"
      });

      AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Right,
        KeyTriggers = new List<Key>() { Key.LeftCtrl },
        CustomParameters = @"Website\bin"
      });


      //LEFT SHIFT
      AddMapping(InstanceClickKey, new RunPage()
      {
        MouseButtonTrigger = MouseButtons.Left,
        KeyTriggers = new List<Key>() { Key.LeftShift },
        CustomParameters = @"b|sitecore/admin/showconfig.aspx"
      });

      AddMapping(InstanceClickKey, new OpenCurrentLog()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>() { Key.LeftShift },
      });

      AddMapping(InstanceClickKey, new RunPage()
      {
        MouseButtonTrigger = MouseButtons.Right,
        KeyTriggers = new List<Key>() { Key.LeftShift },
        CustomParameters = @"b|sitecore/admin/cache.aspx"
      });


      // LEFT CTRL + SHIFT

      AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Left,
        KeyTriggers = new List<Key>() { Key.LeftShift, Key.LeftCtrl },
        CustomParameters = @"Website\web.config"
      });

      AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>() { Key.LeftShift, Key.LeftCtrl },
        CustomParameters = @"Website\App_config"
      });

      AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Right,
        KeyTriggers = new List<Key>() { Key.LeftShift, Key.LeftCtrl },
        CustomParameters = @"Website\App_config\Include"
      });


    }

    protected virtual void AddMapping(string key, MouseClickHandlerBase handler)
    {
      List<MouseClickHandlerBase> currentHandlers;
      if (!HandlerMappings.ContainsKey(key))
      {
        currentHandlers = new List<MouseClickHandlerBase>();
        HandlerMappings.Add(key, currentHandlers);
      }
      else
        currentHandlers = HandlerMappings[key];
      currentHandlers.Add(handler);
    }
  }
}
