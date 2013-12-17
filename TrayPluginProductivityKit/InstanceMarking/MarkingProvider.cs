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
using SIM.Tool.Plugins.TrayPlugin.Messaging;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkingProvider
  {
    protected List<string> MarkedInstances { get; set; }
    protected FilesystemMarkingProvider FileSystemProvider { get; set; }
    protected ManualResetEventSlim ConstructingWaiter { get; set; }

    public virtual void Initialize()
    {
      ConstructingWaiter = new ManualResetEventSlim(false);
      //One time subscription, handler immediately unsubscribes
      InstanceManager.InstancesListUpdated += OnInstanceManagerUpdated;
      FileSystemProvider = new FilesystemMarkingProvider();
      SubscribeToMenuRelatedEvents();
    }

    public virtual void ToggleMarking(ToolStripItem menuItem, Instance relatedInstance)
    {
      if (MarkedInstances.Contains(relatedInstance.Name))
        UnMarkInstanceInternal(menuItem, relatedInstance);
      else
        MarkInstanceInternal(menuItem, relatedInstance);
    }

    public virtual void MarkEntry(ToolStripItem menuItem, Instance relatedInstance)
    {
      if (MarkedInstances.Contains(relatedInstance.Name))
        return;
      MarkInstanceInternal(menuItem, relatedInstance);
    }

    public virtual void UnMarkEntry(ToolStripItem menuItem, Instance relatedInstance)
    {
      if (!MarkedInstances.Contains(relatedInstance.Name))
        return;
      UnMarkInstanceInternal(menuItem, relatedInstance);
    }

    protected virtual void OnInstanceManagerUpdated(object sender, EventArgs e)
    {
      //One time subscription
      InstanceManager.InstancesListUpdated -= OnInstanceManagerUpdated;
      Task.Factory.StartNew(PerformInitialInitialization);
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
      MessageBus.Subscribe(StandardMessagesKinds.ContextMenuProviderEntryConstructed, OnMenuEntryConstructed);
    }

    protected virtual void OnMenuEntryConstructed(object sender, TrayPluginMessage message)
    {
      ConstructingWaiter.Wait();
      var constructingParams = message.Arguments as MenuEntryConstructedMessageParams;
      if (constructingParams == null)
        return;
      ToolStripItem menuItem = constructingParams.ContextMenuItem;
      var relatedInstance = menuItem.Tag as Instance;
      if (relatedInstance == null)
        return;
      if (MarkedInstances.Contains(relatedInstance.Name))
      {
        MakeMenuItemMarked(menuItem);
      }
    }

    protected virtual void MarkInstanceInternal(ToolStripItem menuItem, Instance relatedInstance)
    {
      ConstructingWaiter.Wait();
      if (!FileSystemProvider.MarkInstance(relatedInstance))
        return;
      MarkedInstances.Add(relatedInstance.Name);
      MakeMenuItemMarked(menuItem);
    }

    protected virtual void UnMarkInstanceInternal(ToolStripItem menuItem, Instance relatedInstance)
    {
      ConstructingWaiter.Wait();
      FileSystemProvider.UnMarkInstance(relatedInstance);
      MarkedInstances.Remove(relatedInstance.Name);
      MakeMenuItemUnmarked(menuItem);
    }

    protected virtual void MakeMenuItemMarked(ToolStripItem menuItem)
    {
      menuItem.Font = new Font(menuItem.Font, FontStyle.Bold);
    }

    protected virtual void MakeMenuItemUnmarked(ToolStripItem menuItem)
    {
      menuItem.Font = new Font(menuItem.Font, FontStyle.Regular);
    }
  }
}
