using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
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
    public static ProcessIDsCustodian Actual { get; set; }

    static ProcessIDsCustodian()
    {
      Actual = new ProcessIDsCustodian();
    }

    protected ProcessMonitor PMonitor { get; set; }
    protected Queue<Dictionary<Instance, ToolStripItem>> TasksToProcess { get; set; }


    public ProcessIDsCustodian()
    {
      PMonitor = new ProcessMonitor();
      TasksToProcess = new Queue<Dictionary<Instance, ToolStripItem>>();
    }

    public virtual void Initialize()
    {
      PMonitor.PIDsChanged += MonitorOnPiDsChanged;
      PMonitor.StartMonitor();
      InstanceMenuCollector.Collector.ContextMenuUpdated += CollectorOnContextMenuUpdated;
      StartUpdaterQueue();
    }

    protected virtual void MonitorOnPiDsChanged(List<int> added, List<int> removed, List<int> allPiDs)
    {
      lock(TasksToProcess)
      {
        TasksToProcess.Enqueue(InstanceMenuCollector.Collector.ContextMenu);
        Monitor.Pulse(TasksToProcess);
      }
    }

    protected virtual void CollectorOnContextMenuUpdated(InstanceMenuCollector instanceMenuCollector)
    {
      lock (TasksToProcess)
      {
        TasksToProcess.Enqueue(instanceMenuCollector.ContextMenu);
        Monitor.Pulse(TasksToProcess);
      }
    }

    protected virtual void StartUpdaterQueue()
    {
      Task.Factory.StartNew(delegate
      {
        TasksToProcess.Enqueue(InstanceMenuCollector.Collector.ContextMenu);
        while (true)
        {
          Dictionary<Instance, ToolStripItem> item;
          lock (TasksToProcess)
          {
            if (TasksToProcess.Count == 0)
            {
              Monitor.Wait(TasksToProcess);
              continue;
            }
            item = TasksToProcess.Dequeue();
          }
          try
          {
            EntirelyUpdateMenu(item);
          }
          catch (UnauthorizedAccessException)
          {
            lock (TasksToProcess)
            {
              TasksToProcess.Enqueue(item);
            }
          }
          catch
          {
            
          }
        }
      });
    }

    protected virtual void EntirelyUpdateMenu(object o)
    {
      var contextMenu = (Dictionary<Instance, ToolStripItem>)o;
      foreach (KeyValuePair<Instance, ToolStripItem> keyVauePair in contextMenu)
      {
        Instance instance = keyVauePair.Key;
        ToolStripItem toolStripItem = keyVauePair.Value;
        var cleanTooltipValue = GetCleanToolTipValue(toolStripItem.Text);
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
  }
}
