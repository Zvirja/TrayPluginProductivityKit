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
    #region Fields

    public volatile int WorkLoopIntervalMsec;

    #endregion

    #region Constructors and Destructors

    public ProcessMonitor()
    {
      this.WorkLoopIntervalMsec = 1000;
      this.PIDs = new List<int>();
    }

    #endregion

    #region Delegates

    public delegate void ActiveProcessesChanged(List<int> added, List<int> removed, List<int> allPIDs);

    #endregion

    #region Public Events

    public event ActiveProcessesChanged PIDsChanged;

    #endregion

    #region Public Properties

    public List<int> PIDs { get; set; }

    #endregion

    #region Properties

    protected CancellationTokenSource CancellationTokenSource { get; set; }

    #endregion

    #region Public Methods and Operators

    public virtual void StartMonitor()
    {
      this.StartWorkingThread();
    }

    public virtual void StopMonitor()
    {
      if (this.CancellationTokenSource == null)
      {
        return;
      }
      this.CancellationTokenSource.Cancel();
      this.CancellationTokenSource = null;
    }

    #endregion

    #region Methods

    protected virtual void OnPiDsChanged(List<int> added, List<int> removed, List<int> allpids)
    {
      ActiveProcessesChanged handler = this.PIDsChanged;
      if (handler != null)
      {
        handler(added, removed, allpids);
      }
    }

    protected virtual void ProcessQueue()
    {
      Process[] processes = Process.GetProcessesByName("w3wp");
      var pids = processes.Select(x => x.Id).ToList();
      var created = pids.Except(this.PIDs).ToList();
      var removed = this.PIDs.Except(pids).ToList();
      this.PIDs = pids;
      if (created.Count != 0 || removed.Count != 0)
      {
        this.OnPiDsChanged(created, removed, pids);
      }
    }

    protected virtual void StartWorkingThread()
    {
      if (this.CancellationTokenSource != null)
      {
        return;
      }
      this.CancellationTokenSource = new CancellationTokenSource();
      Task.Factory.StartNew(this.WorkingLoop, this.CancellationTokenSource.Token, this.CancellationTokenSource.Token);
    }

    protected virtual void WorkingLoop(object tokenObj)
    {
      var token = (CancellationToken)tokenObj;
      while (!token.IsCancellationRequested)
      {
        try
        {
          this.ProcessQueue();
        }
        catch
        {
        }
        Thread.Sleep(this.WorkLoopIntervalMsec);
      }
    }

    #endregion
  }
}