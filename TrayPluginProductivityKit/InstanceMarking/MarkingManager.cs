#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

#endregion

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkingManager
  {
    #region Properties

    public static MarkingProvider ActualProvider { get; set; }

    #endregion

    #region Methods

    public static void Initialize()
    {
      ActualProvider = new MarkingProvider();
      ActualProvider.Initialize();
    }

    public static void MarkInstance(ToolStripItem menuItem, InstanceData instanceData)
    {
      ActualProvider.MarkEntry(menuItem, instanceData);
    }

    public static void MarkSingleInstanceOnly(ToolStripItem menuItem, InstanceData instanceData)
    {
      ActualProvider.MarkSingleInstanceOnly(menuItem, instanceData);
    }

    public static void ToggleInstanceMarking(ToolStripItem menuItem, InstanceData instanceData)
    {
      ActualProvider.ToggleMarking(menuItem, instanceData);
    }

    public static void UnMarkInstance(ToolStripItem menuItem, InstanceData instanceData)
    {
      ActualProvider.UnMarkEntry(menuItem, instanceData);
    }

    #endregion
  }
}