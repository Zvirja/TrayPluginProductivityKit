﻿#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIM;
using SIM.Instances;
using SIM.Tool.Plugins.TrayPlugin;
using SIM.Tool.Plugins.TrayPlugin.TrayIcon.ContextMenu.Eventing;

#endregion

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkingProvider
  {
    #region Fields

    private int m_wasInitialized = 0;

    #endregion

    #region Properties

    protected ManualResetEventSlim ConstructingWaiter { get; set; }
    protected FilesystemMarkingProvider FileSystemProvider { get; set; }
    protected Dictionary<string, MarkedInstance> MarkedInstances { get; set; }

    #endregion

    #region Methods

    public virtual void Initialize()
    {
      this.ConstructingWaiter = new ManualResetEventSlim(false);
      //One time subscription, handler immediately unsubscribes
      InstanceManager.InstancesListUpdated += this.OnInstanceManagerUpdated;
      this.FileSystemProvider = new FilesystemMarkingProvider();
      this.SubscribeToMenuRelatedEvents();
    }

    public virtual void MarkEntry(ToolStripItem menuItem, InstanceData instanceData)
    {
      this.ConstructingWaiter.Wait();
      if (this.MarkedInstances.ContainsKey(instanceData.InstanceName))
      {
        return;
      }
      this.MarkInstanceInternal(menuItem, instanceData);
    }

    public void MarkSingleInstanceOnly(ToolStripItem menuItem, InstanceData instanceData)
    {
      this.ConstructingWaiter.Wait();

      string instanceName = instanceData.InstanceName;

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
          this.UnMarkInstanceInternal(instanceInfo.LastKnownToolstrip, instanceInfo.InstanceData);
        }
      }

      if (needMark)
      {
        this.MarkInstanceInternal(menuItem, instanceData);
      }
    }

    public virtual void ToggleMarking(ToolStripItem menuItem, InstanceData instanceData)
    {
      this.ConstructingWaiter.Wait();
      if (this.MarkedInstances.ContainsKey(instanceData.InstanceName))
      {
        this.UnMarkInstanceInternal(menuItem, instanceData);
      }
      else
      {
        this.MarkInstanceInternal(menuItem, instanceData);
      }
    }

    public virtual void UnMarkEntry(ToolStripItem menuItem, InstanceData instanceData)
    {
      this.ConstructingWaiter.Wait();
      if (!this.MarkedInstances.ContainsKey(instanceData.InstanceName))
      {
        return;
      }
      this.UnMarkInstanceInternal(menuItem, instanceData);
    }

    protected virtual void CleanupNonExistingEntries()
    {
      //Not initialized yet.
      if (this.MarkedInstances == null)
      {
        return;
      }

      var currentInstances = new HashSet<string>(InstanceManager.PartiallyCachedInstances.Select(inst => inst.Name));
      var keysToRemove = this.MarkedInstances.Where(markedInst => !currentInstances.Contains(markedInst.Key)).ToList();

      if (keysToRemove.Count > 0)
      {
        foreach (KeyValuePair<string, MarkedInstance> isntToRemove in keysToRemove)
        {
          this.MarkedInstances.Remove(isntToRemove.Key);
        }
      }
    }

    protected virtual void MakeMenuItemMarked(ToolStripItem menuItem)
    {
      menuItem.Font = new Font(menuItem.Font, FontStyle.Bold);
    }

    protected virtual void MakeMenuItemUnmarked(ToolStripItem menuItem)
    {
      menuItem.Font = new Font(menuItem.Font, FontStyle.Regular);
    }

    protected virtual void MarkInstanceInternal(ToolStripItem menuItem, InstanceData instanceData)
    {
      if (!this.FileSystemProvider.MarkInstance(instanceData))
      {
        return;
      }
      if (menuItem != null)
      {
        this.MakeMenuItemMarked(menuItem);
      }
      this.MarkedInstances.Add(instanceData.InstanceName, new MarkedInstance(instanceData, menuItem));
    }

    protected virtual void OnInstanceManagerUpdated(object sender, EventArgs e)
    {
      //Init for one time only
      if (Interlocked.CompareExchange(ref this.m_wasInitialized, 1, 0) == 0)
      {
        Task.Factory.StartNew(this.PerformInitialInitialization);
      }

      this.CleanupNonExistingEntries();
    }

    protected virtual void OnMenuEntryConstructed(object sender, MenuEntryConstructedArgs args)
    {
      this.ConstructingWaiter.Wait();
      Instance relatedInstance = args.Instance;
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
              markedInstances.Add(instance.Name, new MarkedInstance(InstanceData.FromInstance(instance), null));
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

    protected virtual void UnMarkInstanceInternal(ToolStripItem menuItem, InstanceData instanceData)
    {
      this.FileSystemProvider.UnMarkInstance(instanceData);
      if (menuItem != null)
      {
        this.MakeMenuItemUnmarked(menuItem);
      }
      this.MarkedInstances.Remove(instanceData.InstanceName);
    }

    #endregion
  }
}