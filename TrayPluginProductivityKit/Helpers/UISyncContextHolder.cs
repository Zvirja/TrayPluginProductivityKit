using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TrayPluginProductivityKit.Helpers
{
  static class UISyncContextHolder
  {
    public static void CatchSyncContext()
    {
      UISyncContext = SynchronizationContext.Current;
    }

    public static SynchronizationContext UISyncContext;
  }
}
