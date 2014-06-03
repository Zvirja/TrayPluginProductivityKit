using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SIM.Instances;

namespace TrayPluginProductivityKit.InstanceMarking
{
  public class MarkingManager
  {
    public static MarkingProvider ActualProvider { get; set; }

    public static void Initialize()
    {
      ActualProvider = new MarkingProvider();
      ActualProvider.Initialize();
    }

    public static void MarkInstance(ToolStripItem menuItem, Instance instance)
    {
      ActualProvider.MarkEntry(menuItem,instance);
    }

    public static void UnMarkInstance(ToolStripItem menuItem, Instance instance)
    {
      ActualProvider.UnMarkEntry(menuItem, instance);
    }

    public static void ToggleInstanceMarking(ToolStripItem menuItem, Instance instance)
    {
      ActualProvider.ToggleMarking(menuItem, instance);
    }

    public static void MarkSingleInstanceOnly(ToolStripItem menuItem, Instance instance)
    {
      ActualProvider.MarkSingleInstanceOnly(menuItem, instance);
    }
  }
}
