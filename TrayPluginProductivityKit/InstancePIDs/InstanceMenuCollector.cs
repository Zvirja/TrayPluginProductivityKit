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

namespace TrayPluginProductivityKit.InstancePIDs
{
  public class InstanceMenuCollector
  {
    #region Fields

    public volatile bool IsUnderConstruction;

    #endregion

    #region Constructors and Destructors

    protected InstanceMenuCollector()
    {
    }

    #endregion

    #region Public Events

    public event Action<InstanceMenuCollector> ContextMenuUpdated;

    #endregion

    #region Public Properties

    public static InstanceMenuCollector Collector { get; set; }
    public Dictionary<Instance, ToolStripItem> ContextMenu { get; set; }

    #endregion

    #region Properties

    protected CollectorState CurrentState { get; set; }

    #endregion

    #region Public Methods and Operators

    public static void Initialize()
    {
      Collector = new InstanceMenuCollector();
      Collector.InitializeInstance();
    }

    public void InitializeInstance()
    {
      CurrentState = CollectorState.MenuBuilded;
      ContextMenu = new Dictionary<Instance, ToolStripItem>();
      IsUnderConstruction = false;
      TrayPluginEvents.ContextMenuEntryConstructed += OnMenuEntryConstructed;
      TrayPluginEvents.ContextMenuConstructed += OnContextMenuConstructed;
    }

    #endregion

    #region Methods

    protected virtual void AddInstanceEntry(MenuEntryConstructedArgs args)
    {
      ToolStripItem menuItem = args.ContextMenuItem;
      if (args.Position != MenuEntryPosition.BodyEntry)
        return;
      var relatedInstance = args.Instance;
      if (relatedInstance == null)
        throw new InvalidDataException("menuItem.Tag is not Instance or is null");
      ContextMenu[relatedInstance] = menuItem;
    }

    protected virtual void OnContextMenuConstructed(object sender, ConstructedMenuArgs args)
    {
      if (CurrentState == CollectorState.MenuBuilded)
        throw new Exception("State inconsistency");
      CurrentState = CollectorState.MenuBuilded;
      IsUnderConstruction = false;
      OnContextMenuUpdated();
    }

    protected virtual void OnContextMenuUpdated()
    {
      Action<InstanceMenuCollector> handler = ContextMenuUpdated;
      if (handler != null) handler(this);
    }

    protected virtual void OnMenuEntryConstructed(object sender, MenuEntryConstructedArgs args)
    {
      if (CurrentState == CollectorState.MenuBuilded)
      {
        IsUnderConstruction = true;
        ContextMenu = new Dictionary<Instance, ToolStripItem>();
        CurrentState = CollectorState.MenuBuilding;
      }
      AddInstanceEntry(args);
    }

    #endregion
  }
}