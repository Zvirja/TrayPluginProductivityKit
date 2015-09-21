#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
using SIM;
using TrayPluginProductivityKit.SuperClick.SpecializedHandlers.FreeHandlers;
using TrayPluginProductivityKit.SuperClick.SpecializedHandlers.InstanceHandlers;

#endregion

namespace TrayPluginProductivityKit.SuperClick.ClicksMapping
{
  public class ClickHandlersMappings
  {
    #region Fields

    public static readonly string ExitEntryClickKey = "ExitEntryClick";
    public static readonly string InstanceClickKey = "InstanceClick";
    public static readonly string SimEntryClickKey = "SimEntryClick";
    public static readonly string TrayIconClickKey = "TrayIconClick";

    #endregion

    #region Constructors

    static ClickHandlersMappings()
    {
      ActualMappings = new ClickHandlersMappings();
    }

    public ClickHandlersMappings()
    {
      this.HandlerMappings = new Dictionary<string, List<MouseClickHandlerBase>>();
      this.InitializeMappings();
    }

    #endregion

    #region Properties

    public static ClickHandlersMappings ActualMappings { get; set; }

    protected Dictionary<string, List<MouseClickHandlerBase>> HandlerMappings { get; set; }

    #endregion

    #region Methods

    public virtual void HandleEvent(string groupKey, ClickDetailsWrapper clickDetails)
    {
      if (groupKey.IsNullOrEmpty())
      {
        return;
      }
      if (!this.HandlerMappings.ContainsKey(groupKey))
      {
        return;
      }
      List<MouseClickHandlerBase> handlers = this.HandlerMappings[groupKey];
      foreach (MouseClickHandlerBase mouseClickHandlerBase in handlers)
      {
        if (mouseClickHandlerBase.ProcessClick(clickDetails))
        {
          return;
        }
      }
    }

    protected virtual void AddMapping(string key, MouseClickHandlerBase handler)
    {
      List<MouseClickHandlerBase> currentHandlers;
      if (!this.HandlerMappings.ContainsKey(key))
      {
        currentHandlers = new List<MouseClickHandlerBase>();
        this.HandlerMappings.Add(key, currentHandlers);
      }
      else
      {
        currentHandlers = this.HandlerMappings[key];
      }
      currentHandlers.Add(handler);
    }

    protected void InitializeMappings()
    {
      this.AddMapping(TrayIconClickKey, new IISReset()
      {
        MouseButtonTrigger = MouseButtons.Middle
      });
      this.AddMapping(TrayIconClickKey, new IISProcessIDs()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>
        {
          Key.LeftCtrl
        }
      });

      this.AddMapping(ExitEntryClickKey, new OpenPluginContainingFolderAnywhere()
      {
        MouseButtonTrigger = MouseButtons.Middle
      });

      this.AddMapping(SimEntryClickKey, new InstallInstance()
      {
        MouseButtonTrigger = MouseButtons.Right
      });
      this.AddMapping(SimEntryClickKey, new CallRefreshDialog()
      {
        MouseButtonTrigger = MouseButtons.Middle
      });
      this.AddMapping(SimEntryClickKey, new RunShowConfigBuilder()
      {
        MouseButtonTrigger = MouseButtons.XButton1
      });


      this.AddMapping(InstanceClickKey, new RunPage()
      {
        MouseButtonTrigger = MouseButtons.Right,
        CustomParameters = "f|"
      });
      this.AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Middle
      });
      this.AddMapping(InstanceClickKey, new ToggleInstanceMarking()
      {
        MouseButtonTrigger = MouseButtons.XButton1
      });
      this.AddMapping(InstanceClickKey, new MarkSingleInstanceOnly()
      {
        MouseButtonTrigger = MouseButtons.XButton1,
        KeyTriggers = new List<Key>
        {
          Key.LeftCtrl
        }
      });
      this.AddMapping(InstanceClickKey, new UninstallInstance()
      {
        MouseButtonTrigger = MouseButtons.XButton2
      });

      //LEFT CTRL
      this.AddMapping(InstanceClickKey, new OpenVisualStudioProjectWithConfirmation()
      {
        MouseButtonTrigger = MouseButtons.Left,
        KeyTriggers = new List<Key>()
        {
          Key.LeftCtrl
        },
      });

      this.AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>()
        {
          Key.LeftCtrl
        },
        CustomParameters = @"Data\logs"
      });

      this.AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Right,
        KeyTriggers = new List<Key>()
        {
          Key.LeftCtrl
        },
        CustomParameters = @"Website\bin"
      });

      this.AddMapping(InstanceClickKey, new CreateVSWorkaroundPostAction()
      {
        MouseButtonTrigger = MouseButtons.XButton2,
        KeyTriggers = new List<Key>()
        {
          Key.LeftCtrl
        }
      });


      //LEFT SHIFT
      this.AddMapping(InstanceClickKey, new RunPage()
      {
        MouseButtonTrigger = MouseButtons.Left,
        KeyTriggers = new List<Key>()
        {
          Key.LeftShift
        },
        CustomParameters = @"f|sitecore/admin/showconfig.aspx"
      });

      /*this.AddMapping(InstanceClickKey, new OpenCurrentLog()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>()
        {
          Key.LeftShift
        },
      });*/

      this.AddMapping(InstanceClickKey, new RunPage()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>()
        {
          Key.LeftShift
        },
        CustomParameters = @"f|toolbox"
      });

      this.AddMapping(InstanceClickKey, new RunPage()
      {
        MouseButtonTrigger = MouseButtons.Right,
        KeyTriggers = new List<Key>()
        {
          Key.LeftShift
        },
        CustomParameters = @"b|sitecore/admin/cache.aspx"
      });


      // LEFT CTRL + SHIFT

      this.AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Left,
        KeyTriggers = new List<Key>()
        {
          Key.LeftShift,
          Key.LeftCtrl
        },
        CustomParameters = @"Website\web.config"
      });

      this.AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Middle,
        KeyTriggers = new List<Key>()
        {
          Key.LeftShift,
          Key.LeftCtrl
        },
        CustomParameters = @"Website\App_config"
      });

      this.AddMapping(InstanceClickKey, new OpenFileOrFolderInsideRoot()
      {
        MouseButtonTrigger = MouseButtons.Right,
        KeyTriggers = new List<Key>()
        {
          Key.LeftShift,
          Key.LeftCtrl
        },
        CustomParameters = @"Website\App_config\Include"
      });
    }

    #endregion
  }
}