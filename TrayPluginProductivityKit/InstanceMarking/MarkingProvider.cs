using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIM.Base;
using SIM.Instances;
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkingProvider
  {
    #region Properties

    protected ManualResetEventSlim ConstructingWaiter { get; set; }
    protected FilesystemMarkingProvider FileSystemProvider { get; set; }
    protected Dictionary<string, MarkedInstance> MarkedInstances { get; set; }

    #endregion

    #region Public Methods and Operators

    public virtual void Initialize()
    {
      this.ConstructingWaiter = new ManualResetEventSlim(false);
      //One time subscription, handler immediately unsubscribes
      InstanceManager.InstancesListUpdated += this.OnInstanceManagerUpdated;
      this.FileSystemProvider = new FilesystemMarkingProvider();
      this.SubscribeToMenuRelatedEvents();
    }

    public virtual void MarkEntry(ToolStripItem menuItem, Instance relatedInstance)
    {
      this.ConstructingWaiter.Wait();
      if (this.MarkedInstances.ContainsKey(relatedInstance.Name))
      {
        return;
      }
      this.MarkInstanceInternal(menuItem, relatedInstance);
    }

    public void MarkSingleInstanceOnly(ToolStripItem menuItem, Instance instance)
    {
      this.ConstructingWaiter.Wait();

      string instanceName = instance.Name;

      bool needMark = true;

      Dictionary<string, MarkedInstance> markedInstancesCopy = this.MarkedInstances.ToDictionary(pair => pair.Key, pair => pair.Value);

      foreach (var markedInstance in markedInstancesCopy)
      {
        if (markedInstance.Key.EqualsIgnoreCase(instanceName))
        {
          needMark = false;
          this.MarkedInstances[markedInstance.Key].LastKnownToolstrip = menuItem;
        }
        else
        {
          //We don't have that information initially or if call comes not from toolstrip. So perform a check.
          MarkedInstance instanceInfo = markedInstance.Value;
          this.UnMarkInstanceInternal(instanceInfo.LastKnownToolstrip, instanceInfo.Instance);
        }
      }

      if (needMark)
      {
        this.MarkInstanceInternal(menuItem, instance);
      }
    }

    public virtual void ToggleMarking(ToolStripItem menuItem, Instance relatedInstance)
    {
      this.ConstructingWaiter.Wait();
      if (this.MarkedInstances.ContainsKey(relatedInstance.Name))
      {
        this.UnMarkInstanceInternal(menuItem, relatedInstance);
      }
      else
      {
        this.MarkInstanceInternal(menuItem, relatedInstance);
      }
    }

    public virtual void UnMarkEntry(ToolStripItem menuItem, Instance relatedInstance)
    {
      this.ConstructingWaiter.Wait();
      if (!this.MarkedInstances.ContainsKey(relatedInstance.Name))
      {
        return;
      }
      this.UnMarkInstanceInternal(menuItem, relatedInstance);
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
      if (!this.FileSystemProvider.MarkInstance(relatedInstance))
      {
        return;
      }
      if (menuItem != null)
      {
        this.MakeMenuItemMarked(menuItem);
      }
      this.MarkedInstances.Add(relatedInstance.Name, new MarkedInstance(relatedInstance, menuItem));
    }

    protected virtual void OnInstanceManagerUpdated(object sender, EventArgs e)
    {
      //One time subscription
      InstanceManager.InstancesListUpdated -= this.OnInstanceManagerUpdated;
      Task.Factory.StartNew(this.PerformInitialInitialization);
    }

    protected virtual void OnMenuEntryConstructed(object sender, MenuEntryConstructedArgs args)
    {
      this.ConstructingWaiter.Wait();
      var relatedInstance = args.Instance;
      if (relatedInstance == null)
      {
        return;
      }
      ToolStripItem menuItem = args.ContextMenuItem;
      if (this.MarkedInstances.ContainsKey(relatedInstance.Name))
      {
        MarkedInstance markedInstance = this.MarkedInstances[relatedInstance.Name];
        this.MakeMenuItemMarked(menuItem);
        markedInstance.LastKnownToolstrip = menuItem;
      }
    }


    protected virtual void PerformInitialInitialization()
    {
      var markedInstances = new Dictionary<string, MarkedInstance>();
      try
      {
        IEnumerable<Instance> instances = InstanceManager.PartiallyCachedInstances;
        foreach (Instance instance in instances)
        {
          try
          {
            if (this.FileSystemProvider.IsInstanceMarked(instance))
            {
              markedInstances.Add(instance.Name, new MarkedInstance(instance, null));
            }
          }
          catch (Exception)
          {
            //silent catch
          }
        }
      }
      finally
      {
        this.MarkedInstances = markedInstances;
        this.ConstructingWaiter.Set();
      }
    }

    protected virtual void SubscribeToMenuRelatedEvents()
    {
      TrayPluginEvents.ContextMenuEntryConstructed += this.OnMenuEntryConstructed;
    }

    protected virtual void UnMarkInstanceInternal(ToolStripItem menuItem, Instance relatedInstance)
    {
      this.FileSystemProvider.UnMarkInstance(relatedInstance);
      if(menuItem != null)
      {
        this.MakeMenuItemUnmarked(menuItem);
      }
      this.MarkedInstances.Remove(relatedInstance.Name);
    }

    #endregion
  }
}