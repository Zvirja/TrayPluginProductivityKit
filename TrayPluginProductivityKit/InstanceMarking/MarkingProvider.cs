using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIM.Instances;
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkingProvider
  {
    #region Properties

    protected ManualResetEventSlim ConstructingWaiter { get; set; }
    protected FilesystemMarkingProvider FileSystemProvider { get; set; }
    protected List<string> MarkedInstances { get; set; }

    #endregion

    #region Public Methods and Operators

    public virtual void Initialize()
    {
      ConstructingWaiter = new ManualResetEventSlim(false);
      //One time subscription, handler immediately unsubscribes
      InstanceManager.InstancesListUpdated += OnInstanceManagerUpdated;
      FileSystemProvider = new FilesystemMarkingProvider();
      SubscribeToMenuRelatedEvents();
    }

    public virtual void MarkEntry(ToolStripItem menuItem, Instance relatedInstance)
    {
      if (MarkedInstances.Contains(relatedInstance.Name))
        return;
      MarkInstanceInternal(menuItem, relatedInstance);
    }

    public virtual void ToggleMarking(ToolStripItem menuItem, Instance relatedInstance)
    {
      if (MarkedInstances.Contains(relatedInstance.Name))
        UnMarkInstanceInternal(menuItem, relatedInstance);
      else
        MarkInstanceInternal(menuItem, relatedInstance);
    }

    public virtual void UnMarkEntry(ToolStripItem menuItem, Instance relatedInstance)
    {
      if (!MarkedInstances.Contains(relatedInstance.Name))
        return;
      UnMarkInstanceInternal(menuItem, relatedInstance);
    }

    #endregion

    #region Methods

    protected virtual void MakeMenuItemMarked(ToolStripItem menuItem)
    {
      menuItem.Font = new Font(menuItem.Font, FontStyle.Bold);
    }

    protected virtual void MakeMenuItemUnmarked(ToolStripItem menuItem)
    {
      menuItem.Font = new Font(menuItem.Font, FontStyle.Regular);
    }

    protected virtual void MarkInstanceInternal(ToolStripItem menuItem, Instance relatedInstance)
    {
      ConstructingWaiter.Wait();
      if (!FileSystemProvider.MarkInstance(relatedInstance))
        return;
      MarkedInstances.Add(relatedInstance.Name);
      MakeMenuItemMarked(menuItem);
    }

    protected virtual void OnInstanceManagerUpdated(object sender, EventArgs e)
    {
      //One time subscription
      InstanceManager.InstancesListUpdated -= OnInstanceManagerUpdated;
      Task.Factory.StartNew(PerformInitialInitialization);
    }

    protected virtual void OnMenuEntryConstructed(object sender, MenuEntryConstructedArgs args)
    {
      ConstructingWaiter.Wait();
      var relatedInstance = args.Instance;
      if (relatedInstance == null)
        return;
      ToolStripItem menuItem = args.ContextMenuItem;
      if (MarkedInstances.Contains(relatedInstance.Name))
      {
        MakeMenuItemMarked(menuItem);
      }
    }


    protected virtual void PerformInitialInitialization()
    {
      var markedInstances = new List<string>();
      try
      {
        IEnumerable<Instance> instances = InstanceManager.PartiallyCachedInstances;
        foreach (Instance instance in instances)
        {
          if (FileSystemProvider.IsInstanceMarked(instance))
            markedInstances.Add(instance.Name);
        }
      }
      finally
      {
        MarkedInstances = markedInstances;
        ConstructingWaiter.Set();
      }
    }

    protected virtual void SubscribeToMenuRelatedEvents()
    {
      TrayPluginEvents.ContextMenuEntryConstructed += OnMenuEntryConstructed;
    }

    protected virtual void UnMarkInstanceInternal(ToolStripItem menuItem, Instance relatedInstance)
    {
      ConstructingWaiter.Wait();
      FileSystemProvider.UnMarkInstance(relatedInstance);
      MarkedInstances.Remove(relatedInstance.Name);
      MakeMenuItemUnmarked(menuItem);
    }

    #endregion
  }
}