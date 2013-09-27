using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TrayPluginProductivityKit.InstancePIDs
{
  public class ProcessMonitor
  {
    public delegate void ActiveProcessesChanged(List<int> added, List<int> removed, List<int> allPIDs);

    protected CancellationTokenSource CancellationTokenSource { get; set; }

    public volatile int WorkLoopIntervalMsec;
    public event ActiveProcessesChanged PIDsChanged;
    public List<int> PIDs { get; set; }

    public ProcessMonitor()
    {
      WorkLoopIntervalMsec = 1000;
      PIDs = new List<int>();
    }

    public virtual void StartMonitor()
    {
      StartWorkingThread();
    }

    public virtual void StopMonitor()
    {
      if (CancellationTokenSource == null)
        return;
      CancellationTokenSource.Cancel();
      CancellationTokenSource = null;
    }

    protected virtual void StartWorkingThread()
    {
      if (CancellationTokenSource != null)
        return;
      CancellationTokenSource = new CancellationTokenSource();
      Task.Factory.StartNew(WorkingLoop, CancellationTokenSource.Token, CancellationTokenSource.Token);
    }

    protected virtual void WorkingLoop(object tokenObj)
    {
      var token = (CancellationToken)tokenObj;
      while (!token.IsCancellationRequested)
      { 
        try
        {
          ProcessQueue();
        }
        catch
        {
          
        }
        Thread.Sleep(WorkLoopIntervalMsec);
      }
    }

    protected virtual void ProcessQueue()
    {
      Process[] processes = Process.GetProcessesByName("w3wp");
      var pids = processes.Select(x => x.Id).ToList();
      var created = pids.Except(PIDs).ToList();
      var removed = PIDs.Except(pids).ToList();
      PIDs = pids;
      if(created.Count !=0 || removed.Count != 0)
        OnPiDsChanged(created, removed, pids);
    }

    protected virtual void OnPiDsChanged(List<int> added, List<int> removed, List<int> allpids)
    {
      ActiveProcessesChanged handler = PIDsChanged;
      if (handler != null) handler(added, removed, allpids);
    }
  }
}
