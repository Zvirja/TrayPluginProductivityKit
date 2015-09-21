#region Usings

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Instances;
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;

#endregion

namespace TrayPluginProductivityKit.InstancePIDs
{
  public class InstanceMenuCollector
  {
    #region Fields

    public volatile bool IsUnderConstruction;

    #endregion

    #region Constructors

    protected InstanceMenuCollector()
    {
    }

    #endregion

    #region Properties

    public static InstanceMenuCollector Collector { get; set; }
    public Dictionary<Instance, ToolStripItem> ContextMenu { get; set; }

    protected CollectorState CurrentState { get; set; }

    #endregion

    #region Methods

    public static void Initialize()
    {
      Collector = new InstanceMenuCollector();
      Collector.InitializeInstance();
    }

    public void InitializeInstance()
    {
      this.CurrentState = CollectorState.MenuBuilded;
      this.ContextMenu = new Dictionary<Instance, ToolStripItem>();
      this.IsUnderConstruction = false;
      TrayPluginEvents.ContextMenuEntryConstructed += this.OnMenuEntryConstructed;
      TrayPluginEvents.ContextMenuConstructed += this.OnContextMenuConstructed;
    }

    protected virtual void AddInstanceEntry(MenuEntryConstructedArgs args)
    {
      ToolStripItem menuItem = args.ContextMenuItem;
      if (args.Position != MenuEntryPosition.BodyEntry)
      {
        return;
      }
      var relatedInstance = args.Instance;
      if (relatedInstance == null)
      {
        throw new InvalidDataException("menuItem.Tag is not Instance or is null");
      }
      this.ContextMenu[relatedInstance] = menuItem;
    }

    protected virtual void OnContextMenuConstructed(object sender, ConstructedMenuArgs args)
    {
      if (this.CurrentState == CollectorState.MenuBuilded)
      {
        throw new Exception("State inconsistency");
      }
      this.CurrentState = CollectorState.MenuBuilded;
      this.IsUnderConstruction = false;
      this.OnContextMenuUpdated();
    }

    protected virtual void OnContextMenuUpdated()
    {
      Action<InstanceMenuCollector> handler = this.ContextMenuUpdated;
      if (handler != null)
      {
        handler(this);
      }
    }

    protected virtual void OnMenuEntryConstructed(object sender, MenuEntryConstructedArgs args)
    {
      if (this.CurrentState == CollectorState.MenuBuilded)
      {
        this.IsUnderConstruction = true;
        this.ContextMenu = new Dictionary<Instance, ToolStripItem>();
        this.CurrentState = CollectorState.MenuBuilding;
      }
      this.AddInstanceEntry(args);
    }

    #endregion

    public event Action<InstanceMenuCollector> ContextMenuUpdated;
  }
}