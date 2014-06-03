using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIM.Base;
using SIM.Instances;

namespace TrayPluginProductivityKit.InstancePIDs
{
  public class ProcessIDsCustodian
  {
    #region Constructors and Destructors

    static ProcessIDsCustodian()
    {
      Actual = new ProcessIDsCustodian();
    }

    public ProcessIDsCustodian()
    {
      this.PMonitor = new ProcessMonitor();
      this.TasksToProcess = new Queue<Dictionary<Instance, ToolStripItem>>();
    }

    #endregion

    #region Public Properties

    public static ProcessIDsCustodian Actual { get; set; }

    #endregion

    #region Properties

    protected ProcessMonitor PMonitor { get; set; }
    protected Queue<Dictionary<Instance, ToolStripItem>> TasksToProcess { get; set; }

    #endregion

    #region Public Methods and Operators

    public virtual void Initialize()
    {
      this.PMonitor.PIDsChanged += this.MonitorOnPiDsChanged;
      this.PMonitor.StartMonitor();
      InstanceMenuCollector.Collector.ContextMenuUpdated += this.CollectorOnContextMenuUpdated;
      this.StartUpdaterQueue();
    }

    #endregion

    #region Methods

    protected virtual void CollectorOnContextMenuUpdated(InstanceMenuCollector instanceMenuCollector)
    {
      lock (this.TasksToProcess)
      {
        this.TasksToProcess.Enqueue(instanceMenuCollector.ContextMenu);
        Monitor.Pulse(this.TasksToProcess);
      }
    }

    protected virtual void EntirelyUpdateMenu(object o)
    {
      var contextMenu = (Dictionary<Instance, ToolStripItem>)o;
      foreach (KeyValuePair<Instance, ToolStripItem> keyVauePair in contextMenu)
      {
        Instance instance = keyVauePair.Key;
        ToolStripItem toolStripItem = keyVauePair.Value;
        var cleanTooltipValue = this.GetCleanToolTipValue(toolStripItem.Text);
        var idsForInstance = instance.ProcessIds.Select(x => x.ToString(CultureInfo.InvariantCulture)).ToList();
        if (idsForInstance.Count == 0)
        {
          toolStripItem.Text = cleanTooltipValue;
          continue;
        }
        var idsLine = string.Join("|", idsForInstance);
        toolStripItem.Text = "{0} <<{1}>>".FormatWith(cleanTooltipValue, idsLine);
      }
    }

    protected virtual string GetCleanToolTipValue(string pollutedValue)
    {
      var indexOf = pollutedValue.IndexOf("<<", StringComparison.OrdinalIgnoreCase);
      return indexOf < 0 ? pollutedValue : pollutedValue.Substring(0, indexOf - 1);
    }

    protected virtual void MonitorOnPiDsChanged(List<int> added, List<int> removed, List<int> allPiDs)
    {
      lock (this.TasksToProcess)
      {
        this.TasksToProcess.Enqueue(InstanceMenuCollector.Collector.ContextMenu);
        Monitor.Pulse(this.TasksToProcess);
      }
    }

    protected virtual void StartUpdaterQueue()
    {
      Task.Factory.StartNew(delegate
      {
        this.TasksToProcess.Enqueue(InstanceMenuCollector.Collector.ContextMenu);
        while (true)
        {
          Dictionary<Instance, ToolStripItem> item;
          lock (this.TasksToProcess)
          {
            if (this.TasksToProcess.Count == 0)
            {
              Monitor.Wait(this.TasksToProcess);
              continue;
            }
            item = this.TasksToProcess.Dequeue();
          }
          try
          {
            this.EntirelyUpdateMenu(item);
          }
          catch (UnauthorizedAccessException)
          {
            lock (this.TasksToProcess)
            {
              this.TasksToProcess.Enqueue(item);
            }
          }
          catch
          {
          }
        }
      });
    }

    #endregion
  }
}