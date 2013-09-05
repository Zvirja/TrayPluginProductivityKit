using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace TrayPluginProductivityKit.InstancePIDs
{
  public class ProcessMonitor
  {
    protected Thread WorkingThread { get; set; }
    protected int[] PIDs { get; set; } 

    public volatile int WorkLoopIntervalMsec;

    public ProcessMonitor()
    {
      WorkLoopIntervalMsec = 1000;
      PIDs = new int[0];
    }

    public virtual void StartMonitor()
    {
      StartWorkingThread();
    }

    public virtual void StopMonitor()
    {
      try
      {
        if(WorkingThread != null)
          WorkingThread.Abort();
      }
      catch
      {
      }
    }

    protected virtual void StartWorkingThread()
    {
      WorkingThread = new Thread(WorkingLoop);
    }

    protected virtual void WorkingLoop()
    {
      try
      {
        while (true)
        {

          Thread.Sleep(WorkLoopIntervalMsec);
        }
      }
      catch (ThreadAbortException)
      {
      }
    }

    protected virtual void ProcessQueue()
    {
      Process[] processes = Process.GetProcessesByName("w3wp.exe");
      var pids = processes.Select(x => x.Id);

    }
  }
}
